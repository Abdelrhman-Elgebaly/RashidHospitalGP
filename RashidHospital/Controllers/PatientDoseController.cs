using Microsoft.AspNet.Identity;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static RashidHospital.Helper.Enum;
using System;
using System.Net.Http;
using System.Threading.Tasks;
namespace RashidHospital.Controllers
{
    public class PatientDoseController : Controller
    {
   

        private void fillBag(int NoteId, int CycleId, int pid)
        {
            ViewBag.PatientId = pid;
           
            ViewBag.CycleId = CycleId;
            ViewBag.NoteId = NoteId;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(pid);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }
        /*
        public ActionResult Index(string patientID, int noteId)
        {
            int _patientID = Convert.ToInt32(patientID);
            fillBag(_patientID);
            PatientVM _Obj = new PatientVM();
            PatientVM _objVM = _Obj.SelectObject(_patientID);



            int _templateID = Convert.ToInt32(_objVM.ChemoTherapyId);
            //  fillBag(_templateID);

            ChemoTherapyDrugVM ObjVm = new ChemoTherapyDrugVM();
          
            List<ChemoTherapyDrugVM> _list = ObjVm.SelectAllByTemplateId(_templateID);

            NurseNoteVM _nObj = new NurseNoteVM();
            NurseNoteVM _nobjVM = _nObj.SelectObject(noteId);


           
           
           


         
                var tuple = new Tuple<NurseNoteVM, List<ChemoTherapyDrugVM>>(_nobjVM, _list);

                return View(tuple);

            

            }

        */
        public ActionResult Calculate(int id, int noteId, string patientID)
        {

            ChemoTherapyDrugVM _Obj = new ChemoTherapyDrugVM();
            ChemoTherapyDrugVM ObjVm = _Obj.SelectObject(id);

            NurseNoteVM _nObj = new NurseNoteVM();
            NurseNoteVM _nobjVM = _nObj.SelectObject(noteId);

            int _patientID = Convert.ToInt32(patientID);
            fillBag(id, noteId, _patientID);
            PatientVM _pObj = new PatientVM();
            PatientVM _pobjVM = _pObj.SelectObject(_patientID);




            if (ObjVm.Unit == 1)
            {
                _nobjVM.Dose_Calculated = ObjVm.Drug_Dose * _nobjVM.SA;
                _nobjVM.Edit();
                
            }

            else if (ObjVm.Unit == 2)
            {
                _nobjVM.Dose_Calculated = ObjVm.Drug_Dose * _nobjVM.Weight;
                _nobjVM.Edit();

            }

            else if (ObjVm.Unit == 5)
            {
                int age = DateTime.Now.Year - _pobjVM.BirthDate.Year;
                var weight = _nobjVM.Weight;
                var serum_creatinine = 8;

                var creat_clearance = (140 - age) * weight / (72 * serum_creatinine);



                if (_pobjVM.Gender == "Male")

                {

                    _nobjVM.Dose_Calculated = ObjVm.Drug_Dose * (creat_clearance + 25);
                    _nobjVM.Edit();
                }
                if (_pobjVM.Gender == "Female")
                {
                    creat_clearance = creat_clearance * 0.85;
                    _nobjVM.Dose_Calculated = ObjVm.Drug_Dose * (creat_clearance + 25);
                    _nobjVM.Edit();

                }

            }


                return RedirectToAction("Index", new { patientID = _patientID, noteId = noteId });


        }


        public ActionResult  Test(int NoteId, int CycleId,int pid)
        {
            CalculateDose(NoteId, CycleId,pid);
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDayVM _Objm = _Obj.SelectObject(CycleId);
            fillBag(NoteId, CycleId, pid);

            ChemoTherapyProtocolVM _Labresults = new ChemoTherapyProtocolVM();
            ChemoTherapyProtocolVM _Objmp = _Labresults.SelectObject(_Objm.TemplateId);

            int _templateID = Convert.ToInt32(_Objmp.Template_ID);

            PatientDoseVM ObjVm = new PatientDoseVM();
            List<PatientDoseVM> _Doselist = ObjVm.SelectAll(NoteId, CycleId);

            ChemoTherapyDrugVM ObjVmd = new ChemoTherapyDrugVM();
            List<ChemoTherapyDrugVM> _Druglist = ObjVmd.SelectAllByTemplateId(_templateID);


          


         //  var tuple = new Tuple<List<PatientDoseVM>, List<ChemoTherapyDrugVM> , ChemoTherapyProtocolVM>(_Doselist, _Druglist, _Objmp);

            return View(_Doselist);
        }
        public ActionResult Summery(int NoteId, int CycleId, int pid)
        {
            // Patient Info
            PatientVM _pObj = new PatientVM();
            PatientVM _pobjVM = _pObj.SelectObject(pid);

            // Cycles Info
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDayVM _Objm = _Obj.SelectObject(CycleId);

            // Patient Dose
            PatientDoseVM ObjVm = new PatientDoseVM();
            List<PatientDoseVM> _Doselist = ObjVm.SelectAll(NoteId, CycleId);


              var tuple = new Tuple<PatientVM, ChemoTherapyCycleDayVM, List<PatientDoseVM>>(_pobjVM, _Objm, _Doselist);

            fillBag(NoteId, CycleId, pid);


            return View(tuple);
        }

