using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Lesson;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.BusinessLayer.Services.Interfaces
{
    public interface ILessonService
    {
        Task<ServiceResult<List<LessonResponse>>> GetLessonsByModuleIdAsync(int moduleId);
        Task<ServiceResult<LessonResponse?>> GetLessonByIdAsync(int id);
        Task<ServiceResult<LessonResponse?>> CreateLessonAsync(CreateLessonRequest request);
        Task<ServiceResult<LessonResponse?>> UpdateLessonAsync(int id, UpdateLessonRequest request);
        Task<ServiceResult<bool>> DeleteLessonAsync(int id);
    }
}
