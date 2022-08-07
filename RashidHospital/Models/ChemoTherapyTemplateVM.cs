using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital.DAL;
using RashidHospital.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using static RashidHospital.Helper.Enum;
namespace RashidHospital.Models
{

    public class ChemoTherapyTemplateVM : BusinessBaseClass<ChemoTherapyTemplate, ChemoTherapyTemplateVM>

    {

        [Key]
        public int Template_ID { get; set; }
        public string Protocol_Name { get; set; }
        public Nullable<int> Frequency { get; set; }
        [Required]
        [RegularExpression(@"[0-9]+(,[0-9]+)*",
                            ErrorMessage = "Please enter a valid Cycle Days")]
        public string Cycle_days { get; set; }
        public Nullable<int> Maximum_cycles { get; set; }
        public string Emetogenic_Level_Value { get; set; }
        public Nullable<int> Emetogenic_Level { get; set; }

        public Nullable<double> FN_risk { get; set; }
        public string Link { get; set; }
        public string Date_Created { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Date_Modified { get; set; }
        public Nullable<System.Guid> Modified_By { get; set; }
        public string Instruction { get; set; }
        public Nullable<int> Admin_Day { get; set; }
        public Nullable<System.DateTime> Admin_Date { get; set; }
        public string Date_Entered { get; set; }
        public string Disease { get; set; }
        public List<int> CycleDays { get; set; }
        public List<DateTime> cycleDates { get; set; }
        public int ProtocolId { get; set; }
        public int DiseaseId { get; set; }
        public Nullable<bool>  IsDeleted { get; set; }


        internal override ChemoTherapyTemplate Convert(ChemoTherapyTemplateVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new ChemoTherapyTemplate
                {
                    Template_ID = Obj.Template_ID,
                    Protocol_Name = Obj.Protocol_Name,
                    Frequency = Obj.Frequency,
                    Cycle_days = Obj.Cycle_days,
                    Maximum_cycles = Obj.Maximum_cycles,
                    Emetogenic_Level = Obj.Emetogenic_Level,
                    FN_risk = Obj.FN_risk,
                    Link = Obj.Link,
                    Date_Created = Obj.Date_Created,
                    Created_By = Obj.Created_By,
                    Date_Modified = Obj.Date_Modified,
                    Modified_By = Obj.Modified_By,
                    Instruction = Obj.Instruction,
                    Admin_Day = Obj.Admin_Day,

                    Admin_Date = Obj.Admin_Date,
                    //     Patient_ID = Obj.Patient_ID,
                    Date_Entered = Obj.Date_Entered,
                    Disease = Obj.Disease,
                    ProtocolId = Obj.ProtocolId,
                    DiseaseId = Obj.DiseaseId,
                    IsDeleted = Obj.IsDeleted,



                };
            }
            return _Obj;
        }



        internal override ChemoTherapyTemplateVM Convert(ChemoTherapyTemplate Obj)
        {
            if (Obj == null)
                return null;
            else
            {
               
                var Site = (Emetogenic_Level)Obj?.Emetogenic_Level;
                var enumType = EnumHelper<Emetogenic_Level>.GetDisplayValue(Site);
                DiseaseVM clinicVm = new DiseaseVM();
                DiseaseVM clinicObj = clinicVm.SelectObject(Obj.DiseaseId);
                protocolVM cplinicVm = new protocolVM();
                protocolVM cplinicObj = cplinicVm.SelectObject(Obj.ProtocolId);
                return new ChemoTherapyTemplateVM
                {


                    Template_ID = Obj.Template_ID,

                    Frequency = Obj.Frequency,
                    Cycle_days = Obj.Cycle_days,
                    Maximum_cycles = Obj.Maximum_cycles,
                    Emetogenic_Level = Obj.Emetogenic_Level,
                    Emetogenic_Level_Value = enumType.ToString(),
                    FN_risk = Obj.FN_risk,
                    Link = Obj.Link,
                    Date_Created = Obj.Date_Created,
                    Created_By = Obj.Created_By,
                    Date_Modified = Obj.Date_Modified,
                    Modified_By = Obj.Modified_By,
                    Instruction = Obj.Instruction,
                    Admin_Day = Obj.Admin_Day,

                    Admin_Date = Obj.Admin_Date,
                    //     Patient_ID = Obj.Patient_ID,
                    Date_Entered = Obj.Date_Entered,

                    ProtocolId = Obj.ProtocolId,
                    DiseaseId = Obj.DiseaseId,
                    Disease = clinicObj?.DiseaseName,
                    Protocol_Name = cplinicObj?.ProtocolName,
                    IsDeleted = Obj.IsDeleted,

                };
               

            }
        }




        public void Create()
        {
            _Obj = Convert(this);

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
        public List<ChemoTherapyTemplateVM> SelectAll()
        {
            ChemoTherapyTemplateVM _Obj = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplate _BClass = new ChemoTherapyTemplate();
            List<ChemoTherapyTemplate> dbList = _BClass.GetAll<ChemoTherapyTemplate>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public ChemoTherapyTemplateVM SelectObject(int Id)
        {
            ChemoTherapyTemplate _BClass = new ChemoTherapyTemplate();

            ChemoTherapyTemplateVM Object = Convert(_BClass.Getobject(Id));
            return Object;
        }


        public List<SelectListItem> GetELevelSelectList()
        {
            return System.Enum.GetValues(typeof(Emetogenic_Level)).Cast<Emetogenic_Level>().Select(v => new SelectListItem
            {
                Text = EnumHelper<Emetogenic_Level>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();
        }

        public List<SelectListItem> ProtocolSelectList(int DiseaseId)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<ChemoTherapyTemplateVM> CliniList = SelectAll().Where(x => x.DiseaseId == DiseaseId).ToList();

            foreach (ChemoTherapyTemplateVM Obj in CliniList)
            {
                SelectListItem _item = new SelectListItem();
                _item.Text = Obj.Protocol_Name;
                _item.Value = Obj.Template_ID.ToString();
                listItems.Add(_item);
            }

            return listItems;
        }

        public List<SelectListItem> DiseaseSelectList()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<ChemoTherapyTemplateVM> CliniList = SelectAll();
          
            foreach (ChemoTherapyTemplateVM Obj in CliniList)
            {
                SelectListItem _item = new SelectListItem();

                int count = 0;
                foreach (var item in listItems)
                {
                    if (item.Text == Obj.Disease)
                    {
                        count = 1;
                    }

                }

                if (count == 0)


                {
                    _item.Text = Obj.Disease;
                    _item.Value = Obj.DiseaseId.ToString();
                    listItems.Add(_item);

                }


            
            }

            return listItems;
        }

    }
}