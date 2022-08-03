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
namespace RashidHospital.Controllers
{
    public class ChemoCycleToxictyController : Controller
    {
        // GET: ChemoCycleToxicty
    


        public ActionResult Index(int Id, string pid)
        {
            int _patientID = Convert.ToInt32(pid);
            fillBag(Id, _patientID);

            ToxictyVM ObjVm = new ToxictyVM();
            List<ToxictyVM> _list = ObjVm.SelectAllByPatientId(_patientID);
            return View(_list);
        }


     
        public ActionResult Create(int Id, string pid)
        {
            ddlViewBags();
            int _patientID = Convert.ToInt32(pid);
            fillBag(Id, _patientID);
            ddlViewBags();
            ToxictyVM toxictyVM = new ToxictyVM();
            toxictyVM.PatientID = _patientID;
            fillBag(Id, _patientID);
            ddlViewBags();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToxictyVM input)
        {
            if (ModelState.IsValid)
            {
                input.CreatedDate = DateTime.Now;
                input.ModifiedDate = DateTime.Now;
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                input.DoctorId = userId;
                input.Create();
                return RedirectToAction("Index", new { Id = ViewBag.CycleId, pid = input.PatientID });
            }
            else
            {
                return View(input);

            }

        }
        private void ddlViewBags()
        {
            ToxictyTypeVM toxictyTypeVM = new ToxictyTypeVM();
            ViewBag.ToxictyTypeList = toxictyTypeVM.GetSelectList();
            ToxictyVM toxictyVM = new ToxictyVM();
            ViewBag.ToxictyGrade = toxictyVM.GetToxictyGradeSelectList();
            ViewBag.ToxictyCondition = toxictyVM.GetToxictyConditionSelectList();

        }
        private void fillBag(int CycleId, int PatientId)
        {
            PatientVM _patientvm = new PatientVM();
            ViewBag.PatientId = PatientId;
            ViewBag.CycleId = CycleId;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(PatientId);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }






    }
}