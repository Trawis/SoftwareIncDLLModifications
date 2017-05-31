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
            var behavior = behaviours.OfType<HostingBehaviour>().First();
            List<GameObject> objs = new List<GameObject>();

            var label = WindowManager.SpawnLabel();
            label.text = "This mod is created by Trawis";
            WindowManager.AddElementToElement(label.gameObject, parent.gameObject, new Rect(0, 0, 250, 32), new Rect(0, 0, 0, 0));

            var dist = WindowManager.SpawnCheckbox();
            dist.GetComponentInChildren<UnityEngine.UI.Text>().text = "Auto Distribution Deals";
            dist.isOn = behavior.dDeal;
            dist.onValueChanged.AddListener(a => behavior.dDealBool());
            objs.Add(dist.gameObject);

            int i = 1;
            foreach (var item in objs)
            {
                WindowManager.AddElementToElement(item, parent.gameObject, new Rect(0, i * 32, 250, 32),
                new Rect(0, 0, 0, 0));
                i++;
            }

        }
        public string Name
        {
            get { return "Hosting Inc"; }
        }
    }
}
