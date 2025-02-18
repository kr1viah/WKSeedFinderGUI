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
    public partial class LoadoutWindow : Form {
        public static LoadoutWindow? Instance { get; private set; }
        private MainWindow? mainWindow;
        public LoadoutWindow() {
            InitializeComponent();
            Instance = this;
        }

        private void LoadoutWindow_Load(object sender, EventArgs e) {
            mainWindow = Application.OpenForms["Form1"] as MainWindow;
        }

        private void LoadoutWindow_SizeChanged(object sender, EventArgs e) {
        }

        private async void LoadoutWindow_FormClosing(object sender, FormClosingEventArgs e) {
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
    }
}
