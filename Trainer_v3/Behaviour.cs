using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trainer_v3
{
    public class Behaviour : ModBehaviour
    {
        public static bool ModActive;
        public override void OnActivate()
        {
            ModActive = true;
        }

        public override void OnDeactivate()
        {
            ModActive = false;
        }
    }
}
