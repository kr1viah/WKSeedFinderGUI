using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowkillSeedFinderGUI {
    public partial class SeedFound : Form {
        public static MiscWindow? Instance { get; private set; }
        private MainWindow? mainWindow;
        public SeedFound() {
            InitializeComponent();
        }

        private void SeedFound_SizeChanged(object sender, EventArgs e) {
        }

        private void SeedFound_Load(object sender, EventArgs e) {
            mainWindow = Application.OpenForms["Form1"] as MainWindow;
        }

        private async void SeedFound_FormClosing(object sender, FormClosingEventArgs e) {
            if (!isClosing) {
                isClosing = true;
                List<Form> openForms = Application.OpenForms.Cast<Form>()
                    .Reverse()
                    .ToList();

                foreach (Form form in openForms) {
                    await Task.Delay(100);
                    form.BringToFront();
                    if (!form.IsDisposed) {
                        form.Close();
                        form.Hide();
                    }
                }
            }
        }
        bool isClosing = false;
        private static readonly string[] separator1 = new string[] { "; " };
        private static readonly string[] separator2 = new string[] { "Boss order: [" };

        public void Populate(List<string> outputLines) {
            (List<string> _, List<string> list1) = SplitListAtSeparator(outputLines, "Average time per seed:");
            (List<string> stats, list1) = SplitListAtSeparator(list1, "Seed:");
            (List<string> stuffs, list1) = SplitListAtSeparator(list1, "Boss order:");

            string statsstring = string.Join(Environment.NewLine, stats);
            string findingsstring = string.Join(Environment.NewLine, stuffs);
            LabelStats.Text = statsstring;
            LabelFindings.Text = findingsstring;

            (List<string> _, List<string> bosses) = SplitListAtSeparator(list1, "[");
            bosses = new List<string>(string.Join(Environment.NewLine, bosses).Split(separator2, StringSplitOptions.None));
            bosses = new List<string>(string.Join(Environment.NewLine, bosses).Split(separator1, StringSplitOptions.None));
            bosses.RemoveAt(bosses.Count - 1); // cba to figure out how to remove the last element which has ";]" trailing it
            bosses = AddPrefix(bosses);
            ListViewBosses.Items.Clear();
            foreach (string line in bosses) {
                ListViewBosses.Items.Add(line);
            }
        }
        static List<string> AddPrefix(List<string> lines) {
            List<string> result = new List<string>();

            for (int i = 0; i < lines.Count; i++)
                result.Add($"{i} | {lines[i]}");

            return result;
        }
        static (List<string>, List<string>) SplitListAtSeparator(List<string> lines, string separator) {
            int index = lines.FindIndex(line => line.Contains(separator)); // Find first occurrence
            if (index == -1) return (new List<string>(lines), new List<string>()); // No match found

            List<string> list1 = lines.GetRange(0, index);
            List<string> list2 = lines.GetRange(index, lines.Count - index);

            return (list1, list2);
        }

        private void ListViewBosses_SelectedIndexChanged(object sender, EventArgs e) {
            ListViewBosses.SelectedItems.Clear();
        }

        private void SeedFound_FormClosing_1(object sender, FormClosingEventArgs e) {
            e.Cancel = true; // Cancel the close event
            this.Hide(); // Hide the form instead
        }
    }
}