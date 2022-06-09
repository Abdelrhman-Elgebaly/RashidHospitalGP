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
    public class PatientDoseController : Controller
    {
        /*
        public ActionResult Index(int patientID,int noteID)
        {
            //Get Patient
            PatientVM _Obj = new PatientVM();
            PatientVM _objVM = _Obj.SelectObject(patientID);

          


            // Get drug

            //    fillBag(_objVM.ChemoTherapyId);
            ChemoTherapyDrugVM _cObjVm = new ChemoTherapyDrugVM();
            List<ChemoTherapyDrugVM> _list = _cObjVm.SelectAllByTemplateId(_objVM.ChemoTherapyId);

            //GetNureseNote
    
            NurseNoteVM _nObj = new NurseNoteVM();
            NurseNoteVM _nobjVM = _nObj.SelectObject(noteID);

            // Calculate 
            PatientDoseVM patientDose = new PatientDoseVM();

            patientDose.Patient_ID = _objVM.Id;
            patientDose.Template_ID = _objVM.ChemoTherapyId;
            patientDose.NurseNote_ID = _nobjVM.ID;

            if(_cObjVm.Unit == "mg/m2")
            {
                patientDose.Dose_Calculated = _cObjVm.Drug_Dose* _nobjVM.SA;
            }
          
                 if (_cObjVm.Unit == "mg/kg")
            {
                patientDose.Dose_Calculated = _cObjVm.Drug_Dose * _nobjVM.Weight;
            }

            if (_cObjVm.Unit == "AUC") 
            {
                int age = DateTime.Now.Year - _objVM.BirthDate.Year;
                var weight = _nobjVM.Weight;
                var serum_creatinine = 8;

                var creat_clearance = (140 - age) * weight / (72 * serum_creatinine);



                if (_objVM.Gender == "male")

                {
                    
                    patientDose.Dose_Calculated = _cObjVm.Drug_Dose * (creat_clearance + 25);
                }
                if (_objVM.Gender == "female")
                {
                    creat_clearance= creat_clearance * 0.85;
                    patientDose.Dose_Calculated = _cObjVm.Drug_Dose * (creat_clearance + 25);

                }
            }
            //return (_list);

        }

        */


        private void fillBag(int patientID)
        {
            ViewBag.PatientId = patientID;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }

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


        public ActionResult Calculate(int id, int noteId, string patientID)
        {

            ChemoTherapyDrugVM _Obj = new ChemoTherapyDrugVM();
            ChemoTherapyDrugVM ObjVm = _Obj.SelectObject(id);

            NurseNoteVM _nObj = new NurseNoteVM();
            NurseNoteVM _nobjVM = _nObj.SelectObject(noteId);

            int _patientID = Convert.ToInt32(patientID);
            fillBag(_patientID);
            PatientVM _pObj = new PatientVM();
            PatientVM _pobjVM = _pObj.SelectObject(_patientID);




            if (ObjVm.Unit == "mg/m2")
            {
                _nobjVM.Dose_Calculated = ObjVm.Drug_Dose * _nobjVM.SA;
                _nobjVM.Edit();
            }

            else if (ObjVm.Unit == "mg/kg")
            {
                _nobjVM.Dose_Calculated = ObjVm.Drug_Dose * _nobjVM.Weight;
                _nobjVM.Edit();

            }

            else if (ObjVm.Unit == "AUC")
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



            }
    }

