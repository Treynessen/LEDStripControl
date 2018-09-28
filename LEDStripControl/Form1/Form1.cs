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

        Modes current_mode = Modes.NotConnected;
        DateTime time_alcm = DateTime.Now; // time after last change mode

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
            TimeSpan one_second = new TimeSpan(0, 0, 1);
            TimeSpan time = DateTime.Now - time_alcm;
            if (one_second > time) Thread.Sleep(one_second - time);

            CloseThePort();
            ConnectContextMenu.Enabled = true;
            DisconnectContextMenu.Enabled = false;
            DisableModes();
            current_mode = Modes.NotConnected;
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
            TimeSpan one_second = new TimeSpan(0, 0, 1);
            TimeSpan time = DateTime.Now - time_alcm;
            if (one_second > time) Thread.Sleep(one_second - time);

            byte[] buffer = new byte[4];
            buffer[0] = (byte)Modes.StaticColor;
            buffer[1] = standart_rgb[0];
            buffer[2] = standart_rgb[1];
            buffer[3] = standart_rgb[2];
            port.Write(buffer, 0, buffer.Length);
            current_mode = Modes.StaticColor;
            EnableModes(current_mode);

            time_alcm = DateTime.Now;
        }

        private void AnimationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeSpan one_second = new TimeSpan(0, 0, 1);
            TimeSpan time = DateTime.Now - time_alcm;
            if (one_second > time) Thread.Sleep(one_second - time);

            byte[] buffer = new byte[1];
            buffer[0] = (byte)Modes.Animation;
            port.Write(buffer, 0, buffer.Length);
            current_mode = Modes.Animation;
            EnableModes(current_mode);

            time_alcm = DateTime.Now;
        }

        private void AmbilightToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void PolishFlagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeSpan one_second = new TimeSpan(0, 0, 1);
            TimeSpan time = DateTime.Now - time_alcm;
            if (one_second > time) Thread.Sleep(one_second - time);

            byte[] buffer = new byte[1];
            buffer[0] = (byte)Modes.PolishFlag;
            port.Write(buffer, 0, buffer.Length);
            current_mode = Modes.PolishFlag;
            EnableModes(current_mode);

            time_alcm = DateTime.Now;
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
                current_mode = Modes.NotConnected;
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
            if (current_mode == Modes.StaticColor) StaticToolStripMenuItem_Click(this, null);
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
            AnimationToolStripMenuItem.Enabled = true;
            AmbilightToolStripMenuItem.Enabled = true;
            PolishFlagToolStripMenuItem.Enabled = true;
        }

        private void EnableModes(Modes without_mode)
        {
            StaticToolStripMenuItem.Enabled = true;
            AnimationToolStripMenuItem.Enabled = true;
            AmbilightToolStripMenuItem.Enabled = true;
            PolishFlagToolStripMenuItem.Enabled = true;
            if (without_mode == Modes.StaticColor) StaticToolStripMenuItem.Enabled = false;
            else if (without_mode == Modes.Animation) AnimationToolStripMenuItem.Enabled = false;
            else if (without_mode == Modes.Ambilight) AmbilightToolStripMenuItem.Enabled = false;
            else if (without_mode == Modes.PolishFlag) PolishFlagToolStripMenuItem.Enabled = false;
        }

        private void DisableModes()
        {
            StaticToolStripMenuItem.Enabled = false;
            AnimationToolStripMenuItem.Enabled = false;
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
                buffer[0] = (byte)Modes.NotConnected;
                port.Write(buffer, 0, buffer.Length);
                port.Close();
                connected = false;
            }
        }
    }
}