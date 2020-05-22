using Hospital.DAL;
using RashidHospital.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using static RashidHospital.Helper.Enum;

namespace RashidHospital.Models
{
    public class PatientVM : BusinessBaseClass<Patient, PatientVM>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [RegularExpression(@"(201)[0-9]{9}",
                            ErrorMessage = "Please enter a valid Mobile")]
        public string PhoneNumber1 { get; set; }

        [Required]
        [RegularExpression(@"(201)[0-9]{9}",
                            ErrorMessage = "Please enter a valid Mobile")]

        public string PhoneNumber2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [RegularExpression(@"(2|3)[0-9][1-9][0-1][1-9][0-3][1-9](01|02|03|04|11|12|13|14|15|16|17|18|19|21|22|23|24|25|26|27|28|29|31|32|33|34|35|88)\d\d\d\d\d",
                            ErrorMessage = "Please enter a valid ID Number")]

        public string NationalID { get; set; }

        
        public string MedicalID { get; set; }
        [Required]
        public string MaritalStatus { get; set; }
        [Required]
        public int UnitID { get; set; }
        [Required]
        public string Gender { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
        [Required]
        [StringLength(50)]
        public string Address1 { get; set; }

        public int? NoOfChildren { get; set; }

        public int DiagnoseId { get; set; }
        public string DiagnoseName { get; set; }

        [Required]
        public string NameArabic { get; set; }
        public string LastVisitDate { get; set; }


        [Required]
        [RegularExpression(@"(201)[0-9]{9}",
                            ErrorMessage = "Please enter a valid Mobile")]
        public string PhoneNumber3 { get; set; }

        public bool IsDeleted { get; set; }
        public string[] Genders = new[] { "Male", "Female" };
        public string[] MaritalStatusList = new[] { "Single", "Married", "Widow/widower", "Divorced"};
        public string PatientUnitName { get; set; }
        public bool FromCallBoard { get; set; }

        internal override Patient Convert(PatientVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new Patient
                {
                    Id = Obj.Id,
                    Name = Obj.Name,
                    Address1 = Obj.Address1,
                    BirthDate = Obj.BirthDate,
                    City = Obj.City,
                    Gender = Obj.Gender,
                    IsDeleted = Obj.IsDeleted,
                    MedicalID = Obj.MedicalID,
                    ModifiedDate = Obj.ModifiedDate,
                    NameArabic = Obj.NameArabic,
                    MaritalStatus = Obj.MaritalStatus,
                    NationalID = Obj.NationalID,
                    PhoneNumber1 = Obj.PhoneNumber1,
                    NoOfChildren = Obj.NoOfChildren,
                    PhoneNumber2 = Obj.PhoneNumber2,
                    UnitID = Obj.UnitID,
                    CreatedDate = Obj.CreatedDate,
                    LastVisitDate = Obj.LastVisitDate,
                    PhoneNumber3 = Obj.PhoneNumber3,
                    DiagnoseId = Obj.DiagnoseId
                };
            }
            return _Obj;
        }

        internal override PatientVM Convert(Patient DbObj)
        {
            return new PatientVM()
            {
                Id = DbObj.Id,
                Name = DbObj.Name,
                Address1 = DbObj.Address1,
                BirthDate = DbObj.BirthDate,
                City = DbObj.City,
                Gender = DbObj.Gender,
                IsDeleted = DbObj.IsDeleted,
                MedicalID = DbObj.MedicalID,
                ModifiedDate = DbObj.ModifiedDate,
                NameArabic = DbObj.NameArabic,
                MaritalStatus = DbObj.MaritalStatus,
                NationalID = DbObj.NationalID,
                PhoneNumber1 = DbObj.PhoneNumber1,
                NoOfChildren = DbObj.NoOfChildren,
                PhoneNumber2 = DbObj.PhoneNumber2,
                UnitID = DbObj.UnitID,
                CreatedDate = DbObj.CreatedDate,
                PatientUnitName = DbObj.PatientUnit?.Name,
                LastVisitDate = DbObj.LastVisitDate,
                PhoneNumber3 = DbObj.PhoneNumber3,
                DiagnoseId=DbObj.DiagnoseId,
                DiagnoseName = DbObj.Diagons?.Title,
                
            };
        }

        #region Functions
        public void Create()
        {
            _Obj = Convert(this);
            _Obj.MedicalID = Patient.NextPatientId();
            _Obj.AddNew();
        }

        public void Edit()
        {
            _Obj = Convert(this);
            _Obj.Edit();
        }

        public void Delete()
        {
            _Obj = Convert(this);
            _Obj.Delete();
        }
        public List<PatientVM> SelectAll()
        {
            PatientVM _Obj = new PatientVM();
            Patient _BClass = new Patient();
            List<Patient> dbList = _BClass.GetAll<Patient>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public PatientVM SelectObject(int Id)
        {
            Patient _BClass = new Patient();

            PatientVM Object = Convert(_BClass.Getobject(Id));
            return Object;
        }

        public List<SelectListItem> PatientSelectList()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<PatientVM> _List = SelectAll();
            
            foreach (PatientVM Obj in _List)
            {
                SelectListItem _item = new SelectListItem();
                _item.Text = Obj.Name;
                _item.Value = Obj.Id.ToString();
                listItems.Add(_item);
            }

            return listItems;
        }
        public List<SelectListItem> PatientFullSelectList()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<PatientVM> _List = SelectAll();
          
            foreach (PatientVM Obj in _List)
            {
                SelectListItem _item = new SelectListItem();
                _item.Text = Obj.MedicalID+"-"+Obj.Name+"-"+Obj.NationalID;
                _item.Value = Obj.Id.ToString();
                listItems.Add(_item);
            }

            return listItems;
        }

        #endregion
    }
}