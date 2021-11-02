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

    public class AttachmentController : Controller
    {
        // GET: Attachment
        public ActionResult Index(int patientId)
        {
                AttachmentVM attachmentObj = new AttachmentVM();
                List<AttachmentVM> AppointmentList = attachmentObj.GetAttachmentsByPatientId(patientId).OrderByDescending(a => a.FileDate).ToList();
                
                ViewBag.PatientId = patientId;
                return View(AppointmentList);
           
        }
        // GET: Clinics/Create
        public ActionResult Create(int patientId)
        {
            ViewBag.PatientId = patientId;
            AttachmentVM _attachemnt = new AttachmentVM();
            _attachemnt.UserId = patientId;
            return View(_attachemnt);
        }

        // POST: Clinics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttachmentVM attchment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (attchment.fileUploded.ContentLength > 0)
                    {
                       string extension = Path.GetExtension(attchment.fileUploded.FileName);
                        Guid NewName = Guid.NewGuid();
                        attchment.FileName =  NewName + extension;
                            // string _FileName = Path.GetFileName(attchment.fileUploded.FileName);
                            string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), NewName.ToString()+extension);
                        attchment.fileUploded.SaveAs(_path);
                    }
                    ViewBag.Message = "File Uploaded Successfully!!";
                    attchment.Create();

                    return RedirectToAction("Index", "Attachment", new { patientId = attchment.UserId });
                }
                catch(Exception ex)
                {
                    ViewBag.Message = "File upload failed!!";
                    return View();
                }
                return RedirectToAction("Index", "Attachment", new { patientId = attchment.UserId });
            }
            else
            {
                return View(attchment);

            }

        }
    }
}