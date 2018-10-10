using System;
using System.Collections.Generic;

namespace Trainer_v3
{
    public class PropertyHelper
    {
        public static Random rnd { get; set; }
        public static bool DoStuff => ModActive && GameSettings.Instance != null && HUD.Instance != null;

        public static bool ModActive { get; set; }
        public static bool LockAge { get; set; }
        public static bool LockStress { get; set; }
        public static bool LockNeeds{ get; set; }
        public static bool LockEffSat { get; set; }
        public static bool FreeEmployees { get; set; }
        public static bool FreeStaff { get; set; }
        public static bool TempLock { get; set; }
        public static bool NoWaterElect { get; set; }
        public static bool NoiseRed { get; set; }
        public static bool FullEnv { get; set; }
        public static bool CleanRooms{ get; set; }
        public static bool Fullbright { get; set; }
        public static bool NoVacation { get; set; }
        public static bool dDeal { get; set; }
        public static bool MoreHosting { get; set; }
        public static bool IncCourierCap { get; set; }
        public static bool RedISPCost { get; set; }
        public static bool IncPrintSpeed { get; set; }
        public static bool FreePrint { get; set; }
        public static bool IncBookshelfSkill { get; set; }
        public static bool NoMaintenance { get; set; }
        public static bool NoSickness { get; set; }
        public static bool MaxOutEff { get; set; }
        public static bool LockSat { get; set; }
        public static string price_ProductName { get; set; }
        public static bool reward { get; set; }
        public static bool pushed { get; set; }
        public static bool start { get; set; }
        public static Dictionary<string, bool> Settings { get; set; }
    }
}
