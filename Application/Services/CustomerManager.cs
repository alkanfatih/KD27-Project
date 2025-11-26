using Application.DTOs.UserDTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CustomerManager : ICustomerService
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerManager(
            UserManager<Customer> userManager,
            SignInManager<Customer> signInManager,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> RegisterAsync(CreateUserDto dto)
        {
            var user = _mapper.Map<Customer>(dto);
            user.UserName = dto.UserName;

            // Aynı e-mail var mı kontrol et
            var existing = await _userManager.FindByEmailAsync(dto.Email);
            if (existing is not null)
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Email", new[] { "Bu e-posta adresi zaten kullanılmakta." } }
                });

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors
                    .GroupBy(e => e.Code)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.Description).ToArray()));
            }

            return user.Id;
        }

        public async Task<UserDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new NotFoundException("Kullanıcı bulunamadı.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                throw new Exceptions.ApplicationException("Şifre hatalı.");

            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateAsync(UpdateUserDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null)
                throw new NotFoundException("Kullanıcı bulunamadı.");

            _mapper.Map(dto, user);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exceptions.ApplicationException("Kullanıcı güncellenemedi.");
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new NotFoundException("Kullanıcı bulunamadı.");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new Exceptions.ApplicationException("Kullanıcı silinemedi.");
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = _userManager.Users.ToList();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<List<UserListDto>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var list = new List<UserListDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                list.Add(new UserListDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = roles.FirstOrDefault() ?? "Yok"
                });
            }

            return list;
        }

        public async Task<List<UserListDto>> GetUsersByRolesAsync(List<string> roles)
        {
            var users = _userManager.Users.ToList();
            var filteredList = new List<UserListDto>();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var role = userRoles.FirstOrDefault();

                if (role != null && roles.Contains(role))
                {
                    filteredList.Add(new UserListDto
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Role = role
                    });
                }
            }

            return filteredList;
        }
    }
}
