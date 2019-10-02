using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace evip_hf1_dv_fdzk2z
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop.RegisterProduct("A", 1);
            Shop.RegisterProduct("B", 2);

            Shop.RegisterProductWithDiscount("C", 3, 3, 4);

            var price = Shop.GetPrice("ABCCCC"); //12
            Console.WriteLine(price);

            Console.ReadKey();
        }
    }
}
