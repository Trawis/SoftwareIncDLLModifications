using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Trainer
{
    public class TrainerBehaviour : ModBehaviour
    {
        public static bool ModActive = false;
        public static bool LockAge = false;
        public static bool LockStress = false;
        public static bool LockHunger = false;
        public static bool LockNeeds = false;
        public static bool LockEffSat = false;
        public static bool FreeEmployees = false;
        public static bool FreeStaff = false;
        public static bool TempLock = false;
        public static bool NoWaterElect = false;
        public static bool NoiseRed = false;
        public static bool FullEnv = false;
        public static bool CleanRooms = false;
        public static bool Fullbright = false;
        public static bool NoVacation = false;
        public static bool dDeal = false;
        public static bool MoreHosting = false;
        public static bool IncCourierCap = false;
        public static bool RedISPCost = false;
        public static bool IncPrintSpeed = false;
        public static bool FreePrint = false;
        public static bool IncBookshelfSkill = false;
        public static bool NoMaintenance = false;
        public static bool NoSickness = false;
        public static bool MaxOutEff = false;
        
        public bool reward = false;
        public bool pushed = false;

        public static string CompanyText = "";
        public static string price_ProductName = "";
        public static float price_ProductPrice = 10f;

        public Text button = WindowManager.SpawnLabel();
        public bool start = false;
        public bool free = true;
        void Start()
        {
            if (ModActive)
            {
                StartCoroutine(Spremi());
                LockAge = this.LoadSetting<bool>("LockAge", false);
                LockStress = this.LoadSetting<bool>("LockStress", false);
                LockNeeds = this.LoadSetting<bool>("LockNeeds", false);
                FreeEmployees = this.LoadSetting<bool>("FreeEmployees", false);
                LockEffSat = this.LoadSetting<bool>("LockEffSat", false);
                FreeStaff = this.LoadSetting<bool>("FreeStaff", false);
                TempLock = this.LoadSetting<bool>("TempLock", false);
                NoWaterElect = this.LoadSetting<bool>("NoWaterElect", false);
                NoiseRed = this.LoadSetting<bool>("NoiseRed", false);
                FullEnv = this.LoadSetting<bool>("FullEnv", false);
                CleanRooms = this.LoadSetting<bool>("CleanRooms", false);
                Fullbright = this.LoadSetting<bool>("Fullbright", false);
                NoVacation = this.LoadSetting<bool>("NoVacation", false);
                dDeal = this.LoadSetting<bool>("AutoDistDeal", false);
                MoreHosting = this.LoadSetting<bool>("MoreHosting", false);
                IncCourierCap = this.LoadSetting<bool>("IncreaseCourierCapacity", false);
                RedISPCost = this.LoadSetting<bool>("ReduceISPCost", false);
                IncPrintSpeed = this.LoadSetting<bool>("IncPrintSpeed", false);
                FreePrint = this.LoadSetting<bool>("FreePrint", false);
                IncBookshelfSkill = this.LoadSetting<bool>("IncBookshelfSkill", false);
                NoMaintenance = this.LoadSetting<bool>("NoMaintenance", false);
                NoSickness = this.LoadSetting<bool>("NoSickness", false);
                MaxOutEff = this.LoadSetting<bool>("MaxOutEff", false);
            }
        }
        void Update()
        {
            if (start && ModActive && GameSettings.Instance == null && HUD.Instance == null)
                start = false;
            if (ModActive && GameSettings.Instance != null && HUD.Instance != null)
            {
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
                    if (NoWaterElect)
                    {
                        stvar.Water = 0;
                        stvar.Wattage = 0;
                    } 
                }
                foreach (var soba in GameSettings.Instance.sRoomManager.Rooms)
                {
                    if (CleanRooms)
                        soba.ClearDirt();
                    if (TempLock)
                        soba.Temperature = 21f;
                    if (FullEnv)
                        soba.FurnEnvironment = 2;
                    if (Fullbright)
                        soba.IndirectLighting = 4;
                }
                foreach (var item in GameSettings.Instance.sActorManager.Actors)
                {
                    if (LockAge)
                    {
                        item.employee.AgeMonth = Convert.ToInt32(item.employee.Age)*12; //20*12
                        item.UpdateAgeLook();
                    }
                    if (LockStress)
                        item.employee.Stress = 1;
                    if (LockEffSat)
                    {
                        if (item.employee.CurrentRole.ToString() == "Lead")
                        {
                            if (MaxOutEff)
                                item.Effectiveness = 12;
                            else
                                item.Effectiveness = 4;
                        }
                        else
                        {
                            if (MaxOutEff)
                                item.Effectiveness = 10;
                            else
                                item.Effectiveness = 2;
                        }
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
                if (IncCourierCap) CourierAI.MaxBoxes = 108; //54 default
                else CourierAI.MaxBoxes = 54;
                if (RedISPCost) Server.ISPCost = 25f; //50f default
                else Server.ISPCost = 50f;
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
                    else if (hour != 9 && hour != 15 && pushed == true)
                        pushed = false;
                    if (reward == false && hour == 12)
                        Reward();
                    else if (hour != 12 && reward == true)
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
        public static void MaxOutEffBool()
        {
            if (MaxOutEff) MaxOutEff = false;
            else MaxOutEff = true;
        }
        public static void NoSicknessBool()
        {
            if (NoSickness) NoSickness = false;
            else NoSickness = true;
        }
        public static void IncPrintSpeedBool()
        {
            if (IncPrintSpeed) IncPrintSpeed = false;
            else IncPrintSpeed = true;
        }
        public static void FreePrintBool()
        {
            if (FreePrint) FreePrint = false;
            else FreePrint = true;
        }
        public static void IncBookshelfSkillBool()
        {
            if (IncBookshelfSkill) IncBookshelfSkill = false;
            else IncBookshelfSkill = true;
        }
        public static void NoMaintenanceBool()
        {
            if (NoMaintenance) NoMaintenance = false;
            else NoMaintenance = true;
        }
        public static void RedISPCostBool()
        {
            if (RedISPCost) RedISPCost = false;
            else RedISPCost = true;
        }
        public static void IncCourierCapBool()
        {
            if (IncCourierCap) IncCourierCap = false;
            else IncCourierCap = true;
        }
        public static void MoreHostingBool()
        {
            if (MoreHosting) MoreHosting = false;
            else MoreHosting = true;
        }
        public static void dDealBool()
        {
            if (dDeal) dDeal = false;
            else dDeal = true;
        }
        public static void NoVacationBool()
        {
            if (NoVacation) NoVacation = false;
            else NoVacation = true;
        }
        public static void FullbrightBool()
        {
            if (Fullbright) Fullbright = false;
            else Fullbright = true;
        }
        public static void CleanRoomsBool()
        {
            if (CleanRooms) CleanRooms = false;
            else CleanRooms = true;
        }
        public static void FullEnvBool()
        {
            if (FullEnv) FullEnv = false;
            else FullEnv = true;
        }
        public static void NoiseRedBool()
        {
            if (NoiseRed) NoiseRed = false;
            else NoiseRed = true;
        }
        public static void FreeStaffBool()
        {
            if (FreeStaff) FreeStaff = false;
            else FreeStaff = true;
        }
        public static void TempLockBool()
        {
            if (TempLock) TempLock = false;
            else TempLock = true;
        }
        public static void NoWaterElectBool()
        {
            if (NoWaterElect) NoWaterElect = false;
            else NoWaterElect = true;
        }
        public static void LockStressOfEmployees()
        {
            if (LockStress) LockStress = false;
            else LockStress = true;
        }
        public static void DisableNeeds()
        {
            if (LockNeeds) LockNeeds = false;
            else LockNeeds = true;
        }
        public static void LockEmpSal()
        {
            if (FreeEmployees) FreeEmployees = false;
            else FreeEmployees = true;
        }
        public static void FullEffSat()
        {
            if (LockEffSat) LockEffSat = false;
            else LockEffSat = true;
        }
        public static void LockAgeOfEmployees()
        {
            if (LockAge) LockAge = false;
            else LockAge = true;
        }
        
        public void Reward()
        {
            System.Random rnd = new System.Random();
            foreach (Deal x in HUD.Instance.dealWindow.GetActiveDeals())
            {
                if (x.Active && x.ToString() == "ServerDeal")
                    GameSettings.Instance.MyCompany.MakeTransaction(rnd.Next(500, 50000), Company.TransactionCategory.Deals);
            }
            reward = true;
        }
        public void Deals()
        {
            pushed = true;
            List<SoftwareProduct> list = new List<SoftwareProduct>();
            foreach (SoftwareProduct pr in GameSettings.Instance.simulation.GetAllProducts())
            {
                if (pr.Type.ToString() == "CMS" || pr.Type.ToString() == "Office Software" || pr.Type.ToString() == "Operating System" || pr.Type.ToString() == "Game")
                    if (pr.Userbase > 0 && pr.DevCompany.Name != GameSettings.Instance.MyCompany.Name)
                        list.Add(pr);
            }
            System.Random rnd = new System.Random();
            int index = rnd.Next(0, list.Count);
            int year = TimeOfDay.Instance.Year;
            SoftwareProduct prod = GameSettings.Instance.simulation.GetProduct((uint)list[index].SoftwareID, false);
            if (prod.Userbase > 0 && prod.ServerReq > 0 && !prod.ExternalHostingActive)
            {
                ServerDeal deal = new ServerDeal(prod);
                deal.Request = true;
                deal.StillValid(true);
                HUD.Instance.dealWindow.InsertDeal(deal);
            }
        }
        public static void ChangeCompanyName()
        { }
        public static void ForceBankrupt()
        {
            foreach(KeyValuePair<uint, SimulatedCompany> sc in GameSettings.Instance.simulation.Companies)
            {
                if (sc.Value.Name == CompanyText)
                {
                    if (sc.Value.Bankrupt == false)
                    {
                        sc.Value.Bankrupt = true;
                        break;
                    }
                    else
                        sc.Value.Bankrupt = false;
                }
            }
        }
        internal void AIBankrupt()
        {
            foreach (KeyValuePair<uint, SimulatedCompany> sc in GameSettings.Instance.simulation.Companies)
            {
                sc.Value.Bankrupt = true;
            }
        }
        internal void HREmployees()
        {
            if (ModActive && GameSettings.Instance != null && HUD.Instance != null)
            {
                if (!((UnityEngine.Object)SelectorController.Instance != (UnityEngine.Object)null))
                    return;
                foreach (var x in GameSettings.Instance.sActorManager.Actors)
                {
                    if (x.employee.CurrentRole.ToString() == "Lead")
                        x.employee.HR = true;
                }
                HUD.Instance.AddPopupMessage("Trainer: All leaders are now HRed!", "Cogs", "", 0, 0, 0, 0, 1);
            }
        }
        public static void MaxCode()
        {
            List<WorkItem> list = new List<WorkItem>();
            foreach (WorkItem item in GameSettings.Instance.MyCompany.WorkItems)
            {
                if (item.GetType().ToString() == "SoftwareAlpha")
                    list.Add(item);
            }
            foreach (SoftwareAlpha alpha in list)
            {
                if (alpha.Name == price_ProductName && !alpha.InBeta)
                {
                    alpha.CodeProgress = 0.98f;
                    break;
                }
            }
        }
        public static void MaxArt()
        {
            List<WorkItem> list = new List<WorkItem>();
            foreach (WorkItem item in GameSettings.Instance.MyCompany.WorkItems)
            {
                if (item.GetType().ToString() == "SoftwareAlpha")
                    list.Add(item);
            }
            foreach (SoftwareAlpha alpha in list)
            {
                if (alpha.Name == price_ProductName && !alpha.InBeta)
                {
                    alpha.ArtProgress = 0.98f;
                    break;
                }
            }
        }
        public static void FixBugs()
        {
            List<WorkItem> list = new List<WorkItem>();
            foreach (WorkItem item in GameSettings.Instance.MyCompany.WorkItems)
            {
                if (item.GetType().ToString() == "SoftwareAlpha")
                    list.Add(item);
            }
            foreach (SoftwareAlpha alpha in list)
            {
                if (alpha.Name == price_ProductName && alpha.InBeta)
                {
                    alpha.FixedBugs = alpha.MaxBugs;
                    break;
                }
            }
        }
        public static void MaxFollowers()
        {
            List<WorkItem> list = new List<WorkItem>();
            foreach (WorkItem item in GameSettings.Instance.MyCompany.WorkItems)
            {
                if (item.GetType().ToString() == "SoftwareAlpha")
                    list.Add(item);
            }
            foreach (SoftwareAlpha alpha in list)
            {
                if (alpha.Name == price_ProductName && !alpha._paused)
                {
                    alpha.MaxFollowers += 1000000000;
                    alpha.ReEvaluateMaxFollowers();
                    alpha.FollowerChange += 1000000000f;
                    alpha.Followers += 1000000000f;
                    break;
                }
            }
        }
        public static void SetProductPrice()
        {
            foreach (SoftwareProduct product in GameSettings.Instance.MyCompany.Products)
            {
                if (product.Name == price_ProductName)
                {
                    product.Price = price_ProductPrice;
                    HUD.Instance.AddPopupMessage("Trainer: Price for " + product.Name + " has been setted up!", "Cogs", "", 0, 0, 0, 0, 1);
                    break;
                }
            }
        }
        public static void SellProductsStock()
        {
            WindowManager.SpawnDialog("Products stock with no active users have been sold in half a price.", false, DialogWindow.DialogType.Information);
            foreach (SoftwareProduct product in GameSettings.Instance.MyCompany.Products)
            {
                if (product.Userbase == 0)
                {
                    var st = Convert.ToInt32(product.PhysicalCopies) * (Convert.ToInt32(product.Price) / 2);
                    product.PhysicalCopies = 0;
                    GameSettings.Instance.MyCompany.MakeTransaction(st, Company.TransactionCategory.Sales);
                }
            }
        }
        public static void SetProductStock()
        {
            foreach (SoftwareProduct product in GameSettings.Instance.MyCompany.Products)
            {
                if (product.Name == price_ProductName)
                {
                    product.PhysicalCopies = (uint)price_ProductPrice;
                    HUD.Instance.AddPopupMessage("Trainer: Stock for " + product.Name + " has been setted up!", "Cogs", "", 0, 0, 0, 0, 1);
                    break;
                }
            }
        }
        public static void AddActiveUsers()
        {
            foreach (SoftwareProduct product in GameSettings.Instance.MyCompany.Products)
            {
                if (product.Name == price_ProductName)
                {
                    product.Userbase = Convert.ToInt32(price_ProductPrice);
                    HUD.Instance.AddPopupMessage("Trainer: Added " + Convert.ToInt32(price_ProductPrice) + " active users to product " + product.Name, "Cogs", "", 0, 0, 0, 0, 1);
                    break;
                }
            }
        }
        public static void RemoveSoft()
        {
            WindowManager.SpawnDialog("Products that you didn't invent are removed.", false, DialogWindow.DialogType.Information);
            SDateTime time = new SDateTime(1, 70);
            CompanyType type = new CompanyType();
            Dictionary<string, string[]> dict = new Dictionary<string,string[]>();
            SimulatedCompany kompanija = new SimulatedCompany("Trainer Company", time, type, dict, 0f);
            kompanija.CanMakeTransaction(1000000000f);
            foreach (SoftwareProduct prod in GameSettings.Instance.simulation.GetAllProducts())
            {
                if (prod.DevCompany == GameSettings.Instance.MyCompany)
                {
                    if (prod.Inventor != GameSettings.Instance.MyCompany.Name)
                    {
                        prod.Userbase = 0;
                        prod.PhysicalCopies = 0;
                        prod.Marketing = 0;
                        prod.Trade(kompanija);
                    }
                }
            }
        }
        public static void TakeoverCompany()
        {
            if (ModActive && GameSettings.Instance != null && HUD.Instance != null)
            {
                foreach (KeyValuePair<uint, SimulatedCompany> company in GameSettings.Instance.simulation.Companies)
                {
                    SimulatedCompany simulatedCompany = company.Value;
                    if (simulatedCompany.Name == CompanyText)
                    {
                        simulatedCompany.BuyOut(GameSettings.Instance.MyCompany, true);
                        HUD.Instance.AddPopupMessage("Trainer: Company " + simulatedCompany.Name + " has been takovered by you!", "Cogs", "", 0, 0, 0, 0, 1);
                        break;
                    }
                }
            }
        }
        public static void SubDCompany()
        {
            if (ModActive && GameSettings.Instance != null && HUD.Instance != null)
            {
                foreach (KeyValuePair<uint, SimulatedCompany> company in GameSettings.Instance.simulation.Companies)
                {
                    SimulatedCompany simulatedCompany = company.Value;
                    if (simulatedCompany.Name == CompanyText)
                    {
                        simulatedCompany.MakeSubsidiary(GameSettings.Instance.MyCompany);
                        simulatedCompany.IsSubsidiary();
                        HUD.Instance.AddPopupMessage("Trainer: Company " + simulatedCompany.Name + " is now your subsidiary!", "Cogs", "", 0, 0, 0, 0, 1);
                        break;
                    }
                }
            }
        }
        public static void IncreaseMoney()
        {
            if (ModActive && GameSettings.Instance != null && HUD.Instance != null)
            {
                GameSettings.Instance.MyCompany.MakeTransaction(Main.NovacBox.ConvertToInt(Main.NovacBox), Company.TransactionCategory.Deals);
                HUD.Instance.AddPopupMessage("Trainer: Money has been added in category Deals!", "Cogs", "", 0, 0, 0, 0, 1);
            }
        }

        public void ResetAgeOfEmployees()
        {
            if (ModActive && GameSettings.Instance != null && HUD.Instance != null)
            {
                foreach (var item in GameSettings.Instance.sActorManager.Actors)
                {
                    item.employee.AgeMonth = 20 * 12;
                    item.UpdateAgeLook();
                }
                HUD.Instance.AddPopupMessage("Trainer: Age of employees has been reset!", "Cogs", "", 0, 0, 0, 0, 1);
            }
        }

        public override void OnActivate()
        {
            ModActive = true;
            if (ModActive && GameSettings.Instance != null && HUD.Instance != null)
                HUD.Instance.AddPopupMessage("Trainer v2 has been activated!", "Cogs", "", 0, 0, 0, 0, 1);
        }

        internal static void AddRep()
        {
            if (!((UnityEngine.Object)GameSettings.Instance != (UnityEngine.Object)null))
                return;
            GameSettings.Instance.MyCompany.BusinessReputation = 1f;
            SoftwareType random1 = Utilities.GetRandom<SoftwareType>(Enumerable.Where<SoftwareType>((IEnumerable<SoftwareType>)GameSettings.Instance.SoftwareTypes.Values, (Func<SoftwareType, bool>)(x => !x.OneClient)));
            string random2 = Utilities.GetRandom<string>((IEnumerable<string>)random1.Categories.Keys);
            GameSettings.Instance.MyCompany.AddFans(Main.RepBox.ConvertToInt(Main.RepBox), random1.Name, random2);
            HUD.Instance.AddPopupMessage("Trainer: Reputation has been added for SoftwareType: "+random1.Name + ", Category: "+random2, "Cogs", "", 0, 0, 0, 0, 1);
        }

        public override void OnDeactivate()
        {
            ModActive = false;
            if (!ModActive && GameSettings.Instance != null && HUD.Instance != null)
                HUD.Instance.AddPopupMessage("Trainer v2 has been deactivated!", "Cogs", "", 0, 0, 0, 0, 1);
        }

        internal void EmployeesToMax()
        {
            if (ModActive && GameSettings.Instance != null && HUD.Instance != null)
            {
                if (!((UnityEngine.Object)SelectorController.Instance != (UnityEngine.Object)null))
                    return;
                foreach (var x in GameSettings.Instance.sActorManager.Actors)
                {
                    for (int index = 0; index < 5; ++index)
                    {
                        x.employee.ChangeSkill((Employee.EmployeeRole)index, 1f, false);
                        foreach (string specialization in GameSettings.Instance.Specializations)
                        {
                            x.employee.AddToSpecialization(Employee.EmployeeRole.Designer, specialization, 1f, 0, true);
                            x.employee.AddToSpecialization(Employee.EmployeeRole.Artist, specialization, 1f, 0, true);
                            x.employee.AddToSpecialization(Employee.EmployeeRole.Programmer, specialization, 1f, 0, true);
                        }
                    }
                }
                HUD.Instance.AddPopupMessage("Trainer: All employees are now max skilled!", "Cogs", "", 0, 0, 0, 0, 1);
            }
        }

        internal void UnlockAllSpace()
        {
            if (ModActive && GameSettings.Instance != null && HUD.Instance != null)
            {
                if (!((UnityEngine.Object)GameSettings.Instance != (UnityEngine.Object)null))
                    return;
                GameSettings.Instance.BuildableArea = new Rect(9f, 1f, 246f, 254f);
                GameSettings.Instance.ExpandLand(0, 0);
                HUD.Instance.AddPopupMessage("Trainer: All buildable area is now unlocked!", "Cogs", "", 0, 0, 0, 0, 1);
            }
        }

        internal void UnlockAll()
        {
            if (ModActive && GameSettings.Instance != null && HUD.Instance != null)
            {
                Cheats.UnlockFurn = !Cheats.UnlockFurn;
                if (!((UnityEngine.Object)HUD.Instance != (UnityEngine.Object)null))
                    return;
                HUD.Instance.UpdateFurnitureButtons();
                HUD.Instance.AddPopupMessage("Trainer: All furniture has been unlocked!", "Cogs", "", 0, 0, 0, 0, 1);
            }
        }
    }
    public static class Extensions
    {
        public static void SetPrivatePropertyValue<T>(this object obj, string propName, T val)
        {
            Type t = obj.GetType();
            t.InvokeMember(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, obj, new object[] { val });
        }
    }
}
