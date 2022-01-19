using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public class Cloth : Product
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

        public override decimal Discount(DateTime dateOfPurchase)
        {
            string dayOfWeek = dateOfPurchase.Date.DayOfWeek.ToString();

            if (dayOfWeek == DaysOfWeekEnum.Monday.ToString() || dayOfWeek == DaysOfWeekEnum.Tuesday.ToString()
                    || dayOfWeek == DaysOfWeekEnum.Wednesday.ToString() || dayOfWeek == DaysOfWeekEnum.Thursday.ToString()
                    || dayOfWeek == DaysOfWeekEnum.Friday.ToString())
            {
                return this.Price * 0.10m;
            }

            return base.Discount(dateOfPurchase);
        }
    }
}