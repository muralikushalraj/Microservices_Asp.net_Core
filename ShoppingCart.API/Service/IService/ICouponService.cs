using ShoppingCart.API.Models.Dto;

namespace ShoppingCart.API.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
