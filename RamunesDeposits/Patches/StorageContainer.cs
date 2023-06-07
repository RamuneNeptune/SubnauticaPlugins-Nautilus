using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Ramune.RamunesOutcrops.Patches
{
    [HarmonyPatch(typeof(uGUI_InventoryTab), nameof(uGUI_InventoryTab.OnOpenPDA))]
    public static class InventoryPatch
    {
        public static void Postfix(uGUI_InventoryTab __instance, PDATab tab, bool explicitly)
        {
            int usedStorageCount = Inventory.main.GetUsedStorageCount();

            if(usedStorageCount < 2)
            {
                IItemsContainer itemsContainer;

                if(usedStorageCount >= 1) itemsContainer = Inventory.main.GetUsedStorage(0);
                else return;

                IItemsContainer itemsContainer3 = itemsContainer;

                if(itemsContainer3 != null && itemsContainer3 is ItemsContainer)
                {
                    var storageLabel = __instance.storageLabel.gameObject;

                    __instance.storageLabel.color = new Color(0.75f, 0f, 0.75f);

                    //storageLabel.GetComponentInChildren<Image>().mainTexture.set = Utilities.GetTexture("");
                }
            }
        }

        public static IEnumerator Wait()
        {
            yield return new WaitUntil(() => Player.main.pda.isOpen);
        }
    }
}