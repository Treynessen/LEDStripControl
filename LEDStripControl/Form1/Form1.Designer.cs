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
            this.AmbilightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PolishFlagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.LEDControl = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SaveRGB = new System.Windows.Forms.Button();
            this.RedTextBox = new System.Windows.Forms.TextBox();
            this.GreenTextBox = new System.Windows.Forms.TextBox();
            this.BlueTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PortsComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SaveAndConnectPortButton = new System.Windows.Forms.Button();
            this.RefreshPortsButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CorneLEDsCheckBox = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SaveCharacteristicsButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.horizontalLEDTextBox = new System.Windows.Forms.TextBox();
            this.verticalLEDTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.AnimationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 136);
            // 
            // ConnectContextMenu
            // 
            this.ConnectContextMenu.Name = "ConnectContextMenu";
            this.ConnectContextMenu.Size = new System.Drawing.Size(180, 22);
            this.ConnectContextMenu.Text = "Подключить";
            this.ConnectContextMenu.Click += new System.EventHandler(this.ConnectContextMenu_Click);
            // 
            // DisconnectContextMenu
            // 
            this.DisconnectContextMenu.Name = "DisconnectContextMenu";
            this.DisconnectContextMenu.Size = new System.Drawing.Size(180, 22);
            this.DisconnectContextMenu.Text = "Отключить";
            this.DisconnectContextMenu.Click += new System.EventHandler(this.DisconnectContextMenu_Click);
            // 
            // ModesToolStripMenuItem
            // 
            this.ModesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StaticToolStripMenuItem,
            this.AnimationToolStripMenuItem,
            this.AmbilightToolStripMenuItem,
            this.PolishFlagToolStripMenuItem});
            this.ModesToolStripMenuItem.Name = "ModesToolStripMenuItem";
            this.ModesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ModesToolStripMenuItem.Text = "Режимы";
            // 
            // StaticToolStripMenuItem
            // 
            this.StaticToolStripMenuItem.Name = "StaticToolStripMenuItem";
            this.StaticToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.StaticToolStripMenuItem.Text = "Статический";
            this.StaticToolStripMenuItem.Click += new System.EventHandler(this.StaticToolStripMenuItem_Click);
            // 
            // AmbilightToolStripMenuItem
            // 
            this.AmbilightToolStripMenuItem.Name = "AmbilightToolStripMenuItem";
            this.AmbilightToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.AmbilightToolStripMenuItem.Text = "Ambilight";
            this.AmbilightToolStripMenuItem.Click += new System.EventHandler(this.AmbilightToolStripMenuItem_Click);
            // 
            // PolishFlagToolStripMenuItem
            // 
            this.PolishFlagToolStripMenuItem.Name = "PolishFlagToolStripMenuItem";
            this.PolishFlagToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.PolishFlagToolStripMenuItem.Text = "Польский флаг";
            this.PolishFlagToolStripMenuItem.Click += new System.EventHandler(this.PolishFlagToolStripMenuItem_Click);
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.SettingsToolStripMenuItem.Text = "Настройки";
            this.SettingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // ExitContextMenu
            // 
            this.ExitContextMenu.Name = "ExitContextMenu";
            this.ExitContextMenu.Size = new System.Drawing.Size(180, 22);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Красный";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Зеленый";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(406, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Синий";
            // 
            // SaveRGB
            // 
            this.SaveRGB.Location = new System.Drawing.Point(197, 104);
            this.SaveRGB.Name = "SaveRGB";
            this.SaveRGB.Size = new System.Drawing.Size(136, 32);
            this.SaveRGB.TabIndex = 3;
            this.SaveRGB.Text = "Сохранить";
            this.SaveRGB.UseVisualStyleBackColor = true;
            this.SaveRGB.Click += new System.EventHandler(this.SaveRGB_Click);
            // 
            // RedTextBox
            // 
            this.RedTextBox.Location = new System.Drawing.Point(34, 66);
            this.RedTextBox.Name = "RedTextBox";
            this.RedTextBox.Size = new System.Drawing.Size(112, 32);
            this.RedTextBox.TabIndex = 4;
            // 
            // GreenTextBox
            // 
            this.GreenTextBox.Location = new System.Drawing.Point(210, 66);
            this.GreenTextBox.Name = "GreenTextBox";
            this.GreenTextBox.Size = new System.Drawing.Size(112, 32);
            this.GreenTextBox.TabIndex = 5;
            // 
            // BlueTextBox
            // 
            this.BlueTextBox.Location = new System.Drawing.Point(385, 66);
            this.BlueTextBox.Name = "BlueTextBox";
            this.BlueTextBox.Size = new System.Drawing.Size(112, 32);
            this.BlueTextBox.TabIndex = 6;
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
            this.groupBox1.Location = new System.Drawing.Point(12, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(531, 145);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Цвет подсветки по умолчанию";
            // 
            // PortsComboBox
            // 
            this.PortsComboBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PortsComboBox.FormattingEnabled = true;
            this.PortsComboBox.Location = new System.Drawing.Point(59, 24);
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
            this.SaveAndConnectPortButton.Location = new System.Drawing.Point(281, 24);
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
            this.RefreshPortsButton.Location = new System.Drawing.Point(186, 24);
            this.RefreshPortsButton.Name = "RefreshPortsButton";
            this.RefreshPortsButton.Size = new System.Drawing.Size(89, 27);
            this.RefreshPortsButton.TabIndex = 5;
            this.RefreshPortsButton.Text = "Обновить";
            this.RefreshPortsButton.UseVisualStyleBackColor = true;
            this.RefreshPortsButton.Click += new System.EventHandler(this.RefreshPortsButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CorneLEDsCheckBox);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.SaveCharacteristicsButton);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.horizontalLEDTextBox);
            this.groupBox2.Controls.Add(this.verticalLEDTextBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(12, 217);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(531, 241);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Характеристики ленты";
            // 
            // CorneLEDsCheckBox
            // 
            this.CorneLEDsCheckBox.AutoSize = true;
            this.CorneLEDsCheckBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CorneLEDsCheckBox.Location = new System.Drawing.Point(232, 128);
            this.CorneLEDsCheckBox.Name = "CorneLEDsCheckBox";
            this.CorneLEDsCheckBox.Size = new System.Drawing.Size(262, 23);
            this.CorneLEDsCheckBox.TabIndex = 9;
            this.CorneLEDsCheckBox.Text = "Имеются угловые светодиоды";
            this.CorneLEDsCheckBox.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(229, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(287, 15);
            this.label9.TabIndex = 8;
            this.label9.Text = "*Указывается количество на одной стороне";
            // 
            // SaveCharacteristicsButton
            // 
            this.SaveCharacteristicsButton.Location = new System.Drawing.Point(389, 203);
            this.SaveCharacteristicsButton.Name = "SaveCharacteristicsButton";
            this.SaveCharacteristicsButton.Size = new System.Drawing.Size(136, 32);
            this.SaveCharacteristicsButton.TabIndex = 7;
            this.SaveCharacteristicsButton.Text = "Сохранить";
            this.SaveCharacteristicsButton.UseVisualStyleBackColor = true;
            this.SaveCharacteristicsButton.Click += new System.EventHandler(this.SaveCharacteristicsButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(229, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(280, 15);
            this.label8.TabIndex = 6;
            this.label8.Text = "*Сигнал \"ходит\" как показано на рисунке";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(229, 169);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(217, 15);
            this.label7.TabIndex = 5;
            this.label7.Text = "*Не считая светодиоды по углам";
            // 
            // horizontalLEDTextBox
            // 
            this.horizontalLEDTextBox.Location = new System.Drawing.Point(413, 90);
            this.horizontalLEDTextBox.Name = "horizontalLEDTextBox";
            this.horizontalLEDTextBox.Size = new System.Drawing.Size(100, 32);
            this.horizontalLEDTextBox.TabIndex = 4;
            // 
            // verticalLEDTextBox
            // 
            this.verticalLEDTextBox.Location = new System.Drawing.Point(413, 49);
            this.verticalLEDTextBox.Name = "verticalLEDTextBox";
            this.verticalLEDTextBox.Size = new System.Drawing.Size(100, 32);
            this.verticalLEDTextBox.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(228, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(178, 24);
            this.label6.TabIndex = 2;
            this.label6.Text = "По горизонтали";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(228, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 24);
            this.label5.TabIndex = 1;
            this.label5.Text = "По вертикали";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(201, 199);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // AnimationToolStripMenuItem
            // 
            this.AnimationToolStripMenuItem.Name = "AnimationToolStripMenuItem";
            this.AnimationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.AnimationToolStripMenuItem.Text = "Анимация";
            this.AnimationToolStripMenuItem.Click += new System.EventHandler(this.AnimationToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 469);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.RefreshPortsButton);
            this.Controls.Add(this.SaveAndConnectPortButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PortsComboBox);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(572, 508);
            this.MinimumSize = new System.Drawing.Size(572, 508);
            this.Name = "Form1";
            this.Text = "LEDControl";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem StaticToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PolishFlagToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SaveRGB;
        private System.Windows.Forms.TextBox RedTextBox;
        private System.Windows.Forms.TextBox GreenTextBox;
        private System.Windows.Forms.TextBox BlueTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox PortsComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button SaveAndConnectPortButton;
        private System.Windows.Forms.Button RefreshPortsButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button SaveCharacteristicsButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox horizontalLEDTextBox;
        private System.Windows.Forms.TextBox verticalLEDTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox CorneLEDsCheckBox;
        private System.Windows.Forms.ToolStripMenuItem AmbilightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AnimationToolStripMenuItem;
    }
}

