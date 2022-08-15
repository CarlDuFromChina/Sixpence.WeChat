using Sixpence.Core.Auth.Gitee.Model;
using Sixpence.Core.Auth.Github.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixpence.Core.Module.System.Models
{
    public class LoginConfig
    {
        public GithubConfig github { get; set; }
        public GiteeConfig gitee { get; set; }
    }
}
