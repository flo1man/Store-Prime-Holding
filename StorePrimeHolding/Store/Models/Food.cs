using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    class Food : PerishableProduct
    {
        public Food(string name, string brand, decimal price,
            DateTime expirationDate)
            : base(name, brand, price, expirationDate)
        {
        }
    }
}
