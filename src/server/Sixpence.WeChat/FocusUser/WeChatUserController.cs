using Sixpence.Web.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sixpence.WeChat.FocusUser
{
    public class WechatUserController : EntityBaseController<wechat_user, WeChatUserService>
    {
        [HttpGet("focus_user")]
        public FocusUserListModel GetFocusUserList()
        {
            return new FocusUserService().GetFocusUserList();
        }
    }
}
