using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Trainer_v3
{
    //Your mod should have exactly one class that implements the ModMeta interface
    public class Main : ModMeta
    {
        //This function is used to generate the content in the "Mods" section of the options window
        //The behaviors array contains all behaviours that have been spawned for this mod, one for each implementation

        public static string version = "(v3.4)";
        public static bool IsShowed
        {
            get
            {
                return SettingsWindow.shown;
            }
        }

        private TrainerBehaviour _trainerBehaviour;

        //This method is called once when the mod is first loaded
        public override void Initialize(ModController.DLLMod parentMod)
        {
            _trainerBehaviour = parentMod.Behaviors.OfType<TrainerBehaviour>().First();
        }

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
            //The WriteDictionary is a simple dictionary the game uses to save data to a file
            //Note that the type saved needs to be a value type or have an empty contructor
            //You can safely save arrays, lists, hashsets and dictionaries
            //In this case we are saving a 32 bit integer, which is a value type

            var data = new WriteDictionary();
            //foreach (var Pair in PropertyHelper.Settings)
            //{
            //    data[Pair.Key] = PropertyHelper
            //}
            //    LoadSetting(Pair.Key, false);
            //data["Floor"] = _floorBehaviour.MaxFloor;
            return data;
        }

        //This file is called when a save game is loaded, if there is any data relevant to this ACTIVE mod
        //Note that this function is called very early in the loading process, so all save game data might not be initialized yet
        public override void Deserialize(WriteDictionary data, GameReader.LoadMode mode)
        {
            //The WriteDictionary Get function tries to fetch a value from the dictionary
            //but will return a default value if the key is not present
            //In this case MaxFloor is returned if the "Floor" key is not present
            //_floorBehaviour.MaxFloor = data.Get("Floor", _floorBehaviour.MaxFloor);
            //Note that we are not saving this value to the settings file, as this is save file specific
            //This way the player can have a default value for all new games, and a value for specific saves
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