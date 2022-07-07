using System;

namespace Domain.Model
{
    public class Discount
    {
        public string Product { get; set; }

        public string Description { get; set; }

        public double DiscountPercentage { get; set; }

        public DiscountType DiscountType { get; set; }

        public DateTime DiscountTimeGateStart { get; set; }

        public DateTime DiscountTimeGateEnd { get; set; }

        public string DiscountBuyMoreItemsType { get; set; }

        public int DiscountBuyMoreItemsQty { get; set; }
    }
}