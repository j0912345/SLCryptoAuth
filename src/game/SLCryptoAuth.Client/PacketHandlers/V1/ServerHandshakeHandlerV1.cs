using System;
using System.Linq;
using MelonLoader;
using SLCryptoAuth.Client.Core;
using SLCryptoAuth.Client.Utils;
using SLCryptoAuth.Cryptography.DiffieHellman;
using SLCryptoAuth.Cryptography.DigitalSignature;
using SLCryptoAuth.Cryptography.Encryption;
using SLCryptoAuth.IO;
using SLCryptoAuth.Network.DTO;
using SLCryptoAuth.Network.Handlers;
using SLCryptoAuth.Shared;

namespace SLCryptoAuth.Client.PacketHandlers.V1;

public class ServerHandshakeHandlerV1(Ecdsa clientIdentity, Func<Ecdh> getClientSession)
    : IPacketHandler
{
    /// <inheritdoc />
    public byte Id => 50;

    /// <inheritdoc />
    public byte Version => 1;

    /// <inheritdoc />
    public AuthResult ProcessPayload(BinaryReaderExtended payloadReader, object? state)
    {
        try
        {
            #region Parse Packet

            // { DATA_BLOCK }
            var dataBlock = payloadReader.ReadBytes(107);
            var dataBlockReader = new BinaryReaderExtended(dataBlock);

            var timestamp = dataBlockReader.ReadInt64();
            var serverIdentityPublicKey = dataBlockReader.ReadBytes(33);
            var serverSessionPublicKey = dataBlockReader.ReadBytes(33);
            var clientSessionPublicKey = dataBlockReader.ReadBytes(33);

            // { SIGNATURE_BLOCK }
            var signature = payloadReader.ReadRemainingBytes();

            #endregion

            #region Process Packet

            // Check lifetime
            // TODO: maybe add lifetime check

            // Check signature
            var serverIdentity = new EcdsaVerify(serverIdentityPublicKey);
            if (!serverIdentity.Verify(dataBlock, signature))
            {
                MelonLogger.Error("[FAILEd]: Server sent invalid signature!");
                return AuthResultFactory.InvalidSignature();
            }

            var clientSession = getClientSession();

            // Check Client Session Public Key
            if (!clientSession.PublicKeyBytes.SequenceEqual(clientSessionPublicKey))
            {
                MelonLogger.Error("[FAILED]: MITM detected!");
                return AuthResultFactory.InvalidToken();
            }

            // Check Server Identity Public Key For Trusting
            // TODO: need to add check session identity for trusting

            // Shared Secret
            var sharedSecret = clientSession.ComputeSharedSecret(serverSessionPublicKey);
            ClientContext.Instance.SetSharedSecret(sharedSecret);

            MelonLogger.Msg("[SUCCESS] Connected!");
            {
                var responseBinaryWriter = new BinaryWriterExtended();

                // { HEADER_BLOCK }
                responseBinaryWriter.Write((byte)51);
                responseBinaryWriter.Write((byte)1);

                // { ENCRYPTED_BLOCK }
                var sharedEncryption = new Aes(sharedSecret);
                var encryptedBlockData = new BinaryWriterExtended();
                var doNotTrack = DoNotTrackUtils.IsEnabled();
                encryptedBlockData.Write(DateTimeOffset.UtcNow.Ticks); // timestamp
                encryptedBlockData.Write(clientIdentity.PublicKeyBytes);
                encryptedBlockData.Write(serverSessionPublicKey);
                encryptedBlockData.Write(doNotTrack);
                encryptedBlockData.Write(NicknameUtils.GetNickname()); // nickname

                var encryptedBlock = sharedEncryption.Encrypt(encryptedBlockData.ToArray());
                responseBinaryWriter.WriteArrayWithLength(encryptedBlock);

                // { SIGNATURE_BLOCK }
                signature = clientIdentity.Sign(encryptedBlock);
                responseBinaryWriter.Write(signature);

                return AuthResult.Answer(responseBinaryWriter.ToArray());
            }

            #endregion
        }
        catch (Exception ex)
        {
            MelonLogger.Error("[EXCEPTION]: " + ex);
            return AuthResultFactory.InvalidToken();
        }
    }
}