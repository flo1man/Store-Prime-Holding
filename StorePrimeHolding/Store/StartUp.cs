using System;

namespace Store
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Cashier cashier = new Cashier();

            Product product1 = new Food("apples", "BrandA", 1.50m,
                new DateTime(2021,06,14));

            Product product2 = new Beverage("milk", "BrandM", 0.99m,
                new DateTime(2022,02,02));

            Product product3 = new Cloth("T-shirt", "BrandT", 15.99m,
                ClothSizeEnum.M, "violet");

            Product product4 = new Appliance("laptop", "BrandL", 2345,
                "ModelL", new DateTime(2021, 03, 03), 1.125);
            
            cashier.Add(product1, 2.45);
            cashier.Add(product2, 3);
            cashier.Add(product3, 2);
            cashier.Add(product4, 1);
            
            cashier.PrintReceipt(cashier.Products, new DateTime(2021, 06, 14, 12, 34, 56));
        }
    }
}
