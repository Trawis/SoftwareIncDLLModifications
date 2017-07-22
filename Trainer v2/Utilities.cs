using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Trainer
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
            WindowManager.AddElementToWindow(x.gameObject, Main.pr, Button, new Rect(0, 0, 0, 0));
        }

        public static void AddInputBox(String Text, Rect InputBox, UnityAction<string> Action)
        {
            InputField x = WindowManager.SpawnInputbox();
            x.text = Text;
            x.onValueChanged.AddListener(Action);
            WindowManager.AddElementToWindow(x.gameObject, Main.pr, InputBox, new Rect(0, 0, 0, 0));
        }

        public static void AddLabel(String Text, Rect Label)
        {
            Text x = WindowManager.SpawnLabel();
            x.text = "<= This cell is universal for\nPrice, Stock, Active Users";
            WindowManager.AddElementToWindow(x.gameObject, Main.pr, Label, new Rect(0, 0, 0, 0));
        }

        public static void AddCheckBox(String Text, ref bool Bool, ref List<GameObject> Toggles)
        {
            Toggle x = WindowManager.SpawnCheckbox();
            x.GetComponentInChildren<Text>().text = Text;
            x.isOn = Bool;
            Bool = x;
            x.onValueChanged.AddListener((boolean) => x.isOn = !x.isOn);
            Toggles.Add(x.gameObject);
        }

        public static void DoLoops(ref List<GameObject> btn, ref List<GameObject> col1, ref List<GameObject> col2, ref List<GameObject> col3)
        {
            for (int i = 0; i < btn.Count; i++)
            {
                var item = btn[i];

                WindowManager.AddElementToWindow(item, Main.pr, new Rect(1, (i + 7) * 32, 150, 32),
                    new Rect(0, 0, 0, 0));
            }

            for (int i = 0; i < col1.Count; i++)
            {
                var item = col1[i];

                WindowManager.AddElementToWindow(item, Main.pr, new Rect(161, (i + 7) * 32, 150, 32),
                    new Rect(0, 0, 0, 0));
            }

            for (int i = 0; i < col2.Count; i++)
            {
                var item = col2[i];

                WindowManager.AddElementToWindow(item, Main.pr, new Rect(322, (i + 7) * 32, 150, 32),
                    new Rect(0, 0, 0, 0));
            }

            for (int i = 0; i < col3.Count; i++)
            {
                var item = col3[i];

                WindowManager.AddElementToWindow(item, Main.pr, new Rect(483, (i + 7) * 32, 150, 32),
                    new Rect(0, 0, 0, 0));
            }
        }
    }
}