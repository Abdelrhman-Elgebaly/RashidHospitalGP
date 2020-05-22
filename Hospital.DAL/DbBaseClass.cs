using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public class DbBaseClass<T>  where T : class
    {
        //public interface IRepository<T> : IDisposable where T : class

        protected Model1 _Db;
       protected static Model1 _Sdb;
    //   protected static DbSet<T> _sLst;


        protected DbSet _dbSet;


        public DbBaseClass() : this(new Model1())
        {
            _Sdb = new Model1();


        }
        public DbBaseClass(Model1 context) 
        {
            _Db = new Model1();
            
            _dbSet = _Db.Set<T>();
        }


        public virtual bool AddNew()
        {

            _Db.Entry(this).State = System.Data.Entity.EntityState.Added;
           
            return _Db.SaveChanges() > 0;
        }
       
        public virtual bool Edit()
        {
            _Db.Entry(this).State = System.Data.Entity.EntityState.Modified;

            return _Db.SaveChanges() > 0;
        }
        public virtual bool Delete()
        {
            _Db.Entry(this).State = System.Data.Entity.EntityState.Deleted;
            return _Db.SaveChanges() > 0;
        }
        public virtual T Getobject(object Id) {
            return _Db.Set<T>().Find(Id);
        }

        public virtual bool IsDeleted()
        {
            _Db.Entry(this).State = System.Data.Entity.EntityState.Modified;

            return _Db.SaveChanges() > 0;
        }
        public DbSet<T> GetAll<T>()
         where T : class
        {
            return _Db.Set<T>();
        }
        
        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _Db.Set<T>().Where(predicate).AsEnumerable();
        }
       
    }
}
