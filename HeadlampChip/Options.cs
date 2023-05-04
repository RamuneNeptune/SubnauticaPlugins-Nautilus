using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nautilus.Json;
using Nautilus.Options;
using Nautilus.Options.Attributes;

namespace Ramune.HeadlampChip
{
    [Menu("")]
    public class Config : ConfigFile
    {
        [Slider("")]
        public float slider = 1f;
    }
}