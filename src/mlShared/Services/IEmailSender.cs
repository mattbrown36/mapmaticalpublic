using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mlShared.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string _from, string email, string subject, string message);
    }
}
