﻿namespace InvoiceSystemAPI.DTOs
{
    public class InvoiceItemDetailsDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}
