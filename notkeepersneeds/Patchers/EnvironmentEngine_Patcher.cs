﻿using System;
using System.IO;
using Harmony;
using UnityEngine;

namespace NotKeepersNeeds {
	[HarmonyPatch(typeof(EnvironmentEngine))]
	[HarmonyPatch("Update")]
	class EnvironmentEngine_Update_Patch {
		[HarmonyPrefix]
		static bool Prefix(EnvironmentEngine __instance) {
			if (MainGame.game_starting || MainGame.paused || !MainGame.game_started || __instance.IsTimeStopped()) {
				return true;
			}
			Config.Options opts = Config.GetOptions();
			float mult = Time.timeScale == 10f ? opts.SleepTimeMult : opts.TimeMult;
			if (mult != 1) {
				
				float accountableDelta = Time.deltaTime / 225f; // since this._cur_time += deltaTime / 225f
				float adjDelta = accountableDelta * mult - accountableDelta;
				EnvironmentEngine.SetTime(__instance.time_of_day.time_of_day + adjDelta);
			}
			if (opts.ConfigReloadKey.IsPressed()) {
				Config.GetOptions(true);
				EffectBubblesManager.ShowImmediately(MainGame.me.player.pos3, "NKN configuration reloaded");
			}
			else if (opts.AddMoneyKey.IsPressed()) {
				MainGame.me.player.data.money += 100;
				EffectBubblesManager.ShowImmediately(MainGame.me.player.pos3, "1 gold coin granted");
			}
			else if (opts.ResetPrayKey.IsPressed()) {
				MainGame.me.player.SetParam("prayed_this_week", 0f);
				EffectBubblesManager.ShowImmediately(MainGame.me.player.pos3, "Weekly pray count reset");
			}
			else if (opts.TimeScaleSwitchKey.IsPressed()) {
				if (opts.TimeScaleSwitchKey.State == 0) {
					// toggle back to default value
					opts.TimeMult = opts.TimeMultDefault;
				} else {
					// toggle to the custom value
					opts.TimeMult = opts.TimeMultCustom;
				}
				EffectBubblesManager.ShowImmediately(MainGame.me.player.pos3, "TimeScale is set to " + opts.TimeMult);
			}
			else if (opts.CraftingSpeedKey.IsPressed())
			{
				if (opts.CraftingSpeedKey.State == 0){
					opts.CraftingSpeed = 1;
                    // only toggle interactionSpeed if ToggleCraftAndInteraction == true
					opts.InteractionSpeed = opts.ToggleCraftAndInteraction ? 1 : opts.InteractionSpeed;
					
				} else {
					opts.CraftingSpeed = opts.CraftingSpeedCustom;
					// only toggle interactionSpeed if ToggleCraftAndInteraction == true
					opts.InteractionSpeed = opts.ToggleCraftAndInteraction ? opts.InteractionSpeedCustom : opts.InteractionSpeed;
					
				}
				string message = "CraftingSpeed is set to " + opts.CraftingSpeed;
				if (opts.ToggleCraftAndInteraction) {
					message = "CraftingSpeed is set to "+ opts.CraftingSpeed + " \n\n&\n\nInteractionspeed is set to " + opts.InteractionSpeed;
				}

				EffectBubblesManager.ShowImmediately(MainGame.me.player.pos3, message);
			}
			else if (opts.AllowSaveEverywhere) {
				if (opts.SaveGameKey.IsPressed()) {
					PlatformSpecific.SaveGame(MainGame.me.save_slot, MainGame.me.save, (PlatformSpecific.OnSaveCompleteDelegate)(slot => {
						EffectBubblesManager.ShowImmediately(MainGame.me.player.pos3, "Saved succesfully");
					}));
				}
			}
			return true;
		}
	}
}