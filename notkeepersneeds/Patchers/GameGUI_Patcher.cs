using System;
using Harmony;

namespace NotKeepersNeeds {
	[HarmonyPatch(typeof(GameGUI))]
	[HarmonyPatch("Open")]
	[HarmonyPatch(new Type[] {})]
	class GameGUI_Open_Patch {
		[HarmonyPrefix]
		public static bool Prefix(GameGUI __instance) {
			if (Config.GetOptions().DullInventoryMusic) {
				SmartAudioEngine.me.SetDullMusicMode(true);
			}
			return true;
		}
	}

	[HarmonyPatch(typeof(GameGUI))]
	[HarmonyPatch("Hide")]
	[HarmonyPatch(new Type[] { typeof(bool) })]
	class GameGUI_Hide_Patch {
		[HarmonyPrefix]
		public static bool Prefix(GameGUI __instance) {
			if (Config.GetOptions().DullInventoryMusic) {
				SmartAudioEngine.me.SetDullMusicMode(false);
			}
			return true;
		}
	}
}