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
                boxText => PropertyHelper.Product_PriceName = boxText);


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


            Utils.AddToggle("Disable Needs", PropertyHelper.GetProperty("NoNeeds"),
                a => PropertyHelper.SetProperty("NoNeeds", !PropertyHelper.GetProperty("NoNeeds")), ref col1);

            Utils.AddToggle("Disable Stress", PropertyHelper.GetProperty("NoStress"),
                a => PropertyHelper.SetProperty("NoStress", !PropertyHelper.GetProperty("NoStress")), ref col1);

            Utils.AddToggle("Free Employees", PropertyHelper.GetProperty("FreeEmployees"),
                a => PropertyHelper.SetProperty("FreeEmployees", !PropertyHelper.GetProperty("FreeEmployees")), ref col1);

            Utils.AddToggle("Free Staff", PropertyHelper.GetProperty("FreeStaff"),
                a => PropertyHelper.SetProperty("FreeStaff", !PropertyHelper.GetProperty("FreeStaff")), ref col1);

            Utils.AddToggle("Full Efficiency", PropertyHelper.GetProperty("FullEfficiency"),
                a => PropertyHelper.SetProperty("FullEfficiency", !PropertyHelper.GetProperty("FullEfficiency")), ref col1);

            Utils.AddToggle("Full Satisfaction", PropertyHelper.GetProperty("FullSatisfaction"),
                a => PropertyHelper.SetProperty("FullSatisfaction", !PropertyHelper.GetProperty("FullSatisfaction")), ref col1);

            Utils.AddToggle("Lock Age of Employees", PropertyHelper.GetProperty("LockAge"),
                a => PropertyHelper.SetProperty("LockAge", !PropertyHelper.GetProperty("LockAge")), ref col1);

            Utils.AddToggle("No Vacation", PropertyHelper.GetProperty("NoVacation"),
                a => PropertyHelper.SetProperty("NoVacation", !PropertyHelper.GetProperty("NoVacation")), ref col1);

            Utils.AddToggle("No Sickness", PropertyHelper.GetProperty("NoSickness"),
                a => PropertyHelper.SetProperty("NoSickness", !PropertyHelper.GetProperty("NoSickness")), ref col1);

            Utils.AddToggle("Ultra Efficiency (Tick Full Eff first)", PropertyHelper.GetProperty("UltraEfficiency"),
                a => PropertyHelper.SetProperty("UltraEfficiency", !PropertyHelper.GetProperty("UltraEfficiency")), ref col1);

            Utils.AddToggle("Full Environment", PropertyHelper.GetProperty("FullEnvironment"),
                a => PropertyHelper.SetProperty("FullEnvironment", !PropertyHelper.GetProperty("FullEnvironment")), ref col2);

            Utils.AddToggle("Full Sun Light", PropertyHelper.GetProperty("FullRoomBrightness"),
                a => PropertyHelper.SetProperty("FullRoomBrightness", !PropertyHelper.GetProperty("FullRoomBrightness")), ref col2);

            Utils.AddToggle("Lock Temperature To 21", PropertyHelper.GetProperty("TemperatureLock"),
                a => PropertyHelper.SetProperty("TemperatureLock", !PropertyHelper.GetProperty("TemperatureLock")), ref col2);

            Utils.AddToggle("No Maintenance", PropertyHelper.GetProperty("NoMaintenance"),
                a => PropertyHelper.SetProperty("NoMaintenance", !PropertyHelper.GetProperty("NoMaintenance")), ref col2);

            Utils.AddToggle("Noise Reduction", PropertyHelper.GetProperty("NoiseReduction"),
                a => PropertyHelper.SetProperty("NoiseReduction", !PropertyHelper.GetProperty("NoiseReduction")), ref col2);

            Utils.AddToggle("Rooms Never Dirty", PropertyHelper.GetProperty("CleanRooms"),
                a => PropertyHelper.SetProperty("CleanRooms", !PropertyHelper.GetProperty("CleanRooms")), ref col2);

            Utils.AddToggle("Auto Distribution Deals", PropertyHelper.GetProperty("AutoDistributionDeals"),
                a => PropertyHelper.SetProperty("AutoDistributionDeals", !PropertyHelper.GetProperty("AutoDistributionDeals")), ref col3);

            Utils.AddToggle("Free Print", PropertyHelper.GetProperty("FreePrint"),
                a => PropertyHelper.SetProperty("FreePrint", !PropertyHelper.GetProperty("FreePrint")), ref col3);

            Utils.AddToggle("Free Water & Electricity", PropertyHelper.GetProperty("NoWaterElectricity"),
                a => PropertyHelper.SetProperty("NoWaterElectricity", !PropertyHelper.GetProperty("NoWaterElectricity")), ref col3);

            Utils.AddToggle("Increase Bookshelf Skill", PropertyHelper.GetProperty("IncreaseBookshelfSkill"),
                a => PropertyHelper.SetProperty("IncreaseBookshelfSkill", !PropertyHelper.GetProperty("IncreaseBookshelfSkill")), ref col3);

            Utils.AddToggle("Increase Courier Capacity", PropertyHelper.GetProperty("IncreaseCourierCapacity"),
                a => PropertyHelper.SetProperty("IncreaseCourierCapacity", !PropertyHelper.GetProperty("IncreaseCourierCapacity")), ref col3);

            Utils.AddToggle("Increase Print Speed", PropertyHelper.GetProperty("IncreasePrintSpeed"),
                a => PropertyHelper.SetProperty("IncreasePrintSpeed", !PropertyHelper.GetProperty("IncreasePrintSpeed")), ref col3);

            Utils.AddToggle("More Hosting Deals", PropertyHelper.GetProperty("MoreHostingDeals"),
                a => PropertyHelper.SetProperty("MoreHostingDeals", !PropertyHelper.GetProperty("MoreHostingDeals")), ref col3);

            Utils.AddToggle("Reduce Internet Cost", PropertyHelper.GetProperty("ReduceISPCost"),
                a => PropertyHelper.SetProperty("ReduceISPCost", !PropertyHelper.GetProperty("ReduceISPCost")), ref col3);

            Utils.AddToggle("Disable Skill Decay", PropertyHelper.GetProperty("DisableSkillDecay"),
                a => PropertyHelper.SetProperty("DisableSkillDecay", !PropertyHelper.GetProperty("DisableSkillDecay")), ref col3);

            Utils.DoLoops(Buttons.ToArray(), col1.ToArray(), col2.ToArray(), col3.ToArray());
        }
    }
}