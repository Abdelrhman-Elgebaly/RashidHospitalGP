using Hospital.DAL;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RashidHospital.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DeleteTasksController : Controller
    {
        // GET: DeleteTasks
        public ActionResult Index()
        {
            DeleteTasksObject _deleteobject = new DeleteTasksObject();
            List<DeleteTasksObject> retList = new List<DeleteTasksObject>();
            retList.AddRange(_deleteobject.DeletedAppointment());
            retList.AddRange(_deleteobject.DeletedCallBoard());
            retList.AddRange(_deleteobject.DeletedClinic());
            retList.AddRange(_deleteobject.DeletedMedicalCondition());
            retList.AddRange(_deleteobject.DeletedMedicalRecord());
            retList.AddRange(_deleteobject.DeletedPathology());
            retList.AddRange(_deleteobject.DeletedPatient());
            retList.AddRange(_deleteobject.DeletedPatientUnit());
            retList.AddRange(_deleteobject.DeletedRadiologyRequest());
            retList.AddRange(_deleteobject.DeletedRadologyResult());

            return View(retList);
        }

        [HttpPost]
        public JsonResult Delete(int AccountId,string DeleteType)
        {
            int finalResult = 0;
            try
            {
                if (AccountId==0)
                {
                    return Json(new { IsRedirect = false, Content = 1 }, JsonRequestBehavior.AllowGet);

                }
                if (DeleteType == "Appointment") {
                    AppointmentVM Object = new AppointmentVM();
                    AppointmentVM _Object = Object.SelectObject(AccountId);

                    CallBoardVM ObjectCallBoard = new CallBoardVM();
                    List<CallBoardVM> _ListCallBoard = ObjectCallBoard.SelectAll().Where(a => a.AppointmentId == AccountId).ToList();
                    foreach (var item in _ListCallBoard)
                    {
                        item.Delete();
                    }

                    _Object.Delete();
                } else if (DeleteType == "Patient") {
                    PatientVM Object = new PatientVM();
                    PatientVM _Object = Object.SelectObject(AccountId);
                    if (_Object != null) {
                        CallBoardVM Call = new CallBoardVM();
                        List<CallBoardVM> _Call = Call.SelectAll().Where(a => a.PatientId == _Object.Id).ToList();
                        foreach (var item in _Call)
                        {
                            item.Delete();

                        }
                        AppointmentVM appoint = new AppointmentVM();
                        List<AppointmentVM> _appoint = appoint.SelectAll().Where(a => a.PatientId == _Object.Id).ToList();
                        foreach (var item in _appoint)
                        {
                            item.Delete();

                        }
                        MedicalConditionVM Condition = new MedicalConditionVM();
                       List<MedicalConditionVM> _condition = Condition.SelectAll().Where(a => a.PatientId==_Object.Id).ToList();
                        foreach (var item in _condition)
                        {
                            item.Delete();

                        }

                        MedicalRecordVM record = new MedicalRecordVM();
                        List<MedicalRecordVM> _record = record.SelectAll().Where(a => a.PatientID == _Object.Id).ToList();
                        foreach (var item in _record)
                        {
                            item.Delete();

                        }

                        RadiologyRequestVM radiology = new RadiologyRequestVM();
                        List<RadiologyRequestVM> _radiology = radiology.SelectAll().Where(a => a.PateintID == _Object.Id).ToList();
                        foreach (var item in _radiology)
                        {
                            item.Delete();

                        }

                        RadiologyResultVM radiologyres = new RadiologyResultVM();
                        List<RadiologyResultVM> _radiologyres = radiologyres.SelectAllByPatientId(_Object.Id).ToList();
                        foreach (var item in _radiologyres)
                        {
                            item.Delete();

                        }
                        LabResualtVM labRes = new LabResualtVM();
                        List<LabResualtVM> _labRes = labRes.SelectAllByPatientId(_Object.Id).ToList();
                        foreach (var item in _labRes)
                        {
                            item.Delete();

                        }

                        LabOrderVM labOrder = new LabOrderVM();
                        List<LabOrderVM> _labOrder = labOrder.SelectAllByPatientId(_Object.Id).ToList();
                        foreach (var item in _labOrder)
                        {
                            item.Delete();

                        }
                        PathologyVM pathology = new PathologyVM();
                        List<PathologyVM> _pathology = pathology.SelectAllByPatientId( _Object.Id).ToList();
                        foreach (var item in _pathology)
                        {
                            item.Delete();

                        }

                        
                        _Object.Delete();

                    }
                }
                else if (DeleteType == "Callboard")
                {
                    CallBoardVM Object = new CallBoardVM();
                    CallBoardVM _Object = Object.SelectObject(AccountId);
                    _Object.Delete();
                }
                else if (DeleteType == "Clinic")
                {
                    ClinicVM Object = new ClinicVM();
                    ClinicVM _Object = Object.SelectObject(AccountId);

                    AppointmentVM AppointmentObject = new AppointmentVM();
                    List<AppointmentVM> _ListAppointments = AppointmentObject.SelectAll().Where(a=>a.ClinicId == AccountId).ToList();
                    foreach (AppointmentVM item in _ListAppointments)
                    {
                        item.Delete();
                    }

                    MedicalRecordVM MedicalRecordObject = new MedicalRecordVM();
                    List<MedicalRecordVM> _ListMedicalRecord =MedicalRecordObject.SelectAll().Where(a => a.ClinicId == AccountId).ToList();
                    foreach (var item in _ListMedicalRecord)
                    {
                        item.Delete();
                    }

                    _Object.Delete();
                }
                else if (DeleteType == "MedicalCondition")
                {
                    MedicalConditionVM Object = new MedicalConditionVM();
                    MedicalConditionVM _Object = Object.SelectObject(AccountId);
                    _Object.Delete();
                }
                else if (DeleteType == "MedicalRecord")
                {
                    MedicalRecordVM Object = new MedicalRecordVM();
                    MedicalRecordVM _Object = Object.SelectObject(AccountId);
                    _Object.Delete();
                }
                else if (DeleteType == "PatientUnit")
                {
                    PatientUnitVM Object = new PatientUnitVM();
                    PatientUnitVM _Object = Object.SelectObject(AccountId);
                    _Object.Delete();
                }
                else if (DeleteType == "RadiologyRequest")
                {
                    RadiologyRequestVM Object = new RadiologyRequestVM();
                    RadiologyRequestVM _Object = Object.SelectObject(AccountId);
                    _Object.Delete();
                }
                else if (DeleteType == "RadiologyResult")
                {
                    RadiologyResultVM Object = new RadiologyResultVM();
                    RadiologyResultVM _Object = Object.SelectObject(AccountId);
                    _Object.Delete();
                }
                else
                {
                    PathologyVM Object = new PathologyVM();
                    PathologyVM _Object = Object.SelectObject(AccountId);
                    _Object.Delete();
                }
            return Json(new { IsRedirect = false, Content =1 }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception e)
            {
                return Json(new { IsRedirect = false, Content =6 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}