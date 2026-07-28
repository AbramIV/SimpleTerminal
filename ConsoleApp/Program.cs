using Core;
using Core.Enums;
using Core.Helpers;
using static System.Console;

Title = "Modbus interceptor";
ForegroundColor = ConsoleColor.Green;

Modbus modbus = new("COM1", 250000); // port
ProtocolDataUnit pdu = new(ModbusFunction.ReadInputRegisters, 0, 1);
Frame frame = new(1, pdu);

try
{
    // frame.TitledFrame();
    WriteLine(pdu + "\r\n");
    WriteLine(frame.ToString());
}
catch (Exception ex)
{
    var color = ForegroundColor;
    ForegroundColor = ConsoleColor.Red;
    WriteLine($"{ex.Message}\n");
    ForegroundColor = color;
}
finally
{
    WriteLine("\nDone!");
}
    
ReadLine();