using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ZTB.OA.IDAL
{
    public interface IBaseDal<TEntity> where TEntity : class, new()
    {
        #region 查询
        IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>> whereLamba);

        IQueryable<TEntity> GetPageEntities<S>(int pageSize, int pageIndex, out int total, Expression<Func<TEntity, bool>> whereLambda,
           Expression<Func<TEntity, S>> orderLambda, bool isAsc);
        #endregion

        TEntity Add(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);

        bool Delete(int id);
        bool DeleteByLogical(int id);
        bool DeleteListByLogical(List<int> ids);
    }
}
