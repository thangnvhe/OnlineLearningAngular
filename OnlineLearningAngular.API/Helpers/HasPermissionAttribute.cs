using Microsoft.AspNetCore.Authorization;

namespace OnlineLearningAngular.API.Helpers
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission) : base(permission) { }
    }
}
