using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Ramune.HeadlampChip
{
    public class HeadlampChipMono : MonoBehaviour
    {
        public float offset = 0.94f;
        public int lightState;
        public GameObject lightRoot;
        public Equipment chip;
        public Inventory inv;
        public Player player;
        public Light light;
        public Color color = new Color(HeadlampChip.config.red, HeadlampChip.config.green, HeadlampChip.config.blue);
        public static List<HeadlampChipMono> Headlamps = new List<HeadlampChipMono>();

        public void OnEnable() => Headlamps.Add(this);
        public void OnDisable() => Headlamps.Remove(this);

        public void Start()
        {
            player = Player.main;
            inv = Inventory.main;
            lightRoot = new GameObject("lightRoot");
            lightRoot.transform.parent = player.transform;
            light = lightRoot.gameObject.AddComponent<Light>();
            light.enabled = true;
            light.color = color;
            light.range = 30f;
            light.intensity = 0.9f;
            light.spotAngle = 90f;
            light.innerSpotAngle = 80f;
            light.type = LightType.Spot;
            light.shape = LightShape.Cone;
            light.shadows = LightShadows.Hard;
            InvokeRepeating(nameof(Refresh), 1f, 1f);
        }

        public void Update()
        {
            if (!ChipEquipped() || player.isPiloting)
            {
                light.enabled = false;
                return;
            }

            lightRoot.transform.position = inv.cameraSocket.position + inv.cameraSocket.forward * offset;
            lightRoot.transform.rotation = inv.cameraSocket.rotation;
            lightRoot.transform.eulerAngles = inv.cameraSocket.eulerAngles;

            lightState = light.enabled ? 1 : 0;
            if (!Cursor.visible && GameInput.GetKeyDown(HeadlampChip.config.toggle))
            {
                switch (lightState)
                {
                    case 0:
                        light.enabled = true;
                        break;
                    case 1:
                        light.enabled = false;
                        break;
                }
            }
        }

        public bool ChipEquipped()
        {
            Equipment equipment = Inventory.main?.equipment;
            if (equipment == null) return false;

            List<string> Slots = new List<string>();
            equipment.GetSlots(EquipmentType.Chip, Slots);

            for (int i = 0; i < Slots.Count; i++)
            {
                string slot = Slots[i];
                if (equipment.GetTechTypeInSlot(slot) == HeadlampChipItem.thisTechType) return true;
            }
            return false;
        }

        public void Refresh()
        {
            color.r = HeadlampChip.config.red;
            color.g = HeadlampChip.config.green;
            color.b = HeadlampChip.config.blue;
            light.range = 30f * HeadlampChip.config.range;
            light.intensity = 0.9f * HeadlampChip.config.intensity;
            light.spotAngle = 90f * HeadlampChip.config.conesize;
            light.innerSpotAngle = 80f * HeadlampChip.config.conesize;
            light.color = color;
        }
    }
}