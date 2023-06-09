
using System.Collections;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Ramune.LeviathanLocatorChip.Patches;
using RamuneLib;
using UnityEngine;
using static CraftData;
using static RamuneLib.Utilities;

namespace Ramune.LeviathanLocatorChip.Items
{
    public class LeviathanLocatorMono : MonoBehaviour
    {
        public float timeLastUsed;
        public float timeNextUseable;
        public float timeCooldown = 15f;
        public bool set;

        public void Start()
        {
            timeNextUseable = Time.time;
        }


        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.G))
            {
                if(!IsEquipped())
                {
                    if(set) return; set = true;
                    foreach (var p in CreaturePatches.pings) p.SetVisible(false);
                }
                else
                {
                    if(Time.time < timeNextUseable) return;
                    else timeNextUseable = Time.time + timeCooldown;

                    if(!set) return; set = false;
                    foreach(var p in CreaturePatches.pings) p.SetVisible(true);
                }
            }
        }


        public bool IsEquipped()
        {
            if(Inventory.main.equipment.GetCount(LeviathanLocatorChip.Info.TechType) > 0) return true;
            return false;
        }
    }


    public static class LeviathanLocatorChip
    {
        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = CreatePrefabInfo("LeviathanLocatorChip", "Leviathan Locator Chip", "Gotta catch 'em all.", GetSprite(TechType.MapRoomHUDChip), 1, 1);
            var LeviathanLocatorChip = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.MapRoomHUDChip);
           
            var recipe = CreateRecipe(1,
                new Ingredient(TechType.Battery, 1),
                new Ingredient(TechType.Glass, 1),
                new Ingredient(TechType.Lithium, 2),
                new Ingredient(TechType.AdvancedWiringKit, 1));

            LeviathanLocatorChip.SetGameObject(clone);
            LeviathanLocatorChip.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);
            LeviathanLocatorChip.SetEquipment(EquipmentType.Chip);
            LeviathanLocatorChip.SetRecipe(recipe)
                .WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab("Personal", "Equipment");

            LeviathanLocatorChip.Register();
        }
    }
}