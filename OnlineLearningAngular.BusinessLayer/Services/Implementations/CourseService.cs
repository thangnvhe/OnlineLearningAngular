using AutoMapper;
using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Course;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.BusinessLayer.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateCourseRequest> _createCourseRequestValidator;
        private readonly IValidator<UpdateCourseRequest> _updateCourseRequestValidator;

        public CourseService(
            ICourseRepository courseRepository, 
            IMapper mapper, 
            IUnitOfWork unitOfWork,
            IValidator<CreateCourseRequest> createCourseRequestValidator,
            IValidator<UpdateCourseRequest> updateCourseRequestValidator)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createCourseRequestValidator = createCourseRequestValidator;
            _updateCourseRequestValidator = updateCourseRequestValidator;
        }

        public async Task<ServiceResult<List<CourseResponse>>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            var mappedItems = _mapper.Map<List<CourseResponse>>(courses);
            return ServiceResult<List<CourseResponse>>.Success(mappedItems);
        }

        public async Task<ServiceResult<PagedResult<CourseResponse>>> GetPagedCoursesAsync(PagingFilterBase filters)
        {
            var pagedCourses = await _courseRepository.GetPagedAsync(filters);
            
            var pagedResult = new PagedResult<CourseResponse>
            {
                Items = _mapper.Map<List<CourseResponse>>(pagedCourses.Items),
                TotalItems = pagedCourses.TotalItems,
                CurrentPage = pagedCourses.CurrentPage,
                PageSize = pagedCourses.PageSize
            };

            return ServiceResult<PagedResult<CourseResponse>>.Success(pagedResult);
        }

        public async Task<ServiceResult<CourseResponse?>> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.FindByIdAsync(id);
            if (course == null)
            {
                return ServiceResult<CourseResponse?>.Failure("Không tìm thấy khóa học.");
            }

            var response = _mapper.Map<CourseResponse>(course);
            return ServiceResult<CourseResponse?>.Success(response);
        }

        public async Task<ServiceResult<CourseResponse?>> CreateCourseAsync(CreateCourseRequest request)
        {
            if (request == null)
            {
                return ServiceResult<CourseResponse?>.Failure("Dữ liệu tạo khóa học không hợp lệ.");
            }

            var validationResult = await _createCourseRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ServiceResult<CourseResponse?>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var course = _mapper.Map<Course>(request);
            course.CreateAt = DateTime.UtcNow;
            course.UpdateAt = DateTime.UtcNow;

            _courseRepository.Add(course);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<CourseResponse?>.Failure("Thêm khóa học thất bại.");
            }

            var response = _mapper.Map<CourseResponse>(course);
            return ServiceResult<CourseResponse?>.Success(response);
        }

        public async Task<ServiceResult<CourseResponse?>> UpdateCourseAsync(int id, UpdateCourseRequest request)
        {
            if (request == null)
            {
                return ServiceResult<CourseResponse?>.Failure("Dữ liệu cập nhật khóa học không hợp lệ.");
            }

            var validationResult = await _updateCourseRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ServiceResult<CourseResponse?>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var course = await _courseRepository.FindByIdAsync(id);
            if (course == null)
            {
                return ServiceResult<CourseResponse?>.Failure("Không tìm thấy khóa học.");
            }

            _mapper.Map(request, course);
            course.UpdateAt = DateTime.UtcNow;

            _courseRepository.Update(course);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<CourseResponse?>.Failure("Cập nhật khóa học thất bại.");
            }

            var response = _mapper.Map<CourseResponse>(course);
            return ServiceResult<CourseResponse?>.Success(response);
        }

        public async Task<ServiceResult<bool>> DeleteCourseAsync(int id)
        {
            var course = await _courseRepository.FindByIdAsync(id);
            if (course == null)
            {
                return ServiceResult<bool>.Failure("Không tìm thấy khóa học.");
            }

            _courseRepository.Remove(course);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<bool>.Failure("Xóa khóa học thất bại.");
            }

            return ServiceResult<bool>.Success(true);
        }
    }
}
