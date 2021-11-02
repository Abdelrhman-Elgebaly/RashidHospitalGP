using Hospital.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RashidHospital.Models
{
    public class DeleteTasksObject
    {
        public int Id { get; set; }
        public string DeleteType { get; set; }
        public string DeleteTaskPreview { get; set; }
        public string ActionUserName { get; set; }
        public List<DeleteTasksObject> DeletedPatient() {
            List<DeleteTasksObject> retList = new List<DeleteTasksObject>();
            Patient patientObject = new Patient();
           List<Patient>PatientList= patientObject.Where(a => a.IsDeleted == true).ToList();
            foreach (Patient item in PatientList)
            {
                DeleteTasksObject _object = new DeleteTasksObject();
                _object.Id = item.Id;
                _object.DeleteType = "Patient";
                _object.DeleteTaskPreview = item.Name + " " + item.MedicalID;
                if (item.ModifiedBy != null)
                {
                    AspNetUser user = new AspNetUser();
                    AspNetUser _user = user.Getobject(item.ModifiedBy);
                    _object.ActionUserName = _user?.FirstName + " " + _user?.SecondName + " " + _user?.ThirdName;
                }
                retList.Add(_object);
            }
            return retList;
        }
        public List<DeleteTasksObject> DeletedAppointment()
        {
            List<DeleteTasksObject> retList = new List<DeleteTasksObject>();
            Appointment Object = new Appointment();
            List<Appointment> List = Object.Where(a => a.IsDeleted == true).ToList();
            foreach (Appointment item in List)
            {
                DeleteTasksObject _object = new DeleteTasksObject();
                _object.Id = item.Id;

                _object.DeleteType = "Appointment";
                _object.DeleteTaskPreview = item.AppointmentDate.ToShortDateString();
                if (item.ModifiedBy != null)
                {
                    AspNetUser user = new AspNetUser();
                    AspNetUser _user = user.Getobject(item.ModifiedBy);
                    _object.ActionUserName = _user?.FirstName + " " + _user?.SecondName + " " + _user?.ThirdName;
                }
                retList.Add(_object);
            }
            return retList;
        }
        public List<DeleteTasksObject> DeletedCallBoard()
        {
            List<DeleteTasksObject> retList = new List<DeleteTasksObject>();
            CallBoard Object = new CallBoard();
            List<CallBoard> List = Object.Where(a => a.IsDeleted == true).ToList();
            foreach (CallBoard item in List)
            {
                DeleteTasksObject _object = new DeleteTasksObject();
                _object.DeleteType = "Callboard";
                _object.DeleteTaskPreview = item.VisitDate.ToShortDateString();
                _object.Id = item.Id;
                if (item.ModifiedBy != null)
                {
                    AspNetUser user = new AspNetUser();
                    AspNetUser _user = user.Getobject(item.ModifiedBy);
                    _object.ActionUserName = _user?.FirstName + " " + _user?.SecondName + " " + _user?.ThirdName;
                }
                retList.Add(_object);
            }
            return retList;
        }
        public List<DeleteTasksObject> DeletedClinic()
        {
            List<DeleteTasksObject> retList = new List<DeleteTasksObject>();
            Clinic Object = new Clinic();
            List<Clinic> List = Object.Where(a => a.IsDeleted == true).ToList();
            foreach (Clinic item in List)
            {
                DeleteTasksObject _object = new DeleteTasksObject();
                _object.DeleteType = "Clinic";
                _object.Id = item.Id;

                _object.DeleteTaskPreview = item.Name.ToString();
                if (item.ModifiedBy != null)
                {
                    AspNetUser user = new AspNetUser();
                    AspNetUser _user = user.Getobject(item.ModifiedBy);
                    _object.ActionUserName = _user?.FirstName + " " + _user?.SecondName + " " + _user?.ThirdName;
                }
                retList.Add(_object);
            }
            return retList;
        }
        public List<DeleteTasksObject> DeletedMedicalCondition()
        {
            List<DeleteTasksObject> retList = new List<DeleteTasksObject>();
            MedicalCondition Object = new MedicalCondition();
            List<MedicalCondition> List = Object.Where(a => a.IsDeleted == true).ToList();
            foreach (MedicalCondition item in List)
            {
                DeleteTasksObject _object = new DeleteTasksObject();
                _object.DeleteType = "MedicalCondition";
                _object.Id = item.Id;

                _object.DeleteTaskPreview = item.Condition;
                if (item.ModifiedBy != null)
                {
                    AspNetUser user = new AspNetUser();
                    AspNetUser _user = user.Getobject(item.ModifiedBy);
                    _object.ActionUserName = _user?.FirstName + " " + _user?.SecondName + " " + _user?.ThirdName;
                }
                retList.Add(_object);
            }
            return retList;
        }

        public List<DeleteTasksObject> DeletedMedicalRecord()
        {
            List<DeleteTasksObject> retList = new List<DeleteTasksObject>();
            MedicalRecord Object = new MedicalRecord();
            List<MedicalRecord> List = Object.Where(a => a.IsDeleted == true).ToList();
            foreach (MedicalRecord item in List)
            {
                DeleteTasksObject _object = new DeleteTasksObject();
                _object.DeleteType = "MedicalRecord";
                _object.Id = item.Id;

                _object.DeleteTaskPreview = item?.Recommendation?.ToString();
                if (item.ModifiedBy != null)
                {
                    AspNetUser user = new AspNetUser();
                    AspNetUser _user = user.Getobject(item.ModifiedBy);
                    _object.ActionUserName = _user?.FirstName + " " + _user?.SecondName + " " + _user?.ThirdName;
                }
                retList.Add(_object);
            }
            return retList;
        }
        public List<DeleteTasksObject> DeletedPatientUnit()
        {
            List<DeleteTasksObject> retList = new List<DeleteTasksObject>();
            PatientUnit Object = new PatientUnit();
            List<PatientUnit> List = Object.Where(a => a.IsDeleted == true).ToList();
            foreach (PatientUnit item in List)
            {
                DeleteTasksObject _object = new DeleteTasksObject();
                _object.DeleteType = "PatientUnit";
                _object.Id = item.Id;

                _object.DeleteTaskPreview = item.Name.ToString();
                if (item.ModifiedBy != null)
                {
                    AspNetUser user = new AspNetUser();
                    AspNetUser _user = user.Getobject(item.ModifiedBy);
                    _object.ActionUserName = _user?.FirstName + " " + _user?.SecondName + " " + _user?.ThirdName;
                }
                retList.Add(_object);
            }
            return retList;
        }
        public List<DeleteTasksObject> DeletedRadiologyRequest()
        {
            List<DeleteTasksObject> retList = new List<DeleteTasksObject>();
            RadiologyRequest Object = new RadiologyRequest();
            List<RadiologyRequest> List = Object.Where(a => a.IsDeleted == true).ToList();
            foreach (RadiologyRequest item in List)
            {
                DeleteTasksObject _object = new DeleteTasksObject();
                _object.DeleteType = "RadiologyRequest";
                _object.Id = item.Id;

                _object.DeleteTaskPreview = item.RequestDate.ToString();
                if (item.ModifiedBy != null)
                {
                    AspNetUser user = new AspNetUser();
                    AspNetUser _user = user.Getobject(item.ModifiedBy);
                    _object.ActionUserName = _user?.FirstName + " " + _user?.SecondName + " " + _user?.ThirdName;
                }
                retList.Add(_object);
            }
            return retList;
        }
        public List<DeleteTasksObject> DeletedRadologyResult()
        {
            List<DeleteTasksObject> retList = new List<DeleteTasksObject>();
            RadiologyResult Object = new RadiologyResult();
            List<RadiologyResult> List = Object.Where(a => a.IsDeleted == true).ToList();
            foreach (RadiologyResult item in List)
            {
                DeleteTasksObject _object = new DeleteTasksObject();
                _object.DeleteType = "RadiologyResult";
                _object.Id = item.Id;

                _object.DeleteTaskPreview = item.RadiologyDate.ToString();
                if (item.ModifiedBy != null)
                {
                    AspNetUser user = new AspNetUser();
                    AspNetUser _user = user.Getobject(item.ModifiedBy);
                    _object.ActionUserName = _user?.FirstName + " " + _user?.SecondName + " " + _user?.ThirdName;
                }
                retList.Add(_object);
            }
            return retList;
        }

        public List<DeleteTasksObject> DeletedPathology()
        {
            List<DeleteTasksObject> retList = new List<DeleteTasksObject>();
            Pathology Object = new Pathology();
            List<Pathology> List = Object.Where(a => a.IsDeleted == true).ToList();
            foreach (Pathology item in List)
            {
                DeleteTasksObject _object = new DeleteTasksObject();
                _object.DeleteType = "Pathology";
                _object.Id = item.Id;

                _object.DeleteTaskPreview = item.Date.ToString();
                if (item.ModifiedBy != null)
                {
                    AspNetUser user = new AspNetUser();
                    AspNetUser _user = user.Getobject(item.ModifiedBy);
                    _object.ActionUserName = _user?.FirstName + " " + _user?.SecondName + " " + _user?.ThirdName;
                }
                retList.Add(_object);
            }
            return retList;
        }
    }
}