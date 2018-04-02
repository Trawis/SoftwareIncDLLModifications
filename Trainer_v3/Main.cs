using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using Utils = Trainer_v3.Utilities;

namespace Trainer_v3
{
    //Your mod should have exactly one class that implements the ModMeta interface
    public class Main : ModMeta
    {
        //This function is used to generate the content in the "Mods" section of the options window
        //The behaviors array contains all behaviours that have been spawned for this mod, one for each implementation

        public static string version = "(v3.0)";
        public static bool IsShowed => SettingsWindow.shown;

        public static void Button()
        {
            Button btn = WindowManager.SpawnButton();
            btn.GetComponentInChildren<Text>().text = $"Trainer {version}";
            btn.onClick.AddListener(() => SettingsWindow.Show());

            WindowManager.AddElementToElement(btn.gameObject,
                WindowManager.FindElementPath("MainPanel/Holder/FanPanel").gameObject, new Rect(164, 0, 100, 32),
                new Rect(0, 0, 0, 0));
        }

        public static void Window()
        {
            SettingsWindow.Show();
        }

        public void ConstructOptionsScreen(RectTransform parent, ModBehaviour[] behaviours)
        {
            Text label = WindowManager.SpawnLabel();
            label.text = "Created by LtPain, edit by Trawis\n\n" +
                         "Options have been moved to the Main Screen of the game.\n" +
                         "Please load a game and press 'Trainer' button.";

            WindowManager.AddElementToElement(label.gameObject, parent.gameObject, new Rect(0, 0, 400, 128),
                new Rect(0, 0, 0, 0));
        }

        public string Name => "Trainer v3";
    }
}