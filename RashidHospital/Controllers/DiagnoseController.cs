using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RashidHospital.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class DiagnoseController : Controller
    {
        // GET: Diagnose
        public ActionResult Index()
        {
            DiagonsVM _obj = new DiagonsVM();
            List<DiagonsVM> list = _obj.SelectAll();
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
        public ActionResult Create(DiagonsVM diagnose)
        {
            if (ModelState.IsValid)
            {
                
                diagnose.Create();
                return RedirectToAction("Index");
            }
            else
            {
                return View(diagnose);

            }

        }
    }
}