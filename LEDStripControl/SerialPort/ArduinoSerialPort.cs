using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

public sealed partial class ArduinoSerialPort : IDisposable
{
    private SerialPort port;
    public string[] Ports { get; private set; }
    public string PortName { get; private set; }
    public bool Connected { get; private set; } = false;

    private DataToSend data;
    private bool stop_data_send = true;
    private bool have_corner_leds = false;

    Modes current_mode = Modes.NotConnected;
    byte[] standart_rgb = { 0, 0, 0 }; // данные для статической подсветки

    private DateTime time_lcm = new DateTime(); // time last change mode

    public ArduinoSerialPort()
    {
        RefreshPortsList();
        FindArduino();
    }

    private void FindArduino()
    {
        for (int i = 0; i < Ports.Length; ++i)
        {
            try
            {
                Connected = TryToConnect(Ports[i]);
            }
            catch
            {
                port = null;
                continue;
            }
            if (Connected) break;
        }
        if (port == null) MessageBox.Show("Порт не найден", "Ошибка");
    }

    public bool TryToConnect(string port_name)
    {
        if (port != null && port.IsOpen) return false;
        try
        {
            port = new SerialPort(port_name, 1000000);
            port.Open();
            port.ReadTimeout = 5000;
            for (int i = 0; i < 100; ++i)
            {
                if (port.ReadLine() == "LED Strip")
                {
                    PortName = port.PortName;
                    return true;
                }
            }
        }
        catch
        {
            if (port != null && port.IsOpen) port.Close();
            port = null;
            throw;
        }
        return false;
    }

    public void Disconnect()
    {
        if (port != null && port.IsOpen)
        {
            if (!stop_data_send) stop_data_send = false;
            TimeSpan one_second = new TimeSpan(0, 0, 1);
            TimeSpan time = DateTime.Now - time_lcm;
            if (one_second > time) Thread.Sleep(one_second - time); // т.к. прошивка настроена на 1000 мс
                                                                    // задержку между изменениями режимов
            byte[] buffer = new byte[1];
            buffer[0] = (byte)Modes.NotConnected;
            port.Write(buffer, 0, buffer.Length);
            port.Close();
            current_mode = Modes.NotConnected;
        }
        Connected = false;
    }

    public void RefreshPortsList()
    {
        Ports = SerialPort.GetPortNames();
    }

    public void SetSettings(int num_vertical_leds, int num_horizontal_leds, bool have_corner_leds)
    {
        if (data == null) data = new DataToSend(num_vertical_leds, num_horizontal_leds);
        else data.RefreshSettings(num_vertical_leds, num_horizontal_leds);
        this.have_corner_leds = have_corner_leds;
    }

    public void SetRGB(byte r, byte g, byte b)
    {
        standart_rgb[0] = r;
        standart_rgb[1] = g;
        standart_rgb[2] = b;
        if (current_mode == Modes.StaticColor) SetMode(Modes.StaticColor);
    }

