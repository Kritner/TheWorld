using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Business.Interfaces
{
    public interface IMailService
    {
        bool SendMail(string to, string from, string subject, string body);
    }
}
