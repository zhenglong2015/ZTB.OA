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
        public DbContext Db
        {
            get { return DbContextFactory.GetCurrentDbContext(); }
        }
        #region 查询
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLamba)
        {
            return Db.Set<T>().Where(whereLamba).AsQueryable();
        }

        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderLambda, bool isAsc)
        {
            total = Db.Set<T>().Where(whereLambda).Count();

            return isAsc ? Db.Set<T>().Where(whereLambda).OrderBy(orderLambda)
                    .Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable() :
                     Db.Set<T>().Where(whereLambda).OrderByDescending(orderLambda)
                .Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
        }
        #endregion

        public T Add(T entity)
        {
            Db.Entry(entity).Property("ModifyOn").CurrentValue =Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Db.Set<T>().Add(entity);
            return entity;
        }

        public bool Update(T entity)
        {
           // entity = Db.Set<T>().Find(Db.Entry(entity).Property("Id").CurrentValue);
            Db.Entry(entity).Property("ModifyOn").CurrentValue = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //Db.Entry(entity).Property("ModifyOn").IsModified = true;
            Db.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public bool Delete(T entity)
        {
            Db.Entry(entity).State = EntityState.Deleted;
            return true;
        }

        public bool Delete(int id)
        {
            var entity = Db.Set<T>().Find(id);
            Db.Set<T>().Remove(entity);
            return true;
        }
        public bool DeleteByLogical(int id)
        {
            var entity = Db.Set<T>().Find(id);
            Db.Entry(entity).Property("DelFag").CurrentValue = true;
            Db.Entry(entity).Property("DelFag").IsModified = true;
            return true;
        }

        public bool DeleteListByLogical(List<int> ids)
        {
            foreach (var id in ids)
            {
                var entity = Db.Set<T>().Find(id);
                Db.Entry(entity).Property("DelFag").CurrentValue = true;
                Db.Entry(entity).Property("DelFag").IsModified = true;
            }
            return true;
        }
    }
}
