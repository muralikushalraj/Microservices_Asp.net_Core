using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebApp.Models;
using WebApp.Models.Dto;
using WebApp.Services.IServices;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            ResponseDto responseDto = await _authService.LoginAsync(loginRequestDto);

            if (responseDto != null && responseDto.IsSuccess)
            {
                LoginResponseDto loginResponseDto =
                    JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", responseDto.Message);
                return View(loginRequestDto);
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=StaticData.RoleAdmin,Value=StaticData.RoleAdmin},
                new SelectListItem{Text=StaticData.RoleCustomer,Value=StaticData.RoleCustomer},
            };

            ViewBag.RoleList = roleList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {
            ResponseDto responseDto = await _authService.RegisterAsync(registrationRequestDto);
            ResponseDto assignRole = null;

            if (responseDto != null && responseDto.IsSuccess)
            {
                if (string.IsNullOrEmpty(registrationRequestDto.Role))
                    registrationRequestDto.Role = StaticData.RoleCustomer;

                assignRole = await _authService.AssignRoleAsync(registrationRequestDto);

                if (assignRole != null && assignRole.IsSuccess)
                {
                    TempData["success"] = "Registration Successfull";
                    return RedirectToAction(nameof(Login));
                }
            }
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=StaticData.RoleAdmin,Value=StaticData.RoleAdmin},
                new SelectListItem{Text=StaticData.RoleCustomer,Value=StaticData.RoleCustomer},
            };

            ViewBag.RoleList = roleList;
            return View(registrationRequestDto);
        }

        public IActionResult LogOut()
        {
            return View();
        }
    }
}
