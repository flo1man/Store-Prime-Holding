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
    }
}
