using System.Collections.Generic;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Permission
{
    public class UpdateRolePermissionsRequest
    {
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
