using Core;
using Core.Enums;
using Core.Helpers;

Console.Title = "Modbus interceptor";
Console.ForegroundColor = ConsoleColor.Green;

Modbus modbus = new("COM1", 250000);
Frame frame = new();

var temp1 = NumConverter.DecToHex(0x257);
var temp2 = NumConverter.StringToHex("257");

Console.Write("1. Hexadecimal\n" +
              "2. Decimal\n" +
              "Number system: ");

try
{

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