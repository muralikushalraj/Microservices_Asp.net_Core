using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models;
using WebApp.Services.IServices;

namespace WebApp.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _CouponService;
        public CouponController(ICouponService couponService)
        {
            _CouponService = couponService;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? couponList = new();
            ResponseDto? responseDto = await _CouponService.GetAllCouponAsync();
            if (responseDto != null && responseDto.IsSuccess)
            {
                couponList = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return View(couponList);
        }
        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto couponDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? responseDto = await _CouponService.CreateCouponAsync(couponDto);
                if (responseDto != null && responseDto.IsSuccess)
                {
                    TempData["success"] = "Coupon Created Successfully";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = responseDto?.Message;
                }
            }
            return View(couponDto);
        }
        public async Task<IActionResult> CouponDelete(int couponId)
        {
            CouponDto? coupon = new();
            ResponseDto? responseDto = await _CouponService.GetCouponByIdAsync(couponId);
            if (responseDto != null && responseDto.IsSuccess)
            {
                coupon = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Result));
                return View(coupon);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {            
            ResponseDto? responseDto = await _CouponService.DeleteCouponAsync(couponDto.CouponId);
            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = "Coupon Deleted Successfully";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return View(couponDto);
        }
    }
}
