using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Random = System.Random;

namespace Trainer_v2
{
    public class TrainerBehaviour : ModBehaviour
    {
        #region Fields

        public static Random rnd;
        public static bool DoStuff => ModActive && GameSettings.Instance != null && HUD.Instance != null;

        public static bool ModActive;
        public static bool LockAge;
        public static bool LockStress;
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
        public static bool LockSat;

        public bool reward;
        public bool pushed;

        public static string price_ProductName;

        public bool start;

        #endregion

        private void Start()
        {
            rnd = new Random(); // Random is time based, this makes it more random

            if (!ModActive)
            {
                return;
            }

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
            LockSat = LoadSetting("LockSat", false);
        }

        private void Update()
        {
            if (start && ModActive && GameSettings.Instance == null && HUD.Instance == null)
            {
                start = false;
            }

            if (!ModActive || GameSettings.Instance == null || HUD.Instance == null)
            {
                return;
            }

            if (Input.GetKey(KeyCode.F1))
            {
                Main.Window();
            }

            if (Input.GetKey(KeyCode.F2))
            {
                Main.win.Close();
                Main.opened = false;
            }

            if (start == false)
            {
                Main.Button();
                Main.opened = false;
                start = true;
            }

            if (FreeStaff)
            {
                GameSettings.Instance.StaffSalaryDue = 0f;
            }

            foreach (Furniture item in GameSettings.Instance.sRoomManager.AllFurniture)
            {
                if (NoiseRed)
                {
                    item.ActorNoise = 0f;
                    item.EnvironmentNoise = 0f;
                    item.FinalNoise = 0f;
                    item.Noisiness = 0;
                }

                if (NoWaterElect)
                {
                    item.Water = 0;
                    item.Wattage = 0;
                }
            }

            for (int i = 0; i < GameSettings.Instance.sRoomManager.Rooms.Count; i++)
            {
                Room room = GameSettings.Instance.sRoomManager.Rooms[i];

                if (CleanRooms)
                {
                    room.ClearDirt();
                }

                if (TempLock)
                {
                    room.Temperature = 21.4f;
                }

                if (FullEnv)
                {
                    room.FurnEnvironment = 4;
                }

                if (Fullbright)
                {
                    room.IndirectLighting = 8;
                }
            }

            for (int i = 0; i < GameSettings.Instance.sActorManager.Actors.Count; i++)
            {
                Actor act = GameSettings.Instance.sActorManager.Actors[i];

                if (LockAge)
                {
                    act.employee.AgeMonth = Convert.ToInt32(act.employee.Age) * 12; //20*12
                    act.UpdateAgeLook();
                }

                if (LockStress)
                {
                    act.employee.Stress = 1;
                }

                if (LockEffSat)
                {
                    if (act.employee.CurrentRole.ToString() == "Lead")
                    {
                        act.Effectiveness = MaxOutEff ? 20 : 4;
                    }
                    else
                    {
                        act.Effectiveness = MaxOutEff ? 10 : 2;
                    }
                }
                if (LockSat)
                {
                    act.ChangeSatisfaction(10, 10, Employee.Thought.LoveWork, Employee.Thought.LikeTeamWork, 0);
                }

                if (LockNeeds)
                {
                    act.employee.Bladder = 1;
                    act.employee.Hunger = 1;
                    act.employee.Energy = 1;
                    act.employee.Social = 1;
                }

                if (FreeEmployees)
                {
                    act.employee.Salary = 0;
                    act.NegotiateSalary = false;
                    act.IgnoreOffSalary = true;
                }

                if (NoiseRed)
                {
                    act.Noisiness = 0;
                }

                if (NoVacation)
                {
                    act.VacationMonth = SDateTime.NextMonth(24);
                }
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
                    float m = x.Value.GetMoneyWithInsurance(true);

                    if (m < 10000000f)
                    {
                        x.Value.DistributionDeal = 0.05f;
                    }
                    else if (m > 10000000f && m < 100000000f)
                    {
                        x.Value.DistributionDeal = 0.10f;
                    }
                    else if (m > 100000000f && m < 250000000f)
                    {
                        x.Value.DistributionDeal = 0.15f;
                    }
                    else if (m > 250000000f && m < 500000000f)
                    {
                        x.Value.DistributionDeal = 0.20f;
                    }
                    else if (m > 500000000f && m < 1000000000f)
                    {
                        x.Value.DistributionDeal = 0.25f;
                    }
                    else if (m > 1000000000f)
                    {
                        x.Value.DistributionDeal = 0.30f;
                    }
                }
            }

