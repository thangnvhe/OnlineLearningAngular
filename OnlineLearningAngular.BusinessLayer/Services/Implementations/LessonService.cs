using AutoMapper;
using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Lesson;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.BusinessLayer.Services.Implementations
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateLessonRequest> _createLessonRequestValidator;
        private readonly IValidator<UpdateLessonRequest> _updateLessonRequestValidator;

        public LessonService(
            ILessonRepository lessonRepository, 
            IMapper mapper, 
            IUnitOfWork unitOfWork,
            IValidator<CreateLessonRequest> createLessonRequestValidator,
            IValidator<UpdateLessonRequest> updateLessonRequestValidator)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createLessonRequestValidator = createLessonRequestValidator;
            _updateLessonRequestValidator = updateLessonRequestValidator;
        }

        public async Task<ServiceResult<List<LessonResponse>>> GetLessonsByModuleIdAsync(int moduleId)
        {
            var lessons = await _lessonRepository.GetLessonsByModuleIdAsync(moduleId);
            var mappedItems = _mapper.Map<List<LessonResponse>>(lessons);
            return ServiceResult<List<LessonResponse>>.Success(mappedItems);
        }

        public async Task<ServiceResult<LessonResponse?>> GetLessonByIdAsync(int id)
        {
            var lesson = await _lessonRepository.FindByIdAsync(id);
            if (lesson == null)
            {
                return ServiceResult<LessonResponse?>.Failure("Không tìm thấy bài học.");
            }

            var response = _mapper.Map<LessonResponse>(lesson);
            return ServiceResult<LessonResponse?>.Success(response);
        }

        public async Task<ServiceResult<LessonResponse?>> CreateLessonAsync(CreateLessonRequest request)
        {
            if (request == null)
            {
                return ServiceResult<LessonResponse?>.Failure("Dữ liệu tạo bài học không hợp lệ.");
            }

            var validationResult = await _createLessonRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ServiceResult<LessonResponse?>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var lesson = _mapper.Map<Lesson>(request);

            _lessonRepository.Add(lesson);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<LessonResponse?>.Failure("Thêm bài học thất bại.");
            }

            var response = _mapper.Map<LessonResponse>(lesson);
            return ServiceResult<LessonResponse?>.Success(response);
        }

        public async Task<ServiceResult<LessonResponse?>> UpdateLessonAsync(int id, UpdateLessonRequest request)
        {
            if (request == null)
            {
                return ServiceResult<LessonResponse?>.Failure("Dữ liệu cập nhật bài học không hợp lệ.");
            }

            var validationResult = await _updateLessonRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ServiceResult<LessonResponse?>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var lesson = await _lessonRepository.FindByIdAsync(id);
            if (lesson == null)
            {
                return ServiceResult<LessonResponse?>.Failure("Không tìm thấy bài học.");
            }

            _mapper.Map(request, lesson);

            _lessonRepository.Update(lesson);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<LessonResponse?>.Failure("Cập nhật bài học thất bại.");
            }

            var response = _mapper.Map<LessonResponse>(lesson);
            return ServiceResult<LessonResponse?>.Success(response);
        }

        public async Task<ServiceResult<bool>> DeleteLessonAsync(int id)
        {
            var lesson = await _lessonRepository.FindByIdAsync(id);
            if (lesson == null)
            {
                return ServiceResult<bool>.Failure("Không tìm thấy bài học.");
            }

            _lessonRepository.Remove(lesson);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<bool>.Failure("Xóa bài học thất bại.");
            }

            return ServiceResult<bool>.Success(true);
        }
    }
}
