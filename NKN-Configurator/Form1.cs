using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NKN_Configurator {
	public partial class Form1 : Form {
		private enum Lang {
			EN, RU
		}

		private static Form1 theForm_ = null;
		private static Options options_ = null;
		private static Dictionary<string, CfgComponent> components_ = new Dictionary<string, CfgComponent>();
		private static Dictionary<string, string> selectedLocale_;
		private static string cfgPath_ = "config.txt";

		private static Dictionary<string, string[]> optsOnPage_ = new Dictionary<string, string[]>() {
			{ "System", new string[] { "TimeMult", "SleepTimeMult", "TimeScaleSwitchKey", "InteractionSpeed", "CraftingSpeed",
				"InflationAmount" , "DullInventoryMusic"} },
			{ "Sprint", new string[] { "EnergyDrainMult" , "EnergyReplenMult", "DefaultSpeed", "SprintSpeed",
				"EnergyForSprint", "SprintKey", "SprintToggle"} },
			{ "HP", new string[] { "UnconditionalSleep", "RegenMult", "DmgMult", "GlobalDmgMult"
				, "HealthRegen", "HealIfTired", "HealthRegenPerSecond"} },
			{ "Orbs", new string[] { "OrbsMult", "RoundDown", "OrbsConstAddIfZero", "OrbsConstant"} },
			{ "SystemKeys", new string[] { "AddMoneyKey", "ResetPrayKey", "AllowSaveEverywhere", "SaveGameKey", "ConfigReloadKey"} }
		};
		private static short panelIdx_ = 0;
		private static List<CfgPanel> cfgPanels_ = new List<CfgPanel>(3);

		public Form1() {
			InitializeComponent();
			Size = new Size(620, 420);
			theForm_ = this;
			saveFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
			try {
				if (File.Exists(cfgPath_)) {
					string[] lines = File.ReadAllLines(cfgPath_);
					if (lines.Length > 0) {
						Dictionary<string, string> rawValues = new Dictionary<string, string>();
						foreach (string line in lines) {
							if (line.Length < 3 || line[0] == '#') {
								continue;
							}
							string[] pair = line.Split('=');
							if (pair.Length > 1) {
								rawValues.Add(pair[0], pair[1]);
							}
						}
						if (rawValues.Count > 0) {
							options_ = new Options(rawValues);
						}
					}
				}
			}
			catch {
				MessageBox.Show("An error occured while reading the config file. Options will be set to default.");
			}
			if (options_ == null) {
				options_ = new Options();
			}
		}

		private class CfgPanel : Panel {
			private List<CfgComponent> cfgComponents_ = new List<CfgComponent>(8);
			public List<CfgComponent> CfgComponents => cfgComponents_;
			private Button btnNext_ = null;

			public void Show() {
				BringToFront();
				Visible = true;
			}

			public void Hide() {
				Visible = false;
				theForm_.Controls.Remove(this);
			}

			public CfgPanel(string name, bool isLast) : this(name) {
				btnNext_.Text = selectedLocale_["Finish"];
			}
			public CfgPanel(string name) : base() {
				SuspendLayout();
				Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
				Location = new Point(12, 12);
				Size = new Size(580, 357);
				Visible = false;
				
				string[] opts = optsOnPage_[name];
				foreach (string opt in opts) {
					if (options_.FloatParams.ContainsKey(opt)) {
						Add(new CfgFloatComponent(opt, options_.FloatParams[opt]));
					}
					else if (options_.BoolParams.ContainsKey(opt)) {
						Add(new CfgBoolComponent(opt, options_.BoolParams[opt]));
					}
					else if (options_.ArrayParams.ContainsKey(opt)) {
						Add(new CfgIntArrayComponent(opt, options_.ArrayParams[opt]));
					}
					else if (options_.OptionsParams.ContainsKey(opt)) {
						Add(new CfgComboboxComponent(opt, false, options_.OptionsParams[opt]));
					}
				}
				Font btnFont = new Font("Verdana", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
				Size btnSize = new Size(128, 42);

				Button btnBack = new Button();
				btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
				btnBack.Font = btnFont;
				btnBack.Location = new Point(0, 313);
				btnBack.Name = "btnBack" + name;
				btnBack.Size = btnSize;
				btnBack.Text = selectedLocale_["Back"];
				btnBack.UseVisualStyleBackColor = true;
				btnBack.Click += new EventHandler(btnBack_Click);

				btnNext_ = new Button();
				btnNext_.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
				btnNext_.Font = btnFont;
				btnNext_.Location = new Point(452, 313);
				btnNext_.Name = "btnNext" + name;
				btnNext_.Size = btnSize;
				btnNext_.Text = selectedLocale_["Proceed"];
				btnNext_.UseVisualStyleBackColor = true;
				btnNext_.Click += new EventHandler(btnNext_Click);

				Controls.Add(btnBack);
				Controls.Add(btnNext_);
				theForm_.Controls.Add(this);
				ResumeLayout(false);
				PerformLayout();
			}

			public void Add(CfgComponent value) {
				Controls.Add(value.Label);
				Controls.Add(value.Help);
				Controls.Add(value.Reset);

				int offset = 30 * cfgComponents_.Count;
				value.Label.Location = new Point(6, 11 + offset);
				value.Help.Location = new Point(426, 10 + offset);
				value.Reset.Location = new Point(470, 10 + offset);

				Control input = value.GetInput();
				if (input == null) {
					value.PlaceInput(this);
				}
				else {
					Controls.Add(value.GetInput());
					value.GetInput().Location = new Point(332, 10 + offset);
				}
				cfgComponents_.Add(value);
			}
		}

		private abstract class CfgComponent {
			protected Label label_;
			protected Button help_;
			protected Button reset_;
			protected string name_ = "";

			public Label Label => label_;
			public Button Help => help_;
			public Button Reset => reset_;

			public abstract string GetValue();
			public abstract void SetDefault();
			public abstract Control GetInput();
			public abstract void PlaceInput(CfgPanel panel);

			public string GetCfgString() {
				string value = GetValue();
				return value == null ? "" : name_ + "=" + value + "\r\n";
			}

			private void reset_Click(object sender, EventArgs e) {
				SetDefault();
			}

			public CfgComponent(string name) {
				name_ = name;
				label_ = new Label();
				label_.Anchor = AnchorStyles.None;
				label_.Font = new Font("Verdana", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
				label_.Location = new Point(6, 11);
				label_.Name = "lb" + name;
				label_.Size = new Size(320, 20);
				label_.TextAlign = ContentAlignment.MiddleRight;
				label_.Text = selectedLocale_[name];

				help_ = new Button();
				help_.Anchor = AnchorStyles.None;
				help_.BackgroundImage = Properties.Resources.help;
				help_.BackgroundImageLayout = ImageLayout.Center;
				help_.Cursor = Cursors.Hand;
				help_.FlatAppearance.BorderSize = 0;
				help_.FlatStyle = FlatStyle.Flat;
				help_.Name = "help" + name;
				help_.Size = new Size(36, 24);
				help_.UseVisualStyleBackColor = true;

				help_.Click += new EventHandler(btnHelp_Click);

				reset_ = new Button();
				reset_.Anchor = AnchorStyles.None;
				//reset_.Cursor = Cursors.Hand;
				reset_.Name = "reset" + name;
				reset_.Size = new Size(100, 24);
				reset_.Text = selectedLocale_["SetDefault"];

				reset_.Click += new EventHandler(reset_Click);
			}
		}

		private class CfgFloatComponent : CfgComponent {
			protected TextBox textBox_;
			private string default_;

			public override Control GetInput() {
				return textBox_;
			}
			public override void PlaceInput(CfgPanel panel) { }

			public CfgFloatComponent(string name) : base(name) {
				textBox_ = new TextBox();
				textBox_.Anchor = AnchorStyles.None;
				textBox_.Font = new Font("Verdana", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
				textBox_.Name = "tb" + name;
				textBox_.Size = new Size(84, 24);
			}

			public CfgFloatComponent(string name, FloatOption option) : this(name) {
				textBox_.Text = option.Value.ToString().Replace(',', '.');
				default_ = option.Default.ToString().Replace(',', '.');
			}

			public override void SetDefault() {
				textBox_.Text = default_;
			}

			public override string GetValue() {
				string strValue = textBox_.Text;
				if (strValue.Length == 0) {
					return null;
				}
				try {
					double val = Convert.ToDouble(strValue.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
					return val.ToString().Replace(',', '.');
				} catch {
					return null;
				}
			}
		}

		private class CfgIntArrayComponent : CfgComponent {
			private NumericUpDown[] numericInputs_;
			private string[] labelKeys_;
			private int[] default_;

			public override Control GetInput() {
				return null;
			}

			public override void PlaceInput(CfgPanel panel) {
				int offset = 30 * panel.CfgComponents.Count;
				for (int i = 0; i < numericInputs_.Length; i++) {
					NumericUpDown numericInput = numericInputs_[i];
					numericInput.Location = new Point(332, 10 + offset);
					panel.Controls.Add(numericInput);
					if (i > 0) {
						string key = labelKeys_[i - 1];
						Label addLabel = new Label();
						addLabel.Anchor = AnchorStyles.None;
						addLabel.Font = new Font("Verdana", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
						addLabel.Location = new Point(6, 11);
						addLabel.Name = "lb" + name_ + key;
						addLabel.Size = new Size(320, 20);
						addLabel.TextAlign = ContentAlignment.MiddleRight;
						addLabel.Text = selectedLocale_[key];
						addLabel.Location = new Point(6, 11 + offset);
						panel.Controls.Add(addLabel);
					}
					offset += 30;
				}
			}

			public CfgIntArrayComponent(string name, IntArrayOption option) : base(name) {
				numericInputs_ = new NumericUpDown[option.Value.Length];
				labelKeys_ = option.LocaleKeys;
				default_ = option.Default;
				for (int i = 0; i < option.Value.Length; i++) {
					int val = option.Value[i];
					numericInputs_[i] = new NumericUpDown();
					NumericUpDown numericInput = numericInputs_[i];
					numericInput.Anchor = AnchorStyles.None;
					numericInput.Font = new Font("Verdana", 10F);
					numericInput.Location = new Point(332, 158);
					numericInput.Maximum = new decimal(Math.Max(100000, val));
					numericInput.Minimum = new decimal(Math.Min(-100000, val));
					numericInput.Name = "nude" + name + i;
					numericInput.Size = new Size(84, 24);
					numericInput.Value = val;
				}
			}

			public override void SetDefault() {
				for (int i = 0; i < numericInputs_.Length; i++) {
					numericInputs_[i].Value = default_[i];
				}
			}

			public override string GetValue() {
				string strval = "";
				foreach (NumericUpDown nude in numericInputs_) {
					int value = Convert.ToInt32(nude.Value);
					strval += value + ":";
				}
				return strval;
			}
		}

		private class CfgBoolComponent : CfgComponent {
			protected ComboBox comboBox_;
			private bool default_;

			public override Control GetInput() {
				return comboBox_;
			}
			public override void PlaceInput(CfgPanel panel) {}

			public override void SetDefault() {
				comboBox_.SelectedIndex = default_ ? 1 : 0;
			}

			public CfgBoolComponent(string name) : base(name) {
				comboBox_ = new ComboBox();
				comboBox_.Font = new Font("Verdana", 10F);
				comboBox_.FormattingEnabled = true;
				comboBox_.Anchor = AnchorStyles.None;
				comboBox_.Name = "cb" + name;
				comboBox_.Size = new Size(84, 24);
				comboBox_.DropDownStyle = ComboBoxStyle.DropDownList;
			}

			public CfgBoolComponent(string name, BooleanOption option) : this(name) {
				comboBox_.Items.Clear();
				comboBox_.Items.Add(selectedLocale_["boolDisabled"]);
				comboBox_.Items.Add(selectedLocale_["boolEnabled"]);
				comboBox_.SelectedIndex = option.Value ? 1 : 0;
				default_ = option.Default;
			}

			public override string GetValue() {
				return comboBox_.SelectedIndex == 1 ? "true" : "false";
			}
		}

		private class CfgComboboxComponent : CfgBoolComponent {
			string[] options_ = null;
			private string default_;

			public CfgComboboxComponent(string name, bool isLoc, StringDropOption option) : base(name) {
				comboBox_.Items.Clear();
				default_ = option.Default;
				options_ = option.Options;
				int idx = 0;
				for (int i = 0; i < options_.Length; i++) {
					string opt = options_[i];
					if (opt == option.Value) {
						idx = i;
					}
					comboBox_.Items.Add(isLoc ? selectedLocale_[options_[i]] : options_[i]);
				}
				comboBox_.SelectedIndex = idx;
			}

			public override void SetDefault() {
				comboBox_.SelectedIndex = comboBox_.Items.IndexOf(default_);
			}

			public override string GetValue() {
				return options_[comboBox_.SelectedIndex];
			}
		}

		private class BooleanOption {
			public bool Value { get; set; }
			public bool Default { get; }

			public BooleanOption() : this(false) { }

			public BooleanOption(bool value) {
				Default = value;
				Value = value;
			}
		}
		private class FloatOption {
			public double Value { get; set; }
			public double Default { get; }

			public FloatOption() : this(1) { }

			public FloatOption(double value) {
				Default = value;
				Value = value;
			}
		}
		private class IntArrayOption {
			public int[] Value { get; set; }
			public int[] Default { get; }
			public string[] LocaleKeys { get; }

			public IntArrayOption(int[] value, string[] locKeys) {
				Default = value;
				Value = value;
				LocaleKeys = locKeys;
			}
		}
		private class StringDropOption {
			public string Value { get; set; }
			public string Default { get; }
			public string[] Options { get; }

			public StringDropOption(string value, string[] options) {
				Default = value;
				Value = value;
				Options = options;
			}
		}

		private class Options {
			private static string[] sysKeyOptions_ = new string[] { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12"
				, "Backspace", "ScrollLock", "Pause", "PageUp", "PageDown", "Insert", "Delete", "Home", "End"
				, "Keypad0", "Keypad1", "Keypad2", "Keypad3", "Keypad4", "Keypad5", "Keypad6", "Keypad7", "Keypad8", "Keypad9"
				, "KeypadDivide", "KeypadMultiply", "KeypadMinus", "KeypadPlus", "KeypadPeriod"
			};

			private Dictionary<string, FloatOption> floatParams_ = new Dictionary<string, FloatOption>() {
				{ "DmgMult", new FloatOption() }
				, { "GlobalDmgMult", new FloatOption() }
				, { "RegenMult", new FloatOption() }
				, { "HealthRegenPerSecond", new FloatOption(0.5) }
				, { "DefaultSpeed", new FloatOption() }
				, { "SprintSpeed", new FloatOption(2) }
				, { "EnergyDrainMult", new FloatOption() }
				, { "EnergyReplenMult", new FloatOption() }
				, { "EnergyForSprint", new FloatOption(0.01) }
				, { "CraftingSpeed", new FloatOption() }
				, { "InteractionSpeed", new FloatOption() }
				, { "TimeMult", new FloatOption() }
				, { "SleepTimeMult", new FloatOption() }
				, { "OrbsMult", new FloatOption() }
				, { "InflationAmount", new FloatOption() }
			};
			private Dictionary<string, BooleanOption> boolParams_ = new Dictionary<string, BooleanOption>() {
				{ "OrbsConstAddIfZero", new BooleanOption() }
				, { "SprintToggle", new BooleanOption() }
				, { "RoundDown", new BooleanOption() }
				, { "DullInventoryMusic", new BooleanOption() }
				, { "HealthRegen", new BooleanOption() }
				, { "HealIfTired", new BooleanOption() }
				, { "UnconditionalSleep", new BooleanOption() }
				, { "AllowSaveEverywhere", new BooleanOption() }
			};
			private Dictionary<string, IntArrayOption> arrayParams_ = new Dictionary<string, IntArrayOption>() {
				{ "OrbsConstant", new IntArrayOption(
					new int[]{ 0, 0, 0 }, new string[] { "OCGreen", "OCBlue" }
				) }
			};
			private Dictionary<string, StringDropOption> optionsParams_ = new Dictionary<string, StringDropOption>() {
				{ "SprintKey", new StringDropOption(
					"LeftShift", new string[] { "LeftShift", "LeftControl", "LeftAlt", "Space"
					, "RightShift", "RightControl", "RightAlt", "Z", "X", "CapsLock", "Backspace", "ScrollLock", "Pause", "PageUp" }
				) }
				, { "TimeScaleSwitchKey", new StringDropOption("F4", sysKeyOptions_) }
				, { "SaveGameKey", new StringDropOption("F5", sysKeyOptions_) }
				, { "ConfigReloadKey", new StringDropOption("F6", sysKeyOptions_) }
				, { "AddMoneyKey", new StringDropOption("F2", sysKeyOptions_) }
				, { "ResetPrayKey", new StringDropOption("F8", sysKeyOptions_) }
			};
			public Dictionary<string, FloatOption> FloatParams => floatParams_;
			public Dictionary<string, BooleanOption> BoolParams => boolParams_;
			public Dictionary<string, IntArrayOption> ArrayParams => arrayParams_;
			public Dictionary<string, StringDropOption> OptionsParams => optionsParams_;

			public Options() { }

			public Options(Dictionary<string, string> rawValues) {
				double fvalue = 0;
				List<string> keyslist = floatParams_.Keys.ToList();
				foreach (string key in keyslist) {
					if (rawValues.ContainsKey(key)) {
						if (double.TryParse(rawValues[key], out fvalue)) {
							floatParams_[key].Value = fvalue;
						}
					}
				}
				keyslist = boolParams_.Keys.ToList();
				foreach (string key in keyslist) {
					if (rawValues.ContainsKey(key)) {
						string bvalue = rawValues[key];
						if (bvalue == "1" || bvalue.ToLower() == "true") {
							boolParams_[key].Value = true;
						}
					}
				}
				if (rawValues.ContainsKey("SprintKey")) {
					optionsParams_["SprintKey"].Value = rawValues["SprintKey"];
				}
				if (rawValues.ContainsKey("OrbsConstant")) {
					string[] ocValues = rawValues["OrbsConstant"].Split(':');
					int[] orbsConst = arrayParams_["OrbsConstant"].Value;
					int ocVal = 0;
					for (int i = 0; (i < ocValues.Length) && (i < 3); i++) {
						if (int.TryParse(ocValues[i], out ocVal)) {
							orbsConst[i] = ocVal;
						}
					}
				}
			}
		}

		private static void btnHelp_Click(object sender, EventArgs e) {
			MessageBox.Show(selectedLocale_[((Control)sender).Name]);
		}
		private static void btnNext_Click(object sender, EventArgs e) {
			if (panelIdx_ == cfgPanels_.Count - 1) {
				theForm_.saveFileDialog1.ShowDialog();
			}
			else {
				panelIdx_++;
				cfgPanels_[panelIdx_].Show();
			}
		}
		private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e) {
			try {
				StringBuilder result = new StringBuilder();
				foreach (CfgPanel panel in cfgPanels_) {
					foreach (CfgComponent cfgComponent in panel.CfgComponents) {
						result.Append(cfgComponent.GetCfgString());
					}
				}
				File.WriteAllText(theForm_.saveFileDialog1.FileName, result.ToString());
				MessageBox.Show(selectedLocale_["savedSuccesfully"]);
			}
			catch (Exception ex) {
				MessageBox.Show("Could not save the configuration file:\r\n\r\n" + ex.Message);
			}
		}
		private static void btnBack_Click(object sender, EventArgs e) {
			if (panelIdx_ == 0) {
				foreach (CfgPanel panel in cfgPanels_) {
					panel.Hide();
					panel.Dispose();
				}
				cfgPanels_.Clear();
			}
			else {
				cfgPanels_[panelIdx_].Visible = false;
				panelIdx_--;
			}
		}

		private void btnLangRu_Click(object sender, EventArgs e) {
			StartConf(Lang.RU);
		}

		private void btnLangEn_Click(object sender, EventArgs e) {
			StartConf(Lang.EN);
		}

		private void StartConf(Lang lang) {
			selectedLocale_ = locales_[lang];
			cfgPanels_.Add(new CfgPanel("System"));
			cfgPanels_.Add(new CfgPanel("Sprint"));
			cfgPanels_.Add(new CfgPanel("HP"));
			cfgPanels_.Add(new CfgPanel("Orbs"));
			cfgPanels_.Add(new CfgPanel("SystemKeys", true));
			cfgPanels_[0].Show();
		}
	}
}
