using System;
using Harmony;

namespace NotKeepersNeeds {
	[HarmonyPatch(typeof(TechPointsSpawner))]
	[HarmonyPatch("Spawn")]
	class TechPointsSpawner_Spawn_Patch {
		[HarmonyPrefix]
		public static bool Prefix(TechPointsSpawner __instance, ref int r, ref int g, ref int b) {
			Config.Options opts = Config.GetOptions();

			r = opts.GetOrbCount(r, 0);
			g = opts.GetOrbCount(g, 1);
			b = opts.GetOrbCount(b, 2);

			return true;
		}
	}
}