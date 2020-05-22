using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class Appointment : DbBaseClass<Appointment>
    {
        public List<Appointment> GetAppointmentsByPatientId(int PatientId,DateTime date)
        {
            List<Appointment> medicalList = _Db.Appointments.Where(a => a.PatientId == PatientId 
            && a.AppointmentDate.Year == date.Date.Year
            && a.AppointmentDate.Month == date.Date.Month
            && a.AppointmentDate.Day == date.Date.Day).ToList();
            return medicalList;

        }
        public List<Appointment> GetAppointmentsByDate( DateTime date)
        {
            List<Appointment> medicalList = _Db.Appointments.Where(a => 
            a.AppointmentDate.Year == date.Date.Year
            && a.AppointmentDate.Month == date.Date.Month
            && a.AppointmentDate.Day == date.Date.Day).ToList();
            return medicalList;

        }

        public int GetClinicsAppointmentsCount(int ClinicId, DateTime date)
        {
            return _Db.Appointments.Where(a => a.ClinicId == ClinicId
            && a.AppointmentDate.Year == date.Date.Year
            && a.AppointmentDate.Month == date.Date.Month
            && a.AppointmentDate.Day == date.Date.Day).Count();
         

        }

    }
}
