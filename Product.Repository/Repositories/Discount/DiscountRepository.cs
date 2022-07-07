using Json.Repository.Entities;
using Json.Repository.Repositories.Generic;
using Newtonsoft.Json;
using System.IO.Abstractions;
using GenericInterfaces;

namespace Json.Repository.Repositories.Discount
{
    public class DiscountRepository : JsonRepository, IDiscountRepository
    {
        private readonly IMapper<JsonDiscount, Domain.Model.Discount> mapper;

        //Dependencies are inject into the classes that need them
        //SOLID principle - D
        public DiscountRepository(IMapper<JsonDiscount, Domain.Model.Discount> mapper, IFileSystem fileSystem, string jsonDataPath) : base(fileSystem, jsonDataPath)
        {
            this.mapper = mapper;
        }

        public List<Domain.Model.Discount> GetAllDiscounts()
        {
            //Decision:
            //this could use with some kind of cache, I left it out because in real case scenarios that's often
            //handled by whichever ORM we use

            var jsonData = base.GetJsonDataAsString();

            var jsonProducts = JsonConvert.DeserializeObject<List<JsonDiscount>>(jsonData);

            return jsonProducts is null
                ? new List<Domain.Model.Discount>()
                : jsonProducts.Select(mapper.Map).ToList();
        }
    }
}