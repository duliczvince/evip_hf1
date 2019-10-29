using System;

namespace evip_hf1_dv_fdzk2z
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop.RegisterProduct("A", 1);
            Shop.RegisterProduct("B", 2);
            Shop.RegisterProduct("B", 1);

            Shop.RegisterProduct("X", 3);
            Shop.RegisterProduct("Y", 4);
            Shop.RegisterProduct("Z", 5);

            //újregisztrálás esetén az olcsóbb számít

            Shop.RegisterProductWithDiscount("C", 3, 3, 4);

            Shop.RegisterAmountDiscount("D", 10, 5, 0.9);

            Shop.RegisterComboDiscount("ABC", 5, true);
            Shop.RegisterComboDiscount("XYZ", 11, false);


            var price = Shop.GetPrice("tABCXYZ"); //14.4 (5+11) * 0.9
             price = Shop.GetPrice("AB");

            Console.WriteLine(price);

            Console.ReadKey();
        }
    }
}