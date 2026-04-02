using Core.Enums;
using Core.Helpers;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Core;

public class ProtocolDataUnit(ModbusFunction function,int startAddress, int numberOfRegisters, byte[]? data = null)
{
    private readonly ModbusFunction function = function;
    private readonly byte startLSB = (byte)startAddress;
    private readonly byte startMSB = (byte)(startAddress >> 8);
    private readonly byte numberLSB = (byte)numberOfRegisters;
    private readonly byte numberMSB = (byte)(numberOfRegisters >> 8);
    private readonly List<byte>? unit = [];
    private readonly byte[]? bytes = data;

    public byte[] GetAsBytesArray()
    {
        unit?.Add((byte)function);
        unit?.Add(startMSB);
        unit?.Add(startLSB);
        unit?.Add(numberMSB);
        unit?.Add(numberLSB);

        if (function > ModbusFunction.ReadInputRegisters && bytes is null)
            throw new Exception("Data is null!");
        else if (bytes is not null)
            unit?.AddRange(bytes);

        if (unit is null) throw new Exception("Unit is null!");

        return [.. unit];
    }
}