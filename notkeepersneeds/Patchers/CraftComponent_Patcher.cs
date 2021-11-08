using System;
using Harmony;

namespace NotKeepersNeeds {
	[HarmonyPatch(typeof(CraftComponent))]
	[HarmonyPatch("DoAction")]
	class CraftComponent_DoAction_Patch {
		[HarmonyPrefix]
		static bool Prefix(CraftComponent __instance, ref WorldGameObject other_obj, ref float delta_time) {

			delta_time *= Config.GetOptions().CraftingSpeed;
			return true;
		}
	}

	[HarmonyPatch(typeof(CraftComponent))]
	[HarmonyPatch("CanSpendPlayerEnergy")]
	class CraftComponent_CanSpendPlayerEnergy_Patch {
		[HarmonyPrefix]
		static bool Prefix(CraftComponent __instance, ref WorldGameObject player_wgo, ref float delta_time) {
			// quite a hack, but delta_time is used to calculate the energy required for action
			delta_time *= Config.GetOptions().EnergyDrainMult;
			return true;
		}
	}
}