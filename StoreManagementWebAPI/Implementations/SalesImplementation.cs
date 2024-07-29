using StoreManagementWebAPI.Models;
using StoreManagementWebAPI.Services;

namespace StoreManagementWebAPI.Implementations
{
    public class SalesImplementation : ISalesServices
    {
        public List<SalesRecord> GetSalesReport(string filepath)
        {
            List<SalesRecord> selledProducts = [];
            if (System.IO.File.Exists(filepath))
            {
                var lines = System.IO.File.ReadAllLines(filepath);

                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    selledProducts.Add(new SalesRecord(data[0], data[1], decimal.Parse(data[2]), int.Parse(data[3])));
                }

                return selledProducts;
            }
            else
            {
                throw new FileNotFoundException("No File Found");
            }
        }

        public void UpdateSalesReport(Product product,int quantity, string filepath)
        {
            if (!File.Exists(filepath))
            {
                File.Create(filepath).Close();
            }

            var lines = File.ReadAllLines(filepath).ToList();
            var found = false;

            for (int i = 0; i < lines.Count; i++)
            {
                var data = lines[i].Split(',');
                if (data[0] == product.Code)
                {
                    var existingQuantity = int.Parse(data[3]);
                    lines[i] = $"{product.Code},{product.Name},{product.Price},{existingQuantity + quantity}";
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                lines.Add($"{product.Code},{product.Name},{product.Price},{quantity}");
            }

            File.WriteAllLines(filepath, lines);
        }
    }
}
