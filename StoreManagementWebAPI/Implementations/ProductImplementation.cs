using StoreManagementWebAPI.Models;
using StoreManagementWebAPI.Services;

namespace StoreManagementWebAPI.Implementations
{
    public class ProductImplementation : IProductServices
    {
        public List<Product> GetProducts(string filepath)
        {
            List<Product> products = [];
            if (System.IO.File.Exists(filepath))
            {
                var lines = System.IO.File.ReadAllLines(filepath);

                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    products.Add(new Product(data[0], data[1], decimal.Parse(data[2]), int.Parse(data[3])));
                }

                return products;
            }
            else
            {
                throw new FileNotFoundException("No File Found");
            }
        }

        public void UpdateProduct(Product product,string filepath)
        {
            var lines = File.ReadAllLines(filepath).ToList();
            int found = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                var data = lines[i].Split(',');
                if (data[0] == product.Code)
                {
                    var existingQuantity = int.Parse(data[3]);
                    lines[i] = $"{product.Code},{product.Name},{product.Price},{product.Stock}";
                    found++;
                    break;
                }
            }
            File.WriteAllLines(filepath, lines);
            if (found == 0)
            {
                File.AppendAllText(filepath,$"{product.Code},{product.Name},{product.Price},{product.Stock}");
            }
            
        }

        public void DeleteProduct(string productCode, string filepath)
        {
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException("No File Found");
            }

            var lines = File.ReadAllLines(filepath).ToList();
            var lineToRemove = lines.FirstOrDefault(line => line.Split(',')[0] == productCode);

            if (lineToRemove != null)
            {
                lines.Remove(lineToRemove);
                File.WriteAllLines(filepath, lines);
            }
            else
            {
                throw new KeyNotFoundException($"Product with code {productCode} not found.");
            }
        }
    }
}
