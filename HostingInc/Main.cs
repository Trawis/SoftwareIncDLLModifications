using UnityEngine;
using System;
using System.Linq;
using System.Net.Mime;
using System.Collections.Generic;

namespace HostingInc
{
    public class Main : ModMeta
    {
        public static void Tipka()
        {
            var btn = WindowManager.SpawnButton();
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Hosting";
            btn.onClick.AddListener(() => HostingBehaviour.Deals());
            //float resX = 1005f * (Screen.width / DesignWidth);

            var width = Screen.width;
            var x = 0;
            switch (width)
            {
                case 1920:
                    x = 1282;
                    break;
                case 1680:
                    x = 1162;
                    break;
                case 1600:
                    x = 1122;
                    break;
                case 1440:
                    x = 1042;
                    break;
                case 1400:
                    x = 1022;
                    break;
                case 1366:
                    x = 1005;
                    break;
                case 1360:
                    x = 1002;
                    break;
                case 1280:
                    x = 963;
                    break;
                case 1152:
                    x = 900;
                    break;
                case 1024:
                    x = 835;
                    break;
                default:
                    x = 1050;
                    break;
            }
            WindowManager.AddElementToElement(btn.gameObject, WindowManager.FindElementPath("MainPanel/Holder", null).gameObject, new Rect(x, 40, 70, 32), new Rect(0, 0, 0, 0));
        }
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
