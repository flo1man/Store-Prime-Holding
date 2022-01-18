using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public abstract class Product
    {
        private string name;
        private string brand;
        private decimal price;

        protected Product(string name, string brand, decimal price)
        {
            this.Name = name;
            this.Brand = brand;
            this.Price = price;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Name cannot be null or empty!");
                }
                name = value;
            }
        }

        public string Brand
        {
            get => brand;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Brand cannot be null or empty!");
                }
                brand = value;
            }
        }

        public decimal Price
        {
            get => price;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be negative number!");
                }
                price = value;
            }
        }
    }
}
