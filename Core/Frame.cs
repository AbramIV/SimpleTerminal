using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core;

internal class Frame
{
    internal byte[]? Start;
    internal byte Address;
    internal ProtocolDataUnit? PDU;
    internal readonly int? CRC;
    internal byte[]? End;

    internal Frame() { }

    internal Frame(byte[]? start, byte address, ProtocolDataUnit? adu, byte[]? end)
    {
        Start = start;
        Address = address;
        PDU = adu;
        End = end;
    }

    internal IEnumerable<byte> GetAsBytes()
    {
        if (Start is null || End is null || PDU is null || CRC is null)
            throw new InvalidOperationException("Frame components cannot be null.");

        return Start
            .Concat([Address])
            .Concat(PDU.GetSequence())
            .Concat(CheckSum.Calculate_CRC16(PDU.GetSequence(), 0x8005))
            .Concat(End);
    }

    /// <summary>
    /// Assemble Frame from received bytes.
    /// </summary>
    /// <param name="bytes">Received bytes.</param>
    /// <returns>FRame as parsed array of bytes.</returns>
    internal static Frame GetFrame(byte[] bytes)
    {
        return new Frame();
    }
}
