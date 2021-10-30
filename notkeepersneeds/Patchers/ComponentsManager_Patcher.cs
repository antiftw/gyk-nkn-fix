using System;
using Harmony;

namespace NotKeepersNeeds {
	[HarmonyPatch(typeof(ComponentsManager))]
	[HarmonyPatch("DoAction")]
	class ComponentsManager_DoAction_Patch {
		[HarmonyPrefix]
		static bool Prefix(ComponentsManager __instance, ref WorldGameObject other_obj, ref float delta_time) {

			delta_time *= Config.GetOptions().InteractionSpeed;
			return true;
		}
	}
}