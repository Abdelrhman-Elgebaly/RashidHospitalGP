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

        public JsonResult GetProrocolList(int DiseaseId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            List<protocol> ProtocolList = _context.protocols.Where(x => x.DiseaseId == DiseaseId).ToList();
            return Json(ProtocolList, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Index()
        {
           
            ChemoTherapyTemplateVM ObjVm = new ChemoTherapyTemplateVM();

            List<ChemoTherapyTemplateVM> _list = ObjVm.SelectAll();

            return View(_list);
        }
 
        [HttpGet]

        public ActionResult Indext()

        {

            EmployeeModel employeeModel = new EmployeeModel();
            ChemoTherapyTemplateVM _Obj = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplate _BClass = new ChemoTherapyTemplate();
            List<ChemoTherapyTemplate> dbList = _BClass.GetAll<ChemoTherapyTemplate>().ToList();
            employeeModel.EmployeeList = dbList;

            //Here i have adding donw dummy value as id

            //In this we will create 5 rows



            return View(employeeModel);

        }
        [HttpPost]

        public ActionResult Indext(EmployeeModel employeeModel)

        {

            //Now save the department id in the database as required

            //Rebind the employee table data again 

            return View(employeeModel);

        }
        public ActionResult Create()
        {
            fillCreateBag();
          
           
            ChemoTherapyTemplateVM radioTherapyVMVM = new ChemoTherapyTemplateVM();



            fillCreateBag();
            return View(radioTherapyVMVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChemoTherapyTemplateVM input)
        {
           // fillCreateBag();

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

                input.Protocol_Name = input.Protocol_Name;
                input.Disease = input.Disease;
                input.Create();

                return RedirectToAction("Index");
            }
          //  fillCreateBag();

            return View(input);
        }

        private void fillCreateBag()
        {
            ChemoTherapyTemplateVM results = new ChemoTherapyTemplateVM();
            ViewBag.ELevel = results.GetELevelSelectList();
            DiseaseVM disease = new DiseaseVM();
           ViewBag.DiseaseList = disease.DiseaseSelectList();
           protocolVM protocol = new protocolVM();
            ViewBag.ProtocolList = protocol.ProtocolSelectList();
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