using Auth.API.Models.Dto;

namespace Auth.API.Services.IServices
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto requestDto);
        Task<LoginResponseDto> Login(LoginRequestDto requestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
