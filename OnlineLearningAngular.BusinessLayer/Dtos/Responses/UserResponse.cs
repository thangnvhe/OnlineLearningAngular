using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly Dob { get; set; }
        public string Address { get; set; }
        public bool IsMale { get; set; } = true;
        public bool IsActive { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string? AvatarUrl { get; set; }
        public List<string> Roles { get; set; }
    }
}
