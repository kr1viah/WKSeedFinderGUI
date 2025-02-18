namespace WindowkillSeedFinderGUI {
    partial class CharacterWindow {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterWindow));
            CheckBoxCharacter = new CheckBox();
            CheckBoxAbility = new CheckBox();
            Character = new ComboBox();
            Ability = new ComboBox();
            CheckBoxAbilityLevel = new CheckBox();
            AbilityLevelMin = new NumericUpDown();
            AbilityLevelMax = new NumericUpDown();
            LabelAbilityLevel = new Label();
            ((System.ComponentModel.ISupportInitialize)AbilityLevelMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AbilityLevelMax).BeginInit();
            SuspendLayout();
            // 
            // CheckBoxCharacter
            // 
            resources.ApplyResources(CheckBoxCharacter, "CheckBoxCharacter");
            CheckBoxCharacter.Name = "CheckBoxCharacter";
            CheckBoxCharacter.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAbility
            // 
            resources.ApplyResources(CheckBoxAbility, "CheckBoxAbility");
            CheckBoxAbility.Name = "CheckBoxAbility";
            CheckBoxAbility.UseVisualStyleBackColor = true;
            // 
            // Character
            // 
            Character.FormattingEnabled = true;
            Character.Items.AddRange(new object[] { resources.GetString("Character.Items"), resources.GetString("Character.Items1"), resources.GetString("Character.Items2"), resources.GetString("Character.Items3"), resources.GetString("Character.Items4"), resources.GetString("Character.Items5") });
            resources.ApplyResources(Character, "Character");
            Character.Name = "Character";
            // 
            // Ability
            // 
            Ability.FormattingEnabled = true;
            Ability.Items.AddRange(new object[] { resources.GetString("Ability.Items"), resources.GetString("Ability.Items1"), resources.GetString("Ability.Items2"), resources.GetString("Ability.Items3"), resources.GetString("Ability.Items4"), resources.GetString("Ability.Items5") });
            resources.ApplyResources(Ability, "Ability");
            Ability.Name = "Ability";
            // 
            // CheckBoxAbilityLevel
            // 
            resources.ApplyResources(CheckBoxAbilityLevel, "CheckBoxAbilityLevel");
            CheckBoxAbilityLevel.Name = "CheckBoxAbilityLevel";
            CheckBoxAbilityLevel.UseVisualStyleBackColor = true;
            // 
            // AbilityLevelMin
            // 
            resources.ApplyResources(AbilityLevelMin, "AbilityLevelMin");
            AbilityLevelMin.Maximum = new decimal(new int[] { 7, 0, 0, 0 });
            AbilityLevelMin.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            AbilityLevelMin.Name = "AbilityLevelMin";
            AbilityLevelMin.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // AbilityLevelMax
            // 
            resources.ApplyResources(AbilityLevelMax, "AbilityLevelMax");
            AbilityLevelMax.Maximum = new decimal(new int[] { 7, 0, 0, 0 });
            AbilityLevelMax.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            AbilityLevelMax.Name = "AbilityLevelMax";
            AbilityLevelMax.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // LabelAbilityLevel
            // 
            resources.ApplyResources(LabelAbilityLevel, "LabelAbilityLevel");
            LabelAbilityLevel.Name = "LabelAbilityLevel";
            // 
            // CharacterWindow
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(LabelAbilityLevel);
            Controls.Add(AbilityLevelMax);
            Controls.Add(AbilityLevelMin);
            Controls.Add(CheckBoxAbilityLevel);
            Controls.Add(Ability);
            Controls.Add(Character);
            Controls.Add(CheckBoxAbility);
            Controls.Add(CheckBoxCharacter);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "CharacterWindow";
            ShowInTaskbar = false;
            FormClosing += CharacterWindow_FormClosing;
            Load += CharacterWindow_Load;
            SizeChanged += CharacterWindow_SizeChanged;
            ((System.ComponentModel.ISupportInitialize)AbilityLevelMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)AbilityLevelMax).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label LabelAbilityLevel;
        public ComboBox Character;
        public ComboBox Ability;
        public NumericUpDown AbilityLevelMin;
        public NumericUpDown AbilityLevelMax;
        public CheckBox CheckBoxCharacter;
        public CheckBox CheckBoxAbility;
        public CheckBox CheckBoxAbilityLevel;
    }
}