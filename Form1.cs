using ManagedCuda;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace WindowkillSeedFinderGUI
{
    public partial class MainWindow : Form {
        private MiscWindow miscWindow;
        private LoadoutWindow loadoutWindow;
        private CharacterWindow characterWindow;
        private SeedFound seedFound;
        private LookingForSeed lookingForSeed;
        private bool isMinimizing = false;
        private bool isNormalizing = false;


        public MainWindow() {
            InitializeComponent();
            miscWindow = new MiscWindow();
            loadoutWindow = new LoadoutWindow();
            characterWindow = new CharacterWindow();
            seedFound = new SeedFound();
            lookingForSeed = new LookingForSeed();
            lookingForSeed.seedFound = seedFound;
        }

        private async void Form1_Load(object sender, EventArgs e) {
            await Task.Delay(100);
            miscWindow.Show();
            await Task.Delay(100);
            loadoutWindow.Show();
            await Task.Delay(100);
            characterWindow.Show();
        }

        public async void ButtonMinimizeAll_Click(object sender, EventArgs e) {

            if (this.WindowState == FormWindowState.Minimized && !isMinimizing) {
                isMinimizing = true;
                await MinimizeAllForms();
                isMinimizing = false;
            }
            if (this.WindowState == FormWindowState.Normal && !isNormalizing) {
                isNormalizing = true;
                await NormalizeAllForms();
                isNormalizing = false;
            }
        }

        private async Task NormalizeAllForms() {
            Form[] forms = { miscWindow, loadoutWindow, characterWindow, seedFound, lookingForSeed, this };
            foreach (Form form in forms) {
                await Task.Delay(50);
                if (form == null || !form.Visible) continue;
                form.WindowState = FormWindowState.Normal;
            }
        }
        private async Task MinimizeAllForms() {
            Form[] forms = { miscWindow, loadoutWindow, characterWindow, seedFound, lookingForSeed, this };
            foreach (Form form in forms) {
                await Task.Delay(50);
                if (form == null || !form.Visible) continue;
                form.WindowState = FormWindowState.Minimized;
            }
        }
        private void CheckBoxCpuMode_CheckedChanged(object sender, EventArgs e) {
            if (CheckBoxCpuMode.Checked) {
                ThreadCount.Enabled = true;
                LabelThreads.Enabled = true;
            }
            else {
                ThreadCount.Enabled = false;
                LabelThreads.Enabled = false;
            }
        }

        private void MainWindow_ResizeBegin(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Minimized) {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void ButtonBossesHelp_Click(object sender, EventArgs e) {
            MessageBox.Show("I was originally planning adding support for filtering for bosses too, but eventually decided it's not worth the hassle. \n\r\n\r If you want to filter for bosses too, consider compiling this project yourself.", "Bosses info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ButtonStart_Click(object sender, EventArgs e) {
            var miscwindow = MiscWindow.Instance;
            var loadoutwindow = LoadoutWindow.Instance;
            var characterwindow = CharacterWindow.Instance;
            string arguments = "";
            if (CheckBoxCpuMode.Checked) {
                arguments += " -cpu=" + ThreadCount.Value;
            }
            if (miscwindow != null && miscwindow.CheckBoxTime.Checked) {
                arguments += " -timemin=" + miscwindow.TimeMin.Value + " -timemax=" + miscwindow.TimeMax.Value;
            }
            if (miscwindow != null && miscwindow.CheckBoxColourState.Checked) {
                arguments += " -colourstate=" + (miscwindow.ColourType.Value-1); // zero based
            }
            if (loadoutwindow != null && loadoutwindow.CheckBoxFireRate.Checked) {
                arguments += " -fireratemin=" + loadoutwindow.FireRateMin.Value + " -fireratemax=" + loadoutwindow.FireRateMax.Value;
            }
            if (loadoutwindow != null && loadoutwindow.CheckBoxFreezing.Checked) {
                arguments += " -freezingmin=" + loadoutwindow.FreezingMin.Value + " -freezingmax=" + loadoutwindow.FreezingMax.Value;
            }
            if (loadoutwindow != null && loadoutwindow.CheckBoxInfection.Checked) {
                arguments += " -infectionmin=" + loadoutwindow.InfectionMin.Value + " -infectionmax=" + loadoutwindow.InfectionMax.Value;
            }
            if (loadoutwindow != null && loadoutwindow.CheckBoxMultishot.Checked) {
                arguments += " -multishotmin=" + loadoutwindow.MultishotMin.Value + " -multishotmax=" + loadoutwindow.MultishotMax.Value;
            }
            if (loadoutwindow != null && loadoutwindow.CheckBoxPiercing.Checked) {
                arguments += " -piercingmin=" + loadoutwindow.PiercingMin.Value + " -piercingmax=" + loadoutwindow.PiercingMax.Value;
            }
            if (loadoutwindow != null && loadoutwindow.CheckBoxSpeed.Checked) {
                arguments += " -speedmin=" + loadoutwindow.SpeedMin.Value + " -speedmax=" + loadoutwindow.SpeedMax.Value;
            }
            if (loadoutwindow != null && loadoutwindow.CheckBoxSplashDamage.Checked) {
                arguments += " -splashdamagemin=" + loadoutwindow.SplashDamageMin.Value + " -splashdamagemax=" + loadoutwindow.SplashDamageMax.Value;
            }
            if (loadoutwindow != null && loadoutwindow.CheckBoxWallPunch.Checked) {
                arguments += " -wallpunchmin=" + loadoutwindow.WallPunchMin.Value + " -wallpunchmax=" + loadoutwindow.WallPunchMax.Value;
            }
            if (characterwindow != null && characterwindow.CheckBoxAbility.Checked) {
                arguments += " -ability=" + characterwindow.Ability.Text;
            }
            if (characterwindow != null && characterwindow.CheckBoxCharacter.Checked) {
                arguments += " -character=" + characterwindow.Character.Text;
            }
            if (characterwindow != null && characterwindow.CheckBoxAbilityLevel.Checked) {
                arguments += " -abilitylevelmin=" + characterwindow.AbilityLevelMin.Value + " -abilitylevelmax=" + characterwindow.AbilityLevelMax.Value;
            }
            Debug.WriteLine(arguments);

            lookingForSeed.Show();
            lookingForSeed.BringToFront();
            lookingForSeed.StartLookingForSeed(arguments);
        }

        private async void MainWindow_FormClosing(object sender, FormClosingEventArgs e) {
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
