using Core;
using Core.Enums;
using Core.Helpers;

Console.Title = "Modbus interceptor";
Console.ForegroundColor = ConsoleColor.Green;

Modbus modbus = new("COM1", 250000);
ProtocolDataUnit pdu = new(ModbusFunction.ReadInputRegisters, 0, 1);
Frame frame = new(1, pdu);

try
{
    var data = frame.GetAsBytes();
    
    for (int i = 0; i < data.Count(); i++)
    {
        Console.Write($"{i}. {data.ElementAt(i)}\n");
    }


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