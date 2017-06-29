using UnityEngine;
using System.Collections.Generic;

namespace Trainer
{
    //Your mod should have exactly one class that implements the ModMeta interface
    public class Main : ModMeta
    {
        //This function is used to generate the content in the "Mods" section of the options window
        //The behaviors array contains all behaviours that have been spawned for this mod, one for each implementation
        private static string novacBox = "";

        public static string NovacBox
        {
            get { return novacBox; }
            set { novacBox = value; }
        }

        private static string repBox = "";

        public static string RepBox
        {
            get { return repBox; }
            set { repBox = value; }
        }
        public static bool opened = false;
        public static GUIWindow pr;
        public static void Tipka()
        {
            var btn = WindowManager.SpawnButton();
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Trainer";
            btn.onClick.AddListener(() => Prozor());
            //float resX = 1005f * (Screen.width / DesignWidth);
            /*
            var width = Screen.width;
            var x = 0;
            switch (width)
            {
                case 1920:
                    x = 1282;
                    break;
                case 1680:
                    x = 1162;
                    break;
                case 1600:
                    x = 1122;
                    break;
                case 1440:
                    x = 1042;
                    break;
                case 1400:
                    x = 1022;
                    break;
                case 1366:
                    x = 1005;
                    break;
                case 1360:
                    x = 1002;
                    break;
                case 1280:
                    x = 963;
                    break;
                case 1152:
                    x = 900;
                    break;
                case 1024:
                    x = 835;
                    break;
                default:
                    x = 1050;
                    break;
            }*/
            WindowManager.AddElementToElement(btn.gameObject, WindowManager.FindElementPath("MainPanel/Holder/FanPanel", null).gameObject, new Rect(164, 0, 70, 32), new Rect(0, 0, 0, 0));
        }
        public static void Prozor()
        {
            if (!opened)
            {
                opened = true;
                pr = WindowManager.SpawnWindow();
                pr.InitialTitle = "Trainer v2 Settings, by Trawis";
                pr.TitleText.text = "Trainer v2 Settings, by Trawis";
                pr.NonLocTitle = "Trainer v2 Settings, by Trawis";
                pr.MinSize.x = 670;
                pr.MinSize.y = 540;
                List<GameObject> btn = new List<GameObject>();
                List<GameObject> col1 = new List<GameObject>();
                List<GameObject> col2 = new List<GameObject>();
                List<GameObject> col3 = new List<GameObject>();
                //TextBox for Money
                var textboxMoney = WindowManager.SpawnInputbox();
                textboxMoney.GetComponentInChildren<UnityEngine.UI.InputField>().text = "100000";
                WindowManager.AddElementToWindow(textboxMoney.gameObject, pr, new Rect(1, 0, 150, 32), new Rect(0, 0, 0, 0));
                //Button for some Money
                var buttonMoney = WindowManager.SpawnButton();
                buttonMoney.GetComponentInChildren<UnityEngine.UI.Text>().text = "Add Money";
                buttonMoney.onClick.AddListener(() =>
                {
                    NovacBox = textboxMoney.text;
                    TrainerBehaviour.IncreaseMoney();
                });
                WindowManager.AddElementToWindow(buttonMoney.gameObject, pr, new Rect(161, 0, 150, 32), new Rect(0, 0, 0, 0));

                //Textbox for Reputation
                var textboxRep = WindowManager.SpawnInputbox();
                textboxRep.GetComponentInChildren<UnityEngine.UI.InputField>().text = "10000";
                WindowManager.AddElementToWindow(textboxRep.gameObject, pr, new Rect(1, 32, 150, 32), new Rect(0, 0, 0, 0));
                //Button for AddRep
                var buttonAddRep = WindowManager.SpawnButton();
                buttonAddRep.GetComponentInChildren<UnityEngine.UI.Text>().text = "Add Reputation";
                buttonAddRep.onClick.AddListener(() =>
                {
                    RepBox = textboxRep.text;
                    TrainerBehaviour.AddRep();
                });
                WindowManager.AddElementToWindow(buttonAddRep.gameObject, pr, new Rect(161, 32, 150, 32), new Rect(0, 0, 0, 0));

                //Change product price for my company
                var inputField3 = WindowManager.SpawnInputbox();
                inputField3.text = "Product Name Here";
                inputField3.onValueChanged.AddListener(boxText => TrainerBehaviour.price_ProductName = boxText);
                WindowManager.AddElementToWindow(inputField3.gameObject, pr, new Rect(1, 64, 150, 32), new Rect(0, 0, 0, 0));

                var inputField4 = WindowManager.SpawnInputbox();
                inputField4.text = "10";
                inputField4.onValueChanged.AddListener(boxText => TrainerBehaviour.price_ProductPrice = boxText.ConvertToFloat(boxText));
                WindowManager.AddElementToWindow(inputField4.gameObject, pr, new Rect(161, 64, 150, 32), new Rect(0, 0, 0, 0));

                var lbl1 = WindowManager.SpawnLabel();
                lbl1.text = "<= This cell is universal for\nPrice, Stock, Active Users";
                WindowManager.AddElementToWindow(lbl1.gameObject, pr, new Rect(322, 64, 400, 32), new Rect(0, 0, 0, 0));

                var button6 = WindowManager.SpawnButton();
                button6.GetComponentInChildren<UnityEngine.UI.Text>().text = "Set Product Price";
                button6.onClick.AddListener(() => TrainerBehaviour.SetProductPrice());
                WindowManager.AddElementToWindow(button6.gameObject, pr, new Rect(1, 96, 150, 32), new Rect(0, 0, 0, 0));

                var button7 = WindowManager.SpawnButton();
                button7.GetComponentInChildren<UnityEngine.UI.Text>().text = "Set Product Stock";
                button7.onClick.AddListener(() => TrainerBehaviour.SetProductStock());
                WindowManager.AddElementToWindow(button7.gameObject, pr, new Rect(161, 96, 150, 32), new Rect(0, 0, 0, 0));

                var button8 = WindowManager.SpawnButton();
                button8.GetComponentInChildren<UnityEngine.UI.Text>().text = "Set Active Users";
                button8.onClick.AddListener(() => TrainerBehaviour.AddActiveUsers());
                WindowManager.AddElementToWindow(button8.gameObject, pr, new Rect(322, 96, 150, 32), new Rect(0, 0, 0, 0));

                var buttonMaxFollowers = WindowManager.SpawnButton();
                buttonMaxFollowers.GetComponentInChildren<UnityEngine.UI.Text>().text = "Max Followers";
                buttonMaxFollowers.onClick.AddListener(() => TrainerBehaviour.MaxFollowers());
                WindowManager.AddElementToWindow(buttonMaxFollowers.gameObject, pr, new Rect(1, 128, 150, 32), new Rect(0, 0, 0, 0));

                var buttonFixBugs = WindowManager.SpawnButton();
                buttonFixBugs.GetComponentInChildren<UnityEngine.UI.Text>().text = "Fix Bugs";
                buttonFixBugs.onClick.AddListener(() => TrainerBehaviour.FixBugs());
                WindowManager.AddElementToWindow(buttonFixBugs.gameObject, pr, new Rect(161, 128, 150, 32), new Rect(0, 0, 0, 0));

                var buttonMaxCode = WindowManager.SpawnButton();
                buttonMaxCode.GetComponentInChildren<UnityEngine.UI.Text>().text = "Max Code";
                buttonMaxCode.onClick.AddListener(() => TrainerBehaviour.MaxCode());
                WindowManager.AddElementToWindow(buttonMaxCode.gameObject, pr, new Rect(322, 128, 150, 32), new Rect(0, 0, 0, 0));

                var buttonMaxArt = WindowManager.SpawnButton();
                buttonMaxArt.GetComponentInChildren<UnityEngine.UI.Text>().text = "Max Art";
                buttonMaxArt.onClick.AddListener(() => TrainerBehaviour.MaxArt());
                WindowManager.AddElementToWindow(buttonMaxArt.gameObject, pr, new Rect(483, 128, 150, 32), new Rect(0, 0, 0, 0));

                //Takeover Company
                var inputField2 = WindowManager.SpawnInputbox();
                inputField2.text = "Company Name Here";
                inputField2.onValueChanged.AddListener(boxText => TrainerBehaviour.CompanyText = boxText);
                WindowManager.AddElementToWindow(inputField2.gameObject, pr, new Rect(1, 160, 150, 32), new Rect(0, 0, 0, 0));

                var button5 = WindowManager.SpawnButton();
                button5.GetComponentInChildren<UnityEngine.UI.Text>().text = "Takeover Company";
                button5.onClick.AddListener(() => TrainerBehaviour.TakeoverCompany());
                WindowManager.AddElementToWindow(button5.gameObject, pr, new Rect(161, 160, 150, 32), new Rect(0, 0, 0, 0));

                var button10 = WindowManager.SpawnButton();
                button10.GetComponentInChildren<UnityEngine.UI.Text>().text = "Subsidiary Company";
                button10.onClick.AddListener(() => TrainerBehaviour.SubDCompany());
                WindowManager.AddElementToWindow(button10.gameObject, pr, new Rect(322, 160, 150, 32), new Rect(0, 0, 0, 0));

                var buttonForceBankrupt = WindowManager.SpawnButton();
                buttonForceBankrupt.GetComponentInChildren<UnityEngine.UI.Text>().text = "Bankrupt";
                buttonForceBankrupt.onClick.AddListener(() => TrainerBehaviour.ForceBankrupt());
                WindowManager.AddElementToWindow(buttonForceBankrupt.gameObject, pr, new Rect(483, 160, 150, 32), new Rect(0, 0, 0, 0));

                TrainerBehaviour tb = new TrainerBehaviour();
                var buttonAIBankrupt = WindowManager.SpawnButton();
                buttonAIBankrupt.GetComponentInChildren<UnityEngine.UI.Text>().text = "AI Bankrupt All";
                buttonAIBankrupt.onClick.AddListener(() => tb.AIBankrupt());
                btn.Add(buttonAIBankrupt.gameObject);

                //Button for Loan Clear
                var buttonClrLoan = WindowManager.SpawnButton();
                buttonClrLoan.GetComponentInChildren<UnityEngine.UI.Text>().text = "Clear all loans";
                buttonClrLoan.onClick.AddListener(() =>
                {
                    tb.ClearLoans();
                });
                btn.Add(buttonClrLoan.gameObject);

                var buttonHREmployees = WindowManager.SpawnButton();
                buttonHREmployees.GetComponentInChildren<UnityEngine.UI.Text>().text = "HR Leaders";
                buttonHREmployees.onClick.AddListener(() => tb.HREmployees());
                btn.Add(buttonHREmployees.gameObject);

                //Button for MaxSkill
                var buttonMaxSkill = WindowManager.SpawnButton();
                buttonMaxSkill.GetComponentInChildren<UnityEngine.UI.Text>().text = "Max Skill of employees";
                buttonMaxSkill.onClick.AddListener(() =>
                {
                    tb.EmployeesToMax();
                });
                btn.Add(buttonMaxSkill.gameObject);

                var button9 = WindowManager.SpawnButton();
                button9.GetComponentInChildren<UnityEngine.UI.Text>().text = "Remove products";
                button9.onClick.AddListener(() => TrainerBehaviour.RemoveSoft());
                btn.Add(button9.gameObject);

                //Button for EmployeeAge
                var buttonEmployeeAge = WindowManager.SpawnButton();
                buttonEmployeeAge.GetComponentInChildren<UnityEngine.UI.Text>().text = "Reset age of employees";
                buttonEmployeeAge.onClick.AddListener(() =>
                {
                    tb.ResetAgeOfEmployees();
                });
                btn.Add(buttonEmployeeAge.gameObject);

                var buttonSellProductsStock = WindowManager.SpawnButton();
                buttonSellProductsStock.GetComponentInChildren<UnityEngine.UI.Text>().text = "Sell products stock";
                buttonSellProductsStock.onClick.AddListener(() => TrainerBehaviour.SellProductsStock());
                btn.Add(buttonSellProductsStock.gameObject);

                //Button for UnlockAll
                var buttonUnlockAll = WindowManager.SpawnButton();
                buttonUnlockAll.GetComponentInChildren<UnityEngine.UI.Text>().text = "Unlock all furniture";
                buttonUnlockAll.onClick.AddListener(() =>
                {
                    tb.UnlockAll();
                });
                btn.Add(buttonUnlockAll.gameObject);

                //Button for MaxLand
                var buttonMaxLand = WindowManager.SpawnButton();
                buttonMaxLand.GetComponentInChildren<UnityEngine.UI.Text>().text = "Unlock all space";
                buttonMaxLand.onClick.AddListener(() =>
                {
                    tb.UnlockAllSpace();
                });
                btn.Add(buttonMaxLand.gameObject);

                /*
                var btnList = WindowManager.SpawnList();
                btnList.ActualItems.Add("1");
                btnList.ActualItems.Add("2");
                btnList.ActualItems.Add("3");
                btn.Add(btnList.gameObject);
                btnList.UpdateElements();
                */

                //Employees Management
                //CheckBox for Disable Needs
                var lockNeeds = WindowManager.SpawnCheckbox();
                lockNeeds.GetComponentInChildren<UnityEngine.UI.Text>().text = "Disable Needs";
                lockNeeds.isOn = TrainerBehaviour.LockNeeds;
                lockNeeds.onValueChanged.AddListener(a => TrainerBehaviour.DisableNeeds());
                col1.Add(lockNeeds.gameObject);

                //CheckBox for LockEmployeeStress
                var lockStress = WindowManager.SpawnCheckbox();
                lockStress.GetComponentInChildren<UnityEngine.UI.Text>().text = "Disable Stress";
                lockStress.isOn = TrainerBehaviour.LockStress;
                lockStress.onValueChanged.AddListener(a => TrainerBehaviour.LockStressOfEmployees());
                col1.Add(lockStress.gameObject);

                //CheckBox for Free Employees
                var lockEmpSal = WindowManager.SpawnCheckbox();
                lockEmpSal.GetComponentInChildren<UnityEngine.UI.Text>().text = "Free Employees";
                lockEmpSal.isOn = TrainerBehaviour.FreeEmployees;
                lockEmpSal.onValueChanged.AddListener(a => TrainerBehaviour.LockEmpSal());
                col1.Add(lockEmpSal.gameObject);

                //CheckBox for Free Staff
                var toggleFreeStaff = WindowManager.SpawnCheckbox();
                toggleFreeStaff.GetComponentInChildren<UnityEngine.UI.Text>().text = "Free Staff";
                toggleFreeStaff.isOn = TrainerBehaviour.FreeStaff;
                toggleFreeStaff.onValueChanged.AddListener(a => TrainerBehaviour.FreeStaffBool());
                col1.Add(toggleFreeStaff.gameObject);

                //CheckBox for Efficiency & Satisfaction
                var lockEffSat = WindowManager.SpawnCheckbox();
                lockEffSat.GetComponentInChildren<UnityEngine.UI.Text>().text = "Full Efficiency & Satisfaction";
                lockEffSat.isOn = TrainerBehaviour.LockEffSat;
                lockEffSat.onValueChanged.AddListener(a => TrainerBehaviour.FullEffSat());
                col1.Add(lockEffSat.gameObject);

                //CheckBox for EmployeeAge
                var lockAge = WindowManager.SpawnCheckbox();
                lockAge.GetComponentInChildren<UnityEngine.UI.Text>().text = "Lock age of employees";
                lockAge.isOn = TrainerBehaviour.LockAge;
                lockAge.onValueChanged.AddListener(a => TrainerBehaviour.LockAgeOfEmployees());
                col1.Add(lockAge.gameObject);

                //CheckBox for No Vacation
                var toggleNoVacation = WindowManager.SpawnCheckbox();
                toggleNoVacation.GetComponentInChildren<UnityEngine.UI.Text>().text = "No Vacation";
                toggleNoVacation.isOn = TrainerBehaviour.NoVacation;
                toggleNoVacation.onValueChanged.AddListener(a => TrainerBehaviour.NoVacationBool());
                col1.Add(toggleNoVacation.gameObject);

                var toggleNoSickness = WindowManager.SpawnCheckbox();
                toggleNoSickness.GetComponentInChildren<UnityEngine.UI.Text>().text = "No Sickness";
                toggleNoSickness.isOn = TrainerBehaviour.NoSickness;
                toggleNoSickness.onValueChanged.AddListener(a => TrainerBehaviour.NoSicknessBool());
                col1.Add(toggleNoSickness.gameObject);

                //CheckBox for Efficiency & Satisfaction
                var toggMaxOutEff = WindowManager.SpawnCheckbox();
                toggMaxOutEff.GetComponentInChildren<UnityEngine.UI.Text>().text = "Ultra Efficiency";
                toggMaxOutEff.isOn = TrainerBehaviour.MaxOutEff;
                toggMaxOutEff.onValueChanged.AddListener(a => TrainerBehaviour.MaxOutEffBool());
                col1.Add(toggMaxOutEff.gameObject);
                //Employees Management END

                //Room Management
                //CheckBox for Full Environment
                var toggleFullEnv = WindowManager.SpawnCheckbox();
                toggleFullEnv.GetComponentInChildren<UnityEngine.UI.Text>().text = "Full Environment";
                toggleFullEnv.isOn = TrainerBehaviour.FullEnv;
                toggleFullEnv.onValueChanged.AddListener(a => TrainerBehaviour.FullEnvBool());
                col2.Add(toggleFullEnv.gameObject);

                //CheckBox for Fullbright
                var toggleFullbright = WindowManager.SpawnCheckbox();
                toggleFullbright.GetComponentInChildren<UnityEngine.UI.Text>().text = "Full Sun light";
                toggleFullbright.isOn = TrainerBehaviour.Fullbright;
                toggleFullbright.onValueChanged.AddListener(a => TrainerBehaviour.FullbrightBool());
                col2.Add(toggleFullbright.gameObject);

                //CheckBox for Lock temperature to 21 degree
                var toggleTempLock = WindowManager.SpawnCheckbox();
                toggleTempLock.GetComponentInChildren<UnityEngine.UI.Text>().text = "Lock temperature to 21";
                toggleTempLock.isOn = TrainerBehaviour.TempLock;
                toggleTempLock.onValueChanged.AddListener(a => TrainerBehaviour.TempLockBool());
                col2.Add(toggleTempLock.gameObject);

                var toggleNoMaintenance = WindowManager.SpawnCheckbox();
                toggleNoMaintenance.GetComponentInChildren<UnityEngine.UI.Text>().text = "No Maintenance";
                toggleNoMaintenance.isOn = TrainerBehaviour.NoMaintenance;
                toggleNoMaintenance.onValueChanged.AddListener(a => TrainerBehaviour.NoMaintenanceBool());
                col2.Add(toggleNoMaintenance.gameObject);

                //CheckBox for Noise Reduction
                var toggleNoiseRed = WindowManager.SpawnCheckbox();
                toggleNoiseRed.GetComponentInChildren<UnityEngine.UI.Text>().text = "Noise Reduction";
                toggleNoiseRed.isOn = TrainerBehaviour.NoiseRed;
                toggleNoiseRed.onValueChanged.AddListener(a => TrainerBehaviour.NoiseRedBool());
                col2.Add(toggleNoiseRed.gameObject);

                //CheckBox for CleanRooms
                var toggleCleanRooms = WindowManager.SpawnCheckbox();
                toggleCleanRooms.GetComponentInChildren<UnityEngine.UI.Text>().text = "Rooms are always clean";
                toggleCleanRooms.isOn = TrainerBehaviour.CleanRooms;
                toggleCleanRooms.onValueChanged.AddListener(a => TrainerBehaviour.CleanRoomsBool());
                col2.Add(toggleCleanRooms.gameObject);
                //Room Management END

                //Company Management
                var toggleAutoDistDeal = WindowManager.SpawnCheckbox();
                toggleAutoDistDeal.GetComponentInChildren<UnityEngine.UI.Text>().text = "Auto Distribution Deals";
                toggleAutoDistDeal.isOn = TrainerBehaviour.dDeal;
                toggleAutoDistDeal.onValueChanged.AddListener(a => TrainerBehaviour.dDealBool());
                col3.Add(toggleAutoDistDeal.gameObject);

                var toggleFreePrint = WindowManager.SpawnCheckbox();
                toggleFreePrint.GetComponentInChildren<UnityEngine.UI.Text>().text = "Free Print";
                toggleFreePrint.isOn = TrainerBehaviour.FreePrint;
                toggleFreePrint.onValueChanged.AddListener(a => TrainerBehaviour.FreePrintBool());
                col3.Add(toggleFreePrint.gameObject);

                //CheckBox for Free Water & Electricity
                var toggleNoWaterElect = WindowManager.SpawnCheckbox();
                toggleNoWaterElect.GetComponentInChildren<UnityEngine.UI.Text>().text = "Free Water & Electricity";
                toggleNoWaterElect.isOn = TrainerBehaviour.NoWaterElect;
                toggleNoWaterElect.onValueChanged.AddListener(a => TrainerBehaviour.NoWaterElectBool());
                col3.Add(toggleNoWaterElect.gameObject);

                var toggleBookshelfSkill = WindowManager.SpawnCheckbox();
                toggleBookshelfSkill.GetComponentInChildren<UnityEngine.UI.Text>().text = "Increase Bookshelf Skill";
                toggleBookshelfSkill.isOn = TrainerBehaviour.IncBookshelfSkill;
                toggleBookshelfSkill.onValueChanged.AddListener(a => TrainerBehaviour.IncBookshelfSkillBool());
                col3.Add(toggleBookshelfSkill.gameObject);

                var toggleIncCourierCap = WindowManager.SpawnCheckbox();
                toggleIncCourierCap.GetComponentInChildren<UnityEngine.UI.Text>().text = "Increase Courier Capacity";
                toggleIncCourierCap.isOn = TrainerBehaviour.IncCourierCap;
                toggleIncCourierCap.onValueChanged.AddListener(a => TrainerBehaviour.IncCourierCapBool());
                col3.Add(toggleIncCourierCap.gameObject);

                var togglePrintSpeed = WindowManager.SpawnCheckbox();
                togglePrintSpeed.GetComponentInChildren<UnityEngine.UI.Text>().text = "Increase Print Speed";
                togglePrintSpeed.isOn = TrainerBehaviour.IncPrintSpeed;
                togglePrintSpeed.onValueChanged.AddListener(a => TrainerBehaviour.IncPrintSpeedBool());
                col3.Add(togglePrintSpeed.gameObject);

                var toggleMoreHosting = WindowManager.SpawnCheckbox();
                toggleMoreHosting.GetComponentInChildren<UnityEngine.UI.Text>().text = "More Hosting Deals";
                toggleMoreHosting.isOn = TrainerBehaviour.MoreHosting;
                toggleMoreHosting.onValueChanged.AddListener(a => TrainerBehaviour.MoreHostingBool());
                col3.Add(toggleMoreHosting.gameObject);

                var toggleRedISPCost = WindowManager.SpawnCheckbox();
                toggleRedISPCost.GetComponentInChildren<UnityEngine.UI.Text>().text = "Reduce Internet Cost";
                toggleRedISPCost.isOn = TrainerBehaviour.RedISPCost;
                toggleRedISPCost.onValueChanged.AddListener(a => TrainerBehaviour.RedISPCostBool());
                col3.Add(toggleRedISPCost.gameObject);
                //Company Management END

                int counter = 7;
                foreach (var item in btn)
                {
                    WindowManager.AddElementToWindow(item, pr, new Rect(1, counter * 32, 150, 32), new Rect(0, 0, 0, 0));
                    counter++;
                }
                counter = 7;
                foreach (var item in col1)
                {
                    WindowManager.AddElementToWindow(item, pr, new Rect(161, counter * 32, 150, 32), new Rect(0, 0, 0, 0));
                    counter++;
                }
                counter = 7;
                foreach (var item in col2)
                {
                    WindowManager.AddElementToWindow(item, pr, new Rect(322, counter * 32, 150, 32), new Rect(0, 0, 0, 0));
                    counter++;
                }
                counter = 7;
                foreach (var item in col3)
                {
                    WindowManager.AddElementToWindow(item, pr, new Rect(483, counter * 32, 150, 32), new Rect(0, 0, 0, 0));
                    counter++;
                }
            }
            else
            {
                pr.Close();
                opened = false;
            }
        }
        
        public void ConstructOptionsScreen(RectTransform parent, ModBehaviour[] behaviours)
        {
            var label = WindowManager.SpawnLabel();
            label.text = "Created by LtPain, edit by Trawis\n\nOptions have been moved to the Main Screen of the game.\nPlease load a game and press 'Trainer' button.";
            WindowManager.AddElementToElement(label.gameObject, parent.gameObject, new Rect(0, 0, 400, 128),
                new Rect(0, 0, 0, 0));
        }

        public string Name
        {
            get { return "Trainer V2"; }
        }
    }
}