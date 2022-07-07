using Domain.Model;
using Json.Repository.Entities;
using GenericInterfaces;

namespace Json.Repository.Mappers
{
    public class JsonProductProductMapper : IMapper<JsonProduct, Product>
    {
        public Product Map(JsonProduct source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(this.GetType().Name);
            }

            return new Product
            {
                Name = source.Name,
                Price = source.Price
            };
        }
    }
}