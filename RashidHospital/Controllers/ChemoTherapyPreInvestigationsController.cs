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
    public class ChemoTherapyPreInvestigationsController : Controller
    {
        // GET: ChemoTherapyPreInvestigations
        public ActionResult Index(string templateID)
        {
            int _templateID = Convert.ToInt32(templateID);
            fillBag(_templateID);
            ChemoTherapyPreInvestigationsVM ObjVm = new ChemoTherapyPreInvestigationsVM();
            List<ChemoTherapyPreInvestigationsVM> _list = ObjVm.SelectAllByTemplateID(_templateID);
            return View(_list);
        }


        public ActionResult Create(int templateID)
        {
            //  int _patientID = Convert.ToInt32(patientID);
            ChemoTherapyPreInvestigationsVM radioTherapyVMVM = new ChemoTherapyPreInvestigationsVM();
            radioTherapyVMVM.Template_ID = templateID;
       
            fillCreateBag();
            return View(radioTherapyVMVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChemoTherapyPreInvestigationsVM input)
        {

            if (ModelState.IsValid)
            {



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

            return View(input);
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



        private void fillCreateBag()
        {
            ChemoTherapyPreInvestigationsVM results = new ChemoTherapyPreInvestigationsVM();
           
            ViewBag.RuleTypesList = results.GetRuleTypsSelectList();


        }


        public ActionResult Edit(int Id)
        {
            //  int _patientID = Convert.ToInt32(patientID);
            ChemoTherapyPreInvestigationsVM chemoTherapyPreInvestigationsVM = new ChemoTherapyPreInvestigationsVM();
            ChemoTherapyPreInvestigationsVM chemoTherapyPreInvestigationsVM1 = chemoTherapyPreInvestigationsVM.SelectObject(Id);

            fillCreateBag();
            return View(chemoTherapyPreInvestigationsVM1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ChemoTherapyPreInvestigationsVM input)
        {

            if (ModelState.IsValid)
            {



                List<string> tokens = input.Days.Split(',').ToList();
                List<int> intlist = new List<int>();

                foreach (String str in tokens)
                {
                    intlist.Add(Convert.ToInt32(Regex.Replace(str, "[^0-9]+", string.Empty)));
                }

                string[] array = new string[1000];

                array = intlist.ConvertAll(x => x.ToString()).ToArray();
                input.Days = string.Join("/", array);


                input.Edit();
                return RedirectToAction("Index", new { templateID = input.Template_ID });
            }

            return View(input);
        }




        [HttpPost]
        public int Delete(int id)
        {
            int finalResult = 0;
            try
            {
                ChemoTherapyPreInvestigationsVM _resultVM = new ChemoTherapyPreInvestigationsVM();
                ChemoTherapyPreInvestigationsVM DeleteObject = _resultVM.SelectObject(id);
                // DeleteObject.IsDeleted = true;
                DeleteObject.Delete();

                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }






    }
}