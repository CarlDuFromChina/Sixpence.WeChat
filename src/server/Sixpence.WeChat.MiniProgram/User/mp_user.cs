using System;
using Sixpence.ORM.Entity;

namespace Sixpence.WeChat.MiniProgram.User
{
    [Entity("mp_user", "微信小程序用户")]
    public class mp_user : BaseEntity
    {
        [PrimaryColumn]
        public string id { get; set; }

        [Column]
        public string openid { get; set; }

        [Column]
        public string unionid { get; set; }

        [Column]
        public string nickname { get; set; }

        [Column]
        public string avatarurl { get; set; }

        [Column]
        public string lang { get; set; }

        [Column]
        public string desc { get; set; }

        /// <summary>
        /// //性别 0：未知、1：男、2：女
        /// </summary>
        [Column]
        public int gender { get; set; }

        [Column]
        public string province { get; set; }

        [Column]
        public string city { get; set; }

        [Column]
        public string country { get; set; }
    }
}

