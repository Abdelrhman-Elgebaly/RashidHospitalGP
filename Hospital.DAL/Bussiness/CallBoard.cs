using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class CallBoard : DbBaseClass<CallBoard>
    {
        public int GetPatientNumber(DateTime date)
        {
           return _Db.CallBoards.Where(a => 
             a.VisitDate.Year == date.Date.Year
            && a.VisitDate.Month == date.Date.Month
            && a.VisitDate.Day == date.Date.Day).Count();
           

        }
        public List<CallBoard> getListByDate(DateTime date) {
            return _Db.CallBoards.Where(a =>
            a.VisitDate.Year == date.Date.Year
           && a.VisitDate.Month == date.Date.Month
           && a.VisitDate.Day == date.Date.Day).ToList();

        }

        public CallBoard getByDateandPatient(DateTime date,int PatientId)
        {
            return _Db.CallBoards.Where(a =>
            a.VisitDate.Year == date.Date.Year
           && a.VisitDate.Month == date.Date.Month
           && a.VisitDate.Day == date.Date.Day&& a.PatientId==PatientId).FirstOrDefault();

        }

    }
}
