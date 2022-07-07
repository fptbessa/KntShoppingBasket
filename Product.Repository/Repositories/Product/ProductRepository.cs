using Json.Repository.Entities;
using Json.Repository.Repositories.Generic;
using Json.Repository.Mappers;
using Newtonsoft.Json;
using System.IO.Abstractions;

namespace Json.Repository.Repositories.Product
{
    public class ProductRepository : JsonRepository, IProductRepository
    {
        private readonly JsonProductProductMapper mapper;

        //Dependencies are inject into the classes that need them
        //SOLID principle - D
        public ProductRepository(JsonProductProductMapper mapper, IFileSystem fileSystem, string jsonDataPath) : base(fileSystem, jsonDataPath)
        {
            this.mapper = mapper;
        }

        public List<Domain.Model.Product> GetAllProducts()
        {
            //Decision:
            //this could use with some kind of cache, I left it out because in real case scenarios that's often
            //handled by whichever ORM we use

            var jsonData = base.GetJsonDataAsString();

            var jsonProducts = JsonConvert.DeserializeObject<List<JsonProduct>>(jsonData);

            return jsonProducts is null
                ? new List<Domain.Model.Product>()
                : jsonProducts.Select(mapper.Map).ToList();
        }
    }
}