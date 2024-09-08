using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core;

public class DataUnit(DataUnitTypes dataUnitType, params byte[] data)
{
    public DataUnitTypes DataUnitType { get; set; } = dataUnitType;

    public byte[] Data { get; set; } = data;
}
