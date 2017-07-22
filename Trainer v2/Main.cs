using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using Utils = Trainer.Utilities;

namespace Trainer
{
    //Your mod should have exactly one class that implements the ModMeta interface
    public class Main : ModMeta
    {
        //This function is used to generate the content in the "Mods" section of the options window
        //The behaviors array contains all behaviours that have been spawned for this mod, one for each implementation
        
        #region Fields

        public static bool opened = false;
        public static GUIWindow pr;
        
        #endregion
        
        public static void Tipka()
        {
            var btn = WindowManager.SpawnButton();
            btn.GetComponentInChildren<Text>().text = "Trainer";
            btn.onClick.AddListener(Prozor);
            WindowManager.AddElementToElement(btn.gameObject, WindowManager.FindElementPath("MainPanel/Holder/FanPanel", null).gameObject, new Rect(164, 0, 70, 32), new Rect(0, 0, 0, 0));
        }
        
        public static void Prozor()
        {
            if (!opened)
            {
                opened = true;
                
                #region Initialization
                
                pr = WindowManager.SpawnWindow();
                pr.InitialTitle = "Trainer v2 Settings, by Trawis";
                pr.TitleText.text = "Trainer v2 Settings, by Trawis";
                pr.NonLocTitle = "Trainer v2 Settings, by Trawis";
                pr.MinSize.x = 670;
                pr.MinSize.y = 550;
                
                List<GameObject> btn = new List<GameObject>(), col1 = new List<GameObject>(), col2 = new List<GameObject>(),
                    col3 = new List<GameObject>();
                
                #endregion
                
                #region Money
                
                Utils.AddInputBox("10000", new Rect(1, 0, 150, 32), input => TrainerBehaviour.novacBox = input);
                
                Utils.AddButton("Add Money", new Rect(161, 0, 150, 32), TrainerBehaviour.IncreaseMoney);
                
                #endregion

                #region Reputation
                
                Utils.AddInputBox("10000", new Rect(1, 32, 150, 32), input => TrainerBehaviour.repBox = input);
                
                Utils.AddButton("Add Reputation", new Rect(161, 32, 150, 32), TrainerBehaviour.AddRep);
                
                #endregion
                
                #region Universal Integer Mods

                //Change product price for my company
                
                Utils.AddInputBox("Product Name Here", new Rect(1, 64, 150, 32), boxText => TrainerBehaviour.price_ProductName = boxText);

                Utils.AddInputBox("10", new Rect(161, 64, 150, 32),
                    boxText => TrainerBehaviour.price_ProductPrice = boxText.ConvertToFloat(boxText));
                    
                Utils.AddLabel("<= This cell is universal for\nPrice, Stock, Active Users", new Rect(322, 64, 400, 32));
                
                Utils.AddButton("Set Product Price", new Rect(1, 96, 150, 32), TrainerBehaviour.SetProductPrice);
                
                Utils.AddButton("Set Product Stock", new Rect(161, 96, 150, 32), TrainerBehaviour.SetProductStock);
                
                Utils.AddButton("Set Active Users", new Rect(322, 96, 150, 32), TrainerBehaviour.AddActiveUsers);
                
                #endregion
                
                #region Maximum

                Utils.AddButton("Max Followers", new Rect(1, 128, 150, 32), TrainerBehaviour.MaxFollowers);
                
                Utils.AddButton("Fix Bugs", new Rect(161, 128, 150, 32), TrainerBehaviour.FixBugs);
                
                Utils.AddButton("Max Code", new Rect(322, 128, 150, 32), TrainerBehaviour.MaxCode);
                
                Utils.AddButton("Max Art", new Rect(483, 128, 150, 32), TrainerBehaviour.MaxArt);
                
                #endregion

                #region Companies
                
                Utils.AddInputBox("Company Name Here", new Rect(1, 160, 150, 32), input => TrainerBehaviour.CompanyText = input);
                
                Utils.AddButton("Takeover Company", new Rect(161, 160, 150, 32), TrainerBehaviour.TakeoverCompany);
                
                Utils.AddButton("Subsidiary Company", new Rect(322, 160, 150, 32), TrainerBehaviour.SubDCompany);
                
                Utils.AddButton("Bankrupt", new Rect(483, 160, 150, 32), TrainerBehaviour.ForceBankrupt);
                
                Utils.AddButton("Bankrupt All", TrainerBehaviour.AIBankrupt, ref btn);
                
                #endregion
                
                #region Buttons
                
                Utils.AddButton("Clear all loans", TrainerBehaviour.ClearLoans, ref btn);
                
                Utils.AddButton("HR Leaders", TrainerBehaviour.HREmployees, ref btn);
                
                Utils.AddButton("Max Employees' Skills", TrainerBehaviour.EmployeesToMax, ref btn);

                Utils.AddButton("Remove products", TrainerBehaviour.RemoveSoft, ref btn);
                
                Utils.AddButton("Reset Employees' Age", TrainerBehaviour.ResetAgeOfEmployees, ref btn);
                
                Utils.AddButton("Sell products stock", TrainerBehaviour.SellProductsStock, ref btn);
                
                Utils.AddButton("Unlock all furniture", TrainerBehaviour.UnlockAll, ref btn);
                
                Utils.AddButton("Unlock all space", TrainerBehaviour.UnlockAllSpace, ref btn);
                
                #endregion
                
                #region Employee Management

                Utils.AddCheckBox("Disable Needs", ref TrainerBehaviour.LockNeeds, ref col1);

                Utils.AddCheckBox("Disable Stress", ref TrainerBehaviour.LockStress, ref col1);

                Utils.AddCheckBox("Free Employees", ref TrainerBehaviour.FreeEmployees, ref col1);
                
                Utils.AddCheckBox("Free Staff", ref TrainerBehaviour.FreeStaff, ref col1);
                
                Utils.AddCheckBox("Full Efficiency & Satisfaction", ref TrainerBehaviour.LockEffSat, ref col1);
                
                Utils.AddCheckBox("Lock employees' age", ref TrainerBehaviour.LockAge, ref col1);

                Utils.AddCheckBox("No Vacation", ref TrainerBehaviour.NoVacation, ref col1);
                
                Utils.AddCheckBox("No Sickness", ref TrainerBehaviour.NoVacation, ref col1);
                
                Utils.AddCheckBox("Ultra Efficiency", ref TrainerBehaviour.MaxOutEff, ref col1);
                
                #endregion
                
                #region Room Management
                
                Utils.AddCheckBox("Full Environment", ref TrainerBehaviour.FullEnv, ref col2);
                
                Utils.AddCheckBox("Full Sunlight", ref TrainerBehaviour.Fullbright, ref col2);
                
                Utils.AddCheckBox("Lock temperature to 21", ref TrainerBehaviour.TempLock, ref col2);
                
                Utils.AddCheckBox("No Maintenance", ref TrainerBehaviour.NoMaintenance, ref col2);
                
                Utils.AddCheckBox("Noise Reduction", ref TrainerBehaviour.NoiseRed, ref col2);
                
                Utils.AddCheckBox("Rooms never dirty", ref TrainerBehaviour.CleanRooms, ref col2);
                
                #endregion
                
                #region Company Management
                
                Utils.AddCheckBox("Auto Distribution Deals", ref TrainerBehaviour.dDeal, ref col3);
                
                Utils.AddCheckBox("Free Print", ref TrainerBehaviour.FreePrint, ref col3);
                
                Utils.AddCheckBox("Free Water & Electricity", ref TrainerBehaviour.NoWaterElect, ref col3);
                
                Utils.AddCheckBox("Increase Bookshelf Skill", ref TrainerBehaviour.IncBookshelfSkill, ref col3);
                
                Utils.AddCheckBox("Increase Courier Capacity", ref TrainerBehaviour.IncCourierCap, ref col3);
                
                Utils.AddCheckBox("Increase Print Speed", ref TrainerBehaviour.IncPrintSpeed, ref col3);

                Utils.AddCheckBox("More Hosting Deals", ref TrainerBehaviour.MoreHosting, ref col3);
                
                Utils.AddCheckBox("Reduce Internet Cost", ref TrainerBehaviour.RedISPCost, ref col3);
                
                #endregion
                
                Utils.DoLoops(ref btn, ref col1, ref col2, ref col3);
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

        public string Name => "Trainer V2";
    }
}