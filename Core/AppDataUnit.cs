using Core.Enums;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Core;

public class AppDataUnit
{
    private readonly DataUnit[] units;

    public AppDataUnit(ProtocolTypes type, byte[]? bytes = null)
    {
        if (bytes is null)
        {
            units = new DataUnit[9];
            units[0] = type == ProtocolTypes.ASCII ?
                               new(DataUnitTypes.Prefix, ":"u8.ToArray()) :
                               new(DataUnitTypes.Prefix, "0000"u8.ToArray());
        }
        else
            units = new DataUnit[bytes.Length];
    }

    #region Methods

    public void AddDataUnit(DataUnit unit)
    {
        ArgumentNullException.ThrowIfNull(unit);

        if (units.Select(u => u?.DataUnitType).Contains(unit.DataUnitType))
            throw new ArgumentException($"{unit.DataUnitType} already added.");

        units[(int)unit.DataUnitType] = unit;
    }

    public void ReplaceDataUnit(DataUnit unit)
    {
        ArgumentNullException.ThrowIfNull(unit);
        units[(int)unit.DataUnitType] = unit;
    }

    public bool Validate()
    {
        return false;
    }

    public IEnumerable<DataUnit> GetProtocolDataUnit()
    {
        return units;
    }

    public static AppDataUnit Parse(byte[] bytes)
    {
        return new AppDataUnit(ProtocolTypes.ASCII);
    }

    //public byte[] GetAsciiCode(string input)
    //{
    //    return input.ToArray();
    //}

    public Dictionary<DataUnitTypes, string> ToDictionary()
    {
        return new Dictionary<DataUnitTypes, string>();
    }

    public override string ToString()
        => string.Join("", units?.Select(u => NumConverter.DecimalToHexAsString(u?.Data)));

    #endregion
}
