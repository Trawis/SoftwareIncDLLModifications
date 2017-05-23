using UnityEngine;
using System;
using System.Linq;
using System.Net.Mime;
using System.Collections.Generic;

namespace HostingInc
{
    public class Main : ModMeta
    {
        public void ConstructOptionsScreen(RectTransform parent, ModBehaviour[] behaviours)
        {

        }
        public string Name
        {
            //This will be displayed as the header in the Options window
            get { return "Hosting Inc"; }
        }
    }
}
