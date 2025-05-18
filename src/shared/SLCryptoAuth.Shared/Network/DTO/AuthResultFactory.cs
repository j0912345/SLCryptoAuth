namespace SLCryptoAuth.Network.DTO;

public static class AuthResultFactory
{
    private static readonly byte[] InvalidTokenData = [(byte)RejectionReason.InvalidToken];
    private static readonly byte[] GameVersionMismatchData = [(byte)RejectionReason.VersionMismatch];
    private static readonly byte[] InternalServerError = [(byte)RejectionReason.Error];
    private static readonly byte[] ExpiredTokenData = [(byte)RejectionReason.ExpiredAuth];

    public static AuthResult InvalidToken() => AuthResult.InvalidPacket(InvalidTokenData);
    public static AuthResult ExpiredToken() => AuthResult.InvalidPacket(ExpiredTokenData);
    public static AuthResult InvalidSignature() => AuthResult.InvalidPacket(InvalidTokenData);
    public static AuthResult GameVersionsMismatch() => AuthResult.InvalidPacket(GameVersionMismatchData);
}