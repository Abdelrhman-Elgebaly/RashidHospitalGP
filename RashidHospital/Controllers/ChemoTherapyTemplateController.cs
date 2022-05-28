using Hospital.DAL;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;

using System.IO;

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

        [HttpPost]
        public int Delete(int? Id)
        {
            int finalResult = 0;
            try
            {
                ChemoTherapyTemplateVM radioTherapyVMVM = new ChemoTherapyTemplateVM();

                ChemoTherapyTemplateVM DeleteObject = radioTherapyVMVM.SelectObject(Id);
                DeleteObject.Delete();

                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }



       




        private void FillViewBags()
        {
            PatientUnitVM _PatientUnitModel = new PatientUnitVM();
            ViewBag.PatientUnit = _PatientUnitModel.GetSelectList();
        }


        private void FillDiagnoseViewBags()
        {

            DiagonsVM _DiagnoseModel = new DiagonsVM();
            ViewBag.Diagnoses = _DiagnoseModel.GetSelectList();
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





    }
}