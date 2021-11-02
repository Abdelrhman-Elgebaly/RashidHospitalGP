using Hospital.DAL;
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
    [Authorize(Roles = "Doctor, Residents,Admin,Assistant lecturer,Consultant,Nurses,physician,Employee,Assistant,Pharmacist")]
    public class LabRequestController : Controller
    {
        // GET: LabRequest
        public ActionResult Index(int PatientId)
        {
            LabOrderVM _LabOrder = new LabOrderVM();
          List<LabOrderVM> OrderList =  _LabOrder.SelectAllByPatientId(PatientId);
            fillBag(PatientId);
            return View(OrderList);
        }

        public ActionResult IndexDetails(int LabOrderId)
        {
            OrderDetailVM _Order = new OrderDetailVM();
            List<OrderDetailVM> _List = _Order.SelectAllByLabOrderId(LabOrderId);
            LabOrderVM _LabOrder = new LabOrderVM();
            LabOrderVM obj = _LabOrder.SelectObject(LabOrderId);
            ViewBag.Note = obj.Note;
            fillBag(obj.PatinentId);
            return View(_List);
        }
        private void fillBag(int patientID)
        {
            PatientVM _patientvm = new PatientVM();
            ViewBag.PatientId = patientID;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;
        }

        [HttpPost]
        public int Delete(int ID)
        {
            int finalResult = 0;
            try
            {
                OrderDetailVM _OrderVM = new OrderDetailVM();
                List<OrderDetailVM> _List = _OrderVM.SelectAllByLabOrderId(ID);
                foreach (var _obj in _List) {
                    _obj.Delete();
                }
                LabOrderVM _labOrdervm = new LabOrderVM();
                LabOrderVM DeleteObject = _labOrdervm.SelectObject(ID);
                DeleteObject.Delete();

                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }
        [HttpGet]

        public ActionResult Create(int PatientId)
        {
            if (PatientId == null)
            {
                return View("Error500");

            }

            fillBag(PatientId);
            return View();

        }

        [HttpPost]
        public ActionResult Create(OrderDetailVM vm)
        {
            try
            {
                LabOrderVM _labOrder = new LabOrderVM();
                _labOrder.PatinentId = vm.PatientId;
                _labOrder.OrderDate = DateTime.Now;
                _labOrder.Note = vm.Note;
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                _labOrder.DoctorId = userId;
               int OrderId= _labOrder.Create();
                foreach (var selected in vm.selectedTypes) {
                    OrderDetailVM _newdetails = new OrderDetailVM();
                    _newdetails.LabOrderId = OrderId;
                    _newdetails.Type = (int)((LabTyps)Enum.Parse(typeof(LabTyps), selected.ToString()));
                    _newdetails.Create();

                }
                //PatientId
                return RedirectToAction("Index", new { PatientId = vm.PatientId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error500");
            }
        }
    }
}