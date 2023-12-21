using ShoppingCart.API.Models.Dto;

namespace ShoppingCart.API.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
