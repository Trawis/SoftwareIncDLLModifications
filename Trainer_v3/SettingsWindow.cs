using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Utils = Trainer_v3.Utilities;


namespace Trainer_v3
{
    public class SettingsWindow : MonoBehaviour
    {
        public static GUIWindow Window;
        private static string title = "Trainer Settings, by Trawis " + Main.version;
        public static bool shown = false;

        public static void Show()
        {
            if (shown)
            {
                Window.Close();
                shown = false;
                WindowManager.ActiveWindows.Remove(Window);
                return;
            }
            Init();
            shown = true;
        }

        private static void Init()
        {
            Window = WindowManager.SpawnWindow();
            Window.InitialTitle =  Window.TitleText.text = Window.NonLocTitle = title;
            Window.MinSize.x = 670;
            Window.MinSize.y = 580;
            Window.name = "TrainerSettings";
            Window.MainPanel.name = "TrainerSettingsPanel";

            //Window.rectTransform = new RectTransform();
            //Window.rectTransform.position = new Vector3
            //{
            //    x = 50f,
            //    y = 50f,
            //    z = 0f
            //};

            WindowManager.ActiveWindows.Add(Window);

            if (Window.name == "TrainerSettings")
            {
                Window.GetComponentsInChildren<Button>()
                  .SingleOrDefault(x => x.name == "CloseButton")
                  .onClick.AddListener(() => shown = false);

            }

            List<GameObject> Buttons = new List<GameObject>();
            List<GameObject> col1 = new List<GameObject>();
            List<GameObject> col2 = new List<GameObject>();
            List<GameObject> col3 = new List<GameObject>();


            Utils.AddInputBox("Product Name Here", new Rect(1, 96, 150, 32),
                boxText => TrainerBehaviour.price_ProductName = boxText);


            Utils.AddButton("Add Money", new Rect(1, 0, 150, 32), TrainerBehaviour.IncreaseMoney);

            Utils.AddButton("Add Reputation", new Rect(161, 0, 150, 32), TrainerBehaviour.AddRep);

            Utils.AddButton("Set Product Price", new Rect(161, 96, 150, 32), TrainerBehaviour.SetProductPrice);

            Utils.AddButton("Set Product Stock", new Rect(322, 96, 150, 32), TrainerBehaviour.SetProductStock);

            Utils.AddButton("Set Active Users", new Rect(483, 96, 150, 32), TrainerBehaviour.AddActiveUsers);

            Utils.AddButton("Max Followers", new Rect(1, 32, 150, 32), TrainerBehaviour.MaxFollowers);

            Utils.AddButton("Fix Bugs", new Rect(161, 32, 150, 32), TrainerBehaviour.FixBugs);

            Utils.AddButton("Max Code", new Rect(322, 32, 150, 32), TrainerBehaviour.MaxCode);

            Utils.AddButton("Max Art", new Rect(483, 32, 150, 32), TrainerBehaviour.MaxArt);

            Utils.AddButton("Takeover Company", new Rect(1, 160, 150, 32), TrainerBehaviour.TakeoverCompany);

            Utils.AddButton("Subsidiary Company", new Rect(161, 160, 150, 32), TrainerBehaviour.SubDCompany);

            Utils.AddButton("Bankrupt", new Rect(322, 160, 150, 32), TrainerBehaviour.ForceBankrupt);

            Utils.AddButton("AI Bankrupt All", TrainerBehaviour.AIBankrupt, ref Buttons);

            Utils.AddButton("Days per month", TrainerBehaviour.MonthDays, ref Buttons);

            Utils.AddButton("Clear all loans", TrainerBehaviour.ClearLoans, ref Buttons);

            Utils.AddButton("HR Leaders", TrainerBehaviour.HREmployees, ref Buttons);

            Utils.AddButton("Max Skill of employees", TrainerBehaviour.EmployeesToMax, ref Buttons);

            Utils.AddButton("Remove Products", TrainerBehaviour.RemoveSoft, ref Buttons);

            Utils.AddButton("Reset age of employees", TrainerBehaviour.ResetAgeOfEmployees, ref Buttons);

            Utils.AddButton("Sell products stock", TrainerBehaviour.SellProductStock, ref Buttons);

            Utils.AddButton("Unlock all furniture", TrainerBehaviour.UnlockAll, ref Buttons);

            Utils.AddButton("Unlock all space", TrainerBehaviour.UnlockAllSpace, ref Buttons);



            Utils.AddToggle("Disable Needs", TrainerBehaviour.LockNeeds,
                a => TrainerBehaviour.LockNeeds = !TrainerBehaviour.LockNeeds, ref col1);

            Utils.AddToggle("Disable Stress", TrainerBehaviour.LockStress,
                a => TrainerBehaviour.LockStress = !TrainerBehaviour.LockStress, ref col1);

            Utils.AddToggle("Free Employees", TrainerBehaviour.FreeEmployees,
                a => TrainerBehaviour.FreeEmployees = !TrainerBehaviour.FreeEmployees, ref col1);

            Utils.AddToggle("Free Staff", TrainerBehaviour.FreeStaff,
                a => TrainerBehaviour.FreeStaff = !TrainerBehaviour.FreeStaff, ref col1);

            Utils.AddToggle("Full Efficiency", TrainerBehaviour.LockEffSat,
                a => TrainerBehaviour.LockEffSat = !TrainerBehaviour.LockEffSat, ref col1);

            Utils.AddToggle("Full Satisfaction", TrainerBehaviour.LockSat,
                a => TrainerBehaviour.LockSat = !TrainerBehaviour.LockSat, ref col1);

            Utils.AddToggle("Lock Age of Employees", TrainerBehaviour.LockAge,
                a => TrainerBehaviour.LockAge = !TrainerBehaviour.LockAge, ref col1);

            Utils.AddToggle("No Vacation", TrainerBehaviour.NoVacation,
                a => TrainerBehaviour.NoVacation = !TrainerBehaviour.NoVacation, ref col1);

            Utils.AddToggle("No Sickness", TrainerBehaviour.NoSickness,
                a => TrainerBehaviour.NoSickness = !TrainerBehaviour.NoSickness, ref col1);

            Utils.AddToggle("Ultra Efficiency (Tick Full Eff first)", TrainerBehaviour.MaxOutEff,
                a => TrainerBehaviour.MaxOutEff = !TrainerBehaviour.MaxOutEff, ref col1);

            Utils.AddToggle("Full Environment", TrainerBehaviour.FullEnv,
                a => TrainerBehaviour.FullEnv = !TrainerBehaviour.FullEnv, ref col2);

            Utils.AddToggle("Full Sun Light", TrainerBehaviour.Fullbright,
                a => TrainerBehaviour.Fullbright = !TrainerBehaviour.Fullbright, ref col2);

            Utils.AddToggle("Lock Temperature To 21", TrainerBehaviour.TempLock,
                a => TrainerBehaviour.TempLock = !TrainerBehaviour.TempLock, ref col2);

            Utils.AddToggle("No Maintenance", TrainerBehaviour.NoMaintenance,
                a => TrainerBehaviour.NoMaintenance = !TrainerBehaviour.NoMaintenance, ref col2);

            Utils.AddToggle("Noise Reduction", TrainerBehaviour.NoiseRed,
                a => TrainerBehaviour.NoiseRed = !TrainerBehaviour.NoiseRed, ref col2);

            Utils.AddToggle("Rooms Never Dirty", TrainerBehaviour.CleanRooms,
                a => TrainerBehaviour.CleanRooms = !TrainerBehaviour.CleanRooms, ref col2);

            Utils.AddToggle("Auto Distribution Deals", TrainerBehaviour.dDeal,
                a => TrainerBehaviour.dDeal = !TrainerBehaviour.dDeal, ref col3);

            Utils.AddToggle("Free Print", TrainerBehaviour.FreePrint,
                a => TrainerBehaviour.FreePrint = !TrainerBehaviour.FreePrint, ref col3);

            Utils.AddToggle("Free Water & Electricity", TrainerBehaviour.NoWaterElect,
                a => TrainerBehaviour.NoWaterElect = !TrainerBehaviour.NoWaterElect, ref col3);

            Utils.AddToggle("Increase Bookshelf Skill", TrainerBehaviour.IncBookshelfSkill,
                a => TrainerBehaviour.IncBookshelfSkill = !TrainerBehaviour.IncBookshelfSkill, ref col3);

            Utils.AddToggle("Increase Courier Capacity", TrainerBehaviour.IncCourierCap,
                a => TrainerBehaviour.IncCourierCap = !TrainerBehaviour.IncCourierCap, ref col3);

            Utils.AddToggle("Increase Print Speed", TrainerBehaviour.IncPrintSpeed,
                a => TrainerBehaviour.IncPrintSpeed = !TrainerBehaviour.IncPrintSpeed, ref col3);

            Utils.AddToggle("More Hosting Deals", TrainerBehaviour.MoreHosting,
                a => TrainerBehaviour.MoreHosting = !TrainerBehaviour.MoreHosting, ref col3);

            Utils.AddToggle("Reduce Internet Cost", TrainerBehaviour.RedISPCost,
                a => TrainerBehaviour.RedISPCost = !TrainerBehaviour.RedISPCost, ref col3);


            Utils.DoLoops(Buttons.ToArray(), col1.ToArray(), col2.ToArray(), col3.ToArray());
        }
    }
}