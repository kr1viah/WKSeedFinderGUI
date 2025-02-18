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
    public partial class CharacterWindow : Form {
        public static CharacterWindow? Instance { get; private set; }
        private MainWindow? mainWindow;
        public CharacterWindow() {
            InitializeComponent();
            Instance = this;
        }

        private void CharacterWindow_Load(object sender, EventArgs e) {
            mainWindow = Application.OpenForms["Form1"] as MainWindow;
        }

        private void CharacterWindow_SizeChanged(object sender, EventArgs e) {
        }

        private async void CharacterWindow_FormClosing(object sender, FormClosingEventArgs e) {
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
