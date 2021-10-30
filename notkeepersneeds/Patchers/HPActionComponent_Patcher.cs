using System;
using Harmony;

namespace NotKeepersNeeds {
	[HarmonyPatch(typeof(HPActionComponent))]
	[HarmonyPatch("CanSpendPlayerEnergy")]
	class HPActionComponent_CanSpendPlayerEnergy_Patch {
		[HarmonyPrefix]
		static bool Prefix(HPActionComponent __instance, ref WorldGameObject player_wgo, ref float delta_time) {
			// quite a hack, but delta_time is used to calculate the energy required for action
			delta_time *= Config.GetOptions().EnergyDrainMult;
			return true;
		}
	}
	[HarmonyPatch(typeof(HPActionComponent))]
	[HarmonyPatch("DecHP")]
	class HPActionComponent_DecHP_Patch {
		[HarmonyPrefix]
		static bool Prefix(HPActionComponent __instance, ref float value) {
			Config.Options opts = Config.GetOptions();
			if (opts.GlobalDmgMult != 1) {
				value *= opts.GlobalDmgMult;
			}
			if (value > 0.0 && __instance.wgo.is_player && (opts.DmgMult != 1.0)) {
				if (opts.DmgMult <= 0) {
					value = 0;
				}
				else {
					WorldGameObject player = __instance.wgo;

					float armor = 0;

					Item equippedItem = player.GetEquippedItem(ItemDefinition.EquipmentType.HeadArmor);
					if (equippedItem != null)
						armor += equippedItem.definition.armor;
					equippedItem = player.GetEquippedItem(ItemDefinition.EquipmentType.BodyArmor);
					if (equippedItem != null)
						armor += equippedItem.definition.armor;
					armor += player.GetParam("add_armor", 0.0f);

					value = (value - armor) * opts.DmgMult + armor;
				}
			}
			return true;
		}
	}
}