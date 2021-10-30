using System;
using Harmony;

namespace NotKeepersNeeds {
	[HarmonyPatch(typeof(SleepGUI))]
	[HarmonyPatch("Open")]
	[HarmonyPatch(new Type[] { typeof(GJCommons.VoidDelegate), typeof(GJCommons.VoidDelegate), typeof(GJCommons.VoidDelegate), typeof(GJCommons.VoidDelegate) })]
	class SleepGUI_Open_Patch {
		[HarmonyPrefix]
		public static bool Prefix(SleepGUI __instance) {
			if (MainGame.me.player.energy.EqualsTo(MainGame.me.save.max_energy, 1E-05f)) {
				if (Config.GetOptions().UnconditionalSleep) {
					MainGame.me.player.energy -= 5;
				}
			}
			return true;
		}
	}
}