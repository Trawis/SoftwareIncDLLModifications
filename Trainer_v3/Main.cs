using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Trainer_v3
{
    public class Main : ModMeta
    {
        public static string version = "(v3.7)";
        public static bool IsShowed
        {
            get
            {
                return SettingsWindow.shown;
            }
        }

        public static Button btn;

        private TrainerBehaviour _trainerBehaviour;

        public override void Initialize(ModController.DLLMod parentMod)
        {
            _trainerBehaviour = parentMod.Behaviors.OfType<TrainerBehaviour>().First();
        }

        public static void SpawnButton()
        {
            btn = WindowManager.SpawnButton();
            btn.GetComponentInChildren<Text>().text = "Trainer " + version;
            btn.onClick.AddListener(() => SettingsWindow.Show());
            btn.name = "TrainerButton";

            WindowManager.AddElementToElement(btn.gameObject,
                WindowManager.FindElementPath("MainPanel/Holder/FanPanel").gameObject, new Rect(164, 0, 100, 32),
                new Rect(0, 0, 0, 0));
        }

        public static void SpawnWindow()
        {
            SettingsWindow.Show();
        }

        public override void ConstructOptionsScreen(RectTransform parent, bool inGame)
        {
            Text label = WindowManager.SpawnLabel();
            label.text = "Created by LtPain, edit by Trawis\n\n" +
                         "Options have been moved to the Main Screen of the game.\n" +
                         "Please load a game and press 'Trainer' button.";

            WindowManager.AddElementToElement(label.gameObject, parent.gameObject, new Rect(0, 0, 400, 128),
                new Rect(0, 0, 0, 0));
        }

        public override WriteDictionary Serialize(GameReader.LoadMode mode)
        {
            var data = new WriteDictionary();

            foreach (var Pair in PropertyHelper.Settings)
            {
                data[Pair.Key] = PropertyHelper.GetProperty(Pair.Key);
            }

            return data;
        }

        public override void Deserialize(WriteDictionary data, GameReader.LoadMode mode)
        {
            var keys = PropertyHelper.Settings.Keys.ToList();
            foreach (var key in keys)
            {
                PropertyHelper.SetProperty(key, data.Get(key, PropertyHelper.GetProperty(key)));
            }
        }

        public override string Name
        {
            get
            {
                return "Trainer v3";
            }
        }
    }
}