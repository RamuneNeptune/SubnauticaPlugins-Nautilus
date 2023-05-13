
using Nautilus.Utility;
using UnityEngine;

namespace Ramune.PrawnSuitLightSwitch
{
    public class PrawnLightSwitch : MonoBehaviour
    {
        public FMODAsset lightOn = AudioUtils.GetFmodAsset("event:/sub/seamoth/seamoth_light_on");
        public FMODAsset lightOff = AudioUtils.GetFmodAsset("event:/sub/seamoth/seamoth_light_off");
        public Light[] lights;
        public bool on;
        public bool flag1;
        public bool flag2;

        public void Start()
        {
            Light[] exosuitLights = gameObject.FindChild("lights_parent").GetComponentsInChildren<Light>(true);
            if(exosuitLights != null) lights = exosuitLights;
        }

        public void Update()
        {
            if(!Cursor.visible && GameInput.GetKeyDown(PrawnSuitLightSwitch.config.toggle))
            {
                on = !on;
                flag1 = PrawnSuitLightSwitch.config.sounds;
                flag2 = PrawnSuitLightSwitch.config.debug;
                if(on)
                {
                    if(flag1) FMODUWE.PlayOneShot(lightOn, transform.position);
                    foreach(var li in lights) li.enabled = false;
                    if(flag2) Subtitles.Add("PRAWN SUIT: Disabling lighting systems");
                    return;
                }
                if(flag1) FMODUWE.PlayOneShot(lightOff, transform.position);
                foreach(var li in lights) li.enabled = true;
                if(flag2) Subtitles.Add("PRAWN SUIT: Enabling lighting systems");
            }
        }
    }
}