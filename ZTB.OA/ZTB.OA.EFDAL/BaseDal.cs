using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZTB.OA.Model;

namespace ZTB.OA.EFDAL
{
    /// <summary>
    /// 封装类的公共的CRUD
    /// </summary>
    public class BaseDal<T> where T : class, new()
    {
        //DataModelContainer db = new DataModelContainer();

       public DbContext Db
        {
            get { return DbContextFactory.GetCurrentDbContext(); }
        } 
        #region 查询
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLamba)
        {
            return db.Set<T>().Where(whereLamba).AsQueryable();
        }

        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderLambda, bool isAsc)
        {
            total = db.Set<T>().Where(whereLambda).Count();
            if (isAsc)
                return db.Set<T>().Where(whereLambda).OrderBy(orderLambda)
                    .Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
            else
                return db.Set<T>().Where(whereLambda).OrderByDescending(orderLambda)
                .Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();

        }
        #endregion

        public T Add(T entity)
        {
            db.Set<T>().Add(entity);
            db.SaveChanges();
            return entity;
        }

        public bool Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public bool Delete(T entity)
        {
            db.Entry(entity).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }
    }
}
