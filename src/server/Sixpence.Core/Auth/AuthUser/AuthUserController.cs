﻿using Sixpence.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sixpence.Core.Auth
{
    public class AuthUserController : EntityBaseController<auth_user, AuthUserService>
    {
        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize(Policy = "Refresh")]
        [Route("refresh_access_token")]
        public Token RefreshAccessToken()
        {
            var tokenHeader = HttpContext.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
            var user = JwtHelper.SerializeJwt(tokenHeader);
            return JwtHelper.CreateAccessToken(user);
        }

        /// <summary>
        /// 锁定用户
        /// </summary>
        /// <param name="id"></param>
        [HttpPut("{id}/lock")]
        public void LockUser(string id)
        {
            new AuthUserService().LockUser(id);
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="id"></param>
        [HttpPut("{id}/unlock")]
        public void UnlockUser(string id)
        {
            new AuthUserService().UnlockUser(id);
        }

        /// <summary>
        /// 绑定第三方系统 账号
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="code"></param>
        [HttpPost("bind")]
        public void BindThirdPartyAccount(ThirdPartyLoginType type, string userid, string code)
        {
            new AuthUserService().BindThirdPartyAccount(type, userid, code);
        }
    }
}