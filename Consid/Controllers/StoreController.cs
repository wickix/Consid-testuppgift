﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Models;

namespace Consid.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ViewResult ListStores()
        {
            return View(StoreManager.getStores());
        }

        public ViewResult ShowStore(Guid Id)
        {
            return View(StoreManager.getStore(Id));
        }

        public void AddStore(string Name, Guid Company, string Address, string City, string Zip, string Country, string Longitude, string Latitude)
        {
            store newStore = new store();
            List<store> listStore = StoreManager.getStores();
            bool IdExist = true;

            newStore.Name = Name;
            newStore.CompanyId = CompanyManager.getcompany(Company).Id;
            newStore.Address = Address;
            newStore.City = City;
            newStore.Zip = Zip;
            newStore.Country = Country;
            newStore.Longitude = Longitude;
            newStore.Latitude = Latitude;
            newStore.Id = Guid.NewGuid();
            //Check if the Id already exist
            while (IdExist)
            {
                foreach (var store in listStore)
                {
                    if (newStore.Id == store.Id)
                    {
                        newStore.Id = Guid.NewGuid();
                        IdExist = true;
                        break;
                    }
                    IdExist = false;
                }
            }

            StoreManager.AddStore(newStore);
            Response.Redirect(Request.UrlReferrer.ToString());
        }

        public void UpdateStore(string Name, Guid Company, string Address, string City, string Zip, string Country, string Longitude, string Latitude, Guid Id)
        {
            store newStore = new store();

            newStore.Name = Name;
            newStore.CompanyId = CompanyManager.getcompany(Company).Id;
            newStore.Address = Address;
            newStore.City = City;
            newStore.Zip = Zip;
            newStore.Country = Country;
            newStore.Longitude = Longitude;
            newStore.Latitude = Latitude;
            newStore.Id = Id;
            //Check if the Id already exist

            StoreManager.UpdateStore(newStore);
            Response.Redirect(Request.UrlReferrer.ToString());
        }

    }
}