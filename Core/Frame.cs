using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core;

public class Frame : IComparable<Frame>
{
    private const int POLYNOM = 0xA001; // Standard Modbus polynomial for CRC16

    public byte Address;
    public ProtocolDataUnit? PDU;
    public readonly int? CRC;

    private readonly byte[] start = [0xFF, 0xFF, 0xFF, 0xFF];
    private readonly byte[] end = [0xFF, 0xFF, 0xFF, 0xFF];

    public Frame() { }

    public Frame(byte address, ProtocolDataUnit? pdu)
    {
        Address = address;
        PDU = pdu;
    }

    public IEnumerable<byte> GetAsBytes()
    {
        if (PDU is null)
            throw new InvalidOperationException("Frame components cannot be null.");

        if (CRC is null)
        {

        }

        return start.Concat([Address])
                    .Concat(PDU.GetAsBytesArray())
                    .Concat(CheckSum.Calculate_CRC16(PDU.GetAsBytesArray(), POLYNOM))
                    .Concat(end);
    }

    /// <summary>
    /// Assemble Frame from received bytes.
    /// </summary>
    /// <param name="bytes">Received bytes.</param>
    /// <returns>FRame as parsed array of bytes.</returns>
    public static Frame GetFrame(byte[] bytes)
    {
        return new Frame();
    }

    public static IEnumerable<byte> GetCRC16()
    {
        return [0x00, 0x00];
    }

    public int CompareTo(Frame? other)
    {
        throw new NotImplementedException();
    }
}
