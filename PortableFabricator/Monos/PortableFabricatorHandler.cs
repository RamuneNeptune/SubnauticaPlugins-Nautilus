
using UWE;

namespace Ramune.PortableFabricator.Monos
{
    public class PortableFabricatorHandler : MonoBehaviour
    {
        public Fabricator Fabricator;
        public GameObject FabricatorPrefab;

        public void Start()
        {
            CoroutineHost.StartCoroutine(SpawnFabricator());
        }

        public IEnumerator SpawnFabricator()
        {
            if(Player.main.transform.Find("Fabricator")) yield break;
            var task = GetPrefabForTechTypeAsync(Buildables.PortableFabricator.Info.TechType);
            yield return task;

            var prefab = task.GetResult();
            FabricatorPrefab = Instantiate(prefab, Player.main.transform);
            foreach(var r in FabricatorPrefab.GetComponentsInChildren<Renderer>()) r.enabled = false;
            foreach(var c in FabricatorPrefab.GetComponentsInChildren<Collider>()) c.enabled = false;

            Fabricator = FabricatorPrefab.GetComponent<Fabricator>();
        }

        public void Update()
        {
            FabricatorPrefab.transform.position = transform.position;
            if(!Inventory.main.container.Contains(Craftables.PortableFabricator.Info.TechType))
            {
                Utilities.Log(Colors.Red, "Inventory doesn't have the item");
                return;
            }
            else Utilities.Log(Colors.Aqua, "Inventory has the item!");

            if(Input.GetKeyDown(KeyCode.R) && !Cursor.visible && !Player.main.IsPiloting() && !Player.main.pda.isOpen) 
            {
                uGUI.main.craftingMenu.Open(Buildables.PortableFabricator.CraftTreeType, Fabricator);
            }
        }
    }
}