using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Trainer
{
    public class TrainerBehaviour : ModBehaviour
    {
        #region Fields

        Random rnd;
        public static bool DoStuff => ModActive && GameSettings.Instance != null && HUD.Instance != null;
        
        public static bool ModActive;
        public static bool LockAge;
        public static bool LockStress;
        public static bool LockHunger = false;
        public static bool LockNeeds;
        public static bool LockEffSat;
        public static bool FreeEmployees;
        public static bool FreeStaff;
        public static bool TempLock;
        public static bool NoWaterElect;
        public static bool NoiseRed;
        public static bool FullEnv;
        public static bool CleanRooms;
        public static bool Fullbright;
        public static bool NoVacation;
        public static bool dDeal;
        public static bool MoreHosting;
        public static bool IncCourierCap;
        public static bool RedISPCost;
        public static bool IncPrintSpeed;
        public static bool FreePrint;
        public static bool IncBookshelfSkill;
        public static bool NoMaintenance;
        public static bool NoSickness;
        public static bool MaxOutEff;
        
        public bool reward;
        public bool pushed;

        public static string CompanyText = "";
        public static string price_ProductName = "";
        public static float price_ProductPrice = 10f;

        public Text button = WindowManager.SpawnLabel();
        public bool start;
        public bool free = true;
        
        #endregion
        
        private void Start()
        {
            rnd = new Random(); // Random is time based, this makes it more random
            
            if (!ModActive) return;
            
            StartCoroutine(Spremi());
            LockAge = LoadSetting("LockAge", false);
            LockStress = LoadSetting("LockStress", false);
            LockNeeds = LoadSetting("LockNeeds", false);
            FreeEmployees = LoadSetting("FreeEmployees", false);
            LockEffSat = LoadSetting("LockEffSat", false);
            FreeStaff = LoadSetting("FreeStaff", false);
            TempLock = LoadSetting("TempLock", false);
            NoWaterElect = LoadSetting("NoWaterElect", false);
            NoiseRed = LoadSetting("NoiseRed", false);
            FullEnv = LoadSetting("FullEnv", false);
            CleanRooms = LoadSetting("CleanRooms", false);
            Fullbright = LoadSetting("Fullbright", false);
            NoVacation = LoadSetting("NoVacation", false);
            dDeal = LoadSetting("AutoDistDeal", false);
            MoreHosting = LoadSetting("MoreHosting", false);
            IncCourierCap = LoadSetting("IncreaseCourierCapacity", false);
            RedISPCost = LoadSetting("ReduceISPCost", false);
            IncPrintSpeed = LoadSetting("IncPrintSpeed", false);
            FreePrint = LoadSetting("FreePrint", false);
            IncBookshelfSkill = LoadSetting("IncBookshelfSkill", false);
            NoMaintenance = LoadSetting("NoMaintenance", false);
            NoSickness = LoadSetting("NoSickness", false);
            MaxOutEff = LoadSetting("MaxOutEff", false);
        }
        
        private void Update()
        {
            if (start && ModActive && GameSettings.Instance == null && HUD.Instance == null)
                start = false;
            
            if (!ModActive || GameSettings.Instance == null || HUD.Instance == null) return;
            
            if (Input.GetKey(KeyCode.F1))
                Main.Prozor();
            
            if (Input.GetKey(KeyCode.F2))
            {
                Main.pr.Close();
                Main.opened = false;
            }
            
            if (start == false)
            {
                Main.Tipka();
                start = true;
            }
            
            if (FreeStaff)
                GameSettings.Instance.StaffSalaryDue = 0f;
            
            foreach (var stvar in GameSettings.Instance.sRoomManager.AllFurniture)
            {
                if (NoiseRed)
                {
                    stvar.ActorNoise = 0f;
                    stvar.EnvironmentNoise = 0f;
                    stvar.FinalNoise = 0f;
                    stvar.Noisiness = 0;
                }
                
                if (!NoWaterElect) continue;
                
                stvar.Water = 0;
                stvar.Wattage = 0;
            }
            
            for (var i = 0; i < GameSettings.Instance.sRoomManager.Rooms.Count; i++)
            {
                var soba = GameSettings.Instance.sRoomManager.Rooms[i];
                
                if (CleanRooms)
                    soba.ClearDirt();
                
                if (TempLock)
                    soba.Temperature = 21f;
                
                if (FullEnv)
                    soba.FurnEnvironment = 2;
                
                if (Fullbright)
                    soba.IndirectLighting = 4;
            }
            
            for (var i = 0; i < GameSettings.Instance.sActorManager.Actors.Count; i++)
            {
                var item = GameSettings.Instance.sActorManager.Actors[i];
                
                if (LockAge)
                {
                    item.employee.AgeMonth = Convert.ToInt32(item.employee.Age) * 12; //20*12
                    item.UpdateAgeLook();
                }
                
                if (LockStress)
                    item.employee.Stress = 1;
                
                if (LockEffSat)
                {
                    if (item.employee.CurrentRole.ToString() == "Lead")
                        item.Effectiveness = MaxOutEff ? 12 : 4;
                    else
                        item.Effectiveness = MaxOutEff ? 10 : 2;
                    
                    item.ChangeSatisfaction(10, 10, Employee.Thought.LoveWork, Employee.Thought.LikeTeamWork, 0);
                }
                
                if (LockNeeds)
                {
                    item.employee.Bladder = 1;
                    item.employee.Hunger = 1;
                    item.employee.Energy = 1;
                    item.employee.Social = 1;
                }
                
                if (FreeEmployees)
                {
                    item.employee.Salary = 0;
                    item.NegotiateSalary = false;
                    item.IgnoreOffSalary = true;
                }
                
                if (NoiseRed)
                    item.Noisiness = 0;
                
                if (NoVacation)
                    item.VacationMonth = SDateTime.NextMonth(24);
            }
            
            LoanWindow.factor = 250000;
            GameSettings.MaxFloor = 75; //10 default
            GameSettings.Instance.scenario.MaxFloor = 75;
            CourierAI.MaxBoxes = IncCourierCap ? 108 : 54;
            Server.ISPCost = RedISPCost ? 25f : 50f;
            
            if (dDeal)
            {
                foreach (var x in GameSettings.Instance.simulation.Companies)
                {
                    var m = x.Value.GetMoneyWithInsurance(true);
                    if (m < 10000000f)
                        x.Value.DistributionDeal = 0.05f;
                    else if(m > 10000000f && m < 100000000f)
                        x.Value.DistributionDeal = 0.10f;
                    else if (m > 100000000f && m < 250000000f)
                        x.Value.DistributionDeal = 0.15f;
                    else if (m > 250000000f && m < 500000000f)
                        x.Value.DistributionDeal = 0.20f;
                    else if (m > 500000000f && m < 1000000000f)
                        x.Value.DistributionDeal = 0.25f;
                    else if (m > 1000000000f)
                        x.Value.DistributionDeal = 0.30f;
                }
            }
            if (MoreHosting)
            {
                int hour = TimeOfDay.Instance.Hour;
                if ((hour == 9 || hour == 15) && pushed == false)
                    Deals();
                else if (hour != 9 && hour != 15 && pushed)
                    pushed = false;
                if (reward == false && hour == 12)
                    Reward();
                else if (hour != 12 && reward)
                    reward = false;
            }
            if (IncPrintSpeed)
            {
                foreach (var x in GameSettings.Instance.ProductPrinters)
                {
                    x.PrintSpeed = 2f;
                }
            }
            if (FreePrint)
            {
                foreach (var x in GameSettings.Instance.ProductPrinters)
                {
                    x.PrintPrice = 0f;
                }
            }
            if (IncBookshelfSkill)
            {
                foreach (Furniture bookshelf in GameSettings.Instance.sRoomManager.AllFurniture)
                {
                    if ("Bookshelf".Equals(bookshelf.Type))
                    {
                        foreach (var x in bookshelf.AuraValues)
                        {
                            bookshelf.AuraValues[1] = 0.75f;
                        }
                    }
                }
            }
            if (NoMaintenance)
            {
                foreach (Furniture furniture in GameSettings.Instance.sRoomManager.AllFurniture)
                {
                    if ("Server".Equals(furniture.Type) || "Computer".Equals(furniture.Type) || "Product Printer".Equals(furniture.Type) || "Ventilation".Equals(furniture.Type) || "Radiator".Equals(furniture.Type) || "Lamp".Equals(furniture.Type) || "Toilet".Equals(furniture.Type))
                        furniture.upg.Quality = 1f;
                }
            }
            if (NoSickness)
                GameSettings.Instance.Insurance.SickRatio = 0f;
        }
        
        IEnumerator<WaitForSeconds> Spremi()
        {
            while (true)
            {
                yield return new WaitForSeconds(10.0f);
                SaveSetting("LockStress", LockStress.ToString());
                SaveSetting("NoVacation", NoVacation.ToString());
                SaveSetting("Fullbright", Fullbright.ToString());
                SaveSetting("CleanRooms", CleanRooms.ToString());
                SaveSetting("FullEnv", FullEnv.ToString());
                SaveSetting("NoiseRed", NoiseRed.ToString());
                SaveSetting("FreeStaff", FreeStaff.ToString());
                SaveSetting("TempLock", TempLock.ToString());
                SaveSetting("NoWaterElect", NoWaterElect.ToString());
                SaveSetting("LockNeeds", LockNeeds.ToString());
                SaveSetting("LockEffSat", LockEffSat.ToString());
                SaveSetting("FreeEmployees", FreeEmployees.ToString());
                SaveSetting("LockAge", LockAge.ToString());
                SaveSetting("AutoDistDeal", dDeal.ToString());
                SaveSetting("MoreHosting", MoreHosting.ToString());
                SaveSetting("IncreaseCourierCapacity", IncCourierCap.ToString());
                SaveSetting("ReduceISPCost", RedISPCost.ToString());
                SaveSetting("IncPrintSpeed", IncPrintSpeed.ToString());
                SaveSetting("FreePrint", FreePrint.ToString());
                SaveSetting("IncBookshelfSkill", IncBookshelfSkill.ToString());
                SaveSetting("NoMaintenance", NoMaintenance.ToString());
                SaveSetting("NoSickness", NoSickness.ToString());
                SaveSetting("MaxOutEff", MaxOutEff.ToString());
            }
        }
        
        internal void ClearLoans()
        {
            GameSettings.Instance.Loans.Clear();
            HUD.Instance.AddPopupMessage("Trainer: All loans are cleared!", "Cogs", "", 0, 0, 0, 0, 1);
        }
        
        public static void MaxOutEffBool() => MaxOutEff = !MaxOutEff;

        public static void NoSicknessBool() => NoSickness = !NoSickness;

        public static void IncPrintSpeedBool() => IncPrintSpeed = !IncPrintSpeed;

        public static void FreePrintBool() => FreePrint = !FreePrint;

        public static void IncBookshelfSkillBool() => IncBookshelfSkill = !IncBookshelfSkill;

        public static void NoMaintenanceBool() => NoMaintenance = !NoMaintenance;

        public static void RedISPCostBool() => RedISPCost = !RedISPCost;

        public static void IncCourierCapBool() => IncCourierCap = !IncCourierCap;

        public static void MoreHostingBool() => MoreHosting = !MoreHosting;

        public static void dDealBool() => dDeal = !dDeal;

        public static void NoVacationBool() => NoVacation = !NoVacation;

        public static void FullbrightBool() => Fullbright = !Fullbright;

        public static void CleanRoomsBool() => CleanRooms = !CleanRooms;

        public static void FullEnvBool() => FullEnv = !FullEnv;

        public static void NoiseRedBool() => NoiseRed = !NoiseRed;

        public static void FreeStaffBool() => FreeStaff = !FreeStaff;

        public static void TempLockBool() => TempLock = !TempLock;

        public static void NoWaterElectBool() => NoWaterElect = !NoWaterElect;

        public static void LockStressOfEmployees() => LockStress = !LockStress;

        public static void DisableNeeds() => LockNeeds = !LockNeeds;

        public static void LockEmpSal() => FreeEmployees = !FreeEmployees;

        public static void FullEffSat() => LockEffSat = !LockEffSat;

        public static void LockAgeOfEmployees() => LockAge = !LockAge;

        public void Reward()
        {
            for (var i = 0; i < HUD.Instance.dealWindow.GetActiveDeals().Count; i++)
            {
                Deal x = HUD.Instance.dealWindow.GetActiveDeals()[i];
                if (x.Active && x.ToString() == "ServerDeal")
                    GameSettings.Instance.MyCompany.MakeTransaction(rnd.Next(500, 50000),
                        Company.TransactionCategory.Deals);
            }
            reward = true;
        }
        
        public void Deals()
        {
            pushed = true;
            HashSet<SoftwareProduct> Products = new HashSet<SoftwareProduct>();
            
            foreach (SoftwareProduct pr in GameSettings.Instance.simulation.GetAllProducts())
            {
                if (pr.Type.ToString() == "CMS" || pr.Type.ToString() == "Office Software" || pr.Type.ToString() == "Operating System" || pr.Type.ToString() == "Game")
                    if (pr.Userbase > 0 && pr.DevCompany.Name != GameSettings.Instance.MyCompany.Name)
                        Products.Add(pr);
            }
            
            int index = rnd.Next(0, Products.Count);
            int year = TimeOfDay.Instance.Year;
            SoftwareProduct prod = GameSettings.Instance.simulation.GetProduct(Products.ElementAt(index).SoftwareID, false);
            
            if (prod.Userbase > 0 && prod.ServerReq > 0 && !prod.ExternalHostingActive)
            {
                ServerDeal deal = new ServerDeal(prod) {Request = true};
                deal.StillValid(true);
                HUD.Instance.dealWindow.InsertDeal(deal);
            }
        }
        
        public static void ChangeCompanyName() { }
        
        public static void ForceBankrupt()
        {
            foreach(KeyValuePair<uint, SimulatedCompany> sc in GameSettings.Instance.simulation.Companies)
            {
                if (sc.Value.Name != CompanyText) continue;
                
                if (sc.Value.Bankrupt == false)
                {
                    sc.Value.Bankrupt = true;
                    break;
                }
                
                sc.Value.Bankrupt = false;
            }
        }
        
        internal void AIBankrupt()
        {
            foreach (KeyValuePair<uint, SimulatedCompany> sc in GameSettings.Instance.simulation.Companies)
                sc.Value.Bankrupt = true;
        }
        
        internal void HREmployees()
        {
            if (!ModActive || GameSettings.Instance == null || HUD.Instance == null) return;
            
            if (!(SelectorController.Instance != null))
                return;

            for (var i = 0; i < GameSettings.Instance.sActorManager.Actors.Count; i++)
            {
                var x = GameSettings.Instance.sActorManager.Actors[i];
                
                if (x.employee.CurrentRole.ToString() == "Lead")
                    x.employee.HR = true;
            }
            
            HUD.Instance.AddPopupMessage("Trainer: All leaders are now HRed!", "Cogs", "", 0, 0, 0, 0, 1);
        }
        
        public static void MaxCode()
        {
            HashSet<WorkItem> WorkItems = GameSettings.Instance.MyCompany.WorkItems
                .Where(item => item.GetType().ToString() == "SoftwareAlpha").ToHashSet();
            
            foreach (SoftwareAlpha alpha in WorkItems)
            {
                if (alpha.Name != price_ProductName || alpha.InBeta) continue;
                
                alpha.CodeProgress = 0.98f;
                break;
            }
        }
        
        public static void MaxArt()
        {
            HashSet<WorkItem> WorkItems = GameSettings.Instance.MyCompany.WorkItems
                .Where(item => item.GetType().ToString() == "SoftwareAlpha").ToHashSet();
            
            foreach (SoftwareAlpha alpha in list)
            {
                if (alpha.Name != price_ProductName || alpha.InBeta) continue;
                
                alpha.ArtProgress = 0.98f;
                break;
            }
        }
        
        public static void FixBugs()
        {
            HashSet<WorkItem> WorkItems = GameSettings.Instance.MyCompany.WorkItems.Where(item => item.GetType().ToString() == "SoftwareAlpha").ToHashSet();
            foreach (SoftwareAlpha alpha in WorkItems)
            {
                if (alpha.Name != price_ProductName || !alpha.InBeta) continue;
                
                alpha.FixedBugs = alpha.MaxBugs;
                break;
            }
        }
        
        public static void MaxFollowers()
        {
            HashSet<WorkItem> WorkItems = GameSettings.Instance.MyCompany.WorkItems.Where(item => item.GetType().ToString() == "SoftwareAlpha").ToHashSet();
            foreach (SoftwareAlpha alpha in WorkItems)
            {
                if (alpha.Name != price_ProductName || alpha._paused) continue;
                
                alpha.MaxFollowers += 1000000000;
                alpha.ReEvaluateMaxFollowers();
                alpha.FollowerChange += 1000000000f;
                alpha.Followers += 1000000000f;
                break;
            }
        }
        
        public static void SetProductPrice()
        {
            SoftwareProduct Product = GameSettings.Instance.MyCompany.Products.FirstOrDefault(product => product.Name == price_ProductName);
            Product.Price = price_ProductPrice;
            HUD.Instance.AddPopupMessage("Trainer: Price for " + Product.Name + " has been setted up!", "Cogs", "", 0, 0, 0, 0, 1);
        }
        
        public static void SellProductsStock()
        {
            WindowManager.SpawnDialog("Products stock with no active users have been sold in half a price.", false, DialogWindow.DialogType.Information);

            SoftwareProduct[] Products =
                GameSettings.Instance.MyCompany.Products.Where(product => product.Userbase == 0).ToArray();

            for (int i = 0; i < Products.Length; i++)
            {
                SoftwareProduct product = Products[i];
                var st = Convert.ToInt32(product.PhysicalCopies) * (Convert.ToInt32(product.Price) / 2);
                product.PhysicalCopies = 0;
                GameSettings.Instance.MyCompany.MakeTransaction(st, Company.TransactionCategory.Sales);
            }
        }
        
        public static void SetProductStock()
        {
            SoftwareProduct Product =
                GameSettings.Instance.MyCompany.Products.FirstOrDefault(product => product.Name == price_ProductName);
            Product.PhysicalCopies = (uint) price_ProductPrice;
            HUD.Instance.AddPopupMessage("Trainer: Stock for " + Product.Name + " has been setted up!", "Cogs",
                "", 0, 0, 0, 0, 1);
        }
        
        public static void AddActiveUsers()
        {
            SoftwareProduct Product =
                GameSettings.Instance.MyCompany.Products.FirstOrDefault(product => product.Name == price_ProductName);
            Product.Userbase = Convert.ToInt32(price_ProductPrice);
            HUD.Instance.AddPopupMessage("Trainer: Added " + Convert.ToInt32(price_ProductPrice) + " active users to product " + Product.Name, "Cogs", "", 0, 0, 0, 0, 1);
        }
        
        public static void RemoveSoft()
        {
            WindowManager.SpawnDialog("Products that you didn't invent are removed.", false, DialogWindow.DialogType.Information);
            SDateTime time = new SDateTime(1, 70);
            CompanyType type = new CompanyType();
            Dictionary<string, string[]> dict = new Dictionary<string,string[]>();
            SimulatedCompany kompanija = new SimulatedCompany("Trainer Company", time, type, dict, 0f);
            kompanija.CanMakeTransaction(1000000000f);

            SoftwareProduct[] Products = GameSettings.Instance.simulation.GetAllProducts().Where(product =>
                product.DevCompany == GameSettings.Instance.MyCompany &&
                product.Inventor != GameSettings.Instance.MyCompany.Name).ToArray();

            if (Products.Length == 0) return;
            
            for (int i = 0; i < Products.Length; i++)
            {
                SoftwareProduct Product = Products[i];
                Product.Userbase = 0;
                Product.PhysicalCopies = 0;
                Product.Marketing = 0;
                Product.Trade(kompanija);
            }
        }
        
        public static void TakeoverCompany()
        {
            if (!DoStuff) return;

            SimulatedCompany Company = GameSettings.Instance.simulation.Companies
                .FirstOrDefault(company => company.Value.Name == CompanyText).Value;
            
            Company.BuyOut(GameSettings.Instance.MyCompany, true);
            HUD.Instance.AddPopupMessage("Trainer: Company " + Company.Name + " has been takovered by you!", "Cogs", "", 0, 0, 0, 0, 1);
        }
        
        public static void SubDCompany()
        {
            if (!DoStuff) return;

            SimulatedCompany Company =
                GameSettings.Instance.simulation.Companies.FirstOrDefault(company => company.Value.Name == CompanyText).Value;
            
            Company.MakeSubsidiary(GameSettings.Instance.MyCompany);
            Company.IsSubsidiary();
            HUD.Instance.AddPopupMessage("Trainer: Company " + Company.Name + " is now your subsidiary!", "Cogs", "", 0, 0, 0, 0, 1);
        }
        
        public static void IncreaseMoney()
        {
            if (!DoStuff) return;
            
            GameSettings.Instance.MyCompany.MakeTransaction(Main.NovacBox.ConvertToInt(Main.NovacBox), Company.TransactionCategory.Deals);
            HUD.Instance.AddPopupMessage("Trainer: Money has been added in category Deals!", "Cogs", "", 0, 0, 0, 0, 1);
        }

        public void ResetAgeOfEmployees()
        {
            if (!DoStuff) return;

            for (var i = 0; i < GameSettings.Instance.sActorManager.Actors.Count; i++)
            {
                var item = GameSettings.Instance.sActorManager.Actors[i];
                item.employee.AgeMonth = 240;
                item.UpdateAgeLook();
            }
            
            HUD.Instance.AddPopupMessage("Trainer: Age of employees has been reset!", "Cogs", "", 0, 0, 0, 0, 1);
        }

        public override void OnActivate()
        {
            ModActive = true;
            
            if (DoStuff)
                HUD.Instance.AddPopupMessage("Trainer v2 has been activated!", "Cogs", "", 0, 0, 0, 0, 1);
        }

        internal static void AddRep()
        {
            if (!(GameSettings.Instance != null))
                return;
            
            GameSettings.Instance.MyCompany.BusinessReputation = 1f;
            SoftwareType random1 = GameSettings.Instance.SoftwareTypes.Values.Where(x => !x.OneClient).GetRandom();
            string random2 = random1.Categories.Keys.GetRandom();
            GameSettings.Instance.MyCompany.AddFans(Main.RepBox.ConvertToInt(Main.RepBox), random1.Name, random2);
            HUD.Instance.AddPopupMessage("Trainer: Reputation has been added for SoftwareType: "+random1.Name + ", Category: "+random2, "Cogs", "", 0, 0, 0, 0, 1);
        }

        public override void OnDeactivate()
        {
            ModActive = false;
            
            if (!DoStuff)
                HUD.Instance.AddPopupMessage("Trainer v2 has been deactivated!", "Cogs", "", 0, 0, 0, 0, 1);
        }

        internal void EmployeesToMax()
        {
            if (!DoStuff || SelectorController.Instance == null) return;

            for (var index1 = 0; index1 < GameSettings.Instance.sActorManager.Actors.Count; index1++)
            {
                var x = GameSettings.Instance.sActorManager.Actors[index1];
                for (int index = 0; index < 5; index++)
                {
                    x.employee.ChangeSkill((Employee.EmployeeRole) index, 1f, false);
                    for (var i = 0; i < GameSettings.Instance.Specializations.Length; i++)
                    {
                        string specialization = GameSettings.Instance.Specializations[i];
                        x.employee.AddToSpecialization(Employee.EmployeeRole.Designer, specialization, 1f, 0, true);
                        x.employee.AddToSpecialization(Employee.EmployeeRole.Artist, specialization, 1f, 0, true);
                        x.employee.AddToSpecialization(Employee.EmployeeRole.Programmer, specialization, 1f, 0, true);
                    }
                }
            }

            HUD.Instance.AddPopupMessage("Trainer: All employees are now max skilled!", "Cogs", "", 0, 0, 0, 0, 1);
        }

        internal void UnlockAllSpace()
        {
            if (!DoStuff) return;
            
            GameSettings.Instance.BuildableArea = new Rect(9f, 1f, 246f, 254f);
            GameSettings.Instance.ExpandLand(0, 0);
            HUD.Instance.AddPopupMessage("Trainer: All buildable area is now unlocked!", "Cogs", "", 0, 0, 0, 0, 1);
        }

        internal void UnlockAll()
        {
            if (!DoStuff) return;
            
            Cheats.UnlockFurn = !Cheats.UnlockFurn;
            HUD.Instance.UpdateFurnitureButtons();
            HUD.Instance.AddPopupMessage("Trainer: All furniture has been unlocked!", "Cogs", "", 0, 0, 0, 0, 1);
        }
    }
}
