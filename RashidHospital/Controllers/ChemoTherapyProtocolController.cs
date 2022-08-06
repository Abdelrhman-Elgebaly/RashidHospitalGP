using Hospital.DAL;
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
    public class ChemoTherapyProtocolController : Controller
    {
        // GET: ChemoTherapyProtocol


        public ActionResult Index(int PatientId)
        {
            fillBag(PatientId);
            ChemoTherapyProtocolVM _Labresults = new ChemoTherapyProtocolVM();
            List<ChemoTherapyProtocolVM> OrderList = _Labresults.SelectAllByPatientID(PatientId);
            fillBag(PatientId);
            return View(OrderList);
        }
        private void fillBag(int patientID)
        {
            PatientVM _patientvm = new PatientVM();
            ViewBag.PatientId = patientID;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;
        }

        public JsonResult Create(int PatientId)
        {
            fillCreateBag();
            if (PatientId == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);


            }
            fillBag(PatientId);
            fillCreateBag();
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("Create", null) }, JsonRequestBehavior.AllowGet);

            //  return View();

        }
        public JsonResult Edit(int PatientId, int Id)
        {
            fillBag(PatientId);

            fillCreateBag();
            fillBag(PatientId);

            ChemoTherapyProtocolVM chemoTherapyProtocolVM = new ChemoTherapyProtocolVM();
            ChemoTherapyProtocolVM chemoTherapyProtocolVM1 = chemoTherapyProtocolVM.SelectObject(Id);
            fillBag(PatientId);
            fillCreateBag();
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("Edit", chemoTherapyProtocolVM1) }, JsonRequestBehavior.AllowGet);

            //  return View();

        }

        public JsonResult GetProrocolList(int DiseaseId)
        {
            //  _context.Configuration.ProxyCreationEnabled = false;
            //   List<protocol> ProtocolList = _context.protocols.Where(x => x.DiseaseId == DiseaseId).ToList();
            ChemoTherapyTemplateVM protocol = new ChemoTherapyTemplateVM();

            ViewBag.ProtocolList = protocol.ProtocolSelectList(DiseaseId);
            return Json(ViewBag.ProtocolList, JsonRequestBehavior.AllowGet);



        }

        private void fillCreateBag()
        {
            ChemoTherapyTemplateVM results = new ChemoTherapyTemplateVM();
            ViewBag.DeasesList = results.DiseaseSelectList();
            //ViewBag.ProtocolList = results.ProtocolSelectList();
            

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
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(ChemoTherapyProtocolVM vm)
        {
            try
            {
             
                vm.Create();

                //PatientId
                return RedirectToAction("Index", new { PatientId = vm.Patient_ID });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error500");
            }
        }
        
        [HttpPost]
        public JsonResult AddResult( int Template_ID, int Patient_ID, int DiseaseId)
        {
            if (  Patient_ID != 0)
            {
                ChemoTherapyProtocolVM _labresults = new ChemoTherapyProtocolVM();
                
                _labresults.Template_ID = Template_ID;
                _labresults.Patient_ID = Patient_ID;
                _labresults.DiseaseId = DiseaseId;
           
                _labresults.Create();
            }

            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditResult(int Template_ID, int Id, int DiseaseId)
        {
          
                ChemoTherapyProtocolVM chemoTherapyProtocolVM = new ChemoTherapyProtocolVM();
                ChemoTherapyProtocolVM chemoTherapyProtocolVM1 = chemoTherapyProtocolVM.SelectObject(Id);
                chemoTherapyProtocolVM1.Template_ID = Template_ID;
              
                chemoTherapyProtocolVM1.DiseaseId = DiseaseId;

                chemoTherapyProtocolVM1.Edit();
        

            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);
        }


    }
}