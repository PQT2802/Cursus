using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface IMailServiceV3
    {
        Task<string> SendForgetPasswordEmail(string toEmail, string userName, string resetLink);

        Task<string> SendCloseCourseMail(int id, string reason, TimeSpan duration);
        Task<string> SendApproveCourseMail(string courseId);
        Task<string> SendRejectCourseMail(string courseId,string reason);

    }
}
