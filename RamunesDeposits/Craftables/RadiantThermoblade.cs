



namespace Ramune.RamunesOutcrops.Craftables
{
    public class RadiantThermobladeMono : HeatBlade
    {
        public int _;
        public bool __ = false;
        public readonly KeyCode[] ___ = { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.A };
        
        public static FMODAsset Seismic = AudioUtils.GetFmodAsset("event:/creature/crash/die");
        public static FMODAsset Explosive = AudioUtils.GetFmodAsset("");
        public static FMODAsset Nanoswarm = AudioUtils.GetFmodAsset("event:/tools/stasis_gun/sphere_enter");
        public static FMODAsset Next = AudioUtils.GetFmodAsset("event:/env/input_number");

        public MeshRenderer renderer;
        public List<string> descriptionInfo = new()
        {
            "<color=#ffffff>None</color>",
            "<color=#ffffff>Seismic</color> (1/3)",
            "<color=#ffffff>Explosive</color> (2/3)",
            "<color=#ffffff>Nanoswarm</color> (3/3)"
        };
        public BladeMode currentMode = BladeMode.None;
        public enum BladeMode { 
            None, 
            Seismic, 
            Explosive, 
            Nanoswarm 
        };
        public bool shouldDisplay;
        public int currentIndex;


        public void Start()
        {
            attackDist = 5f;
            damage = 10f;
            renderer = GetComponentInChildren<MeshRenderer>();
        }

        public bool IsPowered()
        {
            if(GetComponent<EnergyMixin>().HasItem()) return true;
            return false;
        }

        public void Update()
        {
            if(!IsPowered())
            {
                HandReticle.main.SetText(HandReticle.TextType.Use, "Missing vital power source", false);
                HandReticle.main.SetIcon(HandReticle.IconType.None, 1f);
                return;
            }

            if(!shouldDisplay) return;

            if(Input.anyKeyDown && !Cursor.visible)
            {
                if(Input.GetKeyDown(KeyCode.Q))
                {
                    FMODUWE.PlayOneShot(Next, Player.main.transform.position, 1f);
                    SetNextMode();

                    for(int i = 1; i < ___.Length; i++)
                    {
                        if(Input.GetKeyDown(___[i]) || _ == i)
                        {
                            i++;
                            _ = i;
                            Utilities.Log(Colors.Amber, $"{i}");
                            break;
                        }
                        else _ = 0;
                    }
                }
            }

            HandReticle.main.SetText(HandReticle.TextType.Use, "Switch mode", false, GameInput.Button.Deconstruct);
            if(currentIndex == 0) HandReticle.main.SetText(HandReticle.TextType.UseSubscript, $"None", false);
            else HandReticle.main.SetText(HandReticle.TextType.UseSubscript, $"{descriptionInfo[currentIndex]}", false);
            HandReticle.main.SetIcon(HandReticle.IconType.None, 1f);
        }

        public void SetNextMode()
        {
            if(currentMode == BladeMode.None)
            {
                currentIndex = 1;
                currentMode = BladeMode.Seismic;
            }
            else
            if(currentMode == BladeMode.Seismic)
            {
                currentIndex = 2;
                currentMode = BladeMode.Explosive;
            }
            else
            if(currentMode == BladeMode.Explosive)
            {
                currentIndex = 3;
                currentMode = BladeMode.Nanoswarm;
            }
            else
            if(currentMode == BladeMode.Nanoswarm)
            {
                currentIndex = 0;
                currentMode = BladeMode.None;
            }
        }

        public override void OnDraw(Player p)
        {
            shouldDisplay = true;
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            shouldDisplay = false;
            base.OnHolster();
        }

        public override void OnToolUseAnim(GUIHand hand)
        {
            base.OnToolUseAnim(hand);

            Vector3 vector = default;
            GameObject gameObject = null;
            UWE.Utils.TraceFPSTargetPosition(Player.main.gameObject, attackDist, ref gameObject, ref vector, true);

            if(gameObject == null) return;
            var livemixin = gameObject.FindAncestor<LiveMixin>();

            if(!IsValidTarget(livemixin)) return;
            SpecialAttack(currentMode, gameObject, livemixin);
        }

