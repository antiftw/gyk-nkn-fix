using System;
using Harmony;
using UnityEngine;

namespace NotKeepersNeeds {
	[HarmonyPatch(typeof(MovementComponent))]
	[HarmonyPatch("UpdateMovement")]
	internal class MovementComponent_UpdateMovement_Patch {
		[HarmonyPrefix]
		public static bool Prefix(MovementComponent __instance, Vector2 dir, ref float delta_time) {
			if (!__instance.wgo.is_player || __instance.player_controlled_by_script || __instance.wgo.is_dead) {
				return true;
			}
			Config.Options opts = Config.GetOptions();

			float speed = __instance.wgo.data.GetParam("speed", 0.0f);
			if (speed > 0) {
				speed = 3.3f + __instance.wgo.data.GetParam("speed_buff", 0.0f);
				float energydt = delta_time * opts.EnergyForSprint;
				bool isSprintPressed = opts.SprintToggle ? opts.SprintKey.IsToggled() : Input.GetKey(opts.SprintKey.Key);

				if (isSprintPressed && (MainGame.me.player.energy >= energydt)) {
					__instance.SetSpeed(speed * opts.SprintSpeed);
					if (energydt > 0) {
						MainGame.me.player.energy -= energydt;
					}
				}
				else {
					__instance.SetSpeed(speed * opts.DefaultSpeed);
				}
			}
			return true;
		}
	}
}