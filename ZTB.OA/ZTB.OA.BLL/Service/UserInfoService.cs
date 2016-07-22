using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTB.OA.IBLL;
using ZTB.OA.Model;
using ZTB.OA.Model.Param;
using ZTB.OA.Model.ResponseMessage;

namespace ZTB.OA.BLL
{
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        public IQueryable<UserInfo> LoagPageData(UserQueryParam queryParam)
        {
            var temp = DbSession.UserInfoDal.GetEntities(u => !u.DelFag);

            if (!string.IsNullOrEmpty(queryParam.Name))
                temp = temp.Where(u => u.Name.Contains(queryParam.Name)).AsQueryable();

            if (!string.IsNullOrEmpty(queryParam.Pwd))
                temp = temp.Where(u => u.Name.Contains(queryParam.Pwd)).AsQueryable();

            return temp.OrderByDescending(u => u.Id).AsQueryable();
        }

        public AddUserResponse AddUser(UserInfo user)
        {
            AddUserResponse res = new AddUserResponse();
            if (DbSession.UserInfoDal.GetEntities(u => u.Name == user.Name&&!u.DelFag).FirstOrDefault() == null)
            {
                DbSession.UserInfoDal.Add(user);
                DbSession.SaveChanges();
                res.IsSuccess = true;
                res.Message = "添加成功";
            }
            else
            {
                res.Message = "此用户用户名已存在";
            }
            return res;
        }
    }
}
