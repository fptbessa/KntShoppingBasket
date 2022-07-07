using System.Collections.Generic;

namespace Domain.Model
{
    public class Bill
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public List<AppliedDiscount> AppliedDiscounts { get; set; } = new List<AppliedDiscount>();

        public List<string> Warnings { get; set; } = new List<string>();
    }
}