using Core.Enums;
using Core.Helpers;
using System.Net.Sockets;
using System.Text;

namespace Core;

public class AppDataUnit
{
    public readonly ProtocolTypes Protocol;
    private readonly Dictionary<DataUnitTypes, byte[]> units;

    public AppDataUnit(ProtocolTypes type)
    {
        Protocol = type;

        units = new()
        {
            { DataUnitTypes.Address, [0] },
            { DataUnitTypes.Function, [0] },
            { DataUnitTypes.StartRegister, [0] },
            { DataUnitTypes.Length, [0] },
            { DataUnitTypes.Data, [0] },
        };
    }

    public void AddBytes(DataUnitTypes type, byte[] bytes)
    {
        ArgumentNullException.ThrowIfNull(bytes);

        units[type] = Protocol == ProtocolTypes.RTU ? bytes : bytes;
    }

    public byte[] GetSequence()
    {
        if (Protocol == ProtocolTypes.RTU)
        {

        }
    }

    public override string ToString() => 
        string.Join("", units.Values.Select(NumConverter.DecimalToHexAsString));
}