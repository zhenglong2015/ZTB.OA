using WeChatApi.Entity.ReceiveEntity;

namespace WeChatApi.Service
{
    /// <summary>
    /// 用户信息
    /// </summary>
   public class UserService
    {
       /// <summary>
       /// 获取用户信息
       /// </summary>
       /// <param name="openId"></param>
       /// <param name="accessToken"></param>
       /// <returns></returns>
       public static UserInfo GetUserInfo(string openId) 
       {
           string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}", openId, WXService.AccessToken);
           return Utils.GetResult<UserInfo>(url);
       }
       /// <summary>
       /// 获取关注用户列表
       /// </summary>
       /// <param name="accessToken"></param>
       /// <param name="next_openid"></param>
       /// <returns></returns>
       public static UserListEntity GetUserList(string next_openid = "")
       {
           var url = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}", WXService.AccessToken, next_openid);
           var retdata = Utils.GetResult<UserListEntity>(url);
           //判断调用是否成功。当调用成功，且总关注人数大于10000,且本次获取到的用户数量等于10000，则说明有尚未获取到的用户，递归调用，添加到列表
           if (retdata.ErrCode == 0 && retdata.total > 10000 && retdata.count == 10000)
           {
               retdata.data.openid.AddRange(GetUserList(retdata.next_openid).data.openid);
           }
           return retdata;
       }

    }
}
