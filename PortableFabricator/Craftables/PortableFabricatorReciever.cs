


using Nautilus.Handlers;
using UWE;

namespace Ramune.PortableFabricator.Craftables
{
    public static class PortableFabricatorReciever
    {
        public static PrefabInfo Info;

        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("PortableFabricator", "Portable fabricator", "Fabricator accessible from anywhere.", Utilities.GetSprite(TechType.Fabricator), 1, 1);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.Glass)
            {
                ModifyPrefab = go => Player.main.gameObject.EnsureComponent<PortableFabricatorReceiver>()
            };

            prefab.SetGameObject(clone);
            prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);
            prefab.SetEquipment(EquipmentType.None).WithQuickSlotType(QuickSlotType.Selectable);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.PowerCell, 1),
                new Ingredient(TechType.ComputerChip, 1),
                new Ingredient(TechType.Silicone, 2),
                new Ingredient(TechType.PlasteelIngot, 1)))
                .WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab("Personal", "Equipment");
            prefab.Register();
        }
    }

    public class PortableFabricatorReceiver : MonoBehaviour
    {
        public Fabricator Fabricator;
        public GameObject fabricator;

        public void Start()
        {
            CoroutineHost.StartCoroutine(SpawnFabricator());
        }

        public IEnumerator SpawnFabricator()
        {
            var task = GetPrefabForTechTypeAsync(Buildables.PortableFabricator.Info.TechType);
            yield return task;

            var prefab = task.GetResult();
            fabricator = Instantiate(prefab, Player.main.transform);
            foreach(var r in fabricator.GetComponentsInChildren<Renderer>()) r.enabled = false;
            foreach(var c in fabricator.GetComponentsInChildren<Collider>()) c.enabled = false;

            Fabricator = fabricator.GetComponent<Fabricator>();
        }

        public void Update()
        {
            fabricator.transform.position = transform.position;

            if(Input.GetKeyDown(KeyCode.R) && !Cursor.visible && !Player.main.pda.isOpen && !Player.main.IsPiloting())
            {
                uGUI.main.craftingMenu.Open(Buildables.PortableFabricator.CraftTreeType, Fabricator);
            }
        }
    }
}