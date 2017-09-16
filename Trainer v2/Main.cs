using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Utils = Trainer.Utilities;

namespace Trainer
{
    //Your mod should have exactly one class that implements the ModMeta interface
    public class Main : ModMeta
    {
        //This function is used to generate the content in the "Mods" section of the options window
        //The behaviors array contains all behaviours that have been spawned for this mod, one for each implementation

        #region Fields

        public static bool opened;
        public static GUIWindow win;

        static string version = "(v2.12)";

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

            #region Employee Management

            Toggle lockNeeds = WindowManager.SpawnCheckbox();
            lockNeeds.GetComponentInChildren<Text>().text = "Disable Needs";
            lockNeeds.isOn = TrainerBehaviour.LockNeeds;
            lockNeeds.onValueChanged.AddListener(a => TrainerBehaviour.LockNeeds = !TrainerBehaviour.LockNeeds);
            col1.Add(lockNeeds.gameObject);

            //CheckBox for LockEmployeeStress
            Toggle lockStress = WindowManager.SpawnCheckbox();
            lockStress.GetComponentInChildren<Text>().text = "Disable Stress";
            lockStress.isOn = TrainerBehaviour.LockStress;
            lockStress.onValueChanged.AddListener(a => TrainerBehaviour.LockStress = !TrainerBehaviour.LockStress);
            col1.Add(lockStress.gameObject);

            //CheckBox for Free Employees
            Toggle lockEmpSal = WindowManager.SpawnCheckbox();
            lockEmpSal.GetComponentInChildren<Text>().text = "Free Employees";
            lockEmpSal.isOn = TrainerBehaviour.FreeEmployees;
            lockEmpSal.onValueChanged.AddListener(a =>
                TrainerBehaviour.FreeEmployees = !TrainerBehaviour.FreeEmployees);
            col1.Add(lockEmpSal.gameObject);

            //CheckBox for Free Staff
            Toggle toggleFreeStaff = WindowManager.SpawnCheckbox();
            toggleFreeStaff.GetComponentInChildren<Text>().text = "Free Staff";
            toggleFreeStaff.isOn = TrainerBehaviour.FreeStaff;
            toggleFreeStaff.onValueChanged.AddListener(a => TrainerBehaviour.FreeStaff = !TrainerBehaviour.FreeStaff);
            col1.Add(toggleFreeStaff.gameObject);

            //CheckBox for Efficiency
            Toggle lockEffSat = WindowManager.SpawnCheckbox();
            lockEffSat.GetComponentInChildren<Text>().text = "Full Efficiency";
            lockEffSat.isOn = TrainerBehaviour.LockEffSat;
            lockEffSat.onValueChanged.AddListener(a => TrainerBehaviour.LockEffSat = !TrainerBehaviour.LockEffSat);
            col1.Add(lockEffSat.gameObject);

            //CheckBox for Satisfaction
            Toggle lockSat = WindowManager.SpawnCheckbox();
            lockSat.GetComponentInChildren<Text>().text = "Full Satisfaction";
            lockSat.isOn = TrainerBehaviour.LockSat;
            lockSat.onValueChanged.AddListener(a => TrainerBehaviour.LockSat = !TrainerBehaviour.LockSat);
            col1.Add(lockSat.gameObject);

            //CheckBox for EmployeeAge
            Toggle lockAge = WindowManager.SpawnCheckbox();
            lockAge.GetComponentInChildren<Text>().text = "Lock age of employees";
            lockAge.isOn = TrainerBehaviour.LockAge;
            lockAge.onValueChanged.AddListener(a => TrainerBehaviour.LockAge = !TrainerBehaviour.LockAge);
            col1.Add(lockAge.gameObject);

            //CheckBox for No Vacation
            Toggle toggleNoVacation = WindowManager.SpawnCheckbox();
            toggleNoVacation.GetComponentInChildren<Text>().text = "No Vacation";
            toggleNoVacation.isOn = TrainerBehaviour.NoVacation;
            toggleNoVacation.onValueChanged.AddListener(a =>
                TrainerBehaviour.NoVacation = !TrainerBehaviour.NoVacation);
            col1.Add(toggleNoVacation.gameObject);

