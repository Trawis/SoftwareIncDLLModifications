using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Trainer_v3
{
    public class ModHelper : ModBehaviour
    {
        public static bool ModActive;
        public static bool DoStuff => ModActive && GameSettings.Instance != null && HUD.Instance != null;
        public bool reward, pushed, start;
        public static string price_ProductName;
        public static System.Random rnd;
        public static Dictionary<string, bool> Settings;
        public static bool LockAge, LockStress, LockNeeds, LockEffSat, FreeEmployees, FreeStaff, TempLock;
        public static bool NoWaterElect, NoiseRed, FullEnv, CleanRooms, Fullbright, NoVacation, dDeal, MoreHosting;
        public static bool IncCourierCap, RedISPCost, IncPrintSpeed, FreePrint, IncBookshelfSkill, NoMaintenance;
        public static bool NoSickness, MaxOutEff, LockSat;

        private void Start()
        {
            rnd = new System.Random(); // Random is time based, this makes it more random

            if (!ModActive)
                return;

            Settings = new Dictionary<string, bool>
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

            foreach (var Pair in Settings)
            {
                LoadSetting(Pair.Key, false);
            }

            StartCoroutine(DebugHelper());
        }

        public void Save()
        {
            foreach (var Pair in Settings)
            {
                SaveSetting(Pair.Key, Pair.Value.ToString());
            }
        }

        IEnumerator<WaitForSeconds> DebugHelper()
        {
            while (true)
            {
                yield return new WaitForSeconds(5.0f);
                DevConsole.Console.Log(start);
            }
        }

        public override void OnActivate()
        {
            ModActive = true;
        }

        public override void OnDeactivate()
        {
            ModActive = false;
        }
    }
}
