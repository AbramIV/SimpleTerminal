using Core.Enums;
using Core.Helpers;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Core;

public abstract class ProtocolDataUnit(int startAddress)
{
    private readonly byte startLSB = (byte)startAddress;
    private readonly byte startMSB = (byte)(startAddress >> 8);
    private readonly List<byte>? unit = [];

    public byte Function => (byte)function;s
    public byte StartLSB => startLSB;
    public byte StartMSB => startMSB;

    public IEnumerable<byte> GetAsBytes()
    {
        unit?.Add((byte)function);
        unit?.Add(startMSB);
        unit?.Add(startLSB);
        unit?.Add(numberMSB);
        unit?.Add(numberLSB);

        if (unit is null) throw new Exception("Unit is null!");

        return [.. unit];
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