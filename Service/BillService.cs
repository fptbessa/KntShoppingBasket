using Domain.Model;
using Json.Repository.Repositories.Discount;
using Json.Repository.Repositories.Product;
using Service.Helpers;

namespace Service
{
    public class BillService
    {
        private readonly IProductRepository productRepository;
        private readonly IDiscountRepository discountRepository;

        //Dependencies are inject into the classes that need them
        //SOLID principle - D
        public BillService(IProductRepository productRepository, IDiscountRepository discountRepository)
        {
            this.productRepository = productRepository;
            this.discountRepository = discountRepository;
        }

        /// <summary>
        ///     Fetches data from the repositories and creates a Bill for purchaseabla products
        /// </summary>
        /// <param name="productsToBuy"></param>
        /// <returns>An itemized bill that also contains applied discounts and warnings for missing items</returns>
        public Bill ShopForProducts(IEnumerable<string> productsToBuy)
        {
            var bill = this.CreateBill(productsToBuy);

            var availableDiscounts = this.discountRepository.GetAllDiscounts();

            ApplyDiscounts(bill, availableDiscounts);

            return bill;
        }

        //This method uses no instance data and is essentially a wrapper for the helper invocation,
        //so I'm marking it as static
        private static void ApplyDiscounts(Bill bill, List<Discount> availableDiscounts)
        {
            foreach (var availableDiscount in availableDiscounts)
            {
                bill.AppliedDiscounts.AddRange(DiscountHelper.DiscountAppliesTo(bill.Products, availableDiscount));
            }
        }

        private Bill CreateBill(IEnumerable<string> productsToBuy)
        {
            var availableProducts = this.productRepository.GetAllProducts();

            var unavailableProducts = productsToBuy
                    .Except(availableProducts
                        .Select(availableProduct => availableProduct.Name.ToLower())).ToList();

            var bill = new Bill
            {
                Warnings = unavailableProducts,
            };

            //We only want to add to the bill products that we have for sale
            foreach (var productToBuy in productsToBuy)
            {
                var matchingProduct = availableProducts.FirstOrDefault(
                        availableProduct => availableProduct.Name.ToLower().Equals(productToBuy));

                if (matchingProduct != null)
                {
                    bill.Products.Add(matchingProduct);
                }
            }

            return bill;
        }
    }
}