            if (MoreHosting)
            {
                int hour = TimeOfDay.Instance.Hour;

                if ((hour == 9 || hour == 15) && pushed == false)
                {
                    Deals();
                }
                else if (hour != 9 && hour != 15 && pushed)
                {
                    pushed = false;
                }
                if (reward == false && hour == 12)
                {
                    Reward();
                }
                else if (hour != 12 && reward)
                {
                    reward = false;
                }
            }

            if (IncPrintSpeed)
            {
                for (int i = 0; i < GameSettings.Instance.ProductPrinters.Count; i++)
                {
                    GameSettings.Instance.ProductPrinters[i].PrintSpeed = 2f;
                }
            }

            //add printspeed and printprice when it's disabled (else) TODO
            if (FreePrint)
            {
                for (int i = 0; i < GameSettings.Instance.ProductPrinters.Count; i++)
                {
                    GameSettings.Instance.ProductPrinters[i].PrintPrice = 0f;
                }
            }

            if (IncBookshelfSkill)
            {
                foreach (Furniture bookshelf in GameSettings.Instance.sRoomManager.AllFurniture)
                {
                    if ("Bookshelf".Equals(bookshelf.Type))
                    {
                        foreach (float x in bookshelf.AuraValues)
                        {
                            bookshelf.AuraValues[1] = 0.75f;
                        }
                    }
                }
            }

            //else 0.25 TODO
            if (NoMaintenance)
            {
                foreach (Furniture furniture in GameSettings.Instance.sRoomManager.AllFurniture)
                {
                    if ("Server".Equals(furniture.Type) || "Computer".Equals(furniture.Type) ||
                        "Product Printer".Equals(furniture.Type) || "Ventilation".Equals(furniture.Type) ||
                        "Radiator".Equals(furniture.Type) || "Lamp".Equals(furniture.Type) ||
                        "Toilet".Equals(furniture.Type))
                    {
                        furniture.upg.Quality = 1f;
                    }
                }
            }

            if (NoSickness)
            {
                GameSettings.Instance.Insurance.SickRatio = 0f;
            }
        }

        IEnumerator<WaitForSeconds> Spremi()
        {
            while (true)
            {
                yield return new WaitForSeconds(15.0f);

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
                SaveSetting("LockSat", LockSat.ToString());
            }
        }

        public static void ClearLoans()
        {
            GameSettings.Instance.Loans.Clear();
            HUD.Instance.AddPopupMessage("Trainer: All loans are cleared!", "Cogs", "", 0, 0, 0, 0);
        }

        public void Reward()
        {
            Deal[] Deals = HUD.Instance.dealWindow.GetActiveDeals().Where(deal => deal.ToString() == "ServerDeal")
                .ToArray();

            if (Deals.Length == 0)
            {
                return;
            }

            for (int i = 0; i < Deals.Length; i++)
            {
                GameSettings.Instance.MyCompany.MakeTransaction(rnd.Next(500, 50000),
                       Company.TransactionCategory.Deals);
            }

            reward = true;
        }

        public void Deals()
        {
            pushed = true;

            SoftwareProduct[] Products = GameSettings.Instance.simulation.GetAllProducts().Where(pr =>
                (pr.Type.ToString() == "CMS" || pr.Type.ToString() == "Office Software" ||
                 pr.Type.ToString() == "Operating System" || pr.Type.ToString() == "Game") && pr.Userbase > 0 &&
                pr.DevCompany.Name != GameSettings.Instance.MyCompany.Name && pr.ServerReq > 0 &&
                !pr.ExternalHostingActive).ToArray();

            int index = rnd.Next(0, Products.Length);

            SoftwareProduct prod =
                GameSettings.Instance.simulation.GetProduct(Products.ElementAt(index).SoftwareID, false);


            ServerDeal deal = new ServerDeal(Products[index]) { Request = true };
            deal.StillValid(true);
            HUD.Instance.dealWindow.InsertDeal(deal);
        }

        /* public static void ChangeCompanyName(string Name) => typeof(Company).GetField("Name",
            BindingFlags.Instance | BindingFlags.Public).SetValue(GameSettings.Instance.MyCompany, Name); */

        public static void ChangeEducationDays()
        {

        }

        public static void AIBankrupt()
        {
            SimulatedCompany[] Companies = GameSettings.Instance.simulation.Companies.Values.ToArray();

            for (int i = 0; i < Companies.Length; i++)
            {
                Companies[i].Bankrupt = true;
            }
        }

