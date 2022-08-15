using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.WeChat
{
    public class Startup : Sixpence.Core.Startup
    {
        public Startup(IConfiguration configuration) : base(configuration) { }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor accessor)
        {
            base.Configure(app, env, accessor);
        }
    }
}
