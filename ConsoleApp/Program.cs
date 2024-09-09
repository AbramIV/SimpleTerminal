using Core;
using Core.Enums;
using Core.Helpers;
using System.ComponentModel;
using System.Text;

Console.Title = "Modbus terminal";
Console.ForegroundColor = ConsoleColor.Green;

AppDataUnit apu = new(ProtocolTypes.ASCII);

try
{
    apu.AddDataUnit(new(DataUnitTypes.Address, "2"u8.ToArray()));
    apu.AddDataUnit(new(DataUnitTypes.Function, "3"u8.ToArray()));
    apu.AddDataUnit(new(DataUnitTypes.StartRegister, "180"u8.ToArray()));
    apu.AddDataUnit(new(DataUnitTypes.Length, "1"u8.ToArray()));
    apu.AddDataUnit(new(DataUnitTypes.Data, "100"u8.ToArray()));
    apu.AddDataUnit(new(DataUnitTypes.CRC, "55"u8.ToArray()));
    apu.AddDataUnit(new(DataUnitTypes.CR, "\r"u8.ToArray()));
    apu.AddDataUnit(new(DataUnitTypes.LF, "\n"u8.ToArray()));
    Console.WriteLine("APU: " + apu);
}
catch (Exception ex)
{
    var color = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"{ex.Message}\n");
    Console.ForegroundColor = color;
}
finally
{

}

Console.ReadLine();