    public void SetMode(Modes mode)
    {
        if (mode == Modes.NotConnected) Disconnect();
        else if (port != null && port.IsOpen)
        {
            TimeSpan one_second = new TimeSpan(0, 0, 1);
            TimeSpan time = DateTime.Now - time_lcm;
            if (one_second > time) Thread.Sleep(one_second - time); // т.к. прошивка настроена на 1000 мс
                                                                    // задержку между изменениями режимов
            if (mode == Modes.Ambilight)
            {
                if (data != null)
                {
                    Thread th1 = new Thread(() =>
                    {
                        while (!stop_data_send)
                        {
                            data.RefreshData();
                        }
                    });
                    Thread th2 = new Thread(() =>
                    {
                        byte[] send_mode = new byte[1];
                        send_mode[0] = (byte)(Modes.Ambilight);
                        byte[] buffer = new byte[3 * (2 * (data.NumHorizontalLeds + data.NumVerticalLeds) + (have_corner_leds ? 4 : 0))];
                        TimeSpan sixteen_ms = new TimeSpan(0, 0, 0, 0, 16);
                        TimeSpan time_for_sleep = new TimeSpan();
                        DateTime dt = new DateTime();
                        port.Write(send_mode, 0, send_mode.Length);
                        while (!stop_data_send)
                        {
                            dt = DateTime.Now;
                            lock (data.Lock)
                            {
                                for (int i = 0, it = 0; i < 2 * (data.NumHorizontalLeds + data.NumVerticalLeds) + (have_corner_leds ? 4 : 0); ++i)
                                {
                                    if (have_corner_leds)
                                    {
                                        if (i == data.NumVerticalLeds)
                                        {
                                            buffer[it++] = data.CornerData[i].R;
                                            buffer[it++] = data.CornerData[i].G;
                                            buffer[it++] = data.CornerData[i].B;
                                        }
                                        else if (i == (data.NumVerticalLeds + data.NumHorizontalLeds))
                                        {
                                            buffer[it++] = data.CornerData[i].R;
                                            buffer[it++] = data.CornerData[i].G;
                                            buffer[it++] = data.CornerData[i].B;
                                        }
                                        else if (i == (2 * data.NumVerticalLeds + data.NumHorizontalLeds))
                                        {
                                            buffer[it++] = data.CornerData[i].R;
                                            buffer[it++] = data.CornerData[i].G;
                                            buffer[it++] = data.CornerData[i].B;
                                        }
                                        else if (i == (2 * data.NumVerticalLeds + 2 * data.NumHorizontalLeds))
                                        {
                                            buffer[it++] = data.CornerData[i].R;
                                            buffer[it++] = data.CornerData[i].G;
                                            buffer[it++] = data.CornerData[i].B;
                                        }
                                    }
                                    else if (i == data.NumVerticalLeds - 1 || i == data.NumVerticalLeds)
                                    {
                                        buffer[it++] = (byte)((data.Data[i].R + data.CornerData[0].R) / 2);
                                        buffer[it++] = (byte)((data.Data[i].G + data.CornerData[0].G) / 2);
                                        buffer[it++] = (byte)((data.Data[i].B + data.CornerData[0].B) / 2);
                                    }
                                    else if (i == data.NumVerticalLeds + data.NumHorizontalLeds - 1 || i == data.NumVerticalLeds + data.NumHorizontalLeds)
                                    {
                                        buffer[it++] = (byte)((data.Data[i].R + data.CornerData[1].R) / 2);
                                        buffer[it++] = (byte)((data.Data[i].G + data.CornerData[1].G) / 2);
                                        buffer[it++] = (byte)((data.Data[i].B + data.CornerData[1].B) / 2);
                                    }
                                    else if (i == 2 * data.NumVerticalLeds + data.NumHorizontalLeds - 1 || i == 2 * data.NumVerticalLeds + data.NumHorizontalLeds)
                                    {
                                        buffer[it++] = (byte)((data.Data[i].R + data.CornerData[2].R) / 2);
                                        buffer[it++] = (byte)((data.Data[i].G + data.CornerData[2].G) / 2);
                                        buffer[it++] = (byte)((data.Data[i].B + data.CornerData[2].B) / 2);
                                    }
                                    else if (i == 2 * data.NumVerticalLeds + 2 * data.NumHorizontalLeds - 1 || i == 2 * data.NumVerticalLeds + 2 * data.NumHorizontalLeds)
                                    {
                                        buffer[it++] = (byte)((data.Data[i].R + data.CornerData[3].R) / 2);
                                        buffer[it++] = (byte)((data.Data[i].G + data.CornerData[3].G) / 2);
                                        buffer[it++] = (byte)((data.Data[i].B + data.CornerData[3].B) / 2);
                                    }
                                    else
                                    {
                                        buffer[it++] = data.Data[i].R;
                                        buffer[it++] = data.Data[i].G;
                                        buffer[it++] = data.Data[i].B;
                                    }
                                }
                            }
                            port.Write(buffer, 0, buffer.Length);
                            time_for_sleep = DateTime.Now - dt;
                            if (sixteen_ms > time_for_sleep) Thread.Sleep(sixteen_ms - time_for_sleep);
                        }
                    });
                    th1.Name = "RefreshData";
                    th2.Name = "SendDataToArduino";
                    stop_data_send = false;
                    th1.Start();
                    th2.Start();
                }
                else throw new ArgumentNullException("Ambilight error");
            }
            else if (mode == Modes.Animation)
            {
                byte[] buffer = new byte[1];
                buffer[0] = (byte)Modes.Animation;
                port.Write(buffer, 0, buffer.Length);
                current_mode = Modes.Animation;
            }
            else if (mode == Modes.PolishFlag)
            {
                byte[] buffer = new byte[1];
                buffer[0] = (byte)Modes.PolishFlag;
                port.Write(buffer, 0, buffer.Length);
                current_mode = Modes.PolishFlag;
            }
            else if (mode == Modes.StaticColor)
            {
                byte[] buffer = new byte[4];
                buffer[0] = (byte)Modes.StaticColor;
                buffer[1] = standart_rgb[0];
                buffer[2] = standart_rgb[1];
                buffer[3] = standart_rgb[2];
                port.Write(buffer, 0, buffer.Length);
                current_mode = Modes.StaticColor;
            }
            time_lcm = DateTime.Now;
        }
    }

    public void Dispose()
    {
        if (data != null) data.Dispose();
    }
}