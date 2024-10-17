using Core;
using Core.Enums;
using Core.Helpers;

Console.Title = "Modbus interceptor";
Console.ForegroundColor = ConsoleColor.Green;

Modbus modbus = new("COM1", 250000);
AppDataUnit apu = new(ProtocolTypes.RTU);

var temp1 = NumConverter.DecToHex(0x257);
var temp2 = NumConverter.StringToHex("257");

Console.Write("1. Hexadecimal\n" +
              "2. Decimal\n" +
              "Number system: ");

try
{
    var system = int.Parse(Console.ReadLine());
    var types = Enum.GetNames<DataUnitTypes>().Select(Enum.Parse<DataUnitTypes>);

    foreach (var type in types)
    {
        int unit;

        Console.Write($"{type}: ");

        unit = int.Parse(Console.ReadLine());

        apu.AddBytes(type, NumConverter.GetNibblesBytes(unit.ToString("X")));
    }

    Console.WriteLine(apu);
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