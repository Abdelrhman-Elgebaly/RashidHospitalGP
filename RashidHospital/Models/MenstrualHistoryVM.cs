using Hospital.DAL;
using RashidHospital.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RashidHospital.Models
{
    public class MenstrualHistoryVM : BusinessBaseClass<MenstrualHistory, MenstrualHistoryVM>
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int Duration { get; set; }

        public bool Premenopausal { get; set; }

        public bool Postmenopausal { get; set; }

        public bool Perimenopausal { get; set; }
        public bool IsDeleted { get; set; }


        internal override MenstrualHistory Convert(MenstrualHistoryVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new MenstrualHistory
                {
                    Id = Obj.Id,
                    Duration = Obj.Duration,
                    Perimenopausal = Obj.Perimenopausal,
                    Postmenopausal = Obj.Postmenopausal,
                    Premenopausal=Obj.Premenopausal,
                    PatientId=Obj.PatientId,
                    IsDeleted=Obj.IsDeleted
                };
            }
            return _Obj;
        }

        internal override MenstrualHistoryVM Convert(MenstrualHistory DbObj)
        {
            if (DbObj != null) {
                return new MenstrualHistoryVM()
                {
                    Id = DbObj.Id,
                    Duration = DbObj.Duration,
                    Perimenopausal = DbObj.Perimenopausal,
                    Postmenopausal = DbObj.Postmenopausal,
                    Premenopausal = DbObj.Premenopausal,
                    PatientId = DbObj.PatientId,
                    IsDeleted=DbObj.IsDeleted

                };
            }
            else
            {
                return null;
            }
           
        }

        #region Functions
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
      
        public MenstrualHistoryVM SelectObject(int Id)
        {
            MenstrualHistory _BClass = new MenstrualHistory();

            MenstrualHistoryVM _Object = Convert(_BClass.Getobject(Id));
            return _Object;
        }

        public MenstrualHistoryVM SelectAllByPatientId(int patientId)
        {
            MenstrualHistoryVM _Obj = new MenstrualHistoryVM();
            MenstrualHistory _BClass = new MenstrualHistory();
            MenstrualHistory _obj = _BClass.GetALlByPatientId(patientId).FirstOrDefault();
            return Convert(_obj);
        }
        #endregion
    }
}