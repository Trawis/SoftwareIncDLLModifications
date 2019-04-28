using System;
using System.Collections.Generic;
using UnityEngine;

namespace Trainer_v3
{
    public static class PropertyHelper
    {
        public static System.Random rnd { get; set; }
        public static bool DoStuff { get { return GetProperty("ModActive") && GameSettings.Instance != null && HUD.Instance != null; } }
        public static bool RewardIsGained { get; set; }
        public static bool DealIsPushed { get; set; }
        public static string Product_PriceName { get; set; }

        private static Dictionary<string, bool> _settings = new Dictionary<string, bool>
        {
            {"NoStress", false},
            {"NoVacation", false},
            {"FullRoomBrightness", false},
            {"CleanRooms", false},
            {"FullEnvironment", false},
            {"NoiseReduction", false},
            {"FreeStaff", false},
            {"TemperatureLock", false},
            {"NoWaterElectricity", false},
            {"NoNeeds", false},
            {"FullEfficiency", false},
            {"FreeEmployees", false},
            {"LockAge", false},
            {"AutoDistributionDeals", false},
            {"MoreHostingDeals", false},
            {"IncreaseCourierCapacity", false},
            {"ReduceISPCost", false},
            {"IncreasePrintSpeed", false},
            {"FreePrint", false},
            {"IncreaseBookshelfSkill", false},
            {"NoMaintenance", false},
            {"NoSickness", false},
            {"UltraEfficiency", false},
            {"FullSatisfaction", false},
            {"DisableSkillDecay", false}
        };

        public static Dictionary<string, bool> Settings
        {
            get { return _settings; }
        }

        public static bool GetProperty(string key)
        {
            bool value;
            if (_settings.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                return false;
            }
        }

        public static void SetProperty(string key, bool value)
        {
            _settings[key] = value;
        }
    }
}
