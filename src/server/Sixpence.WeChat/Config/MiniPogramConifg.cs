using System;
namespace Sixpence.WeChat.Config
{
    public class MiniPogramConfig : BaseSysConfig<MiniPogramConfig>
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
    }
}

