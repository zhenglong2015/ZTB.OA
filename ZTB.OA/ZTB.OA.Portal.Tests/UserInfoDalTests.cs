using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZTB.OA.EFDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTB.OA.Model;
using ZTB.OA.IDAL;

namespace ZTB.OA.EFDAL.Tests
{
    [TestClass]
    public class UserInfoDalTests
    {
        [TestMethod()]
        public void GetUsersTest()
        {
            IUserInfoDal dal = new UserInfoDal();
            //单元测试必须自己处理数据，不能依赖第三方数据

            for (int i = 0; i < 10; i++)
            {
                dal.Add(new UserInfo() { UName = i + "ss" });
            }
            IQueryable<UserInfo> temp = dal.GetEntities(u=>u.UName.Contains("s"));

            //断言
            Assert.AreEqual(true,temp.Count() >=10);
        }
    }
}