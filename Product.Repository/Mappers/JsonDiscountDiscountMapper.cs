using Domain.Model;
using Json.Repository.Entities;
using GenericInterfaces;

namespace Json.Repository.Mappers
{
    //Mapper responsible for turning our persistence-specific class into our domain model
    public class JsonDiscountDiscountMapper : IMapper<JsonDiscount, Discount>
    {
        public Discount Map(JsonDiscount source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(this.GetType().Name);
            }

            return new Discount
            {
                DiscountBuyMoreItemsQty = source.DiscountBuyMoreItemsQty,
                DiscountBuyMoreItemsType = source.DiscountBuyMoreItemsType,
                DiscountTimeGateEnd = source.DiscountTimeGateEnd,
                DiscountTimeGateStart = source.DiscountTimeGateStart,
                DiscountType = source.DiscountType,
                DiscountPercentage = source.DiscountPercentage,
                Description = source.Description,
                Product = source.Product,
            };
        }
    }
}