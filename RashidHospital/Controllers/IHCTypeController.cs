using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RashidHospital.Controllers
{
    [Authorize(Roles = "Admin,Employee")]

    public class IHCTypeController : Controller
    {
        // GET: Diagnose
        public ActionResult Index()
        {
            IHCTypesVM _obj = new IHCTypesVM();
            List<IHCTypesVM> list = _obj.SelectAll();
            return View(list);
        }
        // GET: Clinics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Diagnose/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IHCTypesVM ihcType)
        {
            if (ModelState.IsValid)
            {

                ihcType.Create();
                return RedirectToAction("Index");
            }
            else
            {
                return View(ihcType);

            }

        }
    }
}