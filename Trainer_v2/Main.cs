using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Utils = Trainer_v2.Utilities;

namespace Trainer_v2
{
    //Your mod should have exactly one class that implements the ModMeta interface
    public class Main : ModMeta
    {
        //This function is used to generate the content in the "Mods" section of the options window
        //The behaviors array contains all behaviours that have been spawned for this mod, one for each implementation

        #region Fields

        public static bool opened;
        public static GUIWindow win;

        static string version = "(v2.13)";

        #endregion

        public static void Button()
        {
            Button btn = WindowManager.SpawnButton();
            btn.GetComponentInChildren<Text>().text = $"Trainer {version}";
            btn.onClick.AddListener(Window);

            WindowManager.AddElementToElement(btn.gameObject,
                WindowManager.FindElementPath("MainPanel/Holder/FanPanel").gameObject, new Rect(164, 0, 100, 32),
                new Rect(0, 0, 0, 0));
        }

        public static void Window()
        {
            if (opened)
            {
                win.Close();
                opened = false;
                return;
            }

            opened = !opened;

            #region Initialization

            win = WindowManager.SpawnWindow();
            win.InitialTitle = "Trainer Settings, by Trawis " + version;
            win.TitleText.text = "TrainerSettings, by Trawis " + version;
            win.NonLocTitle = "TrainerSettings, by Trawis " + version;
            win.MinSize.x = 670;
            win.MinSize.y = 580;

            List<GameObject> Buttons = new List<GameObject>();
            List<GameObject> col1 = new List<GameObject>();
            List<GameObject> col2 = new List<GameObject>();
            List<GameObject> col3 = new List<GameObject>();

            #endregion

            Utils.AddInputBox("Product Name Here", new Rect(1, 96, 150, 32),
                boxText => TrainerBehaviour.price_ProductName = boxText);

            #region Buttons

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

            #endregion
            
            #region Toggles

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

            #endregion

            Utils.DoLoops(Buttons.ToArray(), col1.ToArray(), col2.ToArray(), col3.ToArray());
        }

        public void ConstructOptionsScreen(RectTransform parent, ModBehaviour[] behaviours)
        {
            Text label = WindowManager.SpawnLabel();
            label.text = "Created by LtPain, edit by Trawis\n\n" +
                         "Options have been moved to the Main Screen of the game.\n" +
                         "Please load a game and press 'Trainer' button.";
            
            WindowManager.AddElementToElement(label.gameObject, parent.gameObject, new Rect(0, 0, 400, 128),
                new Rect(0, 0, 0, 0));
        }

        public string Name => "Trainer v2";
    }
}