using Sixpence.Core.Entity;
using Sixpence.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Sixpence.Core.Module.SysParamGroup
{
    public class SysParamGroupController : EntityBaseController<sys_paramgroup, SysParamGroupService>
    {
        [HttpGet("options")]
        public IEnumerable<object> GetParams(string code)
        {
            var codeList = code.Split(',');
            return new SysParamGroupService().GetParamsList(codeList);
        }

        [HttpGet("entity_options")]
        public IEnumerable<IEnumerable<SelectOption>> GetEntitiyList(string code)
        {
            var codeList = new string[] { };
            if (!string.IsNullOrEmpty(code))
            {
                codeList = code.Split(',');
            }
            return new SysParamGroupService().GetEntitiyList(codeList);
        }
    }
}