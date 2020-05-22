using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RashidHospital.Abstract
{
    public abstract class BusinessBase<T, Z>
    {
        protected T _Obj;

        protected Z _BsObj;

        protected static Z _Temp;

        protected static List<T> _DbObjLst;

        protected static List<Z> _ObjLst;

        /////////////////////////////////////////////////////////////////////

        internal abstract T Convert(Z Obj);

        internal abstract Z Convert(T Obj);


    }
}