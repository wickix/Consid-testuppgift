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
        public ViewResult ListCompanies(int? page)
        {
            var pager = new Pagination(CompanyManager.getNumberOfCompanies(), page);
            var items = CompanyManager.getCompanies(pager);

            var viewModel = new ListCompaniesModel
            {
                Items = items,
                Pager = pager
            };
            return View(viewModel);
        }

        public ViewResult ShowCompany(Guid Id)
        {
            return View(CompanyManager.getcompany(Id));
        }

        public void AddCompany(string Name, int OrganizationNr, string Notes)
        {
            if (ValidInputs(Name, OrganizationNr))
            {
                company newCompany = new company();
                //List<company> listCompany = CompanyManager.getCompanies();
                //bool IdExist = true;

                newCompany.Name = Name;
                newCompany.OrganizationNumber = OrganizationNr;
                newCompany.Notes = Notes;
                newCompany.Id = Guid.NewGuid();
                //Check if the Id already exist
                //while (IdExist)
                //{
                //    foreach (var company in listCompany)
                //    {
                //        if (newCompany.Id == company.Id)
                //        {
                //            newCompany.Id = Guid.NewGuid();
                //            IdExist = true;
                //            break;
                //        }
                //        IdExist = false;
                //    }
                //}

                //newCompany.Stores = StoresList;

                CompanyManager.AddCompany(newCompany);
               
            }
           Response.Redirect(Request.UrlReferrer.ToString());
        }


        public void UpdateCompany(string Name, int OrganizationNr, string Notes, Guid Id)
        {
            if (ValidInputs(Name, OrganizationNr))
            {
                company updateCompany = new company();

                updateCompany.Name = Name;
                updateCompany.OrganizationNumber = OrganizationNr;
                updateCompany.Notes = Notes;
                updateCompany.Id = Id;

                CompanyManager.UpdateCompany(updateCompany);

            }
            Response.Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult DeleteCompany(Guid Id)
        {
            CompanyManager.DeleteCompany(CompanyManager.getcompany(Id));
            return RedirectToAction("ListCompanies", "Company");
        }

        public bool ValidInputs(string Name, int OrganizationNr)
        {
            if (Name.Length <=0  || Name.Length > 255)
            {
                return false;
            }
            if (OrganizationNr <= 0 || OrganizationNr > int.MaxValue)
            {
                return false;
            }

            return true;
        }
    }
}