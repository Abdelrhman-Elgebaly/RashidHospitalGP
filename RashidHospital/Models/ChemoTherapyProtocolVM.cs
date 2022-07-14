
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital.DAL;
using RashidHospital.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static RashidHospital.Helper.Enum;
using System.Web.Mvc;
namespace RashidHospital.Models
{

    public class ChemoTherapyProtocolVM : BusinessBaseClass<ChemoTherapyProtocol, ChemoTherapyProtocolVM>
    {

        [Key]
        public int ID { get; set; }
        public int Patient_ID { get; set; }
        public int Template_ID { get; set; }
        public Nullable<int> Cycles_Number { get; set; }
        public Nullable<int> ProtocolId { get; set; }
        public Nullable<int> DiseaseId { get; set; }
        public string ProtocolName { get; set; }
        public string DiseaseName { get; set; }


        internal override ChemoTherapyProtocol Convert(ChemoTherapyProtocolVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new ChemoTherapyProtocol
                {
                    ID = Obj.ID,
                    Patient_ID = Obj.Patient_ID,
                    Template_ID = Obj.Template_ID,
                    Cycles_Number = Obj.Cycles_Number,
                    ProtocolId = Obj.ProtocolId,
                    DiseaseId = Obj.DiseaseId,
                };
            }
            return _Obj;
        }


        internal override ChemoTherapyProtocolVM Convert(ChemoTherapyProtocol DbObj)
        {


            ChemoTherapyProtocolVM pl = new ChemoTherapyProtocolVM();



            pl.ID = DbObj.ID;
            pl.Patient_ID = DbObj.Patient_ID;
            pl.DiseaseId = DbObj.DiseaseId;

            pl.Cycles_Number = DbObj.Cycles_Number;
            
            pl.Template_ID = DbObj.Template_ID;
            ChemoTherapyTemplateVM chemoVm = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM chemoObj = chemoVm.SelectObject(DbObj.Template_ID);
            pl.ProtocolId = chemoObj.ProtocolId;
            pl.DiseaseName = chemoObj.Disease;
           pl.ProtocolName = chemoObj.Protocol_Name;
           
            return pl;
        }



        public void Create()
        {
            _Obj = Convert(this);
            _Obj.AddNew();
        }

        public List<ChemoTherapyProtocolVM> SelectAllByPatientID(int PatientId)
        {
            ChemoTherapyProtocolVM _Obj = new ChemoTherapyProtocolVM();
            ChemoTherapyProtocol _BClass = new ChemoTherapyProtocol();
            List<ChemoTherapyProtocol> dbList = _BClass.GetProtocolsByPatientId(PatientId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }



        public ChemoTherapyProtocolVM SelectObject(int Id)
        {
            ChemoTherapyProtocol _BClass = new ChemoTherapyProtocol();
            ChemoTherapyProtocolVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }



 




    }
}