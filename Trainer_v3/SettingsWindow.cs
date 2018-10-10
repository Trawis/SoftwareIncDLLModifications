using System.Collections.Generic;
using System.Linq;
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

            //WindowManager.ActiveWindows.Add(Window);

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
                boxText => PropertyHelper.price_ProductName = boxText);


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

            Utils.AddButton("Unlock all furniture", TrainerBehaviour.UnlockFurniture, ref Buttons);

            Utils.AddButton("Unlock all space", TrainerBehaviour.UnlockAllSpace, ref Buttons);

            //Utils.AddButton("Test", TrainerBehaviour.Test, ref Buttons);


            Utils.AddToggle("Disable Needs", PropertyHelper.LockNeeds,
                a => PropertyHelper.LockNeeds = !PropertyHelper.LockNeeds, ref col1);

            Utils.AddToggle("Disable Stress", PropertyHelper.LockStress,
                a => PropertyHelper.LockStress = !PropertyHelper.LockStress, ref col1);

            Utils.AddToggle("Free Employees", PropertyHelper.FreeEmployees,
                a => PropertyHelper.FreeEmployees = !PropertyHelper.FreeEmployees, ref col1);

            Utils.AddToggle("Free Staff", PropertyHelper.FreeStaff,
                a => PropertyHelper.FreeStaff = !PropertyHelper.FreeStaff, ref col1);

            Utils.AddToggle("Full Efficiency", PropertyHelper.LockEffSat,
                a => PropertyHelper.LockEffSat = !PropertyHelper.LockEffSat, ref col1);

            Utils.AddToggle("Full Satisfaction", PropertyHelper.LockSat,
                a => PropertyHelper.LockSat = !PropertyHelper.LockSat, ref col1);

            Utils.AddToggle("Lock Age of Employees", PropertyHelper.LockAge,
                a => PropertyHelper.LockAge = !PropertyHelper.LockAge, ref col1);

            Utils.AddToggle("No Vacation", PropertyHelper.NoVacation,
                a => PropertyHelper.NoVacation = !PropertyHelper.NoVacation, ref col1);

            Utils.AddToggle("No Sickness", PropertyHelper.NoSickness,
                a => PropertyHelper.NoSickness = !PropertyHelper.NoSickness, ref col1);

            Utils.AddToggle("Ultra Efficiency (Tick Full Eff first)", PropertyHelper.MaxOutEff,
                a => PropertyHelper.MaxOutEff = !PropertyHelper.MaxOutEff, ref col1);

            Utils.AddToggle("Full Environment", PropertyHelper.FullEnv,
                a => PropertyHelper.FullEnv = !PropertyHelper.FullEnv, ref col2);

            Utils.AddToggle("Full Sun Light", PropertyHelper.Fullbright,
                a => PropertyHelper.Fullbright = !PropertyHelper.Fullbright, ref col2);

            Utils.AddToggle("Lock Temperature To 21", PropertyHelper.TempLock,
                a => PropertyHelper.TempLock = !PropertyHelper.TempLock, ref col2);

            Utils.AddToggle("No Maintenance", PropertyHelper.NoMaintenance,
                a => PropertyHelper.NoMaintenance = !PropertyHelper.NoMaintenance, ref col2);

            Utils.AddToggle("Noise Reduction", PropertyHelper.NoiseRed,
                a => PropertyHelper.NoiseRed = !PropertyHelper.NoiseRed, ref col2);

            Utils.AddToggle("Rooms Never Dirty", PropertyHelper.CleanRooms,
                a => PropertyHelper.CleanRooms = !PropertyHelper.CleanRooms, ref col2);

            Utils.AddToggle("Auto Distribution Deals", PropertyHelper.dDeal,
                a => PropertyHelper.dDeal = !PropertyHelper.dDeal, ref col3);

            Utils.AddToggle("Free Print", PropertyHelper.FreePrint,
                a => PropertyHelper.FreePrint = !PropertyHelper.FreePrint, ref col3);

            Utils.AddToggle("Free Water & Electricity", PropertyHelper.NoWaterElect,
                a => PropertyHelper.NoWaterElect = !PropertyHelper.NoWaterElect, ref col3);

            Utils.AddToggle("Increase Bookshelf Skill", PropertyHelper.IncBookshelfSkill,
                a => PropertyHelper.IncBookshelfSkill = !PropertyHelper.IncBookshelfSkill, ref col3);

            Utils.AddToggle("Increase Courier Capacity", PropertyHelper.IncCourierCap,
                a => PropertyHelper.IncCourierCap = !PropertyHelper.IncCourierCap, ref col3);

            Utils.AddToggle("Increase Print Speed", PropertyHelper.IncPrintSpeed,
                a => PropertyHelper.IncPrintSpeed = !PropertyHelper.IncPrintSpeed, ref col3);

            Utils.AddToggle("More Hosting Deals", PropertyHelper.MoreHosting,
                a => PropertyHelper.MoreHosting = !PropertyHelper.MoreHosting, ref col3);

            Utils.AddToggle("Reduce Internet Cost", PropertyHelper.RedISPCost,
                a => PropertyHelper.RedISPCost = !PropertyHelper.RedISPCost, ref col3);


            Utils.DoLoops(Buttons.ToArray(), col1.ToArray(), col2.ToArray(), col3.ToArray());
        }
    }
}