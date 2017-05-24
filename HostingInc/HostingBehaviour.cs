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
        public bool start = false;
        void Start()
        { 
            if (ModActive)
            {
                StartCoroutine(DealGen());
            }
        }
        void Update()
        {
            if (ModActive && GameSettings.Instance != null && HUD.Instance != null)
            {
                if (start == false)
                {
                    Main.Tipka();
                    start = true;
                }
            }
        }
        public static void Deals()
        {
            Company my = GameSettings.Instance.MyCompany;

            SoftwareProduct prod = new SoftwareProduct();
            prod.Server = "Server 0";
            prod.HandleLoad(0.5f);
            prod.ServerReq = 0.5f;
            prod.ExternalHostingActive = false;

            ServerDeal deal = new ServerDeal(prod);
            deal.Request = true;
        }
        IEnumerator<WaitForSeconds> DealGen()
        {
            while (true)
            {
                yield return new WaitForSeconds(60.0f);
                
            }
        }
        public override void OnActivate()
        {
            ModActive = true;
            if (ModActive && GameSettings.Instance != null && HUD.Instance != null)
            {
                HUD.Instance.AddPopupMessage("Hosting Inc has been activated!", "Cogs", "", 0, 0, 0, 0, 1);
            }
        }
        public override void OnDeactivate()
        {
            ModActive = false;
            if (!ModActive && GameSettings.Instance != null && HUD.Instance != null)
            {
                HUD.Instance.AddPopupMessage("Hosting Inc has been deactivated!", "Cogs", "", 0, 0, 0, 0, 1);
            }
        }
    }
    public static class Extensions
    {
        /// <summary>
        /// Sets a _private_ Property Value from a given Object. Uses Reflection.
        /// Throws a ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="obj">Object from where the Property Value is set</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <param name="val">Value to set.</param>
        /// <returns>PropertyValue</returns>
        public static void SetPrivatePropertyValue<T>(this object obj, string propName, T val)
        {
            Type t = obj.GetType();
            t.InvokeMember(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, obj, new object[] { val });
        }
    }
}
