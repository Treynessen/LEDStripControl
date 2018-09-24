using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace LEDStripControl
{
    public partial class Form1 : Form
    {
        bool shut_down = false; // выключение программы
        bool connected = false; // подключение к ардуино
        string[] ports = null; // список доступных COM портов
        string port_name = null;
        SerialPort port = null; // COM порт к которому подключено ардуино
        byte[] standart_rgb = { 0, 0, 0 };
        Modes current_mode = Modes.Waiting;

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
            }
            ConnectContextMenu.Enabled = false;
            if (port != null)
            {
                port_name = port.PortName;
                PortsComboBox.SelectedItem = port_name;
                connected = true;
            }
            Application.ApplicationExit += (o, e) => BeforeShutDown();
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

        /*=============== Контекстное меню ===============*/

        private void ConnectContextMenu_Click(object sender, EventArgs e)
        {
            ConnectToPortButton_Click(this, null);
        }

        private void DisconnectContextMenu_Click(object sender, EventArgs e)
        {
            if (port != null)
            {
                byte[] buffer = new byte[1];
                buffer[0] = (byte)Modes.OFF;
                port.Write(buffer, 0, buffer.Length);
                port.Close();
                connected = false;
            }
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

        private void PolishFlagToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

        /*================================================*/

        private void EnableModes()
        {
            StaticToolStripMenuItem.Enabled = true;
            PolishFlagToolStripMenuItem.Enabled = true;
        }

        private void EnableModes(Modes without_mode)
        {
            StaticToolStripMenuItem.Enabled = true;
            PolishFlagToolStripMenuItem.Enabled = true;
            if (without_mode == Modes.Static) StaticToolStripMenuItem.Enabled = false;
            else if (without_mode == Modes.PolishFlag) PolishFlagToolStripMenuItem.Enabled = false;
        }

        private void DisableModes()
        {
            StaticToolStripMenuItem.Enabled = false;
            PolishFlagToolStripMenuItem.Enabled = false;
        }

        private void BeforeShutDown()
        {
            DisconnectContextMenu.PerformClick();
            shut_down = true;
        }
    }
}
