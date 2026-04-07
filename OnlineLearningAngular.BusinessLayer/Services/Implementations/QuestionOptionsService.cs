using AutoMapper;
using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.QuestionOptions;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.BusinessLayer.Services.Implementations
{
    public class QuestionOptionsService : IQuestionOptionsService
    {
        private readonly IQuestionOptionsRepository _questionOptionsRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateQuestionOptionsRequest> _createQuestionOptionsRequestValidator;
        private readonly IValidator<UpdateQuestionOptionsRequest> _updateQuestionOptionsRequestValidator;

        public QuestionOptionsService(
            IQuestionOptionsRepository questionOptionsRepository, 
            IMapper mapper, 
            IUnitOfWork unitOfWork,
            IValidator<CreateQuestionOptionsRequest> createQuestionOptionsRequestValidator,
            IValidator<UpdateQuestionOptionsRequest> updateQuestionOptionsRequestValidator)
        {
            _questionOptionsRepository = questionOptionsRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createQuestionOptionsRequestValidator = createQuestionOptionsRequestValidator;
            _updateQuestionOptionsRequestValidator = updateQuestionOptionsRequestValidator;
        }

        public async Task<ServiceResult<List<QuestionOptionsResponse>>> GetQuestionOptionsByQuestionIdAsync(int questionId)
        {
            var questionOptionsList = await _questionOptionsRepository.GetQuestionOptionsByQuestionIdAsync(questionId);
            var mappedItems = _mapper.Map<List<QuestionOptionsResponse>>(questionOptionsList);
            return ServiceResult<List<QuestionOptionsResponse>>.Success(mappedItems);
        }

        public async Task<ServiceResult<QuestionOptionsResponse?>> GetQuestionOptionsByIdAsync(int id)
        {
            var questionOptions = await _questionOptionsRepository.FindByIdAsync(id);
            if (questionOptions == null)
            {
                return ServiceResult<QuestionOptionsResponse?>.Failure("Không tìm thấy tùy chọn câu hỏi.");
            }

            var response = _mapper.Map<QuestionOptionsResponse>(questionOptions);
            return ServiceResult<QuestionOptionsResponse?>.Success(response);
        }

        public async Task<ServiceResult<QuestionOptionsResponse?>> CreateQuestionOptionsAsync(CreateQuestionOptionsRequest request)
        {
            if (request == null)
            {
                return ServiceResult<QuestionOptionsResponse?>.Failure("Dữ liệu tạo tùy chọn câu hỏi không hợp lệ.");
            }

            var validationResult = await _createQuestionOptionsRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ServiceResult<QuestionOptionsResponse?>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var questionOptions = _mapper.Map<QuestionOptions>(request);

            _questionOptionsRepository.Add(questionOptions);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<QuestionOptionsResponse?>.Failure("Thêm tùy chọn câu hỏi thất bại.");
            }

            var response = _mapper.Map<QuestionOptionsResponse>(questionOptions);
            return ServiceResult<QuestionOptionsResponse?>.Success(response);
        }

        public async Task<ServiceResult<QuestionOptionsResponse?>> UpdateQuestionOptionsAsync(int id, UpdateQuestionOptionsRequest request)
        {
            if (request == null)
            {
                return ServiceResult<QuestionOptionsResponse?>.Failure("Dữ liệu cập nhật tùy chọn câu hỏi không hợp lệ.");
            }

            var validationResult = await _updateQuestionOptionsRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ServiceResult<QuestionOptionsResponse?>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var questionOptions = await _questionOptionsRepository.FindByIdAsync(id);
            if (questionOptions == null)
            {
                return ServiceResult<QuestionOptionsResponse?>.Failure("Không tìm thấy tùy chọn câu hỏi.");
            }

            _mapper.Map(request, questionOptions);

            _questionOptionsRepository.Update(questionOptions);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<QuestionOptionsResponse?>.Failure("Cập nhật tùy chọn câu hỏi thất bại.");
            }

            var response = _mapper.Map<QuestionOptionsResponse>(questionOptions);
            return ServiceResult<QuestionOptionsResponse?>.Success(response);
        }

        public async Task<ServiceResult<bool>> DeleteQuestionOptionsAsync(int id)
        {
            var questionOptions = await _questionOptionsRepository.FindByIdAsync(id);
            if (questionOptions == null)
            {
                return ServiceResult<bool>.Failure("Không tìm thấy tùy chọn câu hỏi.");
            }

            _questionOptionsRepository.Remove(questionOptions);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<bool>.Failure("Xóa tùy chọn câu hỏi thất bại.");
            }

            return ServiceResult<bool>.Success(true);
        }
    }
}
