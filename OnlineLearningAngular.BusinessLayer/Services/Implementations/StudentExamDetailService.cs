using AutoMapper;
using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.StudentExamDetail;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.BusinessLayer.Services.Implementations
{
    public class StudentExamDetailService : IStudentExamDetailService
    {
        private readonly IStudentExamDetailRepository _studentExamDetailRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateStudentExamDetailRequest> _createValidator;
        private readonly IValidator<UpdateStudentExamDetailRequest> _updateValidator;

        public StudentExamDetailService(
            IStudentExamDetailRepository studentExamDetailRepository, 
            IMapper mapper, 
            IUnitOfWork unitOfWork,
            IValidator<CreateStudentExamDetailRequest> createValidator,
            IValidator<UpdateStudentExamDetailRequest> updateValidator)
        {
            _studentExamDetailRepository = studentExamDetailRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<ServiceResult<List<StudentExamDetailResponse>>> GetStudentExamDetailsByStudentExamIdAsync(int studentExamId)
        {
            var details = await _studentExamDetailRepository.GetStudentExamDetailsByStudentExamIdAsync(studentExamId);
            var mappedItems = _mapper.Map<List<StudentExamDetailResponse>>(details);
            return ServiceResult<List<StudentExamDetailResponse>>.Success(mappedItems);
        }

        public async Task<ServiceResult<StudentExamDetailResponse?>> GetStudentExamDetailByIdAsync(int id)
        {
            var detail = await _studentExamDetailRepository.FindByIdAsync(id);
            if (detail == null)
            {
                return ServiceResult<StudentExamDetailResponse?>.Failure("Không tìm thấy chi tiết bài làm.");
            }

            var response = _mapper.Map<StudentExamDetailResponse>(detail);
            return ServiceResult<StudentExamDetailResponse?>.Success(response);
        }

        public async Task<ServiceResult<StudentExamDetailResponse?>> CreateStudentExamDetailAsync(CreateStudentExamDetailRequest request)
        {
            if (request == null)
                return ServiceResult<StudentExamDetailResponse?>.Failure("Dữ liệu không hợp lệ.");

            var validationResult = await _createValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return ServiceResult<StudentExamDetailResponse?>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var detail = _mapper.Map<StudentExamDetail>(request);

            _studentExamDetailRepository.Add(detail);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
                return ServiceResult<StudentExamDetailResponse?>.Failure("Thêm chi tiết bài làm thất bại.");

            var response = _mapper.Map<StudentExamDetailResponse>(detail);
            return ServiceResult<StudentExamDetailResponse?>.Success(response);
        }

        public async Task<ServiceResult<StudentExamDetailResponse?>> UpdateStudentExamDetailAsync(int id, UpdateStudentExamDetailRequest request)
        {
            if (request == null)
                return ServiceResult<StudentExamDetailResponse?>.Failure("Dữ liệu không hợp lệ.");

            var validationResult = await _updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return ServiceResult<StudentExamDetailResponse?>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var detail = await _studentExamDetailRepository.FindByIdAsync(id);
            if (detail == null)
                return ServiceResult<StudentExamDetailResponse?>.Failure("Không tìm thấy chi tiết bài làm.");

            _mapper.Map(request, detail);

            _studentExamDetailRepository.Update(detail);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
                return ServiceResult<StudentExamDetailResponse?>.Failure("Cập nhật chi tiết bài làm thất bại.");

            var response = _mapper.Map<StudentExamDetailResponse>(detail);
            return ServiceResult<StudentExamDetailResponse?>.Success(response);
        }

        public async Task<ServiceResult<bool>> DeleteStudentExamDetailAsync(int id)
        {
            var detail = await _studentExamDetailRepository.FindByIdAsync(id);
            if (detail == null)
                return ServiceResult<bool>.Failure("Không tìm thấy chi tiết bài làm.");

            _studentExamDetailRepository.Remove(detail);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
                return ServiceResult<bool>.Failure("Xóa chi tiết bài làm thất bại.");

            return ServiceResult<bool>.Success(true);
        }
    }
}
