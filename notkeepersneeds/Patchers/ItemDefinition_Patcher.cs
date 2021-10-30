using System;
using Harmony;

namespace NotKeepersNeeds {
	[HarmonyPatch(typeof(ItemDefinition))]
	[HarmonyPatch("GetPrice")]
	class ItemDefinition_GetPrice_Patch {
		[HarmonyPostfix]
		public static void Postfix(ItemDefinition __instance, ref float __result) {
			float infl = Config.GetOptions().InflationAmount;
			if (infl != 1) {
				__result = __instance.base_price + (__result - __instance.base_price) * infl;
			}
		}
	}
}