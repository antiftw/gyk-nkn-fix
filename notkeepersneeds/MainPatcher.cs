using System.IO;
using Harmony;
using System.Reflection;
using UnityEngine;

namespace NotKeepersNeeds {
	public class MainPatcher {
		public static void Patch() {
			HarmonyInstance harmony = HarmonyInstance.Create("com.koschk.notkeepersneeds.mod");
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}
	}

	public class Config {
		private static Options options_ = null;

		public class SinglePressKey {
			public KeyCode Key => key_;
			private KeyCode key_;
			private bool isPressed_ = false;
			public bool AlreadyPressed => isPressed_;

			public SinglePressKey(KeyCode key) {
				key_ = key;
			}
			public SinglePressKey(KeyCode key, bool alreadyPressed) {
				key_ = key;
				isPressed_ = true;
			}
			public void ChangeKey(KeyCode key) {
				key_ = key;
			}
			public virtual bool IsPressed() {
				if (Input.GetKey(Key)) {
					if (!isPressed_) {
						isPressed_ = true;
						return true;
					}
				}
				else {
					isPressed_ = false;
				}
				return false;
			}
		}

		public class ToggleKey : SinglePressKey {
			private bool toggled_ = false;
			public bool Toggled => toggled_;

			public ToggleKey(KeyCode key) : base(key) { }

			public bool IsToggled() {
				IsPressed();
				return toggled_;
			}

			public override bool IsPressed() {
				if (base.IsPressed()) {
					toggled_ = !toggled_;
					return true;
				}
				return false;
			}
		}

		public class SwitchKey : SinglePressKey {
			private int state_ = 0;
			private int stateNum_ = 2;
			public int State => state_;

			public SwitchKey(KeyCode key) : base(key) { }

			public SwitchKey(KeyCode key, int statenum) : base(key) {
				stateNum_ = statenum;
			}

			public override bool IsPressed() {
				if (base.IsPressed()) {
					state_ = ++state_ % stateNum_;
					return true;
				}
				return false;
			}
		}

		public class Options {
			public float DmgMult = 1;
			public float GlobalDmgMult = 1;
			public float RegenMult = 1;
			public float SprintSpeed = 2;
			public float DefaultSpeed = 1;
			public float EnergyDrainMult = 1;
			public float EnergyReplenMult = 1;
			public float EnergyForSprint = 0;
			public float CraftingSpeed = 1;
			public float CraftingSpeedCustom = 1;
			public float InteractionSpeed = 1;
			public float InteractionSpeedCustom = 1;
			public float TimeMult = 1;
			public float TimeMultDefault = 1;
			public float TimeMultCustom = 1;
			public float SleepTimeMult = 1;
			public float OrbsMult = 1;
			public bool _OrbsHasConst = false;
			public bool OrbsConstAddIfZero = false;
			public bool SprintToggle = false;
			public bool RoundDown = false;
			public bool DullInventoryMusic = false;
			public bool UnconditionalSleep = false;
			public bool AllowSaveEverywhere = false;
			public bool ToggleCraftAndInteraction = false;
			public float InflationAmount = 1;
			public int[] OrbsConstant = new int[] { 0, 0, 0 };

			public ToggleKey SprintKey = new ToggleKey(KeyCode.LeftShift);
			public SwitchKey TimeScaleSwitchKey = new SwitchKey(KeyCode.F4);
			public SwitchKey CraftingSpeedKey = new SwitchKey(KeyCode.F4);

			public SinglePressKey SaveGameKey = new SinglePressKey(KeyCode.F5);
			public SinglePressKey ConfigReloadKey;// = new SinglePressKey(KeyCode.F6);
			public SinglePressKey AddMoneyKey = new SinglePressKey(KeyCode.F2);
			public SinglePressKey ResetPrayKey = new SinglePressKey(KeyCode.F8);

			public bool HealthRegen = false;
			public bool HealIfTired = false;
			public float HealthRegenPerSecond = 0.5f;

			public Options() {
				ConfigReloadKey = new SinglePressKey(KeyCode.F6);
			}
			public Options(Options opts) {
				ConfigReloadKey = new SinglePressKey(KeyCode.F6, opts.ConfigReloadKey.AlreadyPressed);
			}

