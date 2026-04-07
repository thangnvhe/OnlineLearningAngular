using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Module;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.BusinessLayer.Services.Interfaces
{
    public interface IModuleService
    {
        Task<ServiceResult<List<ModuleResponse>>> GetModulesByCourseIdAsync(int courseId);
        Task<ServiceResult<ModuleResponse?>> GetModuleByIdAsync(int id);
        Task<ServiceResult<ModuleResponse?>> CreateModuleAsync(CreateModuleRequest request);
        Task<ServiceResult<ModuleResponse?>> UpdateModuleAsync(int id, UpdateModuleRequest request);
        Task<ServiceResult<bool>> DeleteModuleAsync(int id);
    }
}
