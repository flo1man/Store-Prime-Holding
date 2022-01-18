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
                string typeOfProduct = product.Key.GetType().Name;
                subTotal += product.Key.Price * (decimal)product.Value;

                currentDiscount += CalculateDiscount(typeOfProduct, product.Key, dateOfPurchase, ref perecentage) * (decimal)product.Value;
                receipt.AppendLine(AddProductToReceipt(product, currentDiscount, typeOfProduct, perecentage));
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
        private string AddProductToReceipt(KeyValuePair<Product, double> product, decimal discount, string typeOfProduct, int percentage)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{product.Key.Name} - {product.Key.Brand}");
            sb.AppendLine($"{product.Value} x ${product.Key.Price} = {(decimal)product.Value * product.Key.Price:f2}");
            sb.AppendLine(discount == 0 ? string.Empty : $"#discount {percentage}% -${discount:f2}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Calculate discount for product by type
        /// </summary>
        /// <returns>Add discount for every product</returns>
        private decimal CalculateDiscount(string typeOfProduct, Product product, DateTime dateOfPurchase, ref int percentage)
        {
            decimal discount = 0.0m;
            string dayOfWeek = dateOfPurchase.Date.DayOfWeek.ToString();

            if (typeOfProduct == "Food" || typeOfProduct == "Beverage")
            {
                PerishableProduct perishableProduct = (PerishableProduct)product;
                var daysToExpirate = perishableProduct.ExpirationDate.Subtract(dateOfPurchase).Days;

                if (daysToExpirate == 0)
                {
                    discount = perishableProduct.Price * 0.50m;
                    percentage = 50;
                }
                else if (daysToExpirate <= 5)
                {
                    discount = perishableProduct.Price * 0.10m;
                    percentage = 10;
                }
            }
            else if (typeOfProduct == "Cloth")
            {
                if (dayOfWeek == DaysOfWeekEnum.Monday.ToString() || dayOfWeek == DaysOfWeekEnum.Tuesday.ToString()
                    || dayOfWeek == DaysOfWeekEnum.Wednesday.ToString() || dayOfWeek == DaysOfWeekEnum.Thursday.ToString()
                    || dayOfWeek == DaysOfWeekEnum.Friday.ToString())
                {
                    discount = product.Price * 0.10m;
                    percentage = 10;
                }
            }
            else if (typeOfProduct == "Appliance")
            {
                if ((dayOfWeek == DaysOfWeekEnum.Saturday.ToString() || dayOfWeek == DaysOfWeekEnum.Sunday.ToString()) && product.Price > 999m)
                {
                    discount = product.Price * 0.05m;
                    percentage = 5;
                }
            }
            return discount;
        }

        /// <summary>
        /// Add each product receipt with full description / BONUS METHOD
        /// </summary>
        /// <returns>Product with full description</returns>
        private string AddProductToReceiptWithFullDescription(KeyValuePair<Product, double> product, decimal discount, string typeOfProduct, int percentage)
        {
            StringBuilder sb = new StringBuilder();
            if (typeOfProduct == "Food" || typeOfProduct == "Beverage")
            {
                PerishableProduct perishableProduct = (PerishableProduct)product.Key;
                sb.AppendLine($"{perishableProduct.Name} - {perishableProduct.Brand}");
                sb.AppendLine($"{product.Value} x ${perishableProduct.Price} = {(decimal)product.Value * perishableProduct.Price:f2}");
                sb.AppendLine(discount == 0 ? "\n" : $"#discount {percentage}% -${discount:f2}\n");
            }
            else if (typeOfProduct == "Cloth")
            {
                Cloth cloth = (Cloth)product.Key;
                sb.AppendLine($"{cloth.Name} {cloth.Brand} {cloth.ClothSize} {cloth.Color}");
                sb.AppendLine($"{product.Value} x ${cloth.Price} = {(decimal)product.Value * cloth.Price:f2}");
                sb.AppendLine(discount == 0 ? "\n" : $"#discount {percentage}% -${discount:f2}\n");
            }
            else if (typeOfProduct == "Appliance")
            {
                Appliance appliance = (Appliance)product.Key;
                sb.AppendLine($"{appliance.Name} {appliance.Brand} {appliance.Model}");
                sb.AppendLine($"{product.Value} x ${appliance.Price} = {(decimal)product.Value * appliance.Price:f2}");
                sb.AppendLine(discount == 0 ? "\n" : $"#discount {percentage}% -${discount:f2}\n");
            }
            return sb.ToString();
        }
    }
}
