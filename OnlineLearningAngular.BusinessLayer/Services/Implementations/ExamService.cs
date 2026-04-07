using AutoMapper;
using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Exam;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.BusinessLayer.Services.Implementations
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateExamRequest> _createExamRequestValidator;
        private readonly IValidator<UpdateExamRequest> _updateExamRequestValidator;

        public ExamService(
            IExamRepository examRepository, 
            IMapper mapper, 
            IUnitOfWork unitOfWork,
            IValidator<CreateExamRequest> createExamRequestValidator,
            IValidator<UpdateExamRequest> updateExamRequestValidator)
        {
            _examRepository = examRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createExamRequestValidator = createExamRequestValidator;
            _updateExamRequestValidator = updateExamRequestValidator;
        }

        public async Task<ServiceResult<List<ExamResponse>>> GetExamsByModuleIdAsync(int moduleId)
        {
            var exams = await _examRepository.GetExamsByModuleIdAsync(moduleId);
            var mappedItems = _mapper.Map<List<ExamResponse>>(exams);
            return ServiceResult<List<ExamResponse>>.Success(mappedItems);
        }

        public async Task<ServiceResult<ExamResponse?>> GetExamByIdAsync(int id)
        {
            var exam = await _examRepository.FindByIdAsync(id);
            if (exam == null)
            {
                return ServiceResult<ExamResponse?>.Failure("Không tìm thấy bài thi.");
            }

            var response = _mapper.Map<ExamResponse>(exam);
            return ServiceResult<ExamResponse?>.Success(response);
        }

        public async Task<ServiceResult<ExamResponse?>> CreateExamAsync(CreateExamRequest request)
        {
            if (request == null)
            {
                return ServiceResult<ExamResponse?>.Failure("Dữ liệu tạo bài thi không hợp lệ.");
            }

            var validationResult = await _createExamRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ServiceResult<ExamResponse?>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var exam = _mapper.Map<Exam>(request);

            _examRepository.Add(exam);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<ExamResponse?>.Failure("Thêm bài thi thất bại.");
            }

            var response = _mapper.Map<ExamResponse>(exam);
            return ServiceResult<ExamResponse?>.Success(response);
        }

        public async Task<ServiceResult<ExamResponse?>> UpdateExamAsync(int id, UpdateExamRequest request)
        {
            if (request == null)
            {
                return ServiceResult<ExamResponse?>.Failure("Dữ liệu cập nhật bài thi không hợp lệ.");
            }

            var validationResult = await _updateExamRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ServiceResult<ExamResponse?>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var exam = await _examRepository.FindByIdAsync(id);
            if (exam == null)
            {
                return ServiceResult<ExamResponse?>.Failure("Không tìm thấy bài thi.");
            }

            _mapper.Map(request, exam);

            _examRepository.Update(exam);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<ExamResponse?>.Failure("Cập nhật bài thi thất bại.");
            }

            var response = _mapper.Map<ExamResponse>(exam);
            return ServiceResult<ExamResponse?>.Success(response);
        }

        public async Task<ServiceResult<bool>> DeleteExamAsync(int id)
        {
            var exam = await _examRepository.FindByIdAsync(id);
            if (exam == null)
            {
                return ServiceResult<bool>.Failure("Không tìm thấy bài thi.");
            }

            _examRepository.Remove(exam);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<bool>.Failure("Xóa bài thi thất bại.");
            }

            return ServiceResult<bool>.Success(true);
        }
    }
}
