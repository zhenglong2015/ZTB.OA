using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTB.OA.IBLL;
using ZTB.OA.Model;
using ZTB.OA.Model.Param;

namespace ZTB.OA.BLL
{
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        public IQueryable<UserInfo> LoagPageData(UserQueryParam queryParam)
        {
            var temp = DbSession.UserInfoDal.GetEntities(u => u.DelFag == "1");

            if (!string.IsNullOrEmpty(queryParam.Name))
                temp = temp.Where(u=>u.UName.Contains(queryParam.Name)).AsQueryable();

            if (!string.IsNullOrEmpty(queryParam.Pwd))
                temp = temp.Where(u => u.UName.Contains(queryParam.Pwd)).AsQueryable();

            return temp.OrderByDescending(u=>u.Id).AsQueryable();
        }
    }
}
