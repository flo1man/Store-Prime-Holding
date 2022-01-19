using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public abstract class PerishableProduct : Product
    {
        protected PerishableProduct(string name, string brand, decimal price
            , DateTime expirationDate)
            : base(name, brand, price)
        {
            this.ExpirationDate = expirationDate;
        }

        public DateTime ExpirationDate { get; private set; }

        public override decimal Discount(DateTime dateOfPurchase)
        {
            var daysToExpirate = this.ExpirationDate.Subtract(dateOfPurchase).Days;

            if (daysToExpirate == 0)
            {
                return this.Price * 0.50m;
            }
            else if (daysToExpirate <= 5)
            {
                return this.Price * 0.10m;
            }

            return base.Discount(dateOfPurchase);
        }
    }
}