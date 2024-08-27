using EasyModbus;
using System.Runtime.CompilerServices;

namespace Core;

public class Modbus : IDisposable
{
    private readonly ModbusClient client;

    public Modbus() { }

    public Modbus(string port, int baudRate, int numberOfRetries = 0)
    {
        client = new(port)
        {
            Baudrate = baudRate,
            NumberOfRetries = numberOfRetries,
        };
    }

    public void Request()
    {
        client.Connect();
    }

    public void Dispose()
    {
        client?.Disconnect();
    }
}
