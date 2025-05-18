using System;
using System.IO;
using System.Text;

namespace SLCryptoAuth.IO;

public class BinaryReaderExtended(byte[] buffer)
    : BinaryReader(new MemoryStream(buffer), Encoding.UTF8, false)
{
    public int Length => Convert.ToInt32(BaseStream.Length);

    public int RemainingBytes => Length - Position;

    public int Position
    {
        get => Convert.ToInt32(BaseStream.Position);
        set => BaseStream.Position = value;
    }

    public byte[] ReadRemainingBytes()
        => base.ReadBytes(RemainingBytes);

    public byte[] ReadByteArrayWithLength()
    {
        var length = base.ReadInt32();
        return base.ReadBytes(length);
    }

    public byte ReadByte(int index)
        => DoActionAtIndex(base.ReadByte, index);

    public byte[] ReadBytes(int index, int length)
        => DoActionAtIndex(() => base.ReadBytes(length), index);

    public char ReadChar(int index)
        => DoActionAtIndex(base.ReadChar, index);

    public char[] ReadChars(int index, int length)
        => DoActionAtIndex(() => base.ReadChars(length), index);

    public decimal ReadDecimal(int index)
        => DoActionAtIndex(base.ReadDecimal, index);

    public double ReadDouble(int index)
        => DoActionAtIndex(base.ReadDouble, index);

    public short ReadInt16(int index)
        => DoActionAtIndex(base.ReadInt16, index);

    public int ReadInt32(int index)
        => DoActionAtIndex(base.ReadInt32, index);

    public long ReadInt64(int index)
        => DoActionAtIndex(base.ReadInt64, index);

    public string ReadString(int index)
        => DoActionAtIndex(base.ReadString, index);

    public sbyte ReadSByte(int index)
        => DoActionAtIndex(base.ReadSByte, index);

    public float ReadSingle(int index)
        => DoActionAtIndex(base.ReadSingle, index);

    public ushort ReadUInt16(int index)
        => DoActionAtIndex(base.ReadUInt16, index);

    public uint ReadUInt32(int index)
        => DoActionAtIndex(base.ReadUInt32, index);

    public ulong ReadUInt64(int index)
        => DoActionAtIndex(base.ReadUInt64, index);

    private T DoActionAtIndex<T>(Func<T> action, int index)
    {
        var startPosition = Position;
        Position = index;
        var result = action.Invoke();
        Position = startPosition;
        return result;
    }
}
