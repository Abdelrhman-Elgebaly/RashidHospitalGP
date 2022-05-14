using Hospital.DAL;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;

namespace RashidHospital.Controllers
{
    public class ChemoTherapyTemplateController : Controller
    {
        private readonly Model1 _context;


        public ChemoTherapyTemplateController(Model1 context)
        {
            _context = context;

        }


        public ActionResult Index()
        {
           
            ChemoTherapyTemplateVM ObjVm = new ChemoTherapyTemplateVM();

            List<ChemoTherapyTemplateVM> _list = ObjVm.SelectAll();

            return View(_list);
        }

        public ActionResult Create()
        {
           
            ChemoTherapyTemplateVM radioTherapyVMVM = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM ObjVm = new ChemoTherapyTemplateVM();


            List<ChemoTherapyTemplateVM> _list = ObjVm.SelectAll();



            return View(radioTherapyVMVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChemoTherapyTemplateVM input)
        {

            if (ModelState.IsValid)
            {


                List<string> tokens = input.Cycle_days.Split(',').ToList();
                List<int> intlist = new List<int>();

                foreach (String str in tokens)
                {
                    intlist.Add(Convert.ToInt32(Regex.Replace(str, "[^0-9]+", string.Empty)));
                }
                input.CycleDays = intlist;
                string[] array = new string[1000];

                array = intlist.ConvertAll(x => x.ToString()).ToArray();
                input.Cycle_days = string.Join("/", array);


                input.Create();

                return RedirectToAction("Index");
            }

            return View(input);
        }

        public ActionResult Duplicate(int? Id)
        {
            var result = _context.ChemoTherapyTemplate.Find(Id);
            if (result != null)
            {
                _context.ChemoTherapyTemplate.Add(result);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }


        public ActionResult View(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
            fillBag(_patientID);
            ChemoTherapyTemplateVM ObjVm = new ChemoTherapyTemplateVM();

            List<ChemoTherapyTemplateVM> _list = ObjVm.SelectAll();

          /*  var items = _list;
            if (items != null)
            {
                ViewBag.data = items;
            }

            */

          
            ViewBag.data = new SelectList(_list, "Template_ID", "Protocol_Name");


            return View();
        }




        [HttpGet]
        public ActionResult ViewCycle(string patientID, string templateID)
        {
            if (patientID == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }

            int PatientId = Convert.ToInt32(patientID);
            int templateId = Convert.ToInt32(templateID);
          
            ChemoTherapyTemplateVM _Obj = new ChemoTherapyTemplateVM();
           
            _Obj.Patient_ID = PatientId;
            ChemoTherapyTemplateVM _objVM = _Obj.SelectObject(templateId);
         
            if (_objVM == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            return View(_objVM);


        }



      


        private void fillBag2(int patientID)
        {
            ViewBag.templateID = patientID;
          

        }

        private void fillBag(int patientID)
        {
            ViewBag.PatientId = patientID;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }

        private MultiSelectList GetCountries(string[] selectedValues)
        {

            List<int> primeNumbers = new List<int>();
            primeNumbers.Add(1); // adding elements using add() method
            primeNumbers.Add(3);
            primeNumbers.Add(5);
            primeNumbers.Add(7);


            return new MultiSelectList(primeNumbers, "Template_ID", "Day", selectedValues);

        }

      
        public ActionResult MultiSelectCountry(FormCollection form)
        {

            ViewBag.YouSelected = form["primeNumbers"];

            string selectedValues = form["primeNumbers"];

            ViewBag.Countrieslist = GetCountries(selectedValues.Split(','));

            return View();

        }



    }
}