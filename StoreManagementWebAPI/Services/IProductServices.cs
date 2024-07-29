using StoreManagementWebAPI.Models;

namespace StoreManagementWebAPI.Services
{
    public interface IProductServices
    {
        List<Product> GetProducts(string filepath);

        void UpdateProduct(Product product,string filepath);

        void DeleteProduct(string productCode, string filepath);
    }
}
