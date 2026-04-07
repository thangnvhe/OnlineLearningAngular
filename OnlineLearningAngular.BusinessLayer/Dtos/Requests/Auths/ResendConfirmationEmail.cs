using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Auths
{
    public class ResendConfirmationEmail
    {
        public required string Username { get; set; }
    }
}
