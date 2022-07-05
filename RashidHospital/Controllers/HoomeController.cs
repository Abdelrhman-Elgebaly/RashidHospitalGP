
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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


public class HoomeController : Controller
    {
       



        private readonly Model1 _context;


        public HoomeController(Model1 context)
        {
            _context = context;

        }


        public ActionResult Index()
        {
            List<Disease> DiseaseList = _context.Diseases.ToList();
            ViewBag.DiseaseList = new SelectList(DiseaseList, "DiseaseId", "DiseaseName");
           
            return View();
        }

        public JsonResult GetProrocolList(int DiseaseId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            List<protocol> ProtocolList = _context.protocols.Where(x => x.DiseaseId == DiseaseId).ToList();
            return Json(ProtocolList, JsonRequestBehavior.AllowGet);

        }

    }
}