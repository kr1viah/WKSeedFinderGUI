namespace WindowkillSeedFinderGUI {
    partial class SeedFound {
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
            ListViewItem listViewItem7 = new ListViewItem("1|1 Spiker");
            ListViewItem listViewItem8 = new ListViewItem("2|2 Wyrm");
            ListViewItem listViewItem9 = new ListViewItem("3|2 Slimest");
            ListViewItem listViewItem10 = new ListViewItem("4|3 Smiley");
            ListViewItem listViewItem11 = new ListViewItem("5|3 Orb array");
            ListViewItem listViewItem12 = new ListViewItem(new string[] { "6|4 Miasma", "`1231234" }, -1);
            ListViewBosses = new ListView();
            columnHeader1 = new ColumnHeader();
            LabelFindings = new Label();
            PanelStats = new Panel();
            LabelStats = new Label();
            PanelStats.SuspendLayout();
            SuspendLayout();
            // 
            // ListViewBosses
            // 
            ListViewBosses.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
            ListViewBosses.HeaderStyle = ColumnHeaderStyle.None;
            ListViewBosses.HideSelection = true;
            ListViewBosses.Items.AddRange(new ListViewItem[] { listViewItem7, listViewItem8, listViewItem9, listViewItem10, listViewItem11, listViewItem12 });
            ListViewBosses.LabelWrap = false;
            ListViewBosses.Location = new Point(610, 12);
            ListViewBosses.MultiSelect = false;
            ListViewBosses.Name = "ListViewBosses";
            ListViewBosses.Size = new Size(178, 426);
            ListViewBosses.TabIndex = 0;
            ListViewBosses.UseCompatibleStateImageBehavior = false;
            ListViewBosses.View = View.Details;
            ListViewBosses.SelectedIndexChanged += ListViewBosses_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Width = 150;
            // 
            // LabelFindings
            // 
            LabelFindings.AutoSize = true;
            LabelFindings.Location = new Point(12, 12);
            LabelFindings.Name = "LabelFindings";
            LabelFindings.Size = new Size(37, 15);
            LabelFindings.TabIndex = 1;
            LabelFindings.Text = "Stuffs";
            // 
            // PanelStats
            // 
            PanelStats.Controls.Add(LabelStats);
            PanelStats.Location = new Point(363, 12);
            PanelStats.Name = "PanelStats";
            PanelStats.Size = new Size(241, 153);
            PanelStats.TabIndex = 2;
            // 
            // LabelStats
            // 
            LabelStats.AutoSize = true;
            LabelStats.Location = new Point(3, 3);
            LabelStats.Margin = new Padding(3);
            LabelStats.Name = "LabelStats";
            LabelStats.Size = new Size(32, 15);
            LabelStats.TabIndex = 0;
            LabelStats.Text = "Stats";
            // 
            // SeedFound
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(PanelStats);
            Controls.Add(LabelFindings);
            Controls.Add(ListViewBosses);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "SeedFound";
            ShowInTaskbar = false;
            Text = "Found seed!";
            FormClosing += SeedFound_FormClosing_1;
            Load += SeedFound_Load;
            SizeChanged += SeedFound_SizeChanged;
            PanelStats.ResumeLayout(false);
            PanelStats.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView ListViewBosses;
        private Label LabelFindings;
        private Panel PanelStats;
        private Label LabelStats;
        private ColumnHeader columnHeader1;
    }
}