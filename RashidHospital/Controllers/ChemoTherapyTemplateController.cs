using Hospital.DAL;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static RashidHospital.Helper.Enum;
namespace RashidHospital.Controllers
{
    public class ChemoTherapyTemplateController : Controller
    {
        private readonly Model1 _context;


        public ChemoTherapyTemplateController(Model1 context)
        {
            _context = context;

        }
        // GET
        public ActionResult Index()
        {
            var Result = _context.ChemoTherapy_Template.ToList();

            return View(Result);
        }
        // GET: Patient
     

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChemoTherapy_Template model)
        {
           
            if (ModelState.IsValid)
            {
                _context.ChemoTherapy_Template.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
          
            return View();
        }
        public ActionResult Open(int? Id)
        {
          
            var result = _context.ChemoTherapy_Template.Find(Id);
            return View("Open", result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ChemoTherapy_Template model)
        {
           
            if (model != null)
            {
               //_context.ChemoTherapy_Template.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

           
            return View(model);
        }
        public ActionResult Delete(int? Id)
        {
            var result = _context.ChemoTherapy_Template.Find(Id);
            if (result != null)
            {
                _context.ChemoTherapy_Template.Remove(result);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Duplicate(int? Id)
        {
            var result = _context.ChemoTherapy_Template.Find(Id);
            if (result != null)
            {
                _context.ChemoTherapy_Template.Add(result);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }




       






        //  public ActionResult PreInvestigationsIndex(int? Id)
        //  {



        //     var Result = _context.ChemoTherapyPreInvestigations.ToList();

        //  return View(Result);


        //     }

        /*    public ActionResult PreInvestigationsCreate()
            {
                return View();
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult PreInvestigationsCreate(ChemoTherapyPreInvestigations model)
            {

                if (ModelState.IsValid)
                {
                    _context.ChemoTherapyPreInvestigations.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }

                return View();
            }

            */
        /*  public ActionResult PreInvestigationsIndex(string patientID)
             {
                 int _patientID = Convert.ToInt32(patientID);

                 ChemoTherapyPreInvestigationsVM ObjVm = new ChemoTherapyPreInvestigationsVM();
                 List<ChemoTherapyPreInvestigationsVM> _list = ObjVm.SelectAllByTemplateID(_patientID);
                 return View(_list);
             }


         public ActionResult PreInvestigationsCreate(string patientID)
        {
         int _patientID = Convert.ToInt32(patientID);
             ChemoTherapyPreInvestigationsVM toxictyVM = new ChemoTherapyPreInvestigationsVM();
         toxictyVM.Template_ID = _patientID;

         return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreInvestigationsCreate(ChemoTherapyPreInvestigationsVM input)
        {
         if (ModelState.IsValid)
         {

             input.Create();
             return RedirectToAction("PreInvestigationsIndex", new { patientID = input.Template_ID });
         }
         else
         {
             return View(input);

         }

        }
        */
        public ActionResult PreInvestigationsIndex(string patientID)
 {
     int _patientID = Convert.ToInt32(patientID);
     fillBag(_patientID);
     ChemoTherapyPreInvestigationsVM ObjVm = new ChemoTherapyPreInvestigationsVM();
     List<ChemoTherapyPreInvestigationsVM> _list = ObjVm.SelectAllByTemplateID(_patientID);
     return View(_list);
 }
   

     


      

        public ActionResult PreInvestigationsCreate(int patientID)
        {
          //  int _patientID = Convert.ToInt32(patientID);
            ChemoTherapyPreInvestigationsVM radioTherapyVMVM = new ChemoTherapyPreInvestigationsVM();
            radioTherapyVMVM.Template_ID = patientID;
            return View(radioTherapyVMVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreInvestigationsCreate(ChemoTherapyPreInvestigationsVM input)
        {

            if (ModelState.IsValid)
            {
                input.Create();
                return RedirectToAction("PreInvestigationsIndex", new { patientID = input.Template_ID });
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


        public ActionResult PreLabIndex(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
             fillBag(_patientID);
            ChemoTherapyPreLabVM ObjVm = new ChemoTherapyPreLabVM();
            List<ChemoTherapyPreLabVM> _list = ObjVm.SelectAllByTemplateID(_patientID);
            return View(_list);
        }



        public ActionResult PreLabCreate(int patientID)
        {
            //  int _patientID = Convert.ToInt32(patientID);
            ChemoTherapyPreLabVM radioTherapyVMVM = new ChemoTherapyPreLabVM();
            radioTherapyVMVM.Template_ID = patientID;
            return View(radioTherapyVMVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreLabCreate(ChemoTherapyPreLabVM input)
        {

            if (ModelState.IsValid)
            {
                input.Create();
                return RedirectToAction("PreLabIndex", new { patientID = input.Template_ID });
            }

            return View(input);
        }






    }
}