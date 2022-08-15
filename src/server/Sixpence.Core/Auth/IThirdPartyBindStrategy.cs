using Sixpence.Common.IoC;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixpence.Core.Auth
{
    [ServiceRegister]
    public interface IThirdPartyBindStrategy
    {
        void Bind(string code, string userid);
    }
}
