using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace LEDStripControl
{
    public partial class Form1 : Form
    {
        bool shut_down = false; // выключение программы
        bool connected = false; // подключение к ардуино
        string[] ports = null; // список доступных COM портов
        SerialPort port = null; // COM порт к которому подключено ардуино
        string port_name = null;
        byte[] standart_rgb = { 0, 0, 0 }; // данные для статической подсветки
        ScreenSize screen_size = new ScreenSize(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        bool corner_leds = false;
        int amount_vertical_led = 0;
        int amount_horizontal_led = 0;
        bool ambilight_on = false;
        Modes current_mode = Modes.Waiting;
        Thread th;

        public Form1()
        {
            InitializeComponent();
            RefreshPortsButton_Click(this, null);
            try
            {
                port = WorkWithPort.FindArduino(ports);
            }
            catch (ArgumentNullException e)
            {
                MessageBox.Show(e.Message);
                DisconnectContextMenu.Enabled = false;
                DisableModes();
            }
            finally
            {
                ConnectContextMenu.Enabled = false;
            }
            if (port != null)
            {
                port_name = port.PortName;
                PortsComboBox.SelectedItem = port_name;
                connected = true;
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!shut_down) e.Cancel = true;
            Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseThePort();
        }

        /*=============== Контекстное меню ===============*/

        private void ConnectContextMenu_Click(object sender, EventArgs e)
        {
            ConnectToPortButton_Click(this, null);
        }

        private void DisconnectContextMenu_Click(object sender, EventArgs e)
        {
            if (ambilight_on)
            {
                ambilight_on = false;
                if (th != null) th.Join();
            }
            CloseThePort();
            ConnectContextMenu.Enabled = true;
            DisconnectContextMenu.Enabled = false;
            DisableModes();
            current_mode = Modes.OFF;
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeforeShutDown();
            Application.Exit();
        }

        /*================================================*/

        /*==================== Режимы ====================*/

        private void StaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ambilight_on)
            {
                ambilight_on = false;
                if (th != null) th.Join();
            }
            byte[] buffer = new byte[4];
            buffer[0] = (byte)Modes.Static;
            buffer[1] = standart_rgb[0];
            buffer[2] = standart_rgb[1];
            buffer[3] = standart_rgb[2];
            port.Write(buffer, 0, buffer.Length);
            if (current_mode != Modes.Static)
            {
                current_mode = Modes.Static;
                EnableModes(current_mode);
            }
        }

        private void AmbilightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (amount_vertical_led == 0 || amount_horizontal_led == 0)
            {
                MessageBox.Show("Характеристики светодиодной ленты указаны неверно", "Ошибка");
                return;
            }
            ambilight_on = true;
            th = new Thread(SendDataForAmbilight);
            EnableModes(Modes.Ambilight);
            th.Start();
        }

        private void PolishFlagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ambilight_on)
            {
                ambilight_on = false;
                if (th != null) th.Join();
            }
            byte[] buffer = new byte[1];
            buffer[0] = (byte)Modes.PolishFlag;
            port.Write(buffer, 0, buffer.Length);
            current_mode = Modes.PolishFlag;
            EnableModes(current_mode);
        }

        /*================================================*/

        /*=========== Кнопки на форме настроек ===========*/

        private void ConnectToPortButton_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                if (PortsComboBox.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Не выбран COM-порт", "Ошибка");
                    return;
                }
                try
                {
                    port = WorkWithPort.TryToConnect(PortsComboBox.SelectedItem.ToString());
                }
                catch
                {
                    MessageBox.Show("Невозможно подключиться к выбранному порту", "Ошибка");
                    port = null;
                    port_name = null;
                    connected = false;
                    return;
                }
                port_name = port.PortName;
                connected = true;
                ConnectContextMenu.Enabled = false;
                DisconnectContextMenu.Enabled = true;
                current_mode = Modes.Waiting;
                EnableModes();
                MessageBox.Show($"Успешное подключение к {port.PortName} порту");
            }
            else if (PortsComboBox.SelectedItem.ToString() == port.PortName)
            {
                MessageBox.Show("Подключение к этому порту уже произведено");
            }
            else
            {
                DisconnectContextMenu_Click(this, null);
                SaveAndConnectPortButton.PerformClick();
            }
        }

        private void RefreshPortsButton_Click(object sender, EventArgs e)
        {
            ports = SerialPort.GetPortNames();
            PortsComboBox.Items.Clear();
            PortsComboBox.Items.AddRange(ports);
        }

        private void SaveRGB_Click(object sender, EventArgs e)
        {
            try
            {
                standart_rgb[0] = Convert.ToByte(RedTextBox.Text);
            }
            catch (FormatException)
            {
                RedTextBox.Text = "0";
                standart_rgb[0] = 0;
            }
            try
            {
                standart_rgb[1] = Convert.ToByte(GreenTextBox.Text);
            }
            catch (FormatException)
            {
                GreenTextBox.Text = "0";
                standart_rgb[1] = 0;
            }
            try
            {
                standart_rgb[2] = Convert.ToByte(BlueTextBox.Text);
            }
            catch (FormatException)
            {
                BlueTextBox.Text = "0";
                standart_rgb[2] = 0;
            }
            if (current_mode == Modes.Static) StaticToolStripMenuItem_Click(this, null);
            MessageBox.Show("Изменения сохранены");
        }

        private void SaveCharacteristicsButton_Click(object sender, EventArgs e)
        {
            try
            {
                amount_vertical_led = Convert.ToInt32(verticalLEDTextBox.Text);
                amount_horizontal_led = Convert.ToInt32(horizontalLEDTextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Данные указаны неверно", "Ошибка");
                return;
            }
            if (amount_vertical_led < 0 || amount_horizontal_led < 0)
            {
                amount_vertical_led = 0;
                amount_horizontal_led = 0;
                MessageBox.Show("Данные указаны неверно", "Ошибка");
                verticalLEDTextBox.Text = "0";
                horizontalLEDTextBox.Text = "0";
            }
            corner_leds = CorneLEDsCheckBox.Checked;
        }

        /*================================================*/

        private void EnableModes()
        {
            StaticToolStripMenuItem.Enabled = true;
            AmbilightToolStripMenuItem.Enabled = true;
            PolishFlagToolStripMenuItem.Enabled = true;
        }

        private void EnableModes(Modes without_mode)
        {
            StaticToolStripMenuItem.Enabled = true;
            AmbilightToolStripMenuItem.Enabled = true;
            PolishFlagToolStripMenuItem.Enabled = true;
            if (without_mode == Modes.Static) StaticToolStripMenuItem.Enabled = false;
            else if (without_mode == Modes.Ambilight) AmbilightToolStripMenuItem.Enabled = false;
            else if (without_mode == Modes.PolishFlag) PolishFlagToolStripMenuItem.Enabled = false;
        }

        private void DisableModes()
        {
            StaticToolStripMenuItem.Enabled = false;
            AmbilightToolStripMenuItem.Enabled = false;
            PolishFlagToolStripMenuItem.Enabled = false;
        }

        private void BeforeShutDown()
        {
            DisconnectContextMenu.PerformClick();
            shut_down = true;
        }

        private void CloseThePort()
        {
            if (port != null && connected)
            {
                byte[] buffer = new byte[1];
                buffer[0] = (byte)Modes.OFF;
                port.Write(buffer, 0, buffer.Length);
                port.Close();
                connected = false;
            }
        }

        private void SendDataForAmbilight()
        {
            int height_size = screen_size.Height / (amount_vertical_led + 2);
            int width_size = screen_size.Width / (amount_horizontal_led + 2);

            byte[] corners = new byte[4 * 3];
            byte[] leds = new byte[(amount_vertical_led + amount_horizontal_led) * 2 * 3];

            FastGetPixel get_pixel = new FastGetPixel();
            DateTime dt = new DateTime();

            int r_1 = 0, g_1 = 0, b_1 = 0; // для первой полосы
            int r_2 = 0, g_2 = 0, b_2 = 0; // для второй полосы

            Color color = default(Color);

            int time = 0;

            byte[] mode = new byte[1];
            mode[0] = (byte)Modes.Ambilight;
            port.Write(mode, 0, mode.Length);

            while (ambilight_on)
            {
                dt = DateTime.Now;
                get_pixel.LockWindowImage();

                // Данные для вертикальных полос
                for (int i = 1, it_l = 0, it_r = (2 * amount_vertical_led + amount_horizontal_led) * 3 - 1; i <= amount_vertical_led; ++i)
                {
                    for (int width = 0; width < width_size; ++width)
                    {
                        for (int height = (height_size * (amount_vertical_led + 2 - i)) - 1; height >= (height_size * (amount_vertical_led + 2 - i - 1)); --height)
                        {
                            color = get_pixel.GetLockedPixel(width, height);
                            r_1 += color.R; g_1 += color.G; b_1 += color.B;
                            color = get_pixel.GetLockedPixel(width + screen_size.Width - width_size, height);
                            r_2 += color.R; g_2 += color.G; b_2 += color.B;
                        }
                    }
                    leds[it_l++] = (byte)(r_1 / (height_size * width_size));
                    leds[it_l++] = (byte)(g_1 / (height_size * width_size));
                    leds[it_l++] = (byte)(b_1 / (height_size * width_size));
                    leds[it_r--] = (byte)(b_2 / (height_size * width_size));
                    leds[it_r--] = (byte)(g_2 / (height_size * width_size));
                    leds[it_r--] = (byte)(r_2 / (height_size * width_size));
                    r_1 = g_1 = b_1 = r_2 = g_2 = b_2 = 0;
                }

                // Данные для горизонтальных полос
                for (int i = 0, it_up = amount_vertical_led * 3, it_d = (2 * amount_vertical_led + 2 * amount_horizontal_led) * 3 - 1; i <= amount_horizontal_led + 1; ++i)
                {
                    for (int width = width_size * i; width < width_size * (i + 1); ++width)
                    {
                        for (int height = 0; height < height_size; ++height)
                        {
                            color = get_pixel.GetLockedPixel(width, height);
                            r_1 += color.R; g_1 += color.G; b_1 += color.B;
                            color = get_pixel.GetLockedPixel(width, height + screen_size.Height - height_size);
                            r_2 += color.R; g_2 += color.G; b_2 += color.B;
                        }
                    }
                    if (i > 0 && i <= amount_horizontal_led)
                    {
                        leds[it_up++] = (byte)(r_1 / (height_size * width_size));
                        leds[it_up++] = (byte)(g_1 / (height_size * width_size));
                        leds[it_up++] = (byte)(b_1 / (height_size * width_size));
                        leds[it_d--] = (byte)(b_2 / (height_size * width_size));
                        leds[it_d--] = (byte)(g_2 / (height_size * width_size));
                        leds[it_d--] = (byte)(r_2 / (height_size * width_size));
                    }
                    else
                    {
                        if (i == 0)
                        {
                            corners[0] = (byte)(r_1 / (height_size * width_size));
                            corners[1] = (byte)(g_1 / (height_size * width_size));
                            corners[2] = (byte)(b_1 / (height_size * width_size));
                            corners[9] = (byte)(r_2 / (height_size * width_size));
                            corners[10] = (byte)(g_2 / (height_size * width_size));
                            corners[11] = (byte)(b_2 / (height_size * width_size));
                        }
                        else
                        {
                            corners[3] = (byte)(r_1 / (height_size * width_size));
                            corners[4] = (byte)(g_1 / (height_size * width_size));
                            corners[5] = (byte)(b_1 / (height_size * width_size));
                            corners[6] = (byte)(r_2 / (height_size * width_size));
                            corners[7] = (byte)(g_2 / (height_size * width_size));
                            corners[8] = (byte)(b_2 / (height_size * width_size));
                        }
                    }
                    r_1 = g_1 = b_1 = r_2 = g_2 = b_2 = 0;
                }
                get_pixel.Clear();

                if (corner_leds)
                {
                    port.Write(leds, 0, amount_vertical_led * 3);
                    port.Write(corners, 0, 3);
                    port.Write(leds, amount_vertical_led * 3, amount_horizontal_led * 3);
                    port.Write(corners, 3, 3);
                    port.Write(leds, (amount_vertical_led + amount_horizontal_led) * 3, amount_vertical_led * 3);
                    port.Write(corners, 6, 3);
                    port.Write(leds, (2 * amount_vertical_led + amount_horizontal_led) * 3, amount_horizontal_led * 3);
                    port.Write(corners, 9, 3);
                }
                else
                {
                    // левый верхний угол
                    // вертикальная полоса
                    leds[amount_vertical_led * 3 - 3] = (byte)((leds[amount_vertical_led * 3 - 3] + corners[0]) / 2); // R
                    leds[amount_vertical_led * 3 - 2] = (byte)((leds[amount_vertical_led * 3 - 2] + corners[1]) / 2); // G
                    leds[amount_vertical_led * 3 - 1] = (byte)((leds[amount_vertical_led * 3 - 1] + corners[2]) / 2); // B
                    //горизонтальная полоса
                    leds[amount_vertical_led * 3] = (byte)((leds[amount_vertical_led * 3] + corners[0]) / 2); // R
                    leds[amount_vertical_led * 3 + 1] = (byte)((leds[amount_vertical_led * 3 + 1] + corners[1]) / 2); // G
                    leds[amount_vertical_led * 3 + 2] = (byte)((leds[amount_vertical_led * 3 + 2] + corners[2]) / 2); // B

                    // правый верхний угол
                    // горизонтальная полоса
                    leds[(amount_vertical_led + amount_horizontal_led) * 3 - 3] = (byte)((leds[(amount_vertical_led + amount_horizontal_led) * 3 - 3] + corners[3]) / 2); // R
                    leds[(amount_vertical_led + amount_horizontal_led) * 3 - 2] = (byte)((leds[(amount_vertical_led + amount_horizontal_led) * 3 - 2] + corners[4]) / 2); // G
                    leds[(amount_vertical_led + amount_horizontal_led) * 3 - 1] = (byte)((leds[(amount_vertical_led + amount_horizontal_led) * 3 - 1] + corners[5]) / 2); // B
                    // вертикальная полоса
                    leds[(amount_vertical_led + amount_horizontal_led) * 3] = (byte)((leds[(amount_vertical_led + amount_horizontal_led) * 3] + corners[3]) / 2); // R
                    leds[(amount_vertical_led + amount_horizontal_led) * 3 + 1] = (byte)((leds[(amount_vertical_led + amount_horizontal_led) * 3 + 1] + corners[4]) / 2); // G
                    leds[(amount_vertical_led + amount_horizontal_led) * 3 + 2] = (byte)((leds[(amount_vertical_led + amount_horizontal_led) * 3 + 2] + corners[5]) / 2); // B

                    // правый нижний угол
                    // вертикальная полоса
                    leds[(2 * amount_vertical_led + amount_horizontal_led) * 3 - 3] = (byte)((leds[(2 * amount_vertical_led + amount_horizontal_led) * 3 - 3] + corners[6]) / 2); // R
                    leds[(2 * amount_vertical_led + amount_horizontal_led) * 3 - 2] = (byte)((leds[(2 * amount_vertical_led + amount_horizontal_led) * 3 - 2] + corners[7]) / 2); // G
                    leds[(2 * amount_vertical_led + amount_horizontal_led) * 3 - 1] = (byte)((leds[(2 * amount_vertical_led + amount_horizontal_led) * 3 - 1] + corners[8]) / 2); // B
                    // горизонтальная полоса
                    leds[(2 * amount_vertical_led + amount_horizontal_led) * 3] = (byte)((leds[(2 * amount_vertical_led + amount_horizontal_led) * 3] + corners[6]) / 2); // R
                    leds[(2 * amount_vertical_led + amount_horizontal_led) * 3 + 1] = (byte)((leds[(2 * amount_vertical_led + amount_horizontal_led) * 3 + 1] + corners[7]) / 2); // G
                    leds[(2 * amount_vertical_led + amount_horizontal_led) * 3 + 2] = (byte)((leds[(2 * amount_vertical_led + amount_horizontal_led) * 3 + 2] + corners[8]) / 2); // B

                    // левый нижний угол
                    // горизонтальная полоса
                    leds[(2 * amount_vertical_led + 2 * amount_horizontal_led) * 3 - 3] = (byte)((leds[(2 * amount_vertical_led + 2 * amount_horizontal_led) * 3 - 3] + corners[9]) / 2); // R
                    leds[(2 * amount_vertical_led + 2 * amount_horizontal_led) * 3 - 2] = (byte)((leds[(2 * amount_vertical_led + 2 * amount_horizontal_led) * 3 - 2] + corners[10]) / 2); // G
                    leds[(2 * amount_vertical_led + 2 * amount_horizontal_led) * 3 - 1] = (byte)((leds[(2 * amount_vertical_led + 2 * amount_horizontal_led) * 3 - 1] + corners[11]) / 2); // B
                    // вертикальная полоса
                    leds[0] = (byte)((leds[0] + corners[9]) / 2); //R
                    leds[1] = (byte)((leds[1] + corners[10]) / 2); //G
                    leds[2] = (byte)((leds[2] + corners[11]) / 2); //B

                    port.Write(leds, 0, leds.Length);
                }

                time = 200 - (DateTime.Now - dt).Milliseconds;
                Thread.Sleep(time > 0 ? time : 0);
            }
            Thread.Sleep(1200);
        }
    }
}
