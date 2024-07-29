using StoreManagementWebAPI.Models;

namespace StoreManagementWebAPI.Services
{
    public interface ISalesServices
    {
        List<SalesRecord> GetSalesReport(string filepath);

        void UpdateSalesReport(Product product, int quantity, string filepath);

    }
}
