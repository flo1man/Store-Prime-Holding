using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public class Food : PerishableProduct
    {
        public Food(string name, string brand, decimal price,
            DateTime expirationDate)
            : base(name, brand, price, expirationDate)
        {
        }
    }
}
