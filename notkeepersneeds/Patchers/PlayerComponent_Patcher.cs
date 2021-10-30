using Harmony;
using UnityEngine;

namespace NotKeepersNeeds {
	[HarmonyPatch(typeof(PlayerComponent))]
	[HarmonyPatch("Update")]
	internal class PlayerComponent_Update_Patcher {

		[HarmonyPostfix]
		public static void Postfix(PlayerComponent __instance) {
			Config.Options opts = Config.GetOptions();

			if (opts.HealthRegen) {
				WorldGameObject player = MainGame.me.player;
				float curhp = player.hp;
				if (curhp > 0 && curhp < 100 && (opts.HealIfTired || player.energy > 10)) {
					curhp += opts.HealthRegenPerSecond * Time.deltaTime;
					player.hp = curhp < 100 ? curhp : 100;
				}
			}
		}
	}
	[HarmonyPatch(typeof(PlayerComponent))]
	[HarmonyPatch("CheckEnergy")]
	internal class PlayerComponent_CheckEnergy_Patcher {

		[HarmonyPrefix]
		public static bool Prefix(PlayerComponent __instance, ref float need_energy) {
			if (need_energy != 0) {
				Config.Options opts = Config.GetOptions();
				float mult = need_energy > 0 ? opts.EnergyDrainMult : opts.EnergyReplenMult;
				need_energy *= mult;
			}
			return true;
		}
	}
	[HarmonyPatch(typeof(PlayerComponent))]
	[HarmonyPatch("TrySpendEnergy")]
	internal class PlayerComponent_TrySpendEnergy_Patcher {

		[HarmonyPrefix]
		public static bool Prefix(PlayerComponent __instance, ref float need_energy) {
			if (need_energy != 0) {
				Config.Options opts = Config.GetOptions();
				float mult = need_energy > 0 ? opts.EnergyDrainMult : opts.EnergyReplenMult;
				need_energy *= mult;
			}
			return true;
		}
	}
}