			/**
			 * Calculate the number of orbs that should be dropped
			 * @params
			 *		int r		amount of red orbs that would normally drop
			 *		int g		amount of green orbs that would normally drop
			 *		int b		amount of blue orbs that would normally drop
			 * @returns
			 *		int[] orbs	array with amount of orbs that will actually drop [r, g, b]
			 */
			public int[] GetOrbCount(int r, int g, int b)
			{
				int[] orbs = new int[3] { r, g, b };
				for (int index = 0; index < 3; index++)
				{
					// if we are not getting any orbs normally
					if (orbs[index] == 0) {
						if (OrbsConstAddIfZero) {
							// still add bonux orbs
							orbs[index] += OrbsConstant[index];
						}
					}
					else {
						// if we are getting orbs normally, we always get the bonus orbs
						orbs[index] += OrbsConstant[index];
					}
					// apply multiplier
					float multiplied = orbs[index] * OrbsMult;
					// round either up or down
					multiplied = RoundDown ? Mathf.Round(multiplied - 0.5f) : Mathf.Round(multiplied + 0.5f);
					// cast back to an int
					orbs[index] = (int)(multiplied);
				}
				return orbs;
			}
		}

		public static void Log(string line) {
			File.AppendAllText(@"./QMods/NotKeepersNeeds/log.txt", line);
		}

		private static bool parseBool(string raw) {
			return raw == "1" || raw.ToLower() == "true";
		}
		private static float parseFloat(string raw, float _default) {
			float value = 0;
			if (float.TryParse(raw, out value)) {
				return value;
			}
			return _default;
		}
		private static float parseFloat(string raw, float _default, float threshold) {
			float value = parseFloat(raw, _default);
			if (value > threshold) {
				return value;
			}
			return _default;
		}
		private static float parsePositive(string raw, float _default) {
			return parseFloat(raw, _default, 0);
		}
		private static float parseNonNegative(string raw, float _default) {
			float value = parseFloat(raw, _default);
			return value < 0 ? 0 : value;
		}

