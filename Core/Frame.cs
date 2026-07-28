using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core;

public class Frame : IComparable<Frame>
{
    private const int POLYNOM = 0xA001; // Standard Modbus polynomial for CRC16

    public byte Address;
    public ProtocolDataUnit? PDU;
    public readonly byte CRC_MSB;
    public readonly byte CRC_LSB;

    public Frame() { }

    public Frame(byte address, ProtocolDataUnit? pdu)
    {
        Address = address;
        PDU = pdu;
        var crc = CheckSum.Calculate_CRC16(PDU.GetAsBytes(), POLYNOM);
        CRC_MSB = crc[0];
        CRC_LSB = crc[1];
    }

    public IEnumerable<byte> GetAsBytes()
    {
        if (PDU is null)
            throw new InvalidOperationException("Frame components cannot be null.");

        return new List<byte>([Address])
                     .Concat(PDU.GetAsBytes())
                     .Concat([CRC_MSB])
                     .Concat([CRC_LSB]);
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

    public int CompareTo(Frame? other)
    {
        throw new NotImplementedException();
    }

    public void TitledFrame()
    {
        Console.WriteLine($"Slave address: {NumConverter.FormatHex(Address)}");
        Console.WriteLine($"Function code: {NumConverter.FormatHex(PDU.Function)}");
        Console.WriteLine($"Start address MSB: {NumConverter.FormatHex(PDU.StartMSB)}");
        Console.WriteLine($"Start address LSB: {NumConverter.FormatHex(PDU.StartLSB)}");
        Console.WriteLine($"Quantity MSB: {NumConverter.FormatHex(PDU.StartNumberMSB)}");
        Console.WriteLine($"Quantity LSB: {NumConverter.FormatHex(PDU.StartNumberLSB)}");
        Console.WriteLine($"CRC16 MSB: {NumConverter.FormatHex(CRC_MSB)}");
        Console.WriteLine($"CRC16 LSB: {NumConverter.FormatHex(CRC_LSB)}");
    }

    public override string ToString()
    {
        var data = GetAsBytes();
        StringBuilder sb = new();

        for (int i = 0; i < data.Count(); i++)
           sb.Append($"{NumConverter.FormatHex(data.ElementAt(i))} ");

        return sb.ToString();
    }
}
