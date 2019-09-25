using System;
using System.Collections;
using System.Collections.Generic;

namespace evip_hf1_dv_fdzk2z
{
    public class Shop
    {
        private static List<Shop> productsList = new List<Shop>();
        private string product;
        private int price;

        public string getProduct() { return product; }
        public int getPrice() { return price; }

        public Shop(string product, int price)
        {
            this.product = product;
            this.price = price;
        }

        public static void RegisterProduct(string product, int price)
        {
            //TODO: vizsgálatok + van-e már, érték > 0 stb.

            productsList.Add(new Shop(product, price));
        }

        public static int GetPrice(string products)
        {
            char[] ProductsOneByOne = products.ToCharArray();

            int price = 0;
            for (int i = 0; i < ProductsOneByOne.Length; i++)
            {
                price += GetPriceForOneProduct(ProductsOneByOne[i]);
            }
            return price;
        }

        private static int GetPriceForOneProduct(char product)
        {
            int price = 0;
            for (int i = 0; i < productsList.Count; i++)
            {
                if (productsList[i].getProduct().Equals(product.ToString()))
                {
                    price += productsList[i].getPrice();
                }
            }
            return price;
        }

        


    }
}
