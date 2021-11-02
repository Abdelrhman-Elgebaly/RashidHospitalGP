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
    [Authorize(Roles = "Doctor, Residents,Admin,Assistant lecturer,Consultant,Nurses,physician,Employee,Assistant,Pharmacist")]
    public class MedicalConditionController : Controller
    {
        // GET: MedicalCondition
        [HttpGet]
        public ActionResult Index(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
            MedicalConditionVM ObjVm = new MedicalConditionVM();
            List<MedicalConditionVM> _list = ObjVm.SelectAllByPatientId(_patientID);
            fillBag(_patientID);
            return View(_list);
        }
        private void fillBag(int patientID)
        {
            MedicalConditionVM ObjVm = new MedicalConditionVM();

            PatientVM _patientvm = new PatientVM();
            ViewBag.PatientQuery = patientID;

            ViewBag.PatientId = patientID;
            PatientVM patientObj = _patientvm.SelectObject(patientID);
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
            ViewBag.gender = patientObj.Gender.ToLower();
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;
        }
        //_EditMedical
        public JsonResult _EditMedical(int patientID)
        {
            if (patientID == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            fillMedicalViewBags(patientID);
            MedicalConditionVM _AddMedicalCondition = new MedicalConditionVM();
            _AddMedicalCondition.PatientId = Convert.ToInt32(patientID);
            _AddMedicalCondition.HistroryType = (int)Helper.Enum.HistoryType.Medical;

            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("CreateMedical", _AddMedicalCondition) }, JsonRequestBehavior.AllowGet);
        }
        //_EditSurgical

        public JsonResult _EditSurgical(int patientID)
        {
            if (patientID == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            fillSurgicalViewBags(patientID);
            MedicalConditionVM _AddSurgicalCondition = new MedicalConditionVM();
            _AddSurgicalCondition.PatientId = Convert.ToInt32(patientID);
           _AddSurgicalCondition.HistroryType = (int)Helper.Enum.HistoryType.Surgical;
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("CreateSurgical", _AddSurgicalCondition) }, JsonRequestBehavior.AllowGet);
        }
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        private void fillMedicalViewBags(int patientID) {
            MedicalConditionVM _conditionVM = new MedicalConditionVM();
           ViewBag.MedicalList= _conditionVM.GetMedicalSelectList();
            ViewBag.PatientId = patientID;
        }

        private void fillSurgicalViewBags(int patientID)
        {
            MedicalConditionVM _conditionVM = new MedicalConditionVM();
            ViewBag.SurgicalList = _conditionVM.GetSurgicalSelectList();
            ViewBag.PatientId = patientID;
        }

        //AddCondition
        //[HttpPost]
        //public string AddCondition(MedicalConditionVM vm)
        //{
        //   try {
        //        Guid userId = Guid.Parse(User.Identity.GetUserId());
        //        vm.DoctorId = userId;
        //        vm.Create();
        //        return "Success"; // succcess
        //    }
        //    catch (Exception ex)
        //    {

        //        return "Error500";

        //    }
        //}
        //    data: { "Condition": Condition, "ConditionDate": ConditionDate, "ConditionType": ConditionType, "HistroryType": HistroryType, "PatientId": PatientId },
        [HttpPost]
        public JsonResult AddCondition(string Condition,DateTime ? ConditionDate,int ConditionType,int HistroryType,int PatientId)
        {
            try
            {
                MedicalConditionVM vmObject = new MedicalConditionVM();
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                vmObject.DoctorId = userId;
                vmObject.Condition = Condition;
                vmObject.ConditionDate = ConditionDate!= null? ConditionDate:DateTime.Now;
                vmObject.ConditionType = ConditionType;
                vmObject.HistroryType = HistroryType;
                vmObject.PatientId = PatientId;
                vmObject.Create();
                return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
        }

        //Delete

        [HttpPost]
        public int Delete(int ConditionID)
        {
            int finalResult = 0;
            try
            {
                MedicalConditionVM _VMObject = new MedicalConditionVM();
                MedicalConditionVM _obj = _VMObject.SelectObject(ConditionID);
                _obj.IsDeleted = true;
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                _obj.ModifiedBy = userId;
                _obj.Edit();
              
                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }

        public JsonResult _EditMenstrual(int patientID)
        {
            if (patientID == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            MenstrualHistoryVM _obj = new MenstrualHistoryVM();
            _obj= _obj.SelectAllByPatientId(patientID);
            ViewBag.PatientId = patientID;

            if (_obj == null)
            {_obj = new MenstrualHistoryVM();
                _obj.PatientId = patientID;
                _obj.Perimenopausal = false;
                _obj.Postmenopausal = false;
                _obj.Premenopausal = false;
                _obj.Duration = 0;
            }
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("MenstrualHistory", _obj) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string EditMenstrual(MenstrualHistoryVM vm)
        {
            try
            {
                if (vm.Id== 0)
                {
                    vm.Create();

                }
                else {
                    vm.Edit();

                }
                return "Success"; // succcess
            }
            catch (Exception ex)
            {

                return "Error500";

            }
        }

        //_EditAllergy
        public JsonResult _EditAllergy(int patientID)
        {
            if (patientID == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            fillMedicalViewBags(patientID);
            MedicalConditionVM _AddMedicalCondition = new MedicalConditionVM();
            _AddMedicalCondition.PatientId = Convert.ToInt32(patientID);
            _AddMedicalCondition.HistroryType = (int)Helper.Enum.HistoryType.Allergy;
            _AddMedicalCondition.ConditionType = -1;
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("CreateAllergy", _AddMedicalCondition) }, JsonRequestBehavior.AllowGet);
        }

        //_FamilyModal
        public JsonResult _FamilyModal(int patientID)
        {
            if (patientID == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            fillMedicalViewBags(patientID);
            MedicalConditionVM _AddMedicalCondition = new MedicalConditionVM();
            _AddMedicalCondition.PatientId = Convert.ToInt32(patientID);
            _AddMedicalCondition.HistroryType = (int)Helper.Enum.HistoryType.Family;
            _AddMedicalCondition.ConditionType = -1;
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("CreateFamily", _AddMedicalCondition) }, JsonRequestBehavior.AllowGet);
        }
        //_HypertensionModal
        public JsonResult _HypertensionModal(int patientID)
        {
            if (patientID == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            fillMedicalViewBags(patientID);
            MedicalConditionVM _AddMedicalCondition = new MedicalConditionVM();
            _AddMedicalCondition.PatientId = Convert.ToInt32(patientID);
            _AddMedicalCondition.HistroryType = 0;
            _AddMedicalCondition.ConditionType = (int)Helper.Enum.MedicalHistory.Hypertension;
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("CreateHypertension", _AddMedicalCondition) }, JsonRequestBehavior.AllowGet);
        }
        //_DiapitesModal
        public JsonResult _DiapitesModal(int patientID)
        {
            if (patientID == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            fillMedicalViewBags(patientID);
            MedicalConditionVM _AddMedicalCondition = new MedicalConditionVM();
            _AddMedicalCondition.PatientId = Convert.ToInt32(patientID);
            _AddMedicalCondition.HistroryType = 0;
            _AddMedicalCondition.ConditionType = (int)Helper.Enum.MedicalHistory.Diabetes;
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("CreateDiabetes", _AddMedicalCondition) }, JsonRequestBehavior.AllowGet);
        }
    }
}