        public static void HREmployees()
        {
            if (!DoStuff || SelectorController.Instance == null)
            {
                return;
            }

            Actor[] Actors = GameSettings.Instance.sActorManager.Actors
                .Where(actor => actor.employee.CurrentRole == Employee.EmployeeRole.Lead).ToArray();

            if (Actors.Length == 0)
            {
                return;
            }

            for (var i = 0; i < Actors.Length; i++)
            {
                Actors[i].employee.HR = true;
            }

            HUD.Instance.AddPopupMessage("Trainer: All leaders are now HRed!", "Cogs", "", 0, 0, 0, 0, 1);
        }

        public static void SellProductStock()
        {
            WindowManager.SpawnDialog("Products stock with no active users have been sold in half a price.",
                false, DialogWindow.DialogType.Information);

            SoftwareProduct[] Products =
                GameSettings.Instance.MyCompany.Products.Where(product => product.Userbase == 0).ToArray();

            if (Products.Length == 0)
            {
                return;
            }

            for (int i = 0; i < Products.Length; i++)
            {
                SoftwareProduct product = Products[i];
                int st = Convert.ToInt32(product.PhysicalCopies) * (Convert.ToInt32(product.Price) / 2);

                product.PhysicalCopies = 0;
                GameSettings.Instance.MyCompany.MakeTransaction(st, Company.TransactionCategory.Sales);
            }
        }

        public static void RemoveSoft()
        {
            WindowManager.SpawnDialog("Products that you didn't invent are removed.", false, DialogWindow.DialogType.Information);
            SDateTime time = new SDateTime(1, 70);
            CompanyType type = new CompanyType();
            Dictionary<string, string[]> dict = new Dictionary<string, string[]>();
            SimulatedCompany simComp = new SimulatedCompany("Trainer Company", time, type, dict, 0f);
            simComp.CanMakeTransaction(1000000000f);

            SoftwareProduct[] Products = GameSettings.Instance.simulation.GetAllProducts().Where(product =>
                product.DevCompany == GameSettings.Instance.MyCompany &&
                product.Inventor != GameSettings.Instance.MyCompany.Name).ToArray();

            if (Products.Length == 0)
            {
                return;
            }

            for (int i = 0; i < Products.Length; i++)
            {
                SoftwareProduct Product = Products[i];

                Product.Userbase = 0;
                Product.PhysicalCopies = 0;
                Product.Marketing = 0;
                Product.Trade(simComp);
            }
        }

        public static void ResetAgeOfEmployees()
        {
            for (int i = 0; i < GameSettings.Instance.sActorManager.Actors.Count; i++)
            {
                Actor item = GameSettings.Instance.sActorManager.Actors[i];

                item.employee.AgeMonth = 240;
                item.UpdateAgeLook();
            }

            HUD.Instance.AddPopupMessage("Trainer: Age of employees has been reset!", "Cogs", "", 0, 0, 0, 0);
        }

