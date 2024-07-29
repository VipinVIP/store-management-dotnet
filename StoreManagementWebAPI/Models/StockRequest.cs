﻿namespace StoreManagementWebAPI.Models
{
    public class StockRequest
    {
        public string Code { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int Stock { get; set; }
    }
}
