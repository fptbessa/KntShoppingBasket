using System;
using System.Linq;
using KantarShoppingBasket.Bootstrapper;
using KantarShoppingBasket.Helpers;

namespace KantarShoppingBasket
{
    internal class Program
    {
        /* A note:
         *
         * * I'll be documenting my decision and assumptions in code comments, but obviously you're welcome to
           reach out if you want to know more details
        */

        private static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("Please insert some items to buy!");

                return;
            }

            //Decision: I've already shown the mapping of repository entities to domain entities to reflect the
            //best practises of a layered application. In a real application, I'd also ask our Application layer to
            //return DTOs to our console but since the classes would have the same schema anyway that'd just be
            //busy work, so I'll abstain from doing so here.
            var service = ShoppingBasketBootstrapper.BootstrapApplicationLayer();

            //Sanitize inputs
            var sanitizedArgs = args.Select(arg => arg.ToLower()).ToList();

            var bill = service.ShopForProducts(sanitizedArgs);

            //The bill printer will have a different implementation depending on what it's printed on,
            //so it's implementation should be the responsibility of the project that shows it, in this case
            //the console application -> KantarShoppingBasket.Helpers
            var printedBill = BillPrinterHelper.PrintBill(bill);

            //Necessary to display € symbol
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine(printedBill);
        }
    }
}