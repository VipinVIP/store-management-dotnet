namespace StoreManagementWebAPI.Models
{
    public class SalesRecord
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public SalesRecord(string code, string name, decimal price, int stock)
        {
            Code = code;
            Name = name;
            Price = price;
            Stock = stock;
        }
    }
}
