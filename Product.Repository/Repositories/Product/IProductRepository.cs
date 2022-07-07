namespace Json.Repository.Repositories.Product
{
    public interface IProductRepository
    {
        List<Domain.Model.Product> GetAllProducts();
    }
}