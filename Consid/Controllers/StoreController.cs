using System;
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
    }
}