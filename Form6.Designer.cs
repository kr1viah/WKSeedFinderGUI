namespace WindowkillSeedFinderGUI {
    partial class LookingForSeed {
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
            LabelLookingFor = new Label();
            LabelStatus = new Label();
            SuspendLayout();
            // 
            // LabelLookingFor
            // 
            LabelLookingFor.AutoSize = true;
            LabelLookingFor.Location = new Point(12, 9);
            LabelLookingFor.Name = "LabelLookingFor";
            LabelLookingFor.Size = new Size(338, 30);
            LabelLookingFor.TabIndex = 0;
            LabelLookingFor.Text = "Looking for seed... This can and will take up to several minutes.\r\nMay take up to an hour with cpu mode on.";
            // 
            // LabelStatus
            // 
            LabelStatus.AutoSize = true;
            LabelStatus.Location = new Point(12, 58);
            LabelStatus.Name = "LabelStatus";
            LabelStatus.Size = new Size(39, 15);
            LabelStatus.TabIndex = 1;
            LabelStatus.Text = "Status";
            // 
            // LookingForSeed
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(362, 82);
            Controls.Add(LabelStatus);
            Controls.Add(LabelLookingFor);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LookingForSeed";
            ShowInTaskbar = false;
            Text = "Looking for seed...";
            Load += LookingForSeed_Load;
            SizeChanged += LookingForSeed_SizeChanged;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LabelLookingFor;
        private Label LabelStatus;
    }
}