using Core.Enums;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using Counter = System.Timers.Timer;

namespace Core;

public class Modbus : IDisposable
{
    private readonly SerialPort port;
    private readonly Counter limiter;
    private readonly byte[] buffer;

    public Modbus(string portName, int baudRate = 9600, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
    {
        port = new SerialPort(portName, baudRate, parity, dataBits, stopBits) 
        { 
            PortName = portName,
            BaudRate = baudRate,
            Parity = parity,
            DataBits = dataBits,
            StopBits = stopBits
        };
        port.DataReceived += Port_DataReceived;
        limiter = new(100);
        limiter.Elapsed += Limiter_Elapsed;
        buffer = []; // ascii 513 or rtu 256
    }

    public void Connect()
    {
        port?.Open();
    }

    public void Write(byte[] bytes)
    {
        port.Write(bytes, 0, bytes.Length);
        limiter.Start();
    }

    public byte[] ReadAsBytes() => buffer;

    public string ReadAsString() => string.Join("", buffer);

    public void Dispose()
    {
        port?.Close();
        port?.Dispose();

        limiter?.Stop();
        limiter?.Close();
        limiter?.Dispose();

        GC.Collect();
        GC.SuppressFinalize(this);
    }

    private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        var serial = (SerialPort)sender;
        _ = serial.Read(buffer, 0, serial.BytesToRead);

        if (buffer.Last().Equals(Terminators.LineFeed))
        {
            limiter.Stop();
        }
    }

    private void Limiter_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        limiter.Stop();
        port?.DiscardInBuffer();
        throw new TimeoutException("Response timout exceeded.");
    }
}
