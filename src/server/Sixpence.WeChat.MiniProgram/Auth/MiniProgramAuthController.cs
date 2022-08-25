using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sixpence.WeChat.MiniProgram.Model;
using Sixpence.Web.WebApi;

namespace Sixpence.WeChat.MiniProgram.Auth
{
    public class MiniProgramAuthController : BaseApiController
    {
        [HttpGet]
        public async Task<LoginResponseModel> Login(string code)
        {
            var result = await new AuthService().Login(code);
            return result;
        }
    }
}

