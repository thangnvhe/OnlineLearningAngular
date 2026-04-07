using AutoMapper;
using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Module;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.BusinessLayer.Services.Implementations
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateModuleRequest> _createModuleRequestValidator;
        private readonly IValidator<UpdateModuleRequest> _updateModuleRequestValidator;

        public ModuleService(
            IModuleRepository moduleRepository, 
            IMapper mapper, 
            IUnitOfWork unitOfWork,
            IValidator<CreateModuleRequest> createModuleRequestValidator,
            IValidator<UpdateModuleRequest> updateModuleRequestValidator)
        {
            _moduleRepository = moduleRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createModuleRequestValidator = createModuleRequestValidator;
            _updateModuleRequestValidator = updateModuleRequestValidator;
        }

        public async Task<ServiceResult<List<ModuleResponse>>> GetModulesByCourseIdAsync(int courseId)
        {
            var modules = await _moduleRepository.GetModulesByCourseIdAsync(courseId);
            var mappedItems = _mapper.Map<List<ModuleResponse>>(modules);
            return ServiceResult<List<ModuleResponse>>.Success(mappedItems);
        }

        public async Task<ServiceResult<ModuleResponse?>> GetModuleByIdAsync(int id)
        {
            var module = await _moduleRepository.FindByIdAsync(id);
            if (module == null)
            {
                return ServiceResult<ModuleResponse?>.Failure("Không tìm thấy module.");
            }

            var response = _mapper.Map<ModuleResponse>(module);
            return ServiceResult<ModuleResponse?>.Success(response);
        }

        public async Task<ServiceResult<ModuleResponse?>> CreateModuleAsync(CreateModuleRequest request)
        {
            if (request == null)
            {
                return ServiceResult<ModuleResponse?>.Failure("Dữ liệu tạo module không hợp lệ.");
            }

            var validationResult = await _createModuleRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ServiceResult<ModuleResponse?>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var module = _mapper.Map<Module>(request);

            _moduleRepository.Add(module);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<ModuleResponse?>.Failure("Thêm module thất bại.");
            }

            var response = _mapper.Map<ModuleResponse>(module);
            return ServiceResult<ModuleResponse?>.Success(response);
        }

        public async Task<ServiceResult<ModuleResponse?>> UpdateModuleAsync(int id, UpdateModuleRequest request)
        {
            if (request == null)
            {
                return ServiceResult<ModuleResponse?>.Failure("Dữ liệu cập nhật module không hợp lệ.");
            }

            var validationResult = await _updateModuleRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ServiceResult<ModuleResponse?>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var module = await _moduleRepository.FindByIdAsync(id);
            if (module == null)
            {
                return ServiceResult<ModuleResponse?>.Failure("Không tìm thấy module.");
            }

            _mapper.Map(request, module);

            _moduleRepository.Update(module);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<ModuleResponse?>.Failure("Cập nhật module thất bại.");
            }

            var response = _mapper.Map<ModuleResponse>(module);
            return ServiceResult<ModuleResponse?>.Success(response);
        }

        public async Task<ServiceResult<bool>> DeleteModuleAsync(int id)
        {
            var module = await _moduleRepository.FindByIdAsync(id);
            if (module == null)
            {
                return ServiceResult<bool>.Failure("Không tìm thấy module.");
            }

            _moduleRepository.Remove(module);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return ServiceResult<bool>.Failure("Xóa module thất bại.");
            }

            return ServiceResult<bool>.Success(true);
        }
    }
}
