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
    }
}