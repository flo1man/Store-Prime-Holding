using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public class Appliance : Product
    {
        private string model;
        private double weight;

        public Appliance(string name, string brand, decimal price,
            string model, DateTime productionDate, double weight)
            : base(name, brand, price)
        {
            this.Model = model;
            this.ProductionDate = productionDate;
            this.Weight = weight;
        }

        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Model cannot be null or empty!");
                }
                model = value;
            }
        }

        public DateTime ProductionDate { get; private set; }

        public double Weight
        {
            get => weight;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Weight cannot have zero or negative value!");
                }
                weight = value;
            }
        }
    }
}
