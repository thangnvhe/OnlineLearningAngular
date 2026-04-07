namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.User
{
    public class UpdateUserRequest
    {
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