            [HttpPost]
        public JsonResult CalculateDose(int NoteId, int CycleId,int pid)
        {
            PatientDoseVM pObjVm = new PatientDoseVM();
            List<PatientDoseVM> _Doselist = pObjVm.SelectAll(NoteId, CycleId);
            if (_Doselist.Count == 0)
            {

                ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
                ChemoTherapyCycleDayVM _Objm = _Obj.SelectObject(CycleId);


                ChemoTherapyProtocolVM _Labresults = new ChemoTherapyProtocolVM();
                ChemoTherapyProtocolVM _Objmp = _Labresults.SelectObject(_Objm.TemplateId);

                int _templateID = Convert.ToInt32(_Objmp.Template_ID);



                ChemoTherapyDrugVM ObjVmd = new ChemoTherapyDrugVM();
                List<ChemoTherapyDrugVM> _Druglist = ObjVmd.SelectAllByTemplateId(_templateID);
                NurseNoteVM _nobj = new NurseNoteVM();
                NurseNoteVM _nobjVM = _nobj.SelectObject(NoteId);
                foreach (var item in _Druglist)
                {
                    PatientDoseVM ObjVm = new PatientDoseVM();
                    ObjVm.Cycle_ID = CycleId;
                    ObjVm.NurseNote_ID = NoteId;
                    ObjVm.Therapy_Type = item.Therapy_Type;
                    if (item.Unit == 1)
                    {
                        ObjVm.Dose_Calculated = item.Drug_Dose * _nobjVM.SA;

                    }

                    else if (item.Unit == 2)
                    {
                        ObjVm.Dose_Calculated = item.Drug_Dose * _nobjVM.Weight;


                    }
                    else if (item.Unit == 5)
                    {
                        PatientVM _pObj = new PatientVM();
                        PatientVM _pobjVM = _pObj.SelectObject(pid);
                        int age = DateTime.Now.Year - _pobjVM.BirthDate.Year;
                        var weight = _nobjVM.Weight;
                        var serum_creatinine = 0 ;
                        ChemoTherapyCyclePackageVM _ccObj = new ChemoTherapyCyclePackageVM();

                      
                        List<ChemoTherapyCyclePackageVM> LabList = _ccObj.SelectAllByCycleID(CycleId);
                        foreach (var itemm in LabList)
                        {
                            ChemoTherapyCyclePackageVM Objvm = _ccObj.SelectObject(itemm.ID);
                            if (Objvm.Test_TypeValue == "Ceratinine")
                            {
                                serum_creatinine = Convert.ToInt32(Objvm.Actual_Value);
                            }
                           
                        }


                        var creat_clearance = (140 - age) * weight / (72 * serum_creatinine);

                        

                        if (_pobjVM.Gender == "Male")

                        {

                            ObjVm.Dose_Calculated = item.Drug_Dose * (creat_clearance + 25);
                           
                        }
                        if (_pobjVM.Gender == "Female")
                        {
                            creat_clearance = creat_clearance * 0.85;
                            ObjVm.Dose_Calculated = item.Drug_Dose * (creat_clearance + 25);
                            

                        }

                    }
                    ObjVm.Drug_Name = item.Drug_Name;
                    ObjVm.Drug_Dose = item.Drug_Dose;
                    ObjVm.Unit_Value = item.Unit_Value;
                    ObjVm.Fluid_Vol = item.Fluid_Vol;
                    ObjVm.Create();

                }


            }





            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);

        }




    }
    }

