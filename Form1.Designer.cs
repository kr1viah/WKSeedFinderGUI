namespace WindowkillSeedFinderGUI
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            ButtonStart = new Button();
            CheckBoxCpuMode = new CheckBox();
            ToolTipCpuMode = new ToolTip(components);
            ThreadCount = new NumericUpDown();
            ToolTipBosses = new ToolTip(components);
            TextBoxBosses = new RichTextBox();
            ButtonBossesHelp = new Button();
            CheckBoxBosses = new CheckBox();
            LabelThreads = new Label();
            ((System.ComponentModel.ISupportInitialize)ThreadCount).BeginInit();
            SuspendLayout();
            // 
            // ButtonStart
            // 
            ButtonStart.Location = new Point(272, 415);
            ButtonStart.Name = "ButtonStart";
            ButtonStart.Size = new Size(71, 23);
            ButtonStart.TabIndex = 0;
            ButtonStart.Text = "Start!";
            ButtonStart.UseVisualStyleBackColor = true;
            ButtonStart.Click += ButtonStart_Click;
            // 
            // CheckBoxCpuMode
            // 
            CheckBoxCpuMode.AutoSize = true;
            CheckBoxCpuMode.Location = new Point(12, 390);
            CheckBoxCpuMode.Name = "CheckBoxCpuMode";
            CheckBoxCpuMode.Size = new Size(83, 19);
            CheckBoxCpuMode.TabIndex = 1;
            CheckBoxCpuMode.Text = "CPU mode";
            ToolTipCpuMode.SetToolTip(CheckBoxCpuMode, "If you don't have an NVidia GPU supporting CUDA select this, although note that it will be much slower");
            CheckBoxCpuMode.UseVisualStyleBackColor = true;
            CheckBoxCpuMode.CheckedChanged += CheckBoxCpuMode_CheckedChanged;
            // 
            // ThreadCount
            // 
            ThreadCount.Enabled = false;
            ThreadCount.Location = new Point(69, 415);
            ThreadCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ThreadCount.Name = "ThreadCount";
            ThreadCount.Size = new Size(120, 23);
            ThreadCount.TabIndex = 3;
            ThreadCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // TextBoxBosses
            // 
            TextBoxBosses.DetectUrls = false;
            TextBoxBosses.Enabled = false;
            TextBoxBosses.Location = new Point(12, 41);
            TextBoxBosses.Name = "TextBoxBosses";
            TextBoxBosses.Size = new Size(331, 343);
            TextBoxBosses.TabIndex = 4;
            TextBoxBosses.Text = "";
            ToolTipBosses.SetToolTip(TextBoxBosses, resources.GetString("TextBoxBosses.ToolTip"));
            TextBoxBosses.WordWrap = false;
            // 
            // ButtonBossesHelp
            // 
            ButtonBossesHelp.Location = new Point(261, 12);
            ButtonBossesHelp.Name = "ButtonBossesHelp";
            ButtonBossesHelp.Size = new Size(82, 23);
            ButtonBossesHelp.TabIndex = 6;
            ButtonBossesHelp.Text = "Bosses help";
            ButtonBossesHelp.UseVisualStyleBackColor = true;
            ButtonBossesHelp.Click += ButtonBossesHelp_Click;
            // 
            // CheckBoxBosses
            // 
            CheckBoxBosses.AutoSize = true;
            CheckBoxBosses.Enabled = false;
            CheckBoxBosses.Location = new Point(12, 15);
            CheckBoxBosses.Name = "CheckBoxBosses";
            CheckBoxBosses.Size = new Size(61, 19);
            CheckBoxBosses.TabIndex = 7;
            CheckBoxBosses.Text = "Bosses";
            CheckBoxBosses.UseVisualStyleBackColor = true;
            // 
            // LabelThreads
            // 
            LabelThreads.AutoSize = true;
            LabelThreads.Enabled = false;
            LabelThreads.Location = new Point(12, 419);
            LabelThreads.Name = "LabelThreads";
            LabelThreads.Size = new Size(51, 15);
            LabelThreads.TabIndex = 8;
            LabelThreads.Tag = "LabelThreads";
            LabelThreads.Text = "Threads:";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(355, 450);
            Controls.Add(LabelThreads);
            Controls.Add(CheckBoxBosses);
            Controls.Add(ButtonBossesHelp);
            Controls.Add(TextBoxBosses);
            Controls.Add(ThreadCount);
            Controls.Add(CheckBoxCpuMode);
            Controls.Add(ButtonStart);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainWindow";
            Text = "Windowkill Seed Finder";
            FormClosing += MainWindow_FormClosing;
            Load += Form1_Load;
            SizeChanged += ButtonMinimizeAll_Click;
            ((System.ComponentModel.ISupportInitialize)ThreadCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ButtonStart;
        private CheckBox CheckBoxCpuMode;
        private ToolTip ToolTipCpuMode;
        private NumericUpDown ThreadCount;
        private ToolTip ToolTipBosses;
        private RichTextBox TextBoxBosses;
        private Button ButtonBossesHelp;
        private CheckBox CheckBoxBosses;
        private Label LabelThreads;
    }
}
