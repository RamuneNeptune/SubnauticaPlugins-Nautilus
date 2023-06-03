using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using UnityEngine;
using RamuneLib;
using Nautilus.Assets.Gadgets;

namespace Ramune.IonNuclearReactor.Items
{
    internal class IonNuclearReactor
    {
        public static PrefabInfo Info;
        public static Texture2D Texture = Utilities.GetTexture("Illum");
        public static Texture2D Illum = Utilities.GetTexture("Illum");

        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("IonNuclearReactor", "Ion nuclear reactor", "Procudes power from the ion energy.", SpriteManager.Get(TechType.BaseNuclearReactor), 1, 1);

            var clone = new CloneTemplate(Info, TechType.BaseNuclearReactor)
            {
                ModifyPrefab = go =>
                {
                    var renderers = go.GetComponentsInChildren<MeshRenderer>(true);
                    foreach (var r in renderers)
                    {
                        r.material.mainTexture = Texture;
                        r.material.SetTexture("_SpecTex", Texture);
                        r.material.color = Color.black;
                        r.material.SetTexture("_Illum", Illum);
                        r.material.SetColor("_GlowColor", Color.green);
                        r.material.SetFloat("_GlowStrength", 2f);
                        r.material.SetFloat("_GlowStrengthNight", 3f);
                        r.material.EnableKeyword("MARMO_EMISSION");
                    }
                }
            };
            var prefab = new CustomPrefab(Info);

            prefab.SetGameObject(clone);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new CraftData.Ingredient(TechType.Quartz, 1),
                new CraftData.Ingredient(TechType.Silver, 1)));

            prefab.SetPdaGroupCategory(TechGroup.InteriorPieces, TechCategory.InteriorPiece).SetBuildable(true);
            prefab.Register();
        }
    }
}