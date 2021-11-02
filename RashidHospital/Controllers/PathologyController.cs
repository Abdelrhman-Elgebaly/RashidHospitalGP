using Hospital.DAL;
using Microsoft.AspNet.Identity;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RashidHospital.Controllers
{
    [Authorize(Roles = "Doctor, Residents,Admin,Assistant lecturer,Consultant,Nurses,physician,Employee,Assistant,Pharmacist")]
    public class PathologyController : Controller
    {
        // GET: Pathology
        public ActionResult Index(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
            fillBag(_patientID);
            PathologyVM ObjVm = new PathologyVM();
            List<PathologyVM> _list = ObjVm.SelectAllByPatientId(_patientID);
            return View(_list);
        }

        private void fillBag(int patientID)
        {
            PatientVM _patientvm = new PatientVM();
            ViewBag.PatientId = patientID;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }
        private void ddlViewBags() {
            PathologyVM _pathologyVm = new PathologyVM();
            ViewBag.TumerGrade = _pathologyVm.GetTumerGradeSelectList();
            ViewBag.TumorFocality = _pathologyVm.GetTumorFocalitySelectList();
            ViewBag.TumorMargins = _pathologyVm.GetTumorMarginsSelectList();
            TumerHistologyTypesVM _tymerHstology = new TumerHistologyTypesVM();
            ViewBag.TumerHistologyTypes = _tymerHstology.GetSelectList();

        }
        private void ddlIHCViewBags(int patientID)
        {
            IHCVM _iHCVm = new IHCVM();
            ViewBag.IHCType = _iHCVm.GetIHCTypeSelectList();
            
        }

        // GET: Clinics/Create
        public ActionResult Create(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
            fillBag(_patientID);
            ddlViewBags();
            ddlIHCViewBags(_patientID);
            return View();
        }

        // POST: Pathology/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PathologyVM pathology)
        {
            pathology.Date = DateTime.Now;
            List<IHCVM> ihclist = (List<IHCVM>)HttpContext.Session[pathology.PatientId.ToString()];
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            pathology.DoctorId = userId;
            if (ModelState.IsValid)
            {
                 pathology.Create();
                PathologyVM _patholgy = new PathologyVM();
                _patholgy= _patholgy.SelectAllByPatientId(pathology.PatientId).OrderByDescending(a => a.Date).FirstOrDefault();
                if (ihclist != null) {
                    foreach (var ihc in ihclist)
                    {
                        saveIhcobject(ihc, pathology.PatientId, _patholgy.Id);
                    }
                }
                
                return RedirectToAction("Index", new RouteValueDictionary(
    new { controller = "Pathology", action = "Index", patientID = pathology.PatientId }));
            }
            else
            {
                fillBag(pathology.PatientId);
                ddlViewBags();
                ddlIHCViewBags(pathology.PatientId);
                return View(pathology);

            }

        }
        private void saveIhcobject(IHCVM ihcObject,int PatientId ,int PathologyId) {

            ihcObject.PatientId = PatientId;
            ihcObject.PathologyId = PathologyId;
            ihcObject.ResultDate = DateTime.Now;

            ihcObject.Create();
        }

        public ActionResult CreateIHC(string PathologyID)
        {
            int _PathologyID = Convert.ToInt32(PathologyID);
            Pathology pathology = new Pathology();
           var pathologyObject= pathology.Getobject(_PathologyID);
                if (pathologyObject != null)
                {
                    fillBag(pathologyObject.PatientId);
                    ddlIHCViewBags(pathologyObject.PatientId);
                IHCVM createObject = new IHCVM();
                createObject.PatientId = pathologyObject.PatientId;
                createObject.PathologyId = pathologyObject.Id;
                    return View(createObject);
                }
                else
                {
                return RedirectToAction("Index", new RouteValueDictionary(
new { controller = "Pathology", action = "Index", patientID = pathology.PatientId }));
            }
         }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateIHC(IHCVM ihc)
        {
            
            if (ModelState.IsValid)
            {
                ihc.ResultDate = DateTime.Now;
                ihc.Create();
                return RedirectToAction("Index", new RouteValueDictionary(
   new { controller = "Pathology", action = "Index", patientID = ihc.PatientId }));
            }
            else
            {
                ddlIHCViewBags(ihc.PatientId);
                return View(ihc);

            }

        }
        [HttpPost]
        public int DeletePathology(int PathologyId)
        {
            int finalResult = 0;
            try
            {
                PathologyVM _Pathology = new PathologyVM();
                PathologyVM _obj = _Pathology.SelectObject(PathologyId);
                if (_obj != null) {
                    IHCVM _IHC = new IHCVM();
                    List<IHCVM> IHCSList = _IHC.SelectAllByPathologyId(PathologyId);
                    foreach (var ihc in IHCSList) {
                        ihc.IsDeleted = true;
                        ihc.Edit(); }
                    _obj.IsDeleted = true;
                    _obj.Edit();
                }
                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }

        [HttpPost]
        public int DeleteIHC(int IHCId) {

            int finalResult = 0;
            try
            {
                IHCVM _Ihc = new IHCVM();
                IHCVM _obj = _Ihc.SelectObject(IHCId);
                if (_obj != null)
                {
                    IHCVM _IHC = new IHCVM();
                    IHCVM IHCSobj = _IHC.SelectObject(IHCId);
                    IHCSobj.Delete();

                    if (HttpContext.Session[_obj.PatientId.ToString()] != null)
                    {
                        List<IHCVM> ihcList = (List<IHCVM>)HttpContext.Session[_obj.PatientId.ToString()];
                        ihcList.Remove(IHCSobj);
                        HttpContext.Session[_obj.PatientId.ToString()] = ihcList;
                    }
                    finalResult = 1;
                }

            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }
        [HttpGet]
        //View?PathologyID
        public ActionResult ViewPathology(string PathologyID) {
            ddlViewBags();
            int _PathologyID = Convert.ToInt32(PathologyID);
            PathologyVM pathology = new PathologyVM();
            PathologyVM pathologyObject = pathology.SelectObject(_PathologyID);
            ViewBag.PatientId = pathologyObject.PatientId;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(pathologyObject.PatientId);

            IHCVM _IHC = new IHCVM();
            List<IHCVM> IHClist = _IHC.SelectAllByPathologyId(_PathologyID);
            ViewBag.IHClist = IHClist;
            HttpContext.Session[pathologyObject.PatientId.ToString()] = IHClist;
            ddlIHCViewBags(pathologyObject.PatientId);
            return View("ViewPathology", pathologyObject);
        }
        [HttpGet]

        public ActionResult View(string PathologyID)
        {
            int _PathologyID = Convert.ToInt32(PathologyID);
            PathologyVM pathology = new PathologyVM();
            PathologyVM pathologyObject = pathology.SelectObject(_PathologyID);
            ViewBag.PatientId = pathologyObject.PatientId;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(pathologyObject.PatientId);

            IHCVM _IHC = new IHCVM();
            List<IHCVM> IHClist = _IHC.SelectAllByPathologyId(_PathologyID);
            ViewBag.IHClist = IHClist;
            HttpContext.Session[pathologyObject.PatientId.ToString()] = IHClist;
            ddlIHCViewBags(pathologyObject.PatientId);
            return View("View", pathologyObject);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PathologyVM pathology)
        {
            pathology.Date = DateTime.Now;
            List<IHCVM> ihclist = (List<IHCVM>)HttpContext.Session[pathology.PatientId.ToString()];
            AspNetUser _users = new AspNetUser();
            var _ObjLst = _users.GetAll<AspNetUser>().ToList();
            pathology.DoctorId = _ObjLst.FirstOrDefault().Id;
            if (ModelState.IsValid)
            {
                pathology.Edit();
                //delete old
                IHCVM _IHC = new IHCVM();
                List<IHCVM> IHCSList = _IHC.SelectAllByPathologyId(pathology.Id);
                foreach (var ihc in IHCSList) { ihc.Delete(); }
                //save new
                foreach (var ihc in ihclist)
                {
                    saveIhcobject(ihc, pathology.PatientId, pathology.Id);
                }
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Pathology", action = "Index", patientID = pathology.PatientId }));
            }
            else
            {
                fillBag(pathology.PatientId);
                ddlViewBags();
                ddlIHCViewBags(pathology.PatientId);
                return View(pathology);

            }

        }

        public string SaveIHC(string Value,string Type,string Result , string PatientId)
        {
            try
            {
                if (PatientId != null)
                {
                    IHCVM newIHC = new IHCVM();
                    newIHC.PatientId =Convert.ToInt32( PatientId);
                    newIHC.Result = Result;
                    newIHC.Type =Convert.ToInt32( Type);
                    newIHC.Value = Value;
                    if (HttpContext.Session[PatientId] != null) {
                        List<IHCVM> IHCVMList = (List<IHCVM>)HttpContext.Session[PatientId];
                        IHCVMList.Add(newIHC);
                        HttpContext.Session[PatientId] = IHCVMList;
                    }
                    else{
                        List<IHCVM> IHCVMList =new List<IHCVM>();
                        IHCVMList.Add(newIHC);
                        HttpContext.Session[PatientId] = IHCVMList;
                    }
                   
                }

                return "success";
            }
            catch (Exception ex)
            {

                return "error";

            }

        }
    }
}