using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Auths;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.User;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.BusinessLayer.Services.Implementations
{
    public class UserServcie : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserServcie(
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<UserResponse>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var responses = new List<UserResponse>();

            foreach (var user in users)
            {
                var response = _mapper.Map<UserResponse>(user);
                response.Roles = (await _userManager.GetRolesAsync(user)).ToList();
                responses.Add(response);
            }

            return ServiceResult<List<UserResponse>>.Success(responses);
        }

        public async Task<ServiceResult<PagedResult<UserResponse>>> GetPagedUsersAsync(PagingFilterBase filters)
        {
            var pagedUsers = await _userRepository.GetPagedAsync(filters);
            var mappedItems = new List<UserResponse>();

            foreach (var user in pagedUsers.Items)
            {
                var response = _mapper.Map<UserResponse>(user);
                response.Roles = (await _userManager.GetRolesAsync(user)).ToList();
                mappedItems.Add(response);
            }

            var pagedResult = new PagedResult<UserResponse>
            {
                Items = mappedItems,
                TotalItems = pagedUsers.TotalItems,
                CurrentPage = pagedUsers.CurrentPage,
                PageSize = pagedUsers.PageSize
            };

            return ServiceResult<PagedResult<UserResponse>>.Success(pagedResult);
        }

        public async Task<ServiceResult<UserResponse?>> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.FindByIdAsync(id);
            if (user == null)
            {
                return ServiceResult<UserResponse?>.Failure("Người dùng không tồn tại.");
            }

            var response = _mapper.Map<UserResponse>(user);
            response.Roles = (await _userManager.GetRolesAsync(user)).ToList();
            return ServiceResult<UserResponse?>.Success(response);
        }

        public async Task<ServiceResult<UserResponse?>> CreateUserAsync(RegisterRequest request)
        {
            if (request == null)
            {
                return ServiceResult<UserResponse?>.Failure("Dữ liệu tạo người dùng không hợp lệ.");
            }

            var existingUserByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingUserByEmail != null)
            {
                return ServiceResult<UserResponse?>.Failure("Email đã tồn tại.");
            }

            var existingUserByUsername = await _userManager.FindByNameAsync(request.Username);
            if (existingUserByUsername != null)
            {
                return ServiceResult<UserResponse?>.Failure("Tên đăng nhập đã tồn tại.");
            }

            var newUser = _mapper.Map<ApplicationUser>(request);
            newUser.CreateAt = DateTime.UtcNow;
            newUser.UpdateAt = DateTime.UtcNow;
            newUser.IsActive = true;
            newUser.Dob = request.Dob.ToDateTime(TimeOnly.MinValue);

            var createResult = await _userManager.CreateAsync(newUser, request.Password);
            if (!createResult.Succeeded)
            {
                return ServiceResult<UserResponse?>.Failure(string.Join("; ", createResult.Errors.Select(e => e.Description)));
            }

            var roleResult = await _userManager.AddToRoleAsync(newUser, Utilizer.Role_Student);
            if (!roleResult.Succeeded)
            {
                return ServiceResult<UserResponse?>.Failure("Tạo người dùng thành công nhưng gán quyền thất bại.");
            }

            var response = _mapper.Map<UserResponse>(newUser);
            response.Roles = (await _userManager.GetRolesAsync(newUser)).ToList();
            return ServiceResult<UserResponse?>.Success(response);
        }

        public async Task<ServiceResult<UserResponse?>> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            if (request == null)
            {
                return ServiceResult<UserResponse?>.Failure("Dữ liệu cập nhật người dùng không hợp lệ.");
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return ServiceResult<UserResponse?>.Failure("Người dùng không tồn tại.");
            }

            var existingUserByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingUserByEmail != null && existingUserByEmail.Id != user.Id)
            {
                return ServiceResult<UserResponse?>.Failure("Email đã được sử dụng bởi tài khoản khác.");
            }

            var existingUserByUsername = await _userManager.FindByNameAsync(request.Username);
            if (existingUserByUsername != null && existingUserByUsername.Id != user.Id)
            {
                return ServiceResult<UserResponse?>.Failure("Tên đăng nhập đã được sử dụng bởi tài khoản khác.");
            }

            _mapper.Map(request, user);
            user.UpdateAt = DateTime.UtcNow;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return ServiceResult<UserResponse?>.Failure(string.Join("; ", updateResult.Errors.Select(e => e.Description)));
            }

            if (request.Roles != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                var rolesToRemove = currentRoles.Except(request.Roles).ToList();
                var rolesToAdd = request.Roles.Except(currentRoles).ToList();

                if (rolesToRemove.Count > 0)
                {
                    var removeRoleResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                    if (!removeRoleResult.Succeeded)
                    {
                        return ServiceResult<UserResponse?>.Failure("Không thể cập nhật quyền của người dùng.");
                    }
                }

                if (rolesToAdd.Count > 0)
                {
                    var addRoleResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
                    if (!addRoleResult.Succeeded)
                    {
                        return ServiceResult<UserResponse?>.Failure("Không thể cập nhật quyền của người dùng.");
                    }
                }
            }

            var response = _mapper.Map<UserResponse>(user);
            response.Roles = (await _userManager.GetRolesAsync(user)).ToList();
            return ServiceResult<UserResponse?>.Success(response);
        }

        public async Task<ServiceResult<bool>> DeleteUserAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return ServiceResult<bool>.Failure("Người dùng không tồn tại.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return ServiceResult<bool>.Failure(string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            return ServiceResult<bool>.Success(true);
        }
    }
}
