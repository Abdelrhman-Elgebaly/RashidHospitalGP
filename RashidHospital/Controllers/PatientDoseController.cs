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
      
      
     

        public ActionResult  Index(int NoteId, int CycleId,int pid)
        {
            CalculateDose(NoteId, CycleId,pid);
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDayVM _Objm = _Obj.SelectObject(CycleId);
            _Objm.SelectedNnote = NoteId;
            _Objm.Edit();
            fillBag(NoteId, CycleId, pid);

            ChemoTherapyProtocolVM _Labresults = new ChemoTherapyProtocolVM();
            ChemoTherapyProtocolVM _Objmp = _Labresults.SelectObject(_Objm.TemplateId);

            int _templateID = Convert.ToInt32(_Objmp.Template_ID);

            PatientDoseVM ObjVm = new PatientDoseVM();
            List<PatientDoseVM> _Doselist = ObjVm.SelectAll(NoteId, CycleId);

            ChemoTherapyDrugVM ObjVmd = new ChemoTherapyDrugVM();
            List<ChemoTherapyDrugVM> _Druglist = ObjVmd.SelectAllByTemplateId(_templateID);


          



            return View(_Doselist);
        }
        public ActionResult Summery(int NoteId, int CycleId, int pid)
        {

            fillBag(NoteId, CycleId, pid);

            // Patient Info
            PatientVM _pObj = new PatientVM();
            PatientVM _pobjVM = _pObj.SelectObject(pid);

            // Cycles Info
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDayVM _Objm = _Obj.SelectObject(CycleId);

            // Patient Dose
            PatientDoseVM ObjVm = new PatientDoseVM();
            List<PatientDoseVM> _Doselist = ObjVm.SelectAll(NoteId, CycleId);
            NurseNoteVM _nObj = new NurseNoteVM();
            NurseNoteVM _nObjm = _nObj.SelectObject(NoteId);
            if(_Objm.IsReleased == true || _Objm.IsPending == true || _Objm.IsApproved == true)
            {
                _nObjm.IsSelected = true;
            }
            var tuple = new Tuple<PatientVM, ChemoTherapyCycleDayVM, List<PatientDoseVM>>(_pobjVM, _Objm, _Doselist);



            return View(tuple);
        }

            [HttpPost]
        public JsonResult CalculateDose(int NoteId, int CycleId,int pid)
        {
            PatientDoseVM pObjVm = new PatientDoseVM();
            List<PatientDoseVM> _Doselist = pObjVm.SelectAll(NoteId, CycleId);

                        if (_Doselist.Count == 0) {
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
                        double test = Convert.ToDouble(ObjVm.Dose_Calculated);
                        ObjVm.Dose_Calculated = Math.Round(test, 2);
                    }

                    else if (item.Unit == 2)
                    {
                        ObjVm.Dose_Calculated = item.Drug_Dose * _nobjVM.Weight;
                        double test = Convert.ToDouble(ObjVm.Dose_Calculated);
                        ObjVm.Dose_Calculated = Math.Round(test, 2);

                    }
                    else if (item.Unit == 3 || item.Unit == 4)
                    {
                        ObjVm.Dose_Calculated = item.Drug_Dose;


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

                        if (serum_creatinine == 0)
                        {
                            ObjVm.Dose_Calculated = null;
                        }
                        else
                        {

                            var creat_clearance = (140 - age) * weight / (72 * serum_creatinine);



                            if (_pobjVM.Gender == "Male")

                            {

                                ObjVm.Dose_Calculated = item.Drug_Dose * (creat_clearance + 25);
                                double test = Convert.ToDouble(ObjVm.Dose_Calculated);
                                ObjVm.Dose_Calculated = Math.Round(test, 2);
                            }
                            if (_pobjVM.Gender == "Female")
                            {
                                creat_clearance = creat_clearance * 0.85;
                                ObjVm.Dose_Calculated = item.Drug_Dose * (creat_clearance + 25);
                                double test = Convert.ToDouble(ObjVm.Dose_Calculated);
                                ObjVm.Dose_Calculated = Math.Round(test, 2);

                            }
                        }

                    }

                    ObjVm.Drug_Name = item.Drug_Name;
                    ObjVm.Drug_Dose = item.Drug_Dose;
                    ObjVm.Unit_Value = item.Unit_Value;
                    ObjVm.Fluid_Vol = item.Fluid_Vol;
                    ObjVm.MainDrugId = item.Drug_ID;
                    ObjVm.Patient_ID = pid;
                    ObjVm.Date = _Objm.Date;


                    ObjVm.Create();

                }






            }

            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);

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
        public JsonResult _EditNote(int Id)
        {
            ViewBag.Id = Id;
            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            ViewBag.Id = Id;

            PatientDoseVM _Obj = new PatientDoseVM();
            PatientDoseVM _ObjvM = _Obj.SelectObject(Id);
            //if (_objVM == null)
            //{
            //    return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            //}

            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_EditNote", _ObjvM) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddNote(int Id, string Note, int Dose , string Fluid)

        {
            if(User.IsInAnyRoles("Pharmacist") == false)
    {
                PatientDoseVM _Obj = new PatientDoseVM();
                PatientDoseVM _ObjvM = _Obj.SelectObject(Id);
                _ObjvM.Dose_Calculated = Dose;
                _ObjvM.Fluid_Vol = Fluid;

                _ObjvM.Pharmacist_Note = Note;
                _ObjvM.IsEditByDoctor = true;
                _ObjvM.IsEditByPharmacy = false;

                _ObjvM.Edit();
            }
            if (User.IsInAnyRoles("Pharmacist"))
            {
                PatientDoseVM _Obj = new PatientDoseVM();
                PatientDoseVM _ObjvM = _Obj.SelectObject(Id);
                _ObjvM.Dose_Calculated = Dose;
                _ObjvM.Fluid_Vol = Fluid;
                _ObjvM.Pharmacist_Note = Note;
                _ObjvM.IsEditByPharmacy = true;
                _ObjvM.IsEditByDoctor = false;

                _ObjvM.Edit();
            }

            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);

        }
        //Dr Role
        public int ReleasedToPharmacy(int CycleId)
        {
            int finalResult = 0;
            try
            {
                ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
                ChemoTherapyCycleDayVM _Objm = _Obj.SelectObject(CycleId);
                _Objm.IsReleased = true;
                _Objm.IsPending = false;
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                _Objm.DoctorId = userId;
                _Objm.Edit();

                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }

        public int ApprovedAfterPending(int CycleId)
        {
            int finalResult = 0;
            try
            {
                ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
                ChemoTherapyCycleDayVM _Objm = _Obj.SelectObject(CycleId);
                _Objm.IsReleased = true;
                _Objm.IsPending = false;
                _Objm.DrStatues = "Approved";
                _Objm.Edit();

                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }

        public int DisApprovedAfterPending(int CycleId)
        {
            int finalResult = 0;
            try
            {
                ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
                ChemoTherapyCycleDayVM _Objm = _Obj.SelectObject(CycleId);
                _Objm.IsReleased = true;
                _Objm.IsPending = false;
                _Objm.DrStatues = "disApproved";
                _Objm.Edit();

                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }



        //Pharmacy Role
        public int ReleasedToDoctor(int CycleId)
        {
            int finalResult = 0;
            try
            {
                ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
                ChemoTherapyCycleDayVM _Objm = _Obj.SelectObject(CycleId);
                _Objm.IsReleased = false;
                _Objm.IsPending = true;
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                _Objm.PharmacistId = userId;
                _Objm.Edit();

                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }

        [HttpPost]
        public int FinalApproved(int CycleId)
        {
            int finalResult = 0;
            try
            {
                ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
                ChemoTherapyCycleDayVM _Objm = _Obj.SelectObject(CycleId);
                _Objm.IsReleased = false;
                _Objm.IsPending = false;
                _Objm.IsStart = false;
                _Objm.IsApproved = true;

                _Objm.Edit();
                int NoteId = Convert.ToInt32(_Objm.SelectedNnote);
                PatientDoseVM ObjVm = new PatientDoseVM();
                List<PatientDoseVM> _Doselist = ObjVm.SelectAll(NoteId, CycleId);
                foreach (var item in _Doselist)
                {
                    item.IsApproved = true;
                    item.Edit();
                }
               
                finalResult = 1;
             

            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }


        /*
        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string GridHtml)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }
        }

        */


        [HttpPost]
        public int Omit(int Id)
        {
            int finalResult = 0;
            try
            {
                PatientDoseVM _resultVM = new PatientDoseVM();
                PatientDoseVM DeleteObject = _resultVM.SelectObject(Id);
        
        
                DeleteObject.Delete();


                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }




    }
    }