            Toggle toggleNoSickness = WindowManager.SpawnCheckbox();
            toggleNoSickness.GetComponentInChildren<Text>().text = "No Sickness";
            toggleNoSickness.isOn = TrainerBehaviour.NoSickness;
            toggleNoSickness.onValueChanged.AddListener(a =>
                TrainerBehaviour.NoSickness = !TrainerBehaviour.NoSickness);
            col1.Add(toggleNoSickness.gameObject);

            //CheckBox for Efficiency & Satisfaction
            Toggle toggMaxOutEff = WindowManager.SpawnCheckbox();
            toggMaxOutEff.GetComponentInChildren<Text>().text = "Ultra Efficiency (Tick Full Eff first)";
            toggMaxOutEff.isOn = TrainerBehaviour.MaxOutEff;
            toggMaxOutEff.onValueChanged.AddListener(a => TrainerBehaviour.MaxOutEff = !TrainerBehaviour.MaxOutEff);
            col1.Add(toggMaxOutEff.gameObject);

            #endregion

            #region Room Management

            //CheckBox for Full Environment
            Toggle toggleFullEnv = WindowManager.SpawnCheckbox();
            toggleFullEnv.GetComponentInChildren<Text>().text = "Full Environment";
            toggleFullEnv.isOn = TrainerBehaviour.FullEnv;
            toggleFullEnv.onValueChanged.AddListener(a => TrainerBehaviour.FullEnv = !TrainerBehaviour.FullEnv);
            col2.Add(toggleFullEnv.gameObject);

            //CheckBox for Fullbright
            Toggle toggleFullbright = WindowManager.SpawnCheckbox();
            toggleFullbright.GetComponentInChildren<Text>().text = "Full Sun light";
            toggleFullbright.isOn = TrainerBehaviour.Fullbright;
            toggleFullbright.onValueChanged.AddListener(a =>
                TrainerBehaviour.Fullbright = !TrainerBehaviour.Fullbright);
            col2.Add(toggleFullbright.gameObject);

            //CheckBox for Lock temperature to 21 degree
            Toggle toggleTempLock = WindowManager.SpawnCheckbox();
            toggleTempLock.GetComponentInChildren<Text>().text = "Lock temperature to 21";
            toggleTempLock.isOn = TrainerBehaviour.TempLock;
            toggleTempLock.onValueChanged.AddListener(a => TrainerBehaviour.TempLock = !TrainerBehaviour.TempLock);
            col2.Add(toggleTempLock.gameObject);

            Toggle toggleNoMaintenance = WindowManager.SpawnCheckbox();
            toggleNoMaintenance.GetComponentInChildren<Text>().text = "No Maintenance";
            toggleNoMaintenance.isOn = TrainerBehaviour.NoMaintenance;
            toggleNoMaintenance.onValueChanged.AddListener(a =>
                TrainerBehaviour.NoMaintenance = !TrainerBehaviour.NoMaintenance);
            col2.Add(toggleNoMaintenance.gameObject);

            //CheckBox for Noise Reduction
            Toggle toggleNoiseRed = WindowManager.SpawnCheckbox();
            toggleNoiseRed.GetComponentInChildren<Text>().text = "Noise Reduction";
            toggleNoiseRed.isOn = TrainerBehaviour.NoiseRed;
            toggleNoiseRed.onValueChanged.AddListener(a => TrainerBehaviour.NoiseRed = !TrainerBehaviour.NoiseRed);
            col2.Add(toggleNoiseRed.gameObject);

            //CheckBox for CleanRooms
            Toggle toggleCleanRooms = WindowManager.SpawnCheckbox();
            toggleCleanRooms.GetComponentInChildren<Text>().text = "Rooms are always clean";
            toggleCleanRooms.isOn = TrainerBehaviour.CleanRooms;
            toggleCleanRooms.onValueChanged.AddListener(a => TrainerBehaviour.CleanRooms = !TrainerBehaviour.NoiseRed);
            col2.Add(toggleCleanRooms.gameObject);

