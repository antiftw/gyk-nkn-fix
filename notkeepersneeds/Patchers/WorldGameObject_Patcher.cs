using Harmony;
using UnityEngine;

namespace NotKeepersNeeds {
	[HarmonyPatch(typeof(WorldGameObject))]
	[HarmonyPatch("energy", PropertyMethod.Setter)]
	internal class WorldGameObject_EnergySetter_Patcher {

		[HarmonyPrefix]
		public static bool Prefix(WorldGameObject __instance, ref float value) {
			if (__instance.is_player) {
				float oldVal = __instance.energy;
				float delta = oldVal - value;
				if (delta < 0) {
					Config.Options opts = Config.GetOptions();
					delta *= delta > 0 ? opts.EnergyDrainMult : opts.EnergyReplenMult;
					value = oldVal - delta;
				}
			}
			return true;
		}
	}

	[HarmonyPatch(typeof(WorldGameObject))]
	[HarmonyPatch("GetParam")]
	class WorldGameObject_GetParam_Patch {
		[HarmonyPostfix]
		static void Postfix(WorldGameObject __instance, ref string param_name, ref float __result) {
			if (Time.timeScale == 10f && param_name == "sleep_k_add") {
				float sleepTimeMult = Config.GetOptions().SleepTimeMult;
				if (sleepTimeMult != 1) {
					__result = (__result + 1) * sleepTimeMult - 1;
					if (__result <= -1)
						__result = -0.8f;
					//float dt = Time.deltaTime;
					//Config.Log("\r\nresult: " + __result + " (" + (__result * dt * 0.75f) + ") for " + dt);
				}
			}
		}
	}
}