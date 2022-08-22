using Sixpence.Web.WebApi;
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sixpence.Common;

namespace Sixpence.WeChat.FocusUser
{
    public class WeChatUserService : EntityService<wechat_user>
    {
        #region 构造函数
        public WeChatUserService() : base() { }
       
        public WeChatUserService(IEntityManager manager) : base(manager) { }
        #endregion

        /// <summary>
        /// 关注用户创建数据
        /// </summary>
        /// <param name="openid"></param>
        public void SaveData(string openid)
        {
            var focusUser = GetFocusUser(openid);
            wechat_user user = new wechat_user()
            {
                id = focusUser.openid,
                subscribe = focusUser.subscribe,
                openid = focusUser.openid,
                language = focusUser.language,
                subscribe_time = focusUser.subscribe_time.ToDateTime(),
                unionid = focusUser.unionid,
                remark = focusUser.remark,
                groupid = focusUser.groupid,
                subscribe_scene = focusUser.subscribe_scene,
                qr_scene = focusUser.qr_scene,
                qr_scene_str = focusUser.qr_scene_str
            };
            base.CreateOrUpdateData(user);
        }

        /// <summary>
        /// 获取所有关注用户（返回OpenId集合）
        /// </summary>
        /// <returns></returns>
        public FocusUserListModel GetFocusUserList(string openid = "")
        {
            var resp = WeChatApi.GetFocusUserList(openid);
            var openIds = JsonConvert.DeserializeObject<FocusUserListModel>(resp);
            return openIds;
        }

        /// <summary>
        /// 获取关注用户基本信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public FocusUserModel GetFocusUser(string openid)
        {
            var resp = WeChatApi.GetFocusUser(openid);
            var user = JsonConvert.DeserializeObject<FocusUserModel>(resp);
            return user;
        }

        /// <summary>
        /// 获取关注用户信息
        /// </summary>
        /// <param name="userList">OpenId列表</param>
        /// <returns></returns>
        public FocusUsersModel GetFocusUsers(string userList)
        {
            var resp2 = WeChatApi.BatchGetFocusUser(JsonConvert.SerializeObject(userList));
            var focusUsers = JsonConvert.DeserializeObject<FocusUsersModel>(resp2);
            return focusUsers;
        }
    }
}
