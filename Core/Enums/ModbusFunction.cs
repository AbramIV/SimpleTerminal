using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums;

public enum ModbusFunction
{
    ReadCoils = 1,
    ReadContacts = 2,
    ReadHoldingRegisters = 3,
    ReadInputRegisters = 4,
    WriteSingleCoil = 5,
    WriteSigleRegister = 6,
    None = 0,
}
