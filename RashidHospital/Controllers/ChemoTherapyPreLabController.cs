using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.DAL;
using RashidHospital.Models;
using System.Text.RegularExpressions;

using Microsoft.AspNet.Identity;
namespace RashidHospital.Controllers
{
    public class ChemoTherapyPreLabController : Controller
    {
        // GET: ChemoTherapyPreLab
  
        public ActionResult Index(string templateID)
        {
            int _templateID = Convert.ToInt32(templateID);
            fillBag(_templateID);
            ChemoTherapyPreLabVM ObjVm = new ChemoTherapyPreLabVM();
            List<ChemoTherapyPreLabVM> _list = ObjVm.SelectAllByTemplateID(_templateID);
        
            return View(_list);
        }



        public ActionResult Create(int templateID)
        {
            //  int _patientID = Convert.ToInt32(patientID);
            ChemoTherapyPreLabVM radioTherapyVMVM = new ChemoTherapyPreLabVM();
            radioTherapyVMVM.Template_ID = templateID;
            fillCreateBag();
            return View(radioTherapyVMVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChemoTherapyPreLabVM input)
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



        private void fillBag(int patientID)
        {
            ViewBag.PatientId = patientID;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }

        private void fillCreateBag()
        {
            ChemoTherapyPreLabVM results = new ChemoTherapyPreLabVM();
            ViewBag.LabTypesList = results.GeLabTypsSelectList();
            ViewBag.RuleTypesList = results.GetRuleTypsSelectList();
          

        }

    }
}