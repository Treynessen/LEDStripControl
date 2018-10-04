using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace LEDStripControl
{
    public partial class Form1 : Form
    {
        private ArduinoSerialPort arduino;
        bool shut_down = false; // выключение программы

        public Form1()
        {
            InitializeComponent();
            arduino = new ArduinoSerialPort();
            PortsComboBox.Items.AddRange(arduino.Ports);
            if (arduino.Connected)
            {
                PortsComboBox.SelectedText = arduino.PortName;
                ConnectContextMenu.Enabled = false;
            }
            else
            {
                PortsComboBox.SelectedText = "";
                DisconnectContextMenu.Enabled = false;
                DisableModes();
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
            if (arduino.Connected) arduino.Disconnect();
            if (arduino != null) arduino.Dispose();
        }

        /*=============== Контекстное меню ===============*/

        private void ConnectContextMenu_Click(object sender, EventArgs e)
        {
            if (arduino.Connected)
            {
                MessageBox.Show($"Необходимо отключиться от {arduino.PortName} порта", "Ошибка");
            }
            if (!arduino.TryToConnect(PortsComboBox.SelectedText))
            {
                MessageBox.Show($"Неудалось подключиться к {PortsComboBox.SelectedText} порту", "Ошибка");
            }
            else
            {
                EnableModes();
                ConnectContextMenu.Enabled = false;
                DisconnectContextMenu.Enabled = true;
            }
        }

        private void DisconnectContextMenu_Click(object sender, EventArgs e)
        {
            arduino.Disconnect();
            DisableModes();
            ConnectContextMenu.Enabled = true;
            DisconnectContextMenu.Enabled = false;
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            arduino.Disconnect();
            shut_down = true;
            Application.Exit();
        }

        /*================================================*/

        /*==================== Режимы ====================*/

        private void StaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            arduino.SetMode(Modes.StaticColor);
            EnableModes(Modes.StaticColor);
        }

        private void AnimationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            arduino.SetMode(Modes.Animation);
            EnableModes(Modes.Animation);
        }

        private void AmbilightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                arduino.SetMode(Modes.Ambilight);
            }
            catch (ArgumentNullException ex)
            {
                if (ex.ParamName == "Ambilight error")
                {
                    MessageBox.Show("Характеристики светодиодной ленты указаны неверно", "Ошибка");
                    return;
                }
            }
            EnableModes(Modes.Ambilight);
        }

        private void PolishFlagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            arduino.SetMode(Modes.PolishFlag);
            EnableModes(Modes.PolishFlag);
        }

        /*================================================*/

        /*=========== Кнопки на форме настроек ===========*/

        private void ConnectToPortButton_Click(object sender, EventArgs e)
        {
            ConnectContextMenu.PerformClick();
        }

        private void RefreshPortsButton_Click(object sender, EventArgs e)
        {
            arduino.RefreshPortsList();
        }

        private void SaveRGB_Click(object sender, EventArgs e)
        {
            byte r, g, b;
            try
            {
                r = Convert.ToByte(RedTextBox.Text);
            }
            catch (FormatException)
            {
                RedTextBox.Text = "0";
                r = 0;
            }
            try
            {
                g = Convert.ToByte(GreenTextBox.Text);
            }
            catch (FormatException)
            {
                GreenTextBox.Text = "0";
                g = 0;
            }
            try
            {
                b = Convert.ToByte(BlueTextBox.Text);
            }
            catch (FormatException)
            {
                BlueTextBox.Text = "0";
                b = 0;
            }
            arduino.SetRGB(r, g, b);
            MessageBox.Show("Изменения сохранены");
        }

        private void SaveCharacteristicsButton_Click(object sender, EventArgs e)
        {
            int num_vertical_leds, num_horizontal_leds;
            try
            {
                num_vertical_leds = Convert.ToInt32(verticalLEDTextBox.Text);
                if (num_vertical_leds < 0) throw new FormatException();
            }
            catch (FormatException)
            {
                verticalLEDTextBox.Text = "0";
                num_vertical_leds = 0;
            }
            try
            {
                num_horizontal_leds = Convert.ToInt32(horizontalLEDTextBox.Text);
                if (num_horizontal_leds < 0) throw new FormatException();
            }
            catch (FormatException)
            {
                horizontalLEDTextBox.Text = "0";
                num_horizontal_leds = 0;
            }
            if(num_vertical_leds == 0 || num_horizontal_leds == 0)
            {
                MessageBox.Show("Неверно указаны данные", "Ошибка");
                return;
            }
            else
            {
                arduino.SetSettings(num_vertical_leds, num_horizontal_leds, CorneLEDsCheckBox.Checked);
                MessageBox.Show("Изменения сохранены");
            }
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
    }
}