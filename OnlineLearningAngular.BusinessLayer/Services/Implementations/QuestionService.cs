using AutoMapper;
using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Question;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.BusinessLayer.Services.Implementations
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateQuestionRequest> _createQuestionRequestValidator;
        private readonly IValidator<UpdateQuestionRequest> _updateQuestionRequestValidator;

        public QuestionService(
            IQuestionRepository questionRepository, 
            IMapper mapper, 
            IUnitOfWork unitOfWork,
            IValidator<CreateQuestionRequest> createQuestionRequestValidator,
            IValidator<UpdateQuestionRequest> updateQuestionRequestValidator)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createQuestionRequestValidator = createQuestionRequestValidator;
            _updateQuestionRequestValidator = updateQuestionRequestValidator;
        }

        public async Task<ServiceResult<List<QuestionResponse>>> GetQuestionsByExamIdAsync(int examId)
        {
            var questions = await _questionRepository.GetQuestionsByExamIdAsync(examId);
            var mappedItems = _mapper.Map<List<QuestionResponse>>(questions);
            return ServiceResult<List<QuestionResponse>>.Success(mappedItems);
        }

        public async Task<ServiceResult<QuestionResponse?>> GetQuestionByIdAsync(int id)
        {
            var question = await _questionRepository.FindByIdAsync(id);
            if (question == null)
            {
                return ServiceResult<QuestionResponse?>.Failure("Không tìm thấy câu hỏi.");
            }

            var response = _mapper.Map<QuestionResponse>(question);
            return ServiceResult<QuestionResponse?>.Success(response);
        }

        public async Task<ServiceResult<QuestionResponse?>> CreateQuestionAsync(CreateQuestionRequest request)
        {
            if (request == null)
            {
                return ServiceResult<QuestionResponse?>.Failure("Dữ liệu tạo câu hỏi không hợp lệ.");
            }

            var validationResult = await _createQuestionRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ServiceResult<QuestionResponse?>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var question = _mapper.Map<Question>(request);

            _questionRepository.Add(question);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<QuestionResponse?>.Failure("Thêm câu hỏi thất bại.");
            }

            var response = _mapper.Map<QuestionResponse>(question);
            return ServiceResult<QuestionResponse?>.Success(response);
        }

        public async Task<ServiceResult<QuestionResponse?>> UpdateQuestionAsync(int id, UpdateQuestionRequest request)
        {
            if (request == null)
            {
                return ServiceResult<QuestionResponse?>.Failure("Dữ liệu cập nhật câu hỏi không hợp lệ.");
            }

            var validationResult = await _updateQuestionRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ServiceResult<QuestionResponse?>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var question = await _questionRepository.FindByIdAsync(id);
            if (question == null)
            {
                return ServiceResult<QuestionResponse?>.Failure("Không tìm thấy câu hỏi.");
            }

            _mapper.Map(request, question);

            _questionRepository.Update(question);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<QuestionResponse?>.Failure("Cập nhật câu hỏi thất bại.");
            }

            var response = _mapper.Map<QuestionResponse>(question);
            return ServiceResult<QuestionResponse?>.Success(response);
        }

        public async Task<ServiceResult<bool>> DeleteQuestionAsync(int id)
        {
            var question = await _questionRepository.FindByIdAsync(id);
            if (question == null)
            {
                return ServiceResult<bool>.Failure("Không tìm thấy câu hỏi.");
            }

            _questionRepository.Remove(question);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<bool>.Failure("Xóa câu hỏi thất bại.");
            }

            return ServiceResult<bool>.Success(true);
        }
    }
}
