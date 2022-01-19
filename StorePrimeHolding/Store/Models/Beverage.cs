using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public class Beverage : PerishableProduct
    {
        public Beverage(string name, string brand, decimal price,
            DateTime expirationDate)
            : base(name, brand, price, expirationDate)
        {
        }

    }
}
