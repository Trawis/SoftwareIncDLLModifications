using System;
using System.Collections.Generic;

namespace Trainer_v3
{
    public class PropertyHelper
    {
        public static Random rnd { get; set; }
        public static bool DoStuff { get { return ModActive && GameSettings.Instance != null && HUD.Instance != null; } }

        public static bool ModActive { get; set; }
        public static bool LockAge { get; set; }
        public static bool NoStress { get; set; }
        public static bool NoNeeds{ get; set; }
        public static bool FullEfficiency { get; set; }
        public static bool FreeEmployees { get; set; }
        public static bool FreeStaff { get; set; }
        public static bool TemperatureLock { get; set; }
        public static bool NoWaterElectricity { get; set; }
        public static bool NoiseReduction { get; set; }
        public static bool FullEnvironment { get; set; }
        public static bool CleanRooms{ get; set; }
        public static bool FullRoomBrightness { get; set; }
        public static bool NoVacation { get; set; }
        public static bool AutoDistributionDeals { get; set; }
        public static bool MoreHostingDeals { get; set; }
        public static bool IncreaseCourierCapacity { get; set; }
        public static bool ReduceISPCost { get; set; }
        public static bool IncreasePrintSpeed { get; set; }
        public static bool FreePrint { get; set; }
        public static bool IncreaseBookshelfSkill { get; set; }
        public static bool NoMaintenance { get; set; }
        public static bool NoSickness { get; set; }
        public static bool UltraEfficiency { get; set; }
        public static bool FullSatisfaction { get; set; }
        public static string price_ProductName { get; set; }
        public static bool RewardIsGained { get; set; }
        public static bool DealIsPushed { get; set; }
        public static Dictionary<string, bool> Settings { get; set; }
    }
}
