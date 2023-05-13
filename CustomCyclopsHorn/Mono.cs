
using Nautilus.Utility;
using UnityEngine;

namespace Ramune.CustomCyclopsHorn
{
    public class CyclopsHornSoundHandler : MonoBehaviour
    {
        public BasicText text;
        public string selected;
        public int currentIndex;
        public static int currentIndex_;
        public void Start()
        {
            text = new BasicText();
            text.SetAlign(TMPro.TextAlignmentOptions.TopFlush);
            text.SetFont(FontUtils.Aller_Rg);
            text.SetFontStyle(TMPro.FontStyles.Normal);
            text.SetSize(24);
        }

        public void Update()
        {
            if(GameInput.GetKeyDown(KeyCode.Period))
            {
                currentIndex = (currentIndex + 1) % CustomCyclopsHorn.sounds.Count;
                currentIndex_ = currentIndex;
                selected = CustomCyclopsHorn.sounds[currentIndex];
                text.ShowMessage($"<color=#ffc02a>Selected {currentIndex + 1}/{CustomCyclopsHorn.sounds.Count}</color>: {selected}", 3);
            }
        }
    }
}