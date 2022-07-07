namespace Json.Repository.Repositories.Discount
{
    public interface IDiscountRepository
    {
        List<Domain.Model.Discount> GetAllDiscounts();
    }
}