using Core;
using Core.Enums;
using Core.Helpers;

Console.Title = "Modbus interceptor";
Console.ForegroundColor = ConsoleColor.Green;

Modbus modbus = new("COM1", 250000);
AppDataUnit apu = new(ProtocolTypes.ASCII);

var t = NumConverter.GetNibblesBytes((char)Terminators.LineFeed);

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

        if (type == DataUnitTypes.Prefix) continue;

        if (type == DataUnitTypes.CRC)
        {
            apu.GetCRC8();
            break;
        }

        Console.Write($"{type}: ");

        unit = int.Parse(Console.ReadLine());

        apu.AddBytes(type, NumConverter.GetNibblesBytes(unit.ToString("X")));
    }

    var b = apu.GetBuffer();

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