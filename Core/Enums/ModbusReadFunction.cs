using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums;

public enum ModbusReadFunction
{
    ReadCoils = 1,
    ReadContacts = 2,
    ReadHoldingRegisters = 3,
    ReadInputRegisters = 4,
    None = 0,
}
