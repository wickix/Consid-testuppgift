using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Models;

namespace Consid.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ViewResult ListCompanies()
        {
            return View(CompanyManager.getCompanies());
        }

        public void AddCompany(string Name, int OrganizationNr, string Notes)
        {
            company newCompany = new company();
            List<company> listCompany = CompanyManager.getCompanies();
            bool IdExist = true;

            newCompany.Name = Name;
            newCompany.OrganizationNumber = OrganizationNr;
            newCompany.Notes = Notes;
            newCompany.Id = Guid.NewGuid();
            //Check if the Id already exist
            while (IdExist)
            {
                foreach (var company in listCompany)
                {
                    if (newCompany.Id == company.Id)
                    {
                        newCompany.Id = Guid.NewGuid();
                        IdExist = true;
                        break;
                    }
                    IdExist = false;
                }
            }


            //newCompany.Stores = StoresList;

            CompanyManager.AddCompany(newCompany);
            Response.Redirect(Request.UrlReferrer.ToString());
        }
    }
}