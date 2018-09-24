using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Trainer_v3
{
    public class Utilities
    {

        public static void AddButton(String Text, UnityAction Action, ref List<GameObject> Buttons)
        {
            Button x = WindowManager.SpawnButton();
            x.GetComponentInChildren<Text>().text = Text;
            x.onClick.AddListener(Action);
            Buttons.Add(x.gameObject);
        }
        
        public static void AddButton(String Text, Rect Button, UnityAction Action)
        {
            Button x = WindowManager.SpawnButton();
            x.GetComponentInChildren<UnityEngine.UI.Text>().text = Text;
            x.onClick.AddListener(Action);
            WindowManager.AddElementToWindow(x.gameObject, SettingsWindow.Window, Button, new Rect(0, 0, 0, 0));
        }

        public static void AddInputBox(String Text, Rect InputBox, UnityAction<string> Action)
        {
            InputField x = WindowManager.SpawnInputbox();
            x.text = Text;
            x.onValueChanged.AddListener(Action);
            WindowManager.AddElementToWindow(x.gameObject, SettingsWindow.Window, InputBox, new Rect(0, 0, 0, 0));
        }

        public static void AddLabel(String Text, Rect Label)
        {
            Text x = WindowManager.SpawnLabel();
            x.text = "<= This cell is universal for\nPrice, Stock & Active Users";
            WindowManager.AddElementToWindow(x.gameObject, SettingsWindow.Window, Label, new Rect(0, 0, 0, 0));
        }

        public static void AddToggle(String Text, bool isOn, UnityAction<bool> Action, ref List<GameObject> Toggles)
        {
            SettingsManager settingsManager = new SettingsManager();

            Toggle Toggle = WindowManager.SpawnCheckbox();
            Toggle.GetComponentInChildren<UnityEngine.UI.Text>().text = Text;
            Toggle.isOn = isOn;
            Toggle.onValueChanged.AddListener(Action);
            Toggle.onValueChanged.AddListener((e) => settingsManager.Save());
            Toggles.Add(Toggle.gameObject);
        }

        public static void DoLoops(GameObject[] Buttons, GameObject[] Col1, GameObject[] Col2, GameObject[] Col3)
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                GameObject item = Buttons[i];

                WindowManager.AddElementToWindow(item, SettingsWindow.Window, new Rect(1, (i + 7) * 32, 150, 32),
                    new Rect(0, 0, 0, 0));
            }

            for (int i = 0; i < Col1.Length; i++)
            {
                GameObject item = Col1[i];

                WindowManager.AddElementToWindow(item, SettingsWindow.Window, new Rect(161, (i + 7) * 32, 150, 32),
                    new Rect(0, 0, 0, 0));
            }

            for (int i = 0; i < Col2.Length; i++)
            {
                GameObject item = Col2[i];

                WindowManager.AddElementToWindow(item, SettingsWindow.Window, new Rect(322, (i + 7) * 32, 150, 32),
                    new Rect(0, 0, 0, 0));
            }

            for (int i = 0; i < Col3.Length; i++)
            {
                GameObject item = Col3[i];

                WindowManager.AddElementToWindow(item, SettingsWindow.Window, new Rect(483, (i + 7) * 32, 150, 32),
                    new Rect(0, 0, 0, 0));
            }
        }
    }
}