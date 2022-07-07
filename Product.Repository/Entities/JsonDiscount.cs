using Domain.Model;

namespace Json.Repository.Entities
{
    //Json entity, it technically is the same as our domain model, we could just use that
    //but thinking about extensibility (to other data sources for example), we should own
    //persistence-specific classes
    public class JsonDiscount
    {
        public string? Product { get; set; }

        public string? Description { get; set; }

        public double DiscountPercentage { get; set; }

        public DiscountType DiscountType { get; set; }

        public DateTime DiscountTimeGateStart { get; set; }

        public DateTime DiscountTimeGateEnd { get; set; }

        public string? DiscountBuyMoreItemsType { get; set; }

        public int DiscountBuyMoreItemsQty { get; set; }
    }
}