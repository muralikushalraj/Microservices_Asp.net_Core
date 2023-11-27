using Auth.API.Data;
using Auth.API.Models;
using Auth.API.Models.Dto;
using Auth.API.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.Xml;

namespace Auth.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtGenerator _jwtGenerator;
        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtGenerator jwtGenerator)
        {
            _dbContext = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtGenerator = jwtGenerator;
        }
        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;

        }
        public async Task<LoginResponseDto> Login(LoginRequestDto requestDto)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == requestDto.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, requestDto.Password);

            if (user == null || !isValid)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }
            var token = _jwtGenerator.GenerateToken(user);
            UserDto userDTO = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            return new LoginResponseDto() { User = userDTO, Token = token };
        }

        public async Task<string> Register(RegistrationRequestDto requestDto)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = requestDto.Email,
                Email = requestDto.Email,
                NormalizedEmail = requestDto.Email.ToUpper(),
                PhoneNumber = requestDto.PhoneNumber,
                Name = requestDto.Name
            };
            try
            {
                var result = await _userManager.CreateAsync(user, requestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _dbContext.ApplicationUsers.First(x => x.UserName == requestDto.Email);
                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }
    }
}