        public void SpecialAttack(BladeMode mode, GameObject creature, LiveMixin livemixin)
        {
            switch(mode)
            {
                case BladeMode.None:
                    break;

                case BladeMode.Seismic:
                    WorldForces.AddCurrent(creature.transform.position, DayNightCycle.main.timePassed, 10f, -Vector3.Cross(gameObject.transform.forward, new Vector3(0, 1, 0)), creature.GetComponent<Rigidbody>().mass * 1.5f, 4f);
                    MainCameraControl.main.ShakeCamera(1f, 1.5f, MainCameraControl.ShakeMode.Linear, 1.4f);
                    FMODUWE.PlayOneShot(Seismic, creature.transform.position, 1f);
                    break;

                case BladeMode.Explosive:
                    break;

                case BladeMode.Nanoswarm:
                    livemixin.TakeDamage(10f, creature.transform.position, DamageType.Electrical);
                    FMODUWE.PlayOneShot(Nanoswarm, creature.transform.position);
                    creature.AddComponent<CreatureFreezer>();
                    break;
            }
        }
    }


    public class CreatureFreezer : MonoBehaviour
    {
        public Animator[] animators;
        public Rigidbody rigid;
        public float timeStart;
        public float duration;

        public void Start()
        {
            timeStart = Time.time;
            duration = Time.time + 0.8f;
            rigid = GetComponent<Rigidbody>();
            animators = GetComponentsInChildren<Animator>(true);
        }

        public void Update()
        {
            timeStart = Time.time;
            rigid.velocity = Vector3.zero;

            if(animators.Length > 0 ) foreach(var a in animators) a.enabled = false;
            if(timeStart < duration) return;

            if(animators.Length > 0) foreach (var a in animators) a.enabled = true;
            DestroyImmediate(this);    
        }
    }


    public static class RadiantThermoblade
    {
        public static Texture2D RadiantBladeTexture = Utilities.GetTexture("RadiantBladeTexture");
        public static Texture2D RadiantBladeIllumTexture = Utilities.GetTexture("RadiantBladeIllumTexture");

        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("RadiantHeatBlade", "<color=#8f01ff>Radiant</color> Thermoblade", "Cooks and sterilizes small organisms for immediate consumption.\n\nDAMAGE: +75%\nRANGE: +30%", Utilities.GetSprite("RadiantBladeSprite"), 1, 1);
            var prefab = new CustomPrefab(Info);

            prefab.SetUnlock(TechType.HeatBlade);
            prefab.SetEquipment(EquipmentType.Hand).WithQuickSlotType(QuickSlotType.Selectable);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.HeatBlade, 1),
                new Ingredient(RadiantCrystal.Info.TechType, 2)))
                .WithFabricatorType(RadiantFabricator.CraftTreeType)
                .WithStepsToFabricatorTab("Tools")
                .WithCraftingTime(0.5f);

            var clone = new CloneTemplate(Info, TechType.HeatBlade)
            {
                ModifyPrefab = go =>
                {
                    var energy = PrefabUtils.AddEnergyMixin(go, "RadiantThermobladeEnergySlot", RadiantCube.Info.TechType, new List<TechType> { RadiantCube.Info.TechType });
                    energy.allowBatteryReplacement = true;

                    var renderer = go.GetComponentInChildren<MeshRenderer>(true);
                    foreach(var m in renderer.materials)
                    {
                        m.mainTexture = RadiantBladeIllumTexture;
                        m.SetTexture("_SpecTex", RadiantBladeIllumTexture);
                        m.SetTexture("_Illum", RadiantBladeIllumTexture);
                        m.SetColor("_GlowColor", new Color(0.67f, 0.1f, 0.85f, 0.4f));
                    }

                    var heatblade = go.GetComponent<HeatBlade>();
                    var radiantblade = go.EnsureComponent<RadiantThermobladeMono>().CopyComponent(heatblade);
                    UnityEngine.Object.DestroyImmediate(heatblade);
                }
            };
            prefab.SetGameObject(clone);
            prefab.Register();
        }
    }
}