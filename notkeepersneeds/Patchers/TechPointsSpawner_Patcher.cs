using System;
using Harmony;

namespace NotKeepersNeeds {
	[HarmonyPatch(typeof(TechPointsSpawner))]
	[HarmonyPatch("Spawn")]
	class TechPointsSpawner_Spawn_Patch {
		[HarmonyPrefix]
		public static bool Prefix(TechPointsSpawner __instance, ref int r, ref int g, ref int b) {
			Config.Options opts = Config.GetOptions();
			//float dt = Time.deltaTime;

			int[] orbs = opts.GetOrbCount(r, g, b);
			r = orbs[0];
			g = orbs[1];
			b = orbs[2];
			return true;
		}
	}
}