		public static Options GetOptions() {
			return GetOptions(false);
		}
		public static Options GetOptions(bool forceReload) {
			if (options_ == null) {
				options_ = new Options();
			}
			else if (forceReload) {
				options_ = new Options(options_);
			} else {
				return options_;
			}

			string cfgPath = @"./QMods/NotKeepersNeeds/config.txt";
			if (File.Exists(cfgPath)) {
				string[] lines = File.ReadAllLines(cfgPath);
				foreach (string line in lines) {
					if (line.Length < 3 || line[0] == '#') {
						continue;
					}
					string[] pair = line.Split('=');
					if (pair.Length > 1) {
						string key = pair[0];
						string rawVal = pair[1];
						switch (key) {
							case "DmgMult":
								options_.DmgMult = parseFloat(rawVal, options_.DmgMult, 0.04f);
								break;
							case "GlobalDmgMult":
								options_.GlobalDmgMult = parseFloat(rawVal, options_.GlobalDmgMult, 0.04f);
								break;
							case "RegenMult":
								options_.RegenMult = parsePositive(rawVal, options_.RegenMult);
								break;
							case "HealthRegenPerSecond":
								options_.HealthRegenPerSecond = parsePositive(rawVal, options_.HealthRegenPerSecond);
								break;
							case "SprintSpeed":
								options_.SprintSpeed = parsePositive(rawVal, options_.SprintSpeed);
								break;
							case "DefaultSpeed":
								options_.DefaultSpeed = parsePositive(rawVal, options_.DefaultSpeed);
								break;
							case "TimeMult":
								options_.TimeMult = parseFloat(rawVal, options_.TimeMult, 0.0009f);
								// used save the custom value when toggling to 1, will always equal options_.TimeMult
								options_.TimeMultCustom = options_.TimeMult;
								break;
							case "TimeMultDefault":
								// the other toggle value
								options_.TimeMultDefault = parseFloat(rawVal, options_.TimeMultDefault, 0.0009f);
								break;
							case "SleepTimeMult":
								options_.SleepTimeMult = parseFloat(rawVal, options_.SleepTimeMult, 0.09f);
								break;
							case "EnergyDrainMult":
								options_.EnergyDrainMult = parseNonNegative(rawVal, options_.EnergyDrainMult);
								break;
							case "EnergyReplenMult":
								options_.EnergyReplenMult = parsePositive(rawVal, options_.EnergyReplenMult);
								break;
							case "EnergyForSprint":
								options_.EnergyForSprint = parseNonNegative(rawVal, options_.EnergyForSprint);
								break;
							case "CraftingSpeed":
								options_.CraftingSpeed = parsePositive(rawVal, options_.CraftingSpeed);
								// used to keep the custom values safe when toggling to 1
								options_.InteractionSpeedCustom = options_.InteractionSpeed;
								options_.CraftingSpeedCustom = options_.CraftingSpeed;
								break;
							case "InteractionSpeed":
								options_.InteractionSpeed = parsePositive(rawVal, options_.InteractionSpeed);
								break;
							case "InflationAmount":
								options_.InflationAmount = parseFloat(rawVal, options_.InflationAmount);
								break;
							case "OrbsMult":
								options_.OrbsMult = parseNonNegative(rawVal, options_.OrbsMult);
								break;
							case "SprintKey":
								try {
									options_.SprintKey.ChangeKey(Enum<KeyCode>.Parse(rawVal));
								}
								catch { }
								break;
							case "SaveGameKey":
								try {
									options_.SaveGameKey.ChangeKey(Enum<KeyCode>.Parse(rawVal));
								}
								catch { }
								break;
							case "ConfigReloadKey":
								try {
									options_.ConfigReloadKey.ChangeKey(Enum<KeyCode>.Parse(rawVal));
								}
								catch { }
								break;
							case "AddMoneyKey":
								try {
									options_.AddMoneyKey.ChangeKey(Enum<KeyCode>.Parse(rawVal));
								}
								catch { }
								break;
							case "ResetPrayKey":
								try {
									options_.ResetPrayKey.ChangeKey(Enum<KeyCode>.Parse(rawVal));
								}
								catch { }
								break;
							case "TimeScaleSwitchKey":
								try {
									options_.TimeScaleSwitchKey.ChangeKey(Enum<KeyCode>.Parse(rawVal));
								}
								catch { }
								break;
							case "CraftingSpeedKey":
								try {
									options_.CraftingSpeedKey.ChangeKey(Enum<KeyCode>.Parse(rawVal));
								}
								catch { }
								break;

							case "OrbsConstant": {
									string[] ocValues = rawVal.Split(':');
									options_._OrbsHasConst = true;
									int ocVal = 0;
									for (int i = 0; (i < ocValues.Length) && (i < options_.OrbsConstant.Length); i++) {
										if (int.TryParse(ocValues[i], out ocVal)) {
											options_.OrbsConstant[i] = ocVal;
										}
									}
								}
								break;
							case "SprintToggle":
								options_.SprintToggle = parseBool(rawVal);
								break;
							case "RoundDown":
								options_.RoundDown = parseBool(rawVal);
								break;
							case "OrbsConstAddIfZero":
								options_.OrbsConstAddIfZero = parseBool(rawVal);
								break;
							case "DullInventoryMusic":
								options_.DullInventoryMusic = parseBool(rawVal);
								break;
							case "HealthRegen":
								options_.HealthRegen = parseBool(rawVal);
								break;
							case "HealIfTired":
								options_.HealIfTired = parseBool(rawVal);
								break;
							case "UnconditionalSleep":
								options_.UnconditionalSleep = parseBool(rawVal);
								break;
							case "AllowSaveEverywhere":
								options_.AllowSaveEverywhere = parseBool(rawVal);
								break;
							case "ToggleCraftAndInteraction":
								options_.ToggleCraftAndInteraction = parseBool(rawVal);
								break;
						}					
					}
				}
				if (options_.EnergyDrainMult == 0) {
					options_.UnconditionalSleep = true;
				}
			}
			return options_;
		}
	}
}