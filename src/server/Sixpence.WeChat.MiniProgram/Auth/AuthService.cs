using System;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using Sixpence.Common.Logging;
using Sixpence.Common.Utils;
using Sixpence.WeChat.MiniProgram.Model;

namespace Sixpence.WeChat.MiniProgram.Auth
{
    public class AuthService
    {
        private ILog logger { get; set; }

        public AuthService()
        {
            logger = LoggerFactory.GetLogger(this.GetType().Name);
        }

        public async Task<LoginResponseModel> Login(string code)
        {
            var url = MiniProgramApiConfig.GetValue("Login");
            var appid = MiniProgramConfig.Config.AppId;
            var secret = MiniProgramConfig.Config.AppSecret;
            logger.Debug("微信登录请求地址：" + string.Format(url, appid, secret, code));
            var resp = await HttpUtil.GetAsync(string.Format(url, appid, secret, code));
            logger.Debug("微信登录返回值：" + resp);
            return JsonConvert.DeserializeObject<LoginResponseModel>(resp);
        }
    }
}

