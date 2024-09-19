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
            { DataUnitTypes.CRC, [0] }
        };

        if (type.Equals(ProtocolTypes.RTU))
        {
            units.Add(DataUnitTypes.CR, [(byte)Terminators.CarriageReturn]);
            units.Add(DataUnitTypes.LF, [(byte)Terminators.LineFeed]);

            return;
        }

        units.Add(DataUnitTypes.Prefix, NumConverter.GetNibblesBytes((char)Terminators.Colon));
        units.Add(DataUnitTypes.CR, NumConverter.GetNibblesBytes((char)Terminators.CarriageReturn));
        units.Add(DataUnitTypes.LF, NumConverter.GetNibblesBytes((char)Terminators.LineFeed));
    }

    public void AddBytes(DataUnitTypes type, byte[] bytes)
    {
        ArgumentNullException.ThrowIfNull(bytes);

        if (type == DataUnitTypes.CR || type == DataUnitTypes.LF) return;

        units[type] = Protocol == ProtocolTypes.RTU ? bytes : bytes;
    }

    public IEnumerable<byte> GetProtocolDataUnit() =>
        units[DataUnitTypes.Function].Concat(units[DataUnitTypes.Data]);

    public void GetCRC8()
    {
        if (Protocol == ProtocolTypes.ASCII)
        {


            return;
        }
    }

    public IEnumerable<byte> GetBuffer()
    {
        return units.SelectMany(kvp => kvp.Value);
    }

    public override string ToString() => 
        string.Join("", units.Values.Select(NumConverter.DecimalToHexAsString));
}