using System;
using System.IO.Ports;

enum Modes
{
    Static,
    Ambilight,
    PolishFlag,
    OFF,
    Waiting = 255
}

public static class WorkWithPort
{
    public static SerialPort FindArduino(string[] ports)
    {
        SerialPort port = null;
        for (int i = 0; i < ports.Length; ++i)
        {
            try
            {
                port = TryToConnect(ports[i]);
            }
            catch
            {
                port = null;
                continue;
            }
            if (port != null) break;
        }
        if (port == null) throw new ArgumentNullException("port", "Порт не найден");
        return port;
    }

    public static SerialPort TryToConnect(string port_name)
    {
        SerialPort port = null;
        string str = null;
        try
        {
            port = new SerialPort(port_name, 9600);
            port.Open();
            port.ReadTimeout = 5000;
            str = port.ReadLine();
        }
        catch
        {
            port = null;
            throw;
        }
        if (str == "LED Strip") return port;
        else return null;
    }
}