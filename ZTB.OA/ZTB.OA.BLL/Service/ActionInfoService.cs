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
    public partial class ActionInfoService : BaseService<ActionInfo>, IActionInfoService
    {
        public bool SetRoles(int id, List<int> list)
        {
            var user = DbSession.ActionInfoDal.GetEntities(u => u.Id == id).FirstOrDefault();
            user.RoleInfo.Clear();
            var roles = DbSession.RoleInfoDal.GetEntities(r => list.Contains(r.Id));
            foreach (var role in roles)
            {
                user.RoleInfo.Add(role);
            }
            return DbSession.SaveChanges() > 0;
        }
    }
}
