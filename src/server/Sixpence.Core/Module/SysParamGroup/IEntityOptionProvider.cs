using Sixpence.Core.Entity;
using Sixpence.Common.IoC;
using System.Collections.Generic;

namespace Sixpence.Core.Module.SysParamGroup
{
    [ServiceRegister]
    public interface IEntityOptionProvider
    {
        IEnumerable<SelectOption> GetOptions();
    }
}
