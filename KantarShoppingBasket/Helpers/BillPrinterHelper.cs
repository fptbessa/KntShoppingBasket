using Domain.Model;
using System;
using System.Linq;
using System.Text;

namespace KantarShoppingBasket.Helpers
{
    public static class BillPrinterHelper
    {
        private const string MissingProductsMessage = "Whoops! something went wrong, can't find item with the names: {0}";

        public static string PrintBill(Bill bill)
        {
            StringBuilder billStringBuilder = new("Shopping Cost");

            billStringBuilder.AppendLine();

            if (bill.Warnings.Any())
            {
                billStringBuilder.AppendLine(String.Format(MissingProductsMessage, String.Join(", ", bill.Warnings)));
            }

            if (!bill.Products.Any())
            {
                return billStringBuilder.ToString();
            }

            billStringBuilder.Append("Subtotal: €");

            var subtotal = bill.Products.Sum(product => product.Price);
            billStringBuilder.AppendLine(subtotal.ToString("0.00"));

            if (!bill.AppliedDiscounts.Any())
            {
                billStringBuilder.AppendLine("(No offers available)");
            }
            else
            {
                foreach (var appliedDiscount in bill.AppliedDiscounts)
                {
                    billStringBuilder.Append(appliedDiscount.Product.Name);
                    billStringBuilder.Append(' ');
                    billStringBuilder.Append(appliedDiscount.Description);
                    billStringBuilder.Append(": -");
                    billStringBuilder.Append(appliedDiscount.Savings.ToString("0.00"));
                    billStringBuilder.AppendLine("€");
                }
            }

            billStringBuilder.Append("Total: €");

            var discountTotal = bill.AppliedDiscounts.Sum(appliedDiscount => appliedDiscount.Savings);

            var total = subtotal - discountTotal;
            billStringBuilder.Append(total.ToString("0.00"));

            return billStringBuilder.ToString();
        }
    }
}