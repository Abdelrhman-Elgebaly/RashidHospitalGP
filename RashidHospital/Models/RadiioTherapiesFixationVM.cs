using Hospital.DAL;
using RashidHospital.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace RashidHospital.Models
{
    public class RadiioTherapiesFixationVM : BusinessBaseClass<RadiioTherapiesFixation, RadiioTherapiesFixationVM>
    {
        public int Id { get; set; }
        public int RadioTherapyId { get; set; }
        public int FixationsId { get; set; }
        public string Fixation { get; set; }


        internal override RadiioTherapiesFixationVM Convert(RadiioTherapiesFixation Obj)
        {
            RadiioTherapiesFixationVM _obj = new RadiioTherapiesFixationVM();
            _obj.Id = Obj.Id;
            _obj.RadioTherapyId = Obj.RadioTherapyId;
            _obj.FixationsId = Obj.FixationsId;
            _obj.Fixation = Obj.Fixations.Name;
            return _obj;
        }

        internal override RadiioTherapiesFixation Convert(RadiioTherapiesFixationVM Obj)
        {
            RadiioTherapiesFixation _obj = new RadiioTherapiesFixation();
            _obj.Id = Obj.Id;
            _obj.RadioTherapyId = Obj.RadioTherapyId;
            _obj.FixationsId = Obj.FixationsId;

            return _obj;
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
        public List<RadiioTherapiesFixationVM> RadiioTherapiesFixation(int RadioTherapyId) {
            RadiioTherapiesFixation _obj = new RadiioTherapiesFixation();
            RadiioTherapiesFixationVM _VMobj = new RadiioTherapiesFixationVM();

            List<RadiioTherapiesFixation> dbList= _obj.GetAllRadioTherapyId(RadioTherapyId);
            return dbList.Select(z => _VMobj.Convert(z)).ToList();
        }

        public string RadiioTherapiesFixationString(int RadioTherapyId)
        {
            RadiioTherapiesFixation _obj = new RadiioTherapiesFixation();
            List<RadiioTherapiesFixation> dbList = _obj.GetAllRadioTherapyId(RadioTherapyId);
            string result = string.Empty;
            foreach (var item in dbList)
            {
                result = "  "+item.Fixations.Name;
            }
            return result;
        }

        public int[] RadiioTherapiesListFixationId(int RadioTherapyId)
        {
            RadiioTherapiesFixation _obj = new RadiioTherapiesFixation();
            List<RadiioTherapiesFixation> dbList = _obj.GetAllRadioTherapyId(RadioTherapyId);
            List<int> FixationId = new List<int>();
            foreach (var item in dbList)
            {
                FixationId.Add(item.FixationsId);
            }
            return FixationId.ToArray();
        }

        #endregion
    }
}