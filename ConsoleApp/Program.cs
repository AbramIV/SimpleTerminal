using Core;
using Core.Enums;
using Core.Helpers;
using System.ComponentModel;
using System.Text;

Console.Title = "Modbus interceptor";
Console.ForegroundColor = ConsoleColor.Green;

Modbus modbus = new("COM1", 250000);
AppDataUnit apu = new(ProtocolTypes.ASCII);

try
{
    var unitsNames = Enum.GetNames<DataUnitTypes>();

    apu.AddBytes(DataUnitTypes.Address, "2"u8.ToArray());
    apu.AddBytes(DataUnitTypes.Function, "3"u8.ToArray());
    apu.AddBytes(DataUnitTypes.StartRegister, "180"u8.ToArray());
    apu.AddBytes(DataUnitTypes.Length, "1"u8.ToArray());
    apu.AddBytes(DataUnitTypes.Data, "100"u8.ToArray());
    apu.AddBytes(DataUnitTypes.CRC, "55"u8.ToArray());
    apu.AddBytes(DataUnitTypes.CR, "\r"u8.ToArray());
    apu.AddBytes(DataUnitTypes.LF, "\n"u8.ToArray());
    apu.AddBytes(DataUnitTypes.LF, "\n"u8.ToArray());

    Console.WriteLine();

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
    Console.WriteLine("Done!");
}

Console.ReadLine();