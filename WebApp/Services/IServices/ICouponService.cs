﻿using WebApp.Models;

namespace WebApp.Services.IServices
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponAsync(string couponCode);
        Task<ResponseDto?> GetAllCouponAsync();
        Task<ResponseDto?> GetCouponByIdAsync(int couponId);
        Task<ResponseDto?> CreateCouponAsync(CouponDto coupon);
        Task<ResponseDto?> UpdateCouponAsync(CouponDto coupon);
        Task<ResponseDto?> DeleteCouponAsync(int couponId);
    }
}
