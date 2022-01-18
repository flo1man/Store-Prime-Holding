using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    class Cloth : Product
    {
        private string color;

        public Cloth(string name, string brand, decimal price,
            ClothSizeEnum clothSize, string color)
            : base(name, brand, price)
        {
            this.ClothSize = clothSize;
            this.Color = color;
        }

        public ClothSizeEnum ClothSize { get; private set; }

        public string Color
        {
            get => color;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Color cannot be null or empty!");
                }
                color = value;
            }
        }
    }
}
