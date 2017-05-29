using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace HostingInc
{ 
    public class HostingBehaviour : ModBehaviour
    {
        public bool ModActive = false;
        public bool pushed = false;
        public bool reward = false;
        void Update()
        {
            if (ModActive && GameSettings.Instance != null && HUD.Instance != null)
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
        }
        public void Reward()
        {
            System.Random rnd = new System.Random();
            foreach (Deal x in HUD.Instance.dealWindow.GetActiveDeals())
            {
                if (x.Active)
                    GameSettings.Instance.MyCompany.MakeTransaction(rnd.Next(100, 1500), Company.TransactionCategory.Deals);
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
        public override void OnActivate()
        {
            ModActive = true;
            if (ModActive && GameSettings.Instance != null && HUD.Instance != null)
                HUD.Instance.AddPopupMessage("Hosting Inc has been activated!", "Cogs", "", 0, 0, 0, 0, 1);
        }
        public override void OnDeactivate()
        {
            ModActive = false;
            if (!ModActive && GameSettings.Instance != null && HUD.Instance != null)
                HUD.Instance.AddPopupMessage("Hosting Inc has been deactivated!", "Cogs", "", 0, 0, 0, 0, 1);
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
