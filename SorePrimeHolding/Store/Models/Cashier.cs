using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public class Cashier
    {
        private Dictionary<Product, double> products;

        public Cashier()
        {
            this.products = new Dictionary<Product, double>();
        }

        public IReadOnlyDictionary<Product, double> Products { get => products; }

        public void Add(Product product, double quantity)
        {
            products.Add(product, quantity);
        }

        public void PrintReceipt(IReadOnlyDictionary<Product, double> products, DateTime dateOfPurchase)
        {
            decimal totalDiscount = 0.0m;
            decimal currentDiscount = 0.0m;
            decimal subTotal = 0.0m;
            int perecentage = 0;

            StringBuilder receipt = new StringBuilder();
            receipt.AppendLine($"Date: {dateOfPurchase.Year}-{dateOfPurchase.Month}-{dateOfPurchase.Day}" +
                $" {dateOfPurchase.Hour}:{dateOfPurchase.Minute}:{dateOfPurchase.Second}\n");
            receipt.AppendLine("---Products---\n\n");


            foreach (var product in products)
            {
                subTotal += product.Key.Price * (decimal)product.Value;

                currentDiscount += CalculateDiscount(product.Key, dateOfPurchase, ref perecentage) * (decimal)product.Value;
                receipt.AppendLine(AddProductToReceipt(product, currentDiscount, perecentage));
                totalDiscount += currentDiscount;
                perecentage = 0;
                currentDiscount = 0;
            }

            receipt.AppendLine(new string('-', 40));
            receipt.AppendLine($"SUBTOTAL: ${subTotal:f2}");
            receipt.AppendLine($"DISCOUNT: {(totalDiscount == 0 ? "$0.00" : $"-${totalDiscount:f2}")}\n");
            receipt.AppendLine($"TOTAL: ${subTotal - totalDiscount:f2}");

            Console.WriteLine(receipt);
        }



        /// <summary>
        /// Add product info to receipt
        /// </summary>
        /// <returns>
        /// (name) (brand)
        /// (quantity) x (price per product) = (total price without discount)
        /// #discount (discount %) (discount sum) (if applicable)
        /// </returns>
        private string AddProductToReceipt(KeyValuePair<Product, double> product, decimal discount, int percentage)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{product.Key.Name} - {product.Key.Brand}");
            sb.AppendLine($"{product.Value} x ${product.Key.Price} = ${(decimal)product.Value * product.Key.Price:f2}");
            sb.AppendLine(discount == 0 ? string.Empty : $"#discount {percentage}% -${discount:f2}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Calculate discount for product by type
        /// </summary>
        /// <returns>Add discount for every product</returns>
        private decimal CalculateDiscount(Product product, DateTime dateOfPurchase, ref int percentage)
        {

            decimal discount = product.Discount(dateOfPurchase);

            if (product.Price * 0.50m == discount)
            {
                percentage = 50;
            }
            else if(product.Price * 0.10m == discount)
            {
                percentage = 10;
            }
            else if (product.Price * 0.5m == discount)
            {
                percentage = 5;
            }

            return discount;
        }

    }
}