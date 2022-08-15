﻿using Sixpence.ORM.Entity;
using Sixpence.Core.Utils;
using Sixpence.Common.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Sixpence.ORM.EntityManager;

namespace Sixpence.Core.Module.Vertification.Mail
{
    public class MailVertificationPlugin : IEntityManagerPlugin
    {
        public void Execute(EntityManagerPluginContext context)
        {
            var entity = context.Entity;
            switch (context.Action)
            {
                case EntityAction.PreCreate:
                    {
                        var reciver = entity["mail_address"].ToString();
                        var title = entity["name"].ToString();
                        var content = entity["content"].ToString();
                        MailUtil.SendMail(reciver, title, content);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
