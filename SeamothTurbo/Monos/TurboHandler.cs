using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramune.SeamothTurbo.Monos
{
    public class TurboHandler : Vehicle
    {
        public bool shouldUse;

        public void Start()
        {
            maxCharges.Add(TechType.Accumulator, 1f);
            base.Start();
        }

        public override void OnUpgradeModuleUse(TechType techType, int slotID)
        {
            if(techType != TechType.None) return;
            shouldUse = true;
        }

        public override void OverrideAcceleration(ref Vector3 acceleration)
        {
            if(!shouldUse) return;
            acceleration *= 2f;
            shouldUse = false;
        }
    }
}