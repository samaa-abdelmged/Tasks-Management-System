using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Tasks_Management_System.Application.DTOs.Auth;
using Tasks_Management_System.Application.Interfaces.Auth;
using Tasks_Management_System.Domain.Entities;

namespace Tasks_Management_System.Application.Services
{
    public class AuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public AuthService(
            IAuthRepository authRepository,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            IJwtService jwtService)
        {
            _authRepository = authRepository;
            _signInManager = signInManager;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var user = _mapper.Map<ApplicationUser>(dto);

            var result = await _authRepository.RegisterUserAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    Message = string.Join(",", result.Errors.Select(e => e.Description)),
                    Token = null
                };
            }

            var token = _jwtService.GenerateToken(user);

            return new AuthResponseDto
            {
                Message = "User Registered Successfully",
                Token = token
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _authRepository.GetUserByEmailAsync(dto.Email);

            if (user == null)
            {
                return new AuthResponseDto
                {
                    Message = "User Not Found",
                    Token = null
                };
            }

            var checkPassword = await _authRepository.CheckPasswordAsync(user, dto.Password);

            if (!checkPassword)
            {
                return new AuthResponseDto
                {
                    Message = "Invalid Login Attempt",
                    Token = null
                };
            }

            var token = _jwtService.GenerateToken(user);

            return new AuthResponseDto
            {
                Message = "Login Successful",
                Token = token
            };
        }

        public async Task<AuthResponseDto> LogoutAsync()
        {
            await _authRepository.LogoutAsync();

            return new AuthResponseDto
            {
                Message = "User Logged Out Successfully",
                Token = null
            };
        }
    }
}