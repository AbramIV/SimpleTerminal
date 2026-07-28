using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums;

public enum ModbusWriteFunction
{
    WriteSingleCoil = 1,
    WriteSigleRegister = 2,
    None = 0,
}
