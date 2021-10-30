using System;
using Harmony;
using UnityEngine;

namespace AKeepersNeed
{
    [HarmonyPatch(typeof(Trading))]
    [HarmonyPatch("DoAcceptOffer")]
    class Trading_DoAcceptOffer_Patcher
    {
        public static double multiplier = Convert.ToDouble(Config.getChachedOption("sellMul"));

        [HarmonyPrefix]
        static bool Prefix(ref Trading __instance, ref bool need_check)
        {
            if (need_check && !__instance.CanAcceptOffer())
            {
            }
            float totalBalance = __instance.GetTotalBalance();
            __instance.player_money += totalBalance;
            if (totalBalance > 0f)
            {
                Stats.PlayerAddMoney(totalBalance, __instance.trader.id);
                __instance.player_money += (totalBalance+1) * (float)multiplier;
            }
            else
            {
                Stats.PlayerDecMoney(-totalBalance, __instance.trader.id);
            }
            __instance.trader.cur_money -= totalBalance;
            if (!__instance.trader.inventory.AddItems(__instance.player_offer.inventory))
            {
                Debug.LogError("Can not add player's offer to vendor's inventory");
            }
            foreach (Item item in __instance.trader.cur_offer.inventory)
            {
                if (!MainGame.me.player.AddToInventory(item))
                {
                    Debug.LogError("Can not add vendor's offer's item \"" + item.id + "\" to players's inventory");
                }
                else if (item.definition.equipment_type != ItemDefinition.EquipmentType.None)
                {
                    MainGame.me.player.TryEquipPickupedDrop(item, true);
                }
            }
            foreach (Item item2 in __instance.player_offer.inventory)
            {
                for (int i = 0; i < item2.value; i++)
                {
                    item2.OnTraded();
                }
            }
            foreach (Item item3 in __instance.trader.cur_offer.inventory)
            {
                for (int j = 0; j < item3.value; j++)
                {
                    item3.OnTraded();
                }
            }
            __instance.player_offer.inventory.Clear();
            __instance.trader.cur_offer.inventory.Clear();
            Debug.Log("Accepted offer!");
            __instance.trader.FillDrawingMultiInventory();
            return false;
        }

    }
}
