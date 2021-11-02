using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RashidHospital.Models;
namespace RashidHospital.Controllers
{
    public class ToxictyTypeController : Controller
    {
        // GET: ToxictyType
        public ActionResult Index()
        {
            ToxictyTypeVM _obj = new ToxictyTypeVM();
            List<ToxictyTypeVM> list = _obj.SelectAll();
            return View(list);
        }
        // GET: ToxictyType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToxictyType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToxictyTypeVM input)
        {
            if (ModelState.IsValid)
            {

                input.Create();
                return RedirectToAction("Index");
            }
            else
            {
                return View(input);

            }

        }
    }
}