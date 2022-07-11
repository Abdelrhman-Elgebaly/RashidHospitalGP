
using Microsoft.AspNet.Identity;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static RashidHospital.Helper.Enum;
using System.Text.RegularExpressions;


namespace RashidHospital.Controllers
{
    public class ChemoTherapyDrugController : Controller
    {




        // GET: RadioTherapy
        /*
        public ActionResult Index(string templateID)
        {
            int _templateID = Convert.ToInt32(templateID);
            fillBag(_templateID);
            ChemoTherapyDrugVM ObjVm = new ChemoTherapyDrugVM();
            List<ChemoTherapyDrugVM> _list = ObjVm.SelectAllByTemplateId(_templateID);
            return View(_list);
        }



        private void fillBag(int templateID)
        {
            ViewBag.PatientId = templateID;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(templateID);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }
        public ActionResult Create(int templateID, string therapyType )
        {
          // int _templateID = Convert.ToInt32(templateID);
            ChemoTherapyDrugVM radioTherapyVMVM = new ChemoTherapyDrugVM();
            radioTherapyVMVM.Template_ID = templateID;
            radioTherapyVMVM.Therapy_Type = therapyType;
         ////   fillBag(_templateID);
         //   ddlViewBags();
            return View(radioTherapyVMVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChemoTherapyDrugVM input)
        {
            if (ModelState.IsValid)
            {
               
                input.Create();
                return RedirectToAction("Index", new { templateID = input.Template_ID });
            }
            else
            {
                return View(input);

            }

        }
*/

        private void fillBag(int templateID)
        {
            ViewBag.PatientId = templateID;
           

        }

        public ActionResult Index(string templateID)
        {
            fillCreateBag();

            int _templateID = Convert.ToInt32(templateID);
            fillBag(_templateID);
            fillCreateBag();

            ChemoTherapyDrugVM ObjVm = new ChemoTherapyDrugVM();
            List<ChemoTherapyDrugVM> _list = ObjVm.SelectAllByTemplateId(_templateID);
            fillCreateBag();

            return View(_list);
        }

     

        public ActionResult Create(int templateID, string therapyType)
        {

            fillCreateBag();

            //  int _patientID = Convert.ToInt32(patientID);
            ChemoTherapyDrugVM radioTherapyVMVM = new ChemoTherapyDrugVM();
            fillCreateBag();

            radioTherapyVMVM.Template_ID = templateID;
            radioTherapyVMVM.Therapy_Type = therapyType;



            fillCreateBag();


            return View(radioTherapyVMVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChemoTherapyDrugVM input)
        {
            fillCreateBag();

            if (ModelState.IsValid)
            {
                fillCreateBag();



                List<string> tokens = input.Days.Split(',').ToList();
                List<int> intlist = new List<int>();

                foreach (String str in tokens)
                {
                    intlist.Add(Convert.ToInt32(Regex.Replace(str, "[^0-9]+", string.Empty)));
                }

                string[] array = new string[1000];

                array = intlist.ConvertAll(x => x.ToString()).ToArray();
                input.Days = string.Join("/", array);










                input.Create();

                return RedirectToAction("Index", new { templateID = input.Template_ID });
            }
            fillCreateBag();

            return View(input);
        }


        private void fillCreateBag()
        {
            ChemoTherapyDrugVM results = new ChemoTherapyDrugVM();
            ViewBag.Unit = results.GetUnitSelectList();
            ViewBag.Route = results.GetRouteSelectList();
            ViewBag.Fluid = results.GetFluidTSelectList();



        }

    }
}