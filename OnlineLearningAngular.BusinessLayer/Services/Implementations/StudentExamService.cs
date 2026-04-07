using AutoMapper;
using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.StudentExam;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.BusinessLayer.Services.Implementations
{
    public class StudentExamService : IStudentExamService
    {
        private readonly IStudentExamRepository _studentExamRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateStudentExamRequest> _createValidator;
        private readonly IValidator<UpdateStudentExamRequest> _updateValidator;

        public StudentExamService(
            IStudentExamRepository studentExamRepository, 
            IMapper mapper, 
            IUnitOfWork unitOfWork,
            IValidator<CreateStudentExamRequest> createValidator,
            IValidator<UpdateStudentExamRequest> updateValidator)
        {
            _studentExamRepository = studentExamRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<ServiceResult<PagedResult<StudentExamResponse>>> GetPagedStudentExamByExamIdAsync(int examId, PagingFilterBase filters)
        {
            var pagedData = await _studentExamRepository.GetPagedStudentExamByExamIdAsync(examId, filters);
            
            var pagedResult = new PagedResult<StudentExamResponse>
            {
                Items = _mapper.Map<List<StudentExamResponse>>(pagedData.Items),
                TotalItems = pagedData.TotalItems,
                CurrentPage = pagedData.CurrentPage,
                PageSize = pagedData.PageSize
            };

            return ServiceResult<PagedResult<StudentExamResponse>>.Success(pagedResult);
        }

        public async Task<ServiceResult<StudentExamResponse?>> GetStudentExamByIdAsync(int id)
        {
            var studentExam = await _studentExamRepository.FindByIdAsync(id);
            if (studentExam == null)
            {
                return ServiceResult<StudentExamResponse?>.Failure("Không tìm thấy thông tin thi.");
            }

            var response = _mapper.Map<StudentExamResponse>(studentExam);
            return ServiceResult<StudentExamResponse?>.Success(response);
        }

        public async Task<ServiceResult<StudentExamResponse?>> CreateStudentExamAsync(CreateStudentExamRequest request)
        {
            if (request == null)
                return ServiceResult<StudentExamResponse?>.Failure("Dữ liệu không hợp lệ.");

            var validationResult = await _createValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return ServiceResult<StudentExamResponse?>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var studentExam = _mapper.Map<StudentExam>(request);

            _studentExamRepository.Add(studentExam);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
                return ServiceResult<StudentExamResponse?>.Failure("Thêm thông tin thi thất bại.");

            var response = _mapper.Map<StudentExamResponse>(studentExam);
            return ServiceResult<StudentExamResponse?>.Success(response);
        }

        public async Task<ServiceResult<StudentExamResponse?>> UpdateStudentExamAsync(int id, UpdateStudentExamRequest request)
        {
            if (request == null)
                return ServiceResult<StudentExamResponse?>.Failure("Dữ liệu không hợp lệ.");

            var validationResult = await _updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return ServiceResult<StudentExamResponse?>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var studentExam = await _studentExamRepository.FindByIdAsync(id);
            if (studentExam == null)
                return ServiceResult<StudentExamResponse?>.Failure("Không tìm thấy thông tin thi.");

            _mapper.Map(request, studentExam);

            _studentExamRepository.Update(studentExam);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
                return ServiceResult<StudentExamResponse?>.Failure("Cập nhật thông tin thi thất bại.");

            var response = _mapper.Map<StudentExamResponse>(studentExam);
            return ServiceResult<StudentExamResponse?>.Success(response);
        }

        public async Task<ServiceResult<bool>> DeleteStudentExamAsync(int id)
        {
            var studentExam = await _studentExamRepository.FindByIdAsync(id);
            if (studentExam == null)
                return ServiceResult<bool>.Failure("Không tìm thấy thông tin thi.");

            _studentExamRepository.Remove(studentExam);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
                return ServiceResult<bool>.Failure("Xóa thông tin thi thất bại.");

            return ServiceResult<bool>.Success(true);
        }
    }
}
