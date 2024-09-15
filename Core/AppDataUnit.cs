using Core.Enums;
using Core.Helpers;

namespace Core;

public class AppDataUnit
{
    private readonly Dictionary<DataUnitTypes, byte[]> units;

    public AppDataUnit(ProtocolTypes type)
    {
        byte[] prefix;
        byte[] init;

        if (type.Equals(ProtocolTypes.ASCII))
        {
            prefix = ":"u8.ToArray();
            init = "0"u8.ToArray();
        }
        else
        {
            prefix = [0];
            init = [0];
        } 

        units = new() 
        {
            { DataUnitTypes.Prefix, prefix},
            { DataUnitTypes.Address, init },
            { DataUnitTypes.Function, init },
            { DataUnitTypes.StartRegister, init },
            { DataUnitTypes.Length, init },
            { DataUnitTypes.Data, init },
            { DataUnitTypes.CRC, init },
            { DataUnitTypes.LF, init },
            { DataUnitTypes.CR, init }
        };
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
