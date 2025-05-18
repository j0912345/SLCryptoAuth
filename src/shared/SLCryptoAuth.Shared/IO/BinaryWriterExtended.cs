using System;
using System.IO;

namespace SLCryptoAuth.IO;

public class BinaryWriterExtended : BinaryWriter
{
    public BinaryWriterExtended() : this(new MemoryStream())
    {
    }

    private BinaryWriterExtended(MemoryStream memoryStream) : base(memoryStream)
    {
        MemoryStream = memoryStream;
    }

    public int Length => Convert.ToInt32(base.BaseStream.Length);
    
    private MemoryStream MemoryStream { get; }

    public int Position
    {
        get => Convert.ToInt32(base.BaseStream.Position);
        set => base.BaseStream.Position = value;
    }

    public void WriteArrayWithLength(byte[] buffer)
    {
        base.Write(buffer.Length);
        base.Write(buffer);
    }

    public void WriteArrayWithLength(byte[] buffer, int index, int count)
    {
        if (buffer.Length - index < count)
            count = buffer.Length - index;

        base.Write(count);
        base.Write(buffer, index, count);
    }

    public void Write(BinaryWriterExtended binaryWriterExtended)
    {
        var data = binaryWriterExtended.ToArray();
        base.Write(data);
    }

    public byte[] ToArray()
    {
        return MemoryStream.ToArray();
    }
}
