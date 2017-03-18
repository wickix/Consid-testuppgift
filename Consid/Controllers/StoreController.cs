using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Models;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Consid.Controllers
{
    public class StoreController : Controller
    {

        //Regex regex = new Regex("[a-zA-Z åäöÅÄÖ ]");
        String regex= ("[^a-zA-Z åäöÅÄÖ ]");

        String regexAddress = (@"[^\w åäöÅÄÖ]");
      //  Regex regexAddress = new Regex(@"[\w åäöÅÄÖ]");
        // GET: Store


        public ViewResult ListStores(int? page)
        {
            var pager = new Pagination(StoreManager.getNumberOfStores(), page);
            var items = StoreManager.getStores(pager);

            var viewModel = new ListStoresModel
            {
                Items = items,//.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager
            };
            //return View(items.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize));
            return View(viewModel);
        }

        public ViewResult ShowStore(Guid Id)
        {
            return View(StoreManager.getStore(Id));
        }

        public void AddStore(string Name, Guid Company, string Address, string City, string Zip, string Country, string Longitude, string Latitude)
        {
            if (ValidInput(Name, Company, Address, City, Zip, Country))
            {
                store newStore = new store();
            //    List<store> listStore = StoreManager.getStores();
           //     bool IdExist = true;

                newStore.Name = Name;
                newStore.CompanyId = CompanyManager.getcompany(Company).Id;
                newStore.Address = Address;
                newStore.City = City;
                newStore.Zip = Zip;
                newStore.Country = Country;
                List<double?> location = getLocation(Address, City, Zip, Country);
                newStore.Longitude = location[1].ToString();
                newStore.Latitude = location[0].ToString();
                newStore.Id = Guid.NewGuid();
                //Check if the Id already exist
                //while (IdExist)
                //{
                //    foreach (var store in listStore)
                //    {
                //        if (newStore.Id == store.Id)
                //        {
                //            newStore.Id = Guid.NewGuid();
                //            IdExist = true;
                //            break;
                //        }
                //        IdExist = false;
                //    }
                //}

                StoreManager.AddStore(newStore);
            }
            Response.Redirect(Request.UrlReferrer.ToString());
        }

        public void UpdateStore(string Name, Guid Company, string Address, string City, string Zip, string Country, string Longitude, string Latitude, Guid Id)
        {
            if (ValidInput(Name, Company, Address, City, Zip, Country)) 
            {
                store newStore = new store();

                newStore.Name = Name;
                newStore.CompanyId = CompanyManager.getcompany(Company).Id;
                newStore.Address = Address;
                newStore.City = City;
                newStore.Zip = Zip;
                newStore.Country = Country;
                List<double?> location = getLocation(Address, City, Zip, Country);
                newStore.Longitude = location[1].ToString();
                newStore.Latitude = location[0].ToString();
                newStore.Id = Id;
                //Check if the Id already exist

                StoreManager.UpdateStore(newStore);
            }
            Response.Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult DeleteStore(Guid id)
        {
            StoreManager.DeleteStore(StoreManager.getStore(id));
            return RedirectToAction("ListStores", "Store");
        }

        public List<double?> getLocation(string Address, string City, string Zip, string Country)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://maps.google.com/maps/api/geocode/json?address="+Address.ToString().Replace(' ', '+')+"+"+City+"+"+Country+"&sensor=false");
            httpWebRequest.Method = "GET";


            //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //{

            //    streamWriter.Write(json);
            //}
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
               var responseText = streamReader.ReadToEnd();

                dynamic jsonBody = JsonConvert.DeserializeObject(responseText);
                //List<float?> location = new List<float?>();
                List<double?> location= new List<double?>();

                if (jsonBody["status"] == "OK")
                {
                    dynamic test = jsonBody["results"][0]["geometry"];
                    dynamic test2 = Convert.ToDecimal(jsonBody["results"][0]["geometry"]["location"]["lat"]);
                    location.Add((double)jsonBody["results"][0]["geometry"]["location"]["lat"]);
                    location.Add((double)jsonBody["results"][0]["geometry"]["location"]["lng"]);
                  //  location.Add(Convert.ToDecimal(jsonBody["results"][0]["geometry"]["location"]["lat"]));
                  //  location.Add(jsonBody["results"][0]["geometry"]["location"].lng);
                }
                else
                {
                    location.Add(null);
                    location.Add(null);
                }
                




                //Now you have your response.
                //or false depending on information in the response
                return location;
            }
        }

        public bool ValidInput(string Name, Guid Company, string Address, string City, string Zip, string Country)
        {
            if (Name.Length <= 0 || Name.Length > 100)
            {
                return false;
            }
            if (Company == Guid.Empty)
            {
                return false;
            }
            if (Address.Length <= 0 || Address.Length > 512 || System.Text.RegularExpressions.Regex.IsMatch(Address, regexAddress))
            {
                return false;
            }
           if((System.Text.RegularExpressions.Regex.IsMatch(City, regex)))
            {
                return false;
            }
            if (City.Length <= 0 || City.Length > 512 || System.Text.RegularExpressions.Regex.IsMatch(City, regex))
            {
                    return false;
            }
            if (Zip.Length <= 0 || Zip.Length > 16)
            {
                return false;
            }
            if (Country.Length <= 0 || Country.Length > 50 || System.Text.RegularExpressions.Regex.IsMatch(Country, regex))
            {
                return false;
            }

            return true;
        }

    }
}
