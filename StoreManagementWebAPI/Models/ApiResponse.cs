namespace StoreManagementWebAPI.Models;

public class ApiResponse
{
    public string Status { get; set; }
    public object Data { get; set; }
    public int? Count { get; set; }
    public string Error { get; set; }
}
