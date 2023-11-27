using WebApp.Models;
using WebApp.Services.IServices;
using static WebApp.Utility.StaticData;

namespace WebApp.Services
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> CreateCouponAsync(CouponDto coupon)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = coupon,
                Url = CouponApiUrl + "/api/coupon"
            });
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.DELETE,
                Url = CouponApiUrl + "/api/coupon/" + couponId
            });
        }

        public async Task<ResponseDto?> GetAllCouponAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = CouponApiUrl + "/api/coupon"
            });
        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = CouponApiUrl + "/api/coupon/" + couponId
            });
        }

        public async Task<ResponseDto?> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = CouponApiUrl + "/api/coupon/GetByCode/" + couponCode
            });
        }

        public async Task<ResponseDto?> UpdateCouponAsync(CouponDto coupon)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.PUT,
                Data = coupon,
                Url = CouponApiUrl + $"/api/coupon"
            });
        }
    }
}
