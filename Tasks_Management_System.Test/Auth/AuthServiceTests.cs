using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Tasks_Management_System.Application.DTOs.Auth;
using Tasks_Management_System.Application.Interfaces.Auth;
using Tasks_Management_System.Application.Services;
using Tasks_Management_System.Domain.Entities;
using Xunit;

namespace Tasks_Management_System.Tests.Auth
{
    public class AuthServiceTests
    {
        private readonly Mock<IAuthRepository> _authRepositoryMock;
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _authRepositoryMock = new Mock<IAuthRepository>();
            _jwtServiceMock = new Mock<IJwtService>();
            _mapperMock = new Mock<IMapper>();

            _authService = new AuthService(
                _authRepositoryMock.Object,
                null,
                _mapperMock.Object,
                _jwtServiceMock.Object
            );
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnToken_WhenRegisterSuccess()
        {
            // Arrange
            var dto = new RegisterDto
            {
                Name = "Test User",
                Email = "test@test.com",
                Password = "123456"
            };

            var user = new ApplicationUser();

            _mapperMock.Setup(x => x.Map<ApplicationUser>(dto))
                       .Returns(user);

            _authRepositoryMock.Setup(x => x.RegisterUserAsync(user, dto.Password))
                               .ReturnsAsync(IdentityResult.Success);

            _jwtServiceMock.Setup(x => x.GenerateToken(user))
                           .Returns("fake-jwt-token");

            // Act
            var result = await _authService.RegisterAsync(dto);

            // Assert
            result.Token.Should().Be("fake-jwt-token");
            result.Message.Should().Be("User Registered Successfully");
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnError_WhenRegisterFails()
        {
            // Arrange
            var dto = new RegisterDto
            {
                Name = "Test User",
                Email = "test@test.com",
                Password = "123456"
            };

            var user = new ApplicationUser();

            var identityError = new IdentityError
            {
                Description = "Email already exists"
            };

            _mapperMock.Setup(x => x.Map<ApplicationUser>(dto))
                       .Returns(user);

            _authRepositoryMock.Setup(x => x.RegisterUserAsync(user, dto.Password))
                               .ReturnsAsync(IdentityResult.Failed(identityError));

            // Act
            var result = await _authService.RegisterAsync(dto);

            // Assert
            result.Token.Should().BeNull();
            result.Message.Should().Contain("Email already exists");
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnUserNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var dto = new LoginDto
            {
                Email = "test@test.com",
                Password = "123"
            };

            _authRepositoryMock.Setup(x => x.GetUserByEmailAsync(dto.Email))
                               .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _authService.LoginAsync(dto);

            // Assert
            result.Token.Should().BeNull();
            result.Message.Should().Be("User Not Found");
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnInvalidAttempt_WhenPasswordWrong()
        {
            // Arrange
            var dto = new LoginDto
            {
                Email = "test@test.com",
                Password = "123"
            };

            var user = new ApplicationUser();

            _authRepositoryMock.Setup(x => x.GetUserByEmailAsync(dto.Email))
                               .ReturnsAsync(user);

            _authRepositoryMock.Setup(x => x.CheckPasswordAsync(user, dto.Password))
                               .ReturnsAsync(false);

            // Act
            var result = await _authService.LoginAsync(dto);

            // Assert
            result.Token.Should().BeNull();
            result.Message.Should().Be("Invalid Login Attempt");
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenLoginSuccess()
        {
            // Arrange
            var dto = new LoginDto
            {
                Email = "test@test.com",
                Password = "123"
            };

            var user = new ApplicationUser();

            _authRepositoryMock.Setup(x => x.GetUserByEmailAsync(dto.Email))
                               .ReturnsAsync(user);

            _authRepositoryMock.Setup(x => x.CheckPasswordAsync(user, dto.Password))
                               .ReturnsAsync(true);

            _jwtServiceMock.Setup(x => x.GenerateToken(user))
                           .Returns("fake-token");

            // Act
            var result = await _authService.LoginAsync(dto);

            // Assert
            result.Token.Should().Be("fake-token");
            result.Message.Should().Be("Login Successful");
        }

        [Fact]
        public async Task LogoutAsync_ShouldReturnSuccessMessage()
        {
            // Arrange
            _authRepositoryMock.Setup(x => x.LogoutAsync())
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _authService.LogoutAsync();

            // Assert
            result.Message.Should().Be("User Logged Out Successfully");
            result.Token.Should().BeNull();
        }
    }
}