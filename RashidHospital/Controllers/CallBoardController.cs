using Microsoft.AspNet.Identity;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RashidHospital.Controllers
{
    [Authorize(Roles = "Nurses,Employee,Admin,Consultant, Doctor")]

    public class CallBoardController : Controller
    {
        // GET: CallBoard
        public ActionResult Index()
        {
            CallBoardVM board = new CallBoardVM();
           List<CallBoardVM> callboardList = board.getListByDate(DateTime.Now);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;
            return View(callboardList);
        }

        
        public ActionResult IndexAll()
        {
            CallBoardVM board = new CallBoardVM();
            List<CallBoardVM> callboardList = board.SelectAll();
            return View(callboardList);
        }
        [HttpPost]
        public int CallPatient(int CallBoardId)
        {
            int finalResult = 0;
            try
            {
                CallBoardVM _callBoard = new CallBoardVM();
                CallBoardVM _obj = _callBoard.SelectObject(CallBoardId);
                _obj.CallsNo = _obj.CallsNo + 1 ;
                var userID = User.Identity.GetUserId();
                _obj.DoctorId =Guid.Parse(userID);
                _obj.LastCallTime = DateTime.Now;
                _obj.IsOnCall = true;
                _obj.Edit();
              
                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }

        //[HttpPost]
        //public int SkipCallBoard(int CallBoardId)
        //{
        //    int finalResult = 0;
        //    try
        //    {
        //        CallBoardVM _callBoard = new CallBoardVM();
        //        CallBoardVM _obj = _callBoard.SelectObject(CallBoardId);
        //        _obj.IsSkipped = true;
        //        _obj.Edit();

        //        finalResult = 1;


        //    }
        //    catch (Exception e)
        //    {
        //        finalResult = 6;
        //    }
        //    return finalResult;
        //}
        [HttpPost]
        public int UndoAttened(int CallBoardId)
        {
            int finalResult = 0;
            try
            {
                CallBoardVM _callBoard = new CallBoardVM();
                CallBoardVM _obj = _callBoard.SelectObject(CallBoardId);
                _obj.IsOnCall = false;
                _obj.Edit();

                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }
        
        [HttpGet]
        public ActionResult Edit(int Id) {
            CallBoardVM _callBoard = new CallBoardVM();
           _callBoard= _callBoard.SelectObject(Id);
            _callBoard.Done = true;
            _callBoard.Edit();
            return RedirectToAction("Index");

        }
    }
}