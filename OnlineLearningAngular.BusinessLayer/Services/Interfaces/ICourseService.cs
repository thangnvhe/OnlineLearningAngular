using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Course;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.BusinessLayer.Services.Interfaces
{
    public interface ICourseService
    {
        Task<ServiceResult<List<CourseResponse>>> GetAllCoursesAsync();
        Task<ServiceResult<PagedResult<CourseResponse>>> GetPagedCoursesAsync(PagingFilterBase filters);
        Task<ServiceResult<CourseResponse?>> GetCourseByIdAsync(int id);
        Task<ServiceResult<CourseResponse?>> CreateCourseAsync(CreateCourseRequest request);
        Task<ServiceResult<CourseResponse?>> UpdateCourseAsync(int id, UpdateCourseRequest request);
        Task<ServiceResult<bool>> DeleteCourseAsync(int id);
    }
}