            #endregion

            #region Company Management

            Toggle toggleAutoDistDeal = WindowManager.SpawnCheckbox();
            toggleAutoDistDeal.GetComponentInChildren<Text>().text = "Auto Distribution Deals";
            toggleAutoDistDeal.isOn = TrainerBehaviour.dDeal;
            toggleAutoDistDeal.onValueChanged.AddListener(a => TrainerBehaviour.dDeal = !TrainerBehaviour.dDeal);
            col3.Add(toggleAutoDistDeal.gameObject);

            Toggle toggleFreePrint = WindowManager.SpawnCheckbox();
            toggleFreePrint.GetComponentInChildren<Text>().text = "Free Print";
            toggleFreePrint.isOn = TrainerBehaviour.FreePrint;
            toggleFreePrint.onValueChanged.AddListener(a => TrainerBehaviour.FreePrint = !TrainerBehaviour.FreePrint);
            col3.Add(toggleFreePrint.gameObject);

            //CheckBox for Free Water & Electricity
            Toggle toggleNoWaterElect = WindowManager.SpawnCheckbox();
            toggleNoWaterElect.GetComponentInChildren<Text>().text = "Free Water & Electricity";
            toggleNoWaterElect.isOn = TrainerBehaviour.NoWaterElect;
            toggleNoWaterElect.onValueChanged.AddListener(a =>
                TrainerBehaviour.NoWaterElect = !TrainerBehaviour.NoWaterElect);
            col3.Add(toggleNoWaterElect.gameObject);

            Toggle toggleBookshelfSkill = WindowManager.SpawnCheckbox();
            toggleBookshelfSkill.GetComponentInChildren<Text>().text = "Increase Bookshelf Skill";
            toggleBookshelfSkill.isOn = TrainerBehaviour.IncBookshelfSkill;
            toggleBookshelfSkill.onValueChanged.AddListener(a =>
                TrainerBehaviour.IncBookshelfSkill = !TrainerBehaviour.IncBookshelfSkill);
            col3.Add(toggleBookshelfSkill.gameObject);

            Toggle toggleIncCourierCap = WindowManager.SpawnCheckbox();
            toggleIncCourierCap.GetComponentInChildren<Text>().text = "Increase Courier Capacity";
            toggleIncCourierCap.isOn = TrainerBehaviour.IncCourierCap;
            toggleIncCourierCap.onValueChanged.AddListener(a =>
                TrainerBehaviour.IncCourierCap = !TrainerBehaviour.IncCourierCap);
            col3.Add(toggleIncCourierCap.gameObject);

            Toggle togglePrintSpeed = WindowManager.SpawnCheckbox();
            togglePrintSpeed.GetComponentInChildren<Text>().text = "Increase Print Speed";
            togglePrintSpeed.isOn = TrainerBehaviour.IncPrintSpeed;
            togglePrintSpeed.onValueChanged.AddListener(a =>
                TrainerBehaviour.IncPrintSpeed = !TrainerBehaviour.IncPrintSpeed);
            col3.Add(togglePrintSpeed.gameObject);

            Toggle toggleMoreHosting = WindowManager.SpawnCheckbox();
            toggleMoreHosting.GetComponentInChildren<Text>().text = "More Hosting Deals";
            toggleMoreHosting.isOn = TrainerBehaviour.MoreHosting;
            toggleMoreHosting.onValueChanged.AddListener(a =>
                TrainerBehaviour.MoreHosting = !TrainerBehaviour.MoreHosting);
            col3.Add(toggleMoreHosting.gameObject);

            Toggle toggleRedISPCost = WindowManager.SpawnCheckbox();
            toggleRedISPCost.GetComponentInChildren<Text>().text = "Reduce Internet Cost";
            toggleRedISPCost.isOn = TrainerBehaviour.RedISPCost;
            toggleRedISPCost.onValueChanged.AddListener(a =>
                TrainerBehaviour.RedISPCost = !TrainerBehaviour.RedISPCost);
            col3.Add(toggleRedISPCost.gameObject);

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