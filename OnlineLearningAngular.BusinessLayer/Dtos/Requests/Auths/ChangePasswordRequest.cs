using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Auths
{
    public class ChangePasswordRequest
    {
        // URL-encoded reset token sent to user's email
        public required string CurrentPassword { get; set; }

        // New password to set
        public required string NewPassword { get; set; }
    }
}
