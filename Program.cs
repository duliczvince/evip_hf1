using System;
using System.Collections;
using System.Collections.Generic;

namespace evip_hf1_dv_fdzk2z
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop.RegisterProduct("A", 1);
            Shop.RegisterProduct("B", 2);

            var price = Shop.GetPrice("AAB");
            Console.WriteLine(price);


            Console.ReadKey();
        }
    }
}
