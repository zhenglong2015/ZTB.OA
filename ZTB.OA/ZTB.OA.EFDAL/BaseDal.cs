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
        DataModelContainer db = new DataModelContainer();
        #region 查询
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLamba)
        {
            return db.Set<T>().Where(whereLamba).AsQueryable();
        }

        public IQueryable<T> GetPageUsers<S>(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderLambda, bool isAsc)
        {
            total = db.Set<T>().Where(whereLambda).Count();
            if (isAsc)
                return db.Set<T>().Where(whereLambda).OrderBy<T, S>(orderLambda)
                    .Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
            else
                return db.Set<T>().Where(whereLambda).OrderByDescending<T, S>(orderLambda)
                .Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();

        }
        #endregion

        public T Add(T t)
        {
            db.Set<T>().Add(t);
            db.SaveChanges();
            return t;
        }

        public bool Update(T t)
        {
            db.Entry(t).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public bool Delete(T t)
        {
            db.Entry(t).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }
    }
}
