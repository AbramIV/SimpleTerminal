using Core.Enums;
using Core.Helpers;
using System.Text;

namespace Core;

public class AppDataUnit
{
    private readonly Dictionary<DataUnitTypes, byte[]> units;

    public AppDataUnit(ProtocolTypes type)
    {
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
            units.Add(DataUnitTypes.LF, [(byte)Terminators.CarriageReturn]);
            units.Add(DataUnitTypes.Prefix, [(byte)Terminators.LineFeed]);

            return;
        }

        units.Add(DataUnitTypes.Prefix, NumConverter.GetNibblesBytes((char)Terminators.Colon));
        units.Add(DataUnitTypes.Prefix, NumConverter.GetNibblesBytes((char)Terminators.CarriageReturn));
        units.Add(DataUnitTypes.Prefix, NumConverter.GetNibblesBytes((char)Terminators.LineFeed));
    }

    public void AddBytes(DataUnitTypes type, byte[] bytes)
    {
        ArgumentNullException.ThrowIfNull(bytes);

        units[type] = bytes;
    }

    public IEnumerable<byte> GetProtocolDataUnit() =>
        units[DataUnitTypes.Function].Concat(units[DataUnitTypes.Data]);

    public override string ToString() => 
        string.Join("", units.Values.Select(NumConverter.DecimalToHexAsString));
}