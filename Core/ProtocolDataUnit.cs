using Core.Enums;
using Core.Helpers;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Core;

public class ProtocolDataUnit(ModbusFunction function, byte[] data)
{
    private readonly ModbusFunction function = function;
    private readonly List<byte>? unit;
    private readonly byte[] bytes = data;

    public byte[] GetSequence()
    {
        unit?.Add((byte)function);
        unit?.AddRange(bytes);

        return [.. unit];
    }
}