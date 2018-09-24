using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trainer_v3
{
    public class SettingsManager : Behaviour
    {
        public static bool LockAge, LockStress, LockNeeds, LockEffSat, FreeEmployees, FreeStaff, TempLock;
        public static bool NoWaterElect, NoiseRed, FullEnv, CleanRooms, Fullbright, NoVacation, dDeal, MoreHosting;
        public static bool IncCourierCap, RedISPCost, IncPrintSpeed, FreePrint, IncBookshelfSkill, NoMaintenance;
        public static bool NoSickness, MaxOutEff, LockSat;

        public static Dictionary<String, bool> Settings = new Dictionary<String, bool>
            {
                {"LockStress", LockStress},
                {"NoVacation", NoVacation},
                {"Fullbright", Fullbright},
                {"CleanRooms", CleanRooms},
                {"FullEnv", FullEnv},
                {"NoiseRed", NoiseRed},
                {"FreeStaff", FreeStaff},
                {"TempLock", TempLock},
                {"NoWaterElect", NoWaterElect},
                {"LockNeeds", LockNeeds},
                {"LockEffSat", LockEffSat},
                {"FreeEmployees", FreeEmployees},
                {"LockAge", LockAge},
                {"AutoDistDeal", dDeal},
                {"MoreHosting", MoreHosting},
                {"IncreaseCourierCapacity", IncCourierCap},
                {"ReduceISPCost", RedISPCost},
                {"IncPrintSpeed", IncPrintSpeed},
                {"FreePrint", FreePrint},
                {"IncBookshelfSkill", IncBookshelfSkill},
                {"NoMaintenance", NoMaintenance},
                {"NoSickness", NoSickness},
                {"MaxOutEff", MaxOutEff},
                {"LockSat", LockSat}
            };

        public void Load()
        {
            foreach (var Pair in Settings)
            {
                LoadSetting(Pair.Key, false);
            }
        }

        public void Save()
        {
            foreach (var Pair in Settings)
            {
                SaveSetting(Pair.Key, Pair.Value.ToString());
            }
        }
    }
}
