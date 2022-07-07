using Domain.Model;

namespace Service.Helpers
{
    //This class is responsible only for applying valid discounts to the bill
    //SOLID principle - S
    public static class DiscountHelper
    {
        public static List<AppliedDiscount> DiscountAppliesTo(List<Product> billProducts, Discount discount)
        {
            switch (discount.DiscountType)
            {
                case DiscountType.BuyMoreItems:
                    return BuyMoreItemsDiscountAppliesTo(billProducts, discount);

                case DiscountType.TimeGate:
                    return TimeGateDiscountAppliesTo(billProducts, discount);

                default:
                    //No discounts are applied for not yet implemented discount types, although the class
                    //is open for extension, SOLID principle - O
                    return new List<AppliedDiscount>();
            }
        }

        private static List<AppliedDiscount> TimeGateDiscountAppliesTo(List<Product> billProducts, Discount discount)
        {
            var allDiscounts = new List<AppliedDiscount>();

            var currentDate = DateTime.UtcNow;

            //Reduce the amount of nested code by inverting positive conditions
            if (currentDate > discount.DiscountTimeGateEnd
                && currentDate < discount.DiscountTimeGateStart)
            {
                return new List<AppliedDiscount>();
            }

            //We can only apply the discount to products that match it
            var eligibleBillProducts = billProducts.Where(billProduct =>
                billProduct.Name.ToLower().Equals(discount.Product.ToLower()));

            foreach (var eligibleBillProduct in eligibleBillProducts)
            {
                allDiscounts.Add(new AppliedDiscount
                {
                    Product = eligibleBillProduct,
                    Description = discount.Description,
                    Savings = discount.DiscountPercentage,
                });
            }

            return allDiscounts;
        }

        private static List<AppliedDiscount> BuyMoreItemsDiscountAppliesTo(List<Product> billProducts, Discount discount)
        {
            var eligibleBillProducts = billProducts.Where(billProduct =>
                billProduct.Name.ToLower().Equals(discount.DiscountBuyMoreItemsType.ToLower()));

            if (!eligibleBillProducts.Any())
            {
                return new List<AppliedDiscount>();
            }

            //Decision: The spec was not clear about this, so I'm assuming we can apply a Multi-Item discount
            //multiple times
            var timesWeCanApplyDiscount = Math.Truncate(
                                                (double)eligibleBillProducts.Count() /
                                                discount.DiscountBuyMoreItemsQty);

            var allAppliedDiscounts = new List<AppliedDiscount>();

            for (int i = 0; i < timesWeCanApplyDiscount; i++)
            {
                var pivotProduct = billProducts.First(product => product.Name.ToLower().Equals(discount.Product.ToLower()));

                allAppliedDiscounts.Add(new AppliedDiscount
                {
                    Product = pivotProduct,
                    Description = discount.Description,
                    Savings = pivotProduct.Price * discount.DiscountPercentage,
                });
            }

            return allAppliedDiscounts;
        }
    }
}