        public static void EmployeesToMax()
        {
            if (!DoStuff || SelectorController.Instance == null) return;

            for (int index1 = 0; index1 < GameSettings.Instance.sActorManager.Actors.Count; index1++)
            {
                Actor x = GameSettings.Instance.sActorManager.Actors[index1];
                for (int index = 0; index < 5; index++)
                {
                    x.employee.ChangeSkill((Employee.EmployeeRole)index, 1f, false);
                    for (int i = 0; i < GameSettings.Instance.Specializations.Length; i++)
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

        public static void UnlockAllSpace()
        {
            if (!DoStuff)
            {
                return;
            }

            GameSettings.Instance.BuildableArea = new Rect(9f, 1f, 246f, 254f);
            GameSettings.Instance.ExpandLand(0, 0);
            HUD.Instance.AddPopupMessage("Trainer: All buildable area is now unlocked!", "Cogs", "", 0, 0, 0, 0);
        }

        public static void UnlockAll()
        {
            if (!DoStuff)
            {
                return;
            }

            Cheats.UnlockFurn = !Cheats.UnlockFurn;
            HUD.Instance.UpdateFurnitureButtons();
            HUD.Instance.AddPopupMessage("Trainer: All furniture has been unlocked!", "Cogs", "", 0, 0, 0, 0);
        }

        #region MonthDays

        public static void MonthDaysAction(string input)
        {
            if (!int.TryParse(input, out int Input))
            {
                return;
            }

            GameSettings.DaysPerMonth = Input;
            WindowManager.SpawnDialog("You have changed days per month. Please restart the game.", false, DialogWindow.DialogType.Warning);
        }

        public static void MonthDays() =>
            WindowManager.SpawnInputDialog("How many days per month do you want?", "Days per month", "2", MonthDaysAction);

        #endregion

        #region Max Code

        public static void MaxCodeAction(string input)
        {
            WorkItem WorkItem = GameSettings.Instance.MyCompany.WorkItems
                .Where(item => item.GetType() == typeof(SoftwareAlpha)).FirstOrDefault(item =>
                    (item as SoftwareAlpha).Name == input && !(item as SoftwareAlpha).InBeta);

            if (WorkItem == null)
            {
                return;
            }

            ((SoftwareAlpha)WorkItem).CodeProgress = 0.98f;
        }

        public static void MaxCode() => WindowManager.SpawnInputDialog("Type product name:", "Max Code", "", MaxCodeAction);

        #endregion

        #region Max Art

        public static void MaxArtAction(string input)
        {
            WorkItem WorkItem = GameSettings.Instance.MyCompany.WorkItems
                .Where(item => item.GetType() == typeof(SoftwareAlpha)).FirstOrDefault(item =>
                    (item as SoftwareAlpha).Name == input && !(item as SoftwareAlpha).InBeta);

            if (WorkItem == null)
            {
                return;
            }

            ((SoftwareAlpha)WorkItem).ArtProgress = 0.98f;
        }

        public static void MaxArt() => WindowManager.SpawnInputDialog("Type product name:", "Max Art", "", MaxArtAction);

        #endregion

        #region Fix Bugs

        public static void FixBugsAction(string input)
        {
            WorkItem WorkItem = GameSettings.Instance.MyCompany.WorkItems
                .Where(item => item.GetType() == typeof(SoftwareAlpha)).FirstOrDefault(item =>
                    (item as SoftwareAlpha).Name == input && (item as SoftwareAlpha).InBeta); //! removed

            if (WorkItem == null)
            {
                return;
            }

            ((SoftwareAlpha)WorkItem).FixedBugs = ((SoftwareAlpha)WorkItem).MaxBugs;
        }

        public static void FixBugs() => WindowManager.SpawnInputDialog("Type product name:", "Fix Bugs", "", FixBugsAction);

        #endregion

        #region Max Followers

        public static void MaxFollowersAction(string input)
        {
            WorkItem WorkItem = GameSettings.Instance.MyCompany.WorkItems
                .Where(item => item.GetType() == typeof(SoftwareAlpha)).FirstOrDefault(item =>
                    (item as SoftwareAlpha).Name == input && !(item as SoftwareAlpha).Paused);

            if (WorkItem == null)
            {
                return;
            }

            SoftwareAlpha alpha = (SoftwareAlpha)WorkItem;

            alpha.MaxFollowers += 1000000000;
            alpha.ReEvaluateMaxFollowers();

            alpha.FollowerChange += 1000000000f;
            alpha.Followers += 1000000000f;
        }

        public static void MaxFollowers() =>
            WindowManager.SpawnInputDialog("Type product name:", "Max Followers", "", MaxFollowersAction);

        #endregion

        #region Set Product Price

        public static void SetProductPriceAction(string input)
        {
            SoftwareProduct Product =
                GameSettings.Instance.MyCompany.Products.FirstOrDefault(product => product.Name == price_ProductName);

            if (Product == null)
            {
                return;
            }

            Product.Price = input.ConvertToFloatDef(50f);
            HUD.Instance.AddPopupMessage($"Trainer: Price for {Product.Name} has been setted up!", "Cogs", "", 0, 0, 0, 0);
        }

        public static void SetProductPrice() =>
            WindowManager.SpawnInputDialog("Type product price:", "Product price", "50", SetProductPriceAction);

        #endregion

        #region Set Product Stock

        public static void SetProductStockAction(string input)
        {
            SoftwareProduct Product =
                GameSettings.Instance.MyCompany.Products.FirstOrDefault(product => product.Name == price_ProductName);

            if (Product == null)
            {
                return;
            }

            Product.PhysicalCopies = (uint)input.ConvertToIntDef(100000);
            HUD.Instance.AddPopupMessage($"Trainer: Stock for {Product.Name} has been setted up!", "Cogs", "", 0, 0, 0, 0);
        }

        public static void SetProductStock() =>
            WindowManager.SpawnInputDialog("Type product stock:", "Product stock", "100000", SetProductStockAction);

        #endregion

        #region Add Active Users

        public static void AddActiveUsersAction(string input)
        {
            SoftwareProduct Product =
                GameSettings.Instance.MyCompany.Products.FirstOrDefault(product => product.Name == price_ProductName);

            if (Product == null)
            {
                return;
            }

            Product.Userbase = input.ConvertToIntDef(100000);
            HUD.Instance.AddPopupMessage(
                $"Trainer: Active users for {Product.Name} has been setted up!", "Cogs", "", 0, 0, 0, 0);
        }

        public static void AddActiveUsers() => WindowManager.SpawnInputDialog("Type product active users:",
            "Product active users", "100000", AddActiveUsersAction);

        #endregion

        #region Takeover Company

        public static void TakeoverCompanyAction(string input)
        {
            SimulatedCompany Company = GameSettings.Instance.simulation.Companies
                .FirstOrDefault(company => company.Value.Name == input).Value;

            if (Company == null)
            {
                return;
            }

            Company.BuyOut(GameSettings.Instance.MyCompany, true);
            HUD.Instance.AddPopupMessage("Trainer: Company " + Company.Name + " has been takovered by you!", "Cogs", "", 0, 0, 0, 0);
        }

        public static void TakeoverCompany() =>
            WindowManager.SpawnInputDialog("Type company name:", "Takeover Company", "", TakeoverCompanyAction);

        #endregion

        #region Subsidiary Company

        public static void SubDCompanyAction(string input)
        {
            SimulatedCompany Company =
                GameSettings.Instance.simulation.Companies.FirstOrDefault(company => company.Value.Name == input).Value;

            if (Company == null)
            {
                return;
            }

            Company.MakeSubsidiary(GameSettings.Instance.MyCompany);
            Company.IsSubsidiary();
            HUD.Instance.AddPopupMessage("Trainer: Company " + Company.Name + " is now your subsidiary!", "Cogs", "", 0, 0, 0, 0);
        }

        public static void SubDCompany() =>
            WindowManager.SpawnInputDialog("Type company name:", "Subsidiary Company", "", SubDCompanyAction);

        #endregion

        #region Force Bankrupt

        public static void ForceBankruptAction(string input)
        {
            SimulatedCompany Company =
                GameSettings.Instance.simulation.Companies.FirstOrDefault(company => company.Value.Name == input).Value;

            if (Company == null)
            {
                return;
            }

            Company.Bankrupt = !Company.Bankrupt;
        }

        public static void ForceBankrupt() =>
            WindowManager.SpawnInputDialog("Type company name:", "Force Bankrupt", "", ForceBankruptAction);

        #endregion

        #region Increase Money

        public static void IncreaseMoneyAction(string input)
        {
            GameSettings.Instance.MyCompany.MakeTransaction(input.ConvertToIntDef(100000), Company.TransactionCategory.Deals);
            HUD.Instance.AddPopupMessage("Trainer: Money has been added in category Deals!", "Cogs", "", 0, 0, 0, 0);
        }

        public static void IncreaseMoney() =>
            WindowManager.SpawnInputDialog("How much money do you want to add?", "Add Money", "100000", IncreaseMoneyAction);

        #endregion

        #region Add Rep

        public static void AddRepAction(string input)
        {
            GameSettings.Instance.MyCompany.BusinessReputation = 1f;
            SoftwareType random1 = GameSettings.Instance.SoftwareTypes.Values.Where(x => !x.OneClient).GetRandom();
            string random2 = random1.Categories.Keys.GetRandom();
            GameSettings.Instance.MyCompany.AddFans(input.ConvertToIntDef(10000), random1.Name, random2);
            HUD.Instance.AddPopupMessage("Trainer: Reputation has been added for SoftwareType: " + random1.Name + ", Category: " + random2, "Cogs", "", 0, 0, 0, 0, 1);
        }

        public static void AddRep() =>
            WindowManager.SpawnInputDialog("How much reputation do you want to add?", "Add Reputation", "10000", AddRepAction);

        #endregion

        #region Overrides

        public override void OnActivate()
        {
            ModActive = true;

            if (DoStuff)
            {
                HUD.Instance.AddPopupMessage("Trainer v2 has been activated!", "Cogs", "", 0, 0, 0, 0);
            }
        }

        public override void OnDeactivate()
        {
            ModActive = false;

            if (!DoStuff)
            {
                HUD.Instance.AddPopupMessage("Trainer v2 has been deactivated!", "Cogs", "", 0, 0, 0, 0, 1);
            }
        }

        #endregion
    }
}
