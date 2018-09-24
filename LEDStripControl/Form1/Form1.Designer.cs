namespace LEDStripControl
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ConnectContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DisconnectContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ModesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StaticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PolishFlagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.LEDControl = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BlueTextBox = new System.Windows.Forms.TextBox();
            this.GreenTextBox = new System.Windows.Forms.TextBox();
            this.RedTextBox = new System.Windows.Forms.TextBox();
            this.SaveRGB = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PortsComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SaveAndConnectPortButton = new System.Windows.Forms.Button();
            this.RefreshPortsButton = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectContextMenu,
            this.DisconnectContextMenu,
            this.ModesToolStripMenuItem,
            this.SettingsToolStripMenuItem,
            this.ExitContextMenu});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 114);
            // 
            // ConnectContextMenu
            // 
            this.ConnectContextMenu.Name = "ConnectContextMenu";
            this.ConnectContextMenu.Size = new System.Drawing.Size(144, 22);
            this.ConnectContextMenu.Text = "Подключить";
            this.ConnectContextMenu.Click += new System.EventHandler(this.ConnectContextMenu_Click);
            // 
            // DisconnectContextMenu
            // 
            this.DisconnectContextMenu.Name = "DisconnectContextMenu";
            this.DisconnectContextMenu.Size = new System.Drawing.Size(144, 22);
            this.DisconnectContextMenu.Text = "Отключить";
            this.DisconnectContextMenu.Click += new System.EventHandler(this.DisconnectContextMenu_Click);
            // 
            // ModesToolStripMenuItem
            // 
            this.ModesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StaticToolStripMenuItem,
            this.PolishFlagToolStripMenuItem});
            this.ModesToolStripMenuItem.Name = "ModesToolStripMenuItem";
            this.ModesToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.ModesToolStripMenuItem.Text = "Режимы";
            // 
            // StaticToolStripMenuItem
            // 
            this.StaticToolStripMenuItem.Name = "StaticToolStripMenuItem";
            this.StaticToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.StaticToolStripMenuItem.Text = "Статический";
            this.StaticToolStripMenuItem.Click += new System.EventHandler(this.StaticToolStripMenuItem_Click);
            // 
            // PolishFlagToolStripMenuItem
            // 
            this.PolishFlagToolStripMenuItem.Name = "PolishFlagToolStripMenuItem";
            this.PolishFlagToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.PolishFlagToolStripMenuItem.Text = "Польский флаг";
            this.PolishFlagToolStripMenuItem.Click += new System.EventHandler(this.PolishFlagToolStripMenuItem_Click);
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.SettingsToolStripMenuItem.Text = "Настройки";
            this.SettingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // ExitContextMenu
            // 
            this.ExitContextMenu.Name = "ExitContextMenu";
            this.ExitContextMenu.Size = new System.Drawing.Size(144, 22);
            this.ExitContextMenu.Text = "Выход";
            this.ExitContextMenu.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // LEDControl
            // 
            this.LEDControl.ContextMenuStrip = this.contextMenuStrip1;
            this.LEDControl.Icon = ((System.Drawing.Icon)(resources.GetObject("LEDControl.Icon")));
            this.LEDControl.Text = "LEDControl";
            this.LEDControl.Visible = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BlueTextBox);
            this.groupBox1.Controls.Add(this.GreenTextBox);
            this.groupBox1.Controls.Add(this.RedTextBox);
            this.groupBox1.Controls.Add(this.SaveRGB);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 200);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Цвет подсветки по умолчанию";
            // 
            // BlueTextBox
            // 
            this.BlueTextBox.Location = new System.Drawing.Point(106, 112);
            this.BlueTextBox.Name = "BlueTextBox";
            this.BlueTextBox.Size = new System.Drawing.Size(46, 32);
            this.BlueTextBox.TabIndex = 6;
            // 
            // GreenTextBox
            // 
            this.GreenTextBox.Location = new System.Drawing.Point(106, 74);
            this.GreenTextBox.Name = "GreenTextBox";
            this.GreenTextBox.Size = new System.Drawing.Size(46, 32);
            this.GreenTextBox.TabIndex = 5;
            // 
            // RedTextBox
            // 
            this.RedTextBox.Location = new System.Drawing.Point(106, 36);
            this.RedTextBox.Name = "RedTextBox";
            this.RedTextBox.Size = new System.Drawing.Size(46, 32);
            this.RedTextBox.TabIndex = 4;
            // 
            // SaveRGB
            // 
            this.SaveRGB.Location = new System.Drawing.Point(10, 162);
            this.SaveRGB.Name = "SaveRGB";
            this.SaveRGB.Size = new System.Drawing.Size(136, 32);
            this.SaveRGB.TabIndex = 3;
            this.SaveRGB.Text = "Сохранить";
            this.SaveRGB.UseVisualStyleBackColor = true;
            this.SaveRGB.Click += new System.EventHandler(this.SaveRGB_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Синий";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Зеленый";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Красный";
            // 
            // PortsComboBox
            // 
            this.PortsComboBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PortsComboBox.FormattingEnabled = true;
            this.PortsComboBox.Location = new System.Drawing.Point(71, 24);
            this.PortsComboBox.Name = "PortsComboBox";
            this.PortsComboBox.Size = new System.Drawing.Size(121, 27);
            this.PortsComboBox.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(8, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Порт";
            // 
            // SaveAndConnectPortButton
            // 
            this.SaveAndConnectPortButton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveAndConnectPortButton.Location = new System.Drawing.Point(293, 24);
            this.SaveAndConnectPortButton.Name = "SaveAndConnectPortButton";
            this.SaveAndConnectPortButton.Size = new System.Drawing.Size(109, 27);
            this.SaveAndConnectPortButton.TabIndex = 4;
            this.SaveAndConnectPortButton.Text = "Подключить";
            this.SaveAndConnectPortButton.UseVisualStyleBackColor = true;
            this.SaveAndConnectPortButton.Click += new System.EventHandler(this.ConnectToPortButton_Click);
            // 
            // RefreshPortsButton
            // 
            this.RefreshPortsButton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RefreshPortsButton.Location = new System.Drawing.Point(198, 24);
            this.RefreshPortsButton.Name = "RefreshPortsButton";
            this.RefreshPortsButton.Size = new System.Drawing.Size(89, 27);
            this.RefreshPortsButton.TabIndex = 5;
            this.RefreshPortsButton.Text = "Обновить";
            this.RefreshPortsButton.UseVisualStyleBackColor = true;
            this.RefreshPortsButton.Click += new System.EventHandler(this.RefreshPortsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 450);
            this.Controls.Add(this.RefreshPortsButton);
            this.Controls.Add(this.SaveAndConnectPortButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PortsComboBox);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "LEDControl";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ConnectContextMenu;
        private System.Windows.Forms.ToolStripMenuItem DisconnectContextMenu;
        private System.Windows.Forms.NotifyIcon LEDControl;
        private System.Windows.Forms.ToolStripMenuItem ExitContextMenu;
        private System.Windows.Forms.ToolStripMenuItem SettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModesToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox BlueTextBox;
        private System.Windows.Forms.TextBox GreenTextBox;
        private System.Windows.Forms.TextBox RedTextBox;
        private System.Windows.Forms.Button SaveRGB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox PortsComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem StaticToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PolishFlagToolStripMenuItem;
        private System.Windows.Forms.Button SaveAndConnectPortButton;
        private System.Windows.Forms.Button RefreshPortsButton;
    }
}

