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
    public class ChemoTherapyCycleDayController : Controller
    {

        // GET: ChemoTherapyCycleDay
        public ActionResult Index(int MainCycleId)
        {

       

            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            List<ChemoTherapyCycleDayVM> DatesList = _Obj.SelectAllByMainCycleId(MainCycleId).OrderBy(a => a.Date).ToList();
         

        
            return View(DatesList);

            // return View(OrderList);
        }



   
    }
}