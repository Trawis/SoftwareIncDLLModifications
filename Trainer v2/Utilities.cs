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

        public static void AddCheckBox(String Text, UnityAction<bool> Action, ref List<GameObject> Toggles)
        {
            Toggle x = WindowManager.SpawnCheckbox();
            x.GetComponentInChildren<UnityEngine.UI.Text>().text = Text;
            x.isOn = TrainerBehaviour.LockNeeds;
            x.onValueChanged.AddListener((boolean) =>
            {
                Action.Invoke(boolean);
                x.isOn = !x.isOn;
            });
            Toggles.Add(x.gameObject);
        }
    }
}