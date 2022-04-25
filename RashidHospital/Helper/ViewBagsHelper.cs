using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RashidHospital
{
    public class ViewBagsHelper
    {
        public static string getPatientInfo(int PatientId)
        {
            PatientVM patientVm = new PatientVM();
            PatientVM patientObj = patientVm.SelectObject(PatientId);
            int Years = new DateTime(DateTime.Now.Subtract(patientObj.BirthDate).Ticks).Year - 1;
            DateTime PastYearDate = patientObj.BirthDate.AddYears(Years);
            string PatientInfo = $"<div > <ul class='columns' data-columns='2'><li>Name:" + 
                patientObj.Name + "</li><li>Arabic Name: "+patientObj.NameArabic+"</li> <li>Patient Id: " + patientObj.MedicalID +
                "</li><li>Diagnosis:" + patientObj.DiagnoseName + "</li><li>Register Date: "
                + patientObj.CreatedDate.ToShortDateString() + "</li><li>age:" + Years.ToString() + "</li><li>Last Visit:" +
                patientObj.LastVisitDate + "</li></ul></div>";
            return PatientInfo;
        }



       




    }
}