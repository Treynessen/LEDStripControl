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
        SerialPort port = null; // COM порт к которому подключено ардуино
        byte[] standart_rgb = { 0, 0, 0 };

        public Form1()
        {
            InitializeComponent();
            ports = SerialPort.GetPortNames();
            PortsComboBox.Items.AddRange(ports);
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
            if (port != null) PortsComboBox.SelectedItem = port.PortName;
        }

        private void ConnectContextMenu_Click(object sender, EventArgs e)
        {

        }

        private void DisconnectContextMenu_Click(object sender, EventArgs e)
        {

        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (port != null) port.Close();
            shut_down = true;
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!shut_down) e.Cancel = true;
            Hide();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Hide();
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void ModesToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            StaticToolStripMenuItem.Enabled = true;
            MessageBox.Show("Изменения сохранены");
        }

        private void StaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[4];
            buffer[0] = 0;
            buffer[1] = standart_rgb[0];
            buffer[2] = standart_rgb[1]; // Цвета перепутаны?
            buffer[3] = standart_rgb[2];
            port.Write(buffer, 0, buffer.Length);
            StaticToolStripMenuItem.Enabled = false;
        }
    }
}
