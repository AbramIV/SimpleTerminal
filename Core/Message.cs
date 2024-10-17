using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core;

internal abstract class Message
{
    internal byte[] Address;
    internal byte[] Function;
    internal byte[] StartRegister;
    internal byte[] Length;
    internal byte[] Data;
}
