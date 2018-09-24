using System;
using System.IO.Ports;

public static class WorkWithPort
{
    public static SerialPort FindArduino(string[] ports)
    {
        SerialPort port = null;
        string str = null;
        for (int i = 0; i < ports.Length; ++i)
        {
            try
            {
                port = new SerialPort(ports[i], 9600);
                port.Open();
                port.ReadTimeout = 5000;
                str = port.ReadLine();
            }
            catch
            {
                port = null;
                continue;
            }
            if (str == "LED Strip") break;
            port = null;
        }
        if (port == null) throw new ArgumentNullException("port", "Порт не найден");
        return port;
    }
}