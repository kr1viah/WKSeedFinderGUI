using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowkillSeedFinderGUI {
    public partial class LookingForSeed : Form {
        private Process? runningProcess; // Declare as nullable
        private static TaskCompletionSource<bool> waitForStringTask = new TaskCompletionSource<bool>();
        private string lastOutputLine = "";
        private List<string> outputLines = new List<string>();
        public static MiscWindow? Instance { get; private set; }
        private MainWindow? mainWindow;
        public SeedFound? seedFound;
        public LookingForSeed() {
            InitializeComponent();
            this.FormClosing += MainForm_FormClosing;
            Application.ApplicationExit += Application_ApplicationExit;
        }

        public async void StartLookingForSeed(string arguments) {

            string exeName = "WindowkillSeedFinderGUI.main.exe";
            string dllName = "WindowkillSeedFinderGUI.main.dll";
            string tempPath = Path.Combine(Path.GetTempPath(), "main.exe");
            string tempDLLPath = Path.Combine(Path.GetTempPath(), "main.dll");
            Debug.WriteLine((Assembly.GetExecutingAssembly().GetManifestResourceStream(exeName)));

            try {
                // Read the embedded .exe from resources
                using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(exeName)) {
                    if (stream == null) {
                        MessageBox.Show("Error: Embedded exe not found!");
                        return;
                    }

                    // Write to a temp file
                    using (FileStream fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write)) {
                        stream.CopyTo(fileStream);
                    }
                }
                using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(dllName)) {
                    if (stream == null) {
                        MessageBox.Show("Error: Embedded dll not found!");
                        return;
                    }
                    using (FileStream fileStream = new FileStream(tempDLLPath, FileMode.Create, FileAccess.Write)) {
                        stream.CopyTo(fileStream);
                    }
                }

                LabelStatus.Text = "Starting program...";

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = tempPath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                runningProcess = new Process
                {
                    StartInfo = psi
                };
                outputLines.Clear();
                runningProcess.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data != null) {
                        lastOutputLine = e.Data;  // Store last received line
                        outputLines.Add(e.Data); // Store full output

                        // Update LabelStatus on the UI thread
                        this.Invoke((System.Windows.Forms.MethodInvoker)delegate {
                            LabelStatus.Text = lastOutputLine;
                        });
                    }
                };
                runningProcess.Start();
                runningProcess.BeginOutputReadLine();
                runningProcess.BeginErrorReadLine();

                await Task.Run(() => runningProcess.WaitForExit()); // Wait for process to finish
                if (!hasKilledProcess) {
                    seedFound.Show();

                    seedFound.Populate(outputLines);
                    this.Hide();
                }
                hasKilledProcess = false;
            }
            catch (Exception ex) {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally {
                if (File.Exists(tempPath)) {
                    try { File.Delete(tempPath); } catch { } // Silent fail
                }
                if (File.Exists(tempDLLPath)) {
                    try { File.Delete(tempDLLPath); } catch { } // Silent fail
                }
            }
        }

        private void LookingForSeed_Load(object sender, EventArgs e) {
            mainWindow = Application.OpenForms["Form1"] as MainWindow;
        }
        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e) {
            e.Cancel = true; // Cancel the close event
            this.Hide(); // Hide the form instead
            if (runningProcess != null && !runningProcess.HasExited) {
                runningProcess.Kill();
                runningProcess.WaitForExit();
                hasKilledProcess = true;
            }
        }
        private void Application_ApplicationExit(object? sender, EventArgs e) {
            if (runningProcess != null && !runningProcess.HasExited) {
                runningProcess.Kill();
                runningProcess.WaitForExit();
            }
        }

        private void LookingForSeed_SizeChanged(object sender, EventArgs e) {
        }

        private async void LookingForSeed_FormClosing(object sender, FormClosingEventArgs e) {
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
        private bool hasKilledProcess = false;
    }
}
