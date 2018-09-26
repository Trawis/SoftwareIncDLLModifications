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
                boxText => Trainer.price_ProductName = boxText);


            Utils.AddButton("Add Money", new Rect(1, 0, 150, 32), Trainer.IncreaseMoney);

            Utils.AddButton("Add Reputation", new Rect(161, 0, 150, 32), Trainer.AddRep);

            Utils.AddButton("Set Product Price", new Rect(161, 96, 150, 32), Trainer.SetProductPrice);

            Utils.AddButton("Set Product Stock", new Rect(322, 96, 150, 32), Trainer.SetProductStock);

            Utils.AddButton("Set Active Users", new Rect(483, 96, 150, 32), Trainer.AddActiveUsers);

            Utils.AddButton("Max Followers", new Rect(1, 32, 150, 32), Trainer.MaxFollowers);

            Utils.AddButton("Fix Bugs", new Rect(161, 32, 150, 32), Trainer.FixBugs);

            Utils.AddButton("Max Code", new Rect(322, 32, 150, 32), Trainer.MaxCode);

            Utils.AddButton("Max Art", new Rect(483, 32, 150, 32), Trainer.MaxArt);

            Utils.AddButton("Takeover Company", new Rect(1, 160, 150, 32), Trainer.TakeoverCompany);

            Utils.AddButton("Subsidiary Company", new Rect(161, 160, 150, 32), Trainer.SubDCompany);

            Utils.AddButton("Bankrupt", new Rect(322, 160, 150, 32), Trainer.ForceBankrupt);

            Utils.AddButton("AI Bankrupt All", Trainer.AIBankrupt, ref Buttons);

            Utils.AddButton("Days per month", Trainer.MonthDays, ref Buttons);

            Utils.AddButton("Clear all loans", Trainer.ClearLoans, ref Buttons);

            Utils.AddButton("HR Leaders", Trainer.HREmployees, ref Buttons);

            Utils.AddButton("Max Skill of employees", Trainer.EmployeesToMax, ref Buttons);

            Utils.AddButton("Remove Products", Trainer.RemoveSoft, ref Buttons);

            Utils.AddButton("Reset age of employees", Trainer.ResetAgeOfEmployees, ref Buttons);

            Utils.AddButton("Sell products stock", Trainer.SellProductStock, ref Buttons);

            Utils.AddButton("Unlock all furniture", Trainer.UnlockFurniture, ref Buttons);

            Utils.AddButton("Unlock all space", Trainer.UnlockAllSpace, ref Buttons);

            //Utils.AddButton("Test", TrainerBehaviour.Test, ref Buttons);


            Utils.AddToggle("Disable Needs", ModHelper.LockNeeds,
                a => ModHelper.LockNeeds = !ModHelper.LockNeeds, ref col1);

            Utils.AddToggle("Disable Stress", ModHelper.LockStress,
                a => ModHelper.LockStress = !ModHelper.LockStress, ref col1);

            Utils.AddToggle("Free Employees", ModHelper.FreeEmployees,
                a => ModHelper.FreeEmployees = !ModHelper.FreeEmployees, ref col1);

            Utils.AddToggle("Free Staff", ModHelper.FreeStaff,
                a => ModHelper.FreeStaff = !ModHelper.FreeStaff, ref col1);

            Utils.AddToggle("Full Efficiency", ModHelper.LockEffSat,
                a => ModHelper.LockEffSat = !ModHelper.LockEffSat, ref col1);

            Utils.AddToggle("Full Satisfaction", ModHelper.LockSat,
                a => ModHelper.LockSat = !ModHelper.LockSat, ref col1);

            Utils.AddToggle("Lock Age of Employees", ModHelper.LockAge,
                a => ModHelper.LockAge = !ModHelper.LockAge, ref col1);

            Utils.AddToggle("No Vacation", ModHelper.NoVacation,
                a => ModHelper.NoVacation = !ModHelper.NoVacation, ref col1);

            Utils.AddToggle("No Sickness", ModHelper.NoSickness,
                a => ModHelper.NoSickness = !ModHelper.NoSickness, ref col1);

            Utils.AddToggle("Ultra Efficiency (Tick Full Eff first)", ModHelper.MaxOutEff,
                a => ModHelper.MaxOutEff = !ModHelper.MaxOutEff, ref col1);

            Utils.AddToggle("Full Environment", ModHelper.FullEnv,
                a => ModHelper.FullEnv = !ModHelper.FullEnv, ref col2);

            Utils.AddToggle("Full Sun Light", ModHelper.Fullbright,
                a => ModHelper.Fullbright = !ModHelper.Fullbright, ref col2);

            Utils.AddToggle("Lock Temperature To 21", ModHelper.TempLock,
                a => ModHelper.TempLock = !ModHelper.TempLock, ref col2);

            Utils.AddToggle("No Maintenance", ModHelper.NoMaintenance,
                a => ModHelper.NoMaintenance = !ModHelper.NoMaintenance, ref col2);

            Utils.AddToggle("Noise Reduction", ModHelper.NoiseRed,
                a => ModHelper.NoiseRed = !ModHelper.NoiseRed, ref col2);

            Utils.AddToggle("Rooms Never Dirty", ModHelper.CleanRooms,
                a => ModHelper.CleanRooms = !ModHelper.CleanRooms, ref col2);

            Utils.AddToggle("Auto Distribution Deals", ModHelper.dDeal,
                a => ModHelper.dDeal = !ModHelper.dDeal, ref col3);

            Utils.AddToggle("Free Print", ModHelper.FreePrint,
                a => ModHelper.FreePrint = !ModHelper.FreePrint, ref col3);

            Utils.AddToggle("Free Water & Electricity", ModHelper.NoWaterElect,
                a => ModHelper.NoWaterElect = !ModHelper.NoWaterElect, ref col3);

            Utils.AddToggle("Increase Bookshelf Skill", ModHelper.IncBookshelfSkill,
                a => ModHelper.IncBookshelfSkill = !ModHelper.IncBookshelfSkill, ref col3);

            Utils.AddToggle("Increase Courier Capacity", ModHelper.IncCourierCap,
                a => ModHelper.IncCourierCap = !ModHelper.IncCourierCap, ref col3);

            Utils.AddToggle("Increase Print Speed", ModHelper.IncPrintSpeed,
                a => ModHelper.IncPrintSpeed = !ModHelper.IncPrintSpeed, ref col3);

            Utils.AddToggle("More Hosting Deals", ModHelper.MoreHosting,
                a => ModHelper.MoreHosting = !ModHelper.MoreHosting, ref col3);

            Utils.AddToggle("Reduce Internet Cost", ModHelper.RedISPCost,
                a => ModHelper.RedISPCost = !ModHelper.RedISPCost, ref col3);


            Utils.DoLoops(Buttons.ToArray(), col1.ToArray(), col2.ToArray(), col3.ToArray());
        }
    }
}