using Json.Repository.Mappers;
using Json.Repository.Repositories.Discount;
using Json.Repository.Repositories.Product;
using Service;
using System.IO.Abstractions;

namespace KantarShoppingBasket.Bootstrapper
{
    public static class ShoppingBasketBootstrapper
    {
        public const string AvailableProductsPath = "MockData/PurchasableProducts.json";
        public const string DiscountsPath = "MockData/Discounts.json";

        public static BillService BootstrapApplicationLayer()
        {
            //Decision: Obviously in another kind of application such as a REST API we'd do this by
            //dependency injection to the application containers. Since this is a console application, I
            //don't see the point of going that deep, so I'll implement a basic Bootstrapper that will
            //instantiate our Service class.

            var productMapper = new JsonProductProductMapper();
            var discountMapper = new JsonDiscountDiscountMapper();

            var productRepository = new ProductRepository(productMapper, new FileSystem(), AvailableProductsPath);
            var discountRepository = new DiscountRepository(discountMapper, new FileSystem(), DiscountsPath);

            return new BillService(productRepository, discountRepository);
        }
    }
}