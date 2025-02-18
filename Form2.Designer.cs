namespace WindowkillSeedFinderGUI {
    partial class MiscWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            CheckBoxColourState = new CheckBox();
            ColourType = new NumericUpDown();
            CheckBoxTime = new CheckBox();
            TimeMin = new NumericUpDown();
            LabelTime = new Label();
            TimeMax = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)ColourType).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TimeMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TimeMax).BeginInit();
            SuspendLayout();
            // 
            // CheckBoxColourState
            // 
            CheckBoxColourState.AutoSize = true;
            CheckBoxColourState.Location = new Point(12, 12);
            CheckBoxColourState.Name = "CheckBoxColourState";
            CheckBoxColourState.Size = new Size(91, 19);
            CheckBoxColourState.TabIndex = 0;
            CheckBoxColourState.Text = "Colour type:";
            CheckBoxColourState.UseVisualStyleBackColor = true;
            // 
            // ColourType
            // 
            ColourType.Location = new Point(109, 11);
            ColourType.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            ColourType.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ColourType.Name = "ColourType";
            ColourType.Size = new Size(31, 23);
            ColourType.TabIndex = 2;
            ColourType.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // CheckBoxTime
            // 
            CheckBoxTime.AutoSize = true;
            CheckBoxTime.Location = new Point(12, 40);
            CheckBoxTime.Name = "CheckBoxTime";
            CheckBoxTime.Size = new Size(15, 14);
            CheckBoxTime.TabIndex = 6;
            CheckBoxTime.UseVisualStyleBackColor = true;
            // 
            // TimeMin
            // 
            TimeMin.DecimalPlaces = 2;
            TimeMin.Location = new Point(33, 37);
            TimeMin.Maximum = new decimal(new int[] { 1500, 0, 0, 0 });
            TimeMin.Minimum = new decimal(new int[] { 120, 0, 0, 0 });
            TimeMin.Name = "TimeMin";
            TimeMin.Size = new Size(66, 23);
            TimeMin.TabIndex = 7;
            TimeMin.Value = new decimal(new int[] { 120, 0, 0, 0 });
            TimeMin.ValueChanged += TimeMin_ValueChanged;
            // 
            // LabelTime
            // 
            LabelTime.AutoSize = true;
            LabelTime.Location = new Point(105, 39);
            LabelTime.Name = "LabelTime";
            LabelTime.Size = new Size(122, 15);
            LabelTime.TabIndex = 8;
            LabelTime.Text = "< Time (in seconds) <";
            // 
            // TimeMax
            // 
            TimeMax.DecimalPlaces = 2;
            TimeMax.Location = new Point(233, 37);
            TimeMax.Maximum = new decimal(new int[] { 1500, 0, 0, 0 });
            TimeMax.Minimum = new decimal(new int[] { 120, 0, 0, 0 });
            TimeMax.Name = "TimeMax";
            TimeMax.Size = new Size(66, 23);
            TimeMax.TabIndex = 9;
            TimeMax.Value = new decimal(new int[] { 120, 0, 0, 0 });
            // 
            // MiscWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(318, 74);
            Controls.Add(TimeMax);
            Controls.Add(LabelTime);
            Controls.Add(TimeMin);
            Controls.Add(CheckBoxTime);
            Controls.Add(ColourType);
            Controls.Add(CheckBoxColourState);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MiscWindow";
            ShowInTaskbar = false;
            Text = "Misc";
            FormClosing += MiscWindow_FormClosing;
            Load += Form2_Load;
            SizeChanged += MiscWindow_SizeChanged;
            ((System.ComponentModel.ISupportInitialize)ColourType).EndInit();
            ((System.ComponentModel.ISupportInitialize)TimeMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)TimeMax).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public CheckBox CheckBoxTime;
        public NumericUpDown TimeMin;
        private Label LabelTime;
        public CheckBox CheckBoxColourState;
        public NumericUpDown ColourType;
        public NumericUpDown TimeMax;
    }
}