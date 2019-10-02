using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace evip_hf1_dv_fdzk2z
{
    public class Shop
    {
        private static List<Shop> productsList = new List<Shop>();
        private string product;
        private int price;
        private int discount;
        private int discountVAlue;

        #region __1_FELADAT__

        public string getProduct() { return product; }
        public int getPrice() { return price; }

        public Shop(string product, int price)
        {
            this.product = product;
            this.price = price;
        }

        public Shop(string product, int price, int discount, int discountVAlue)
        {
            this.product = product;
            this.price = price;
            this.discount = discount;
            this.discountVAlue = discountVAlue;
        }

        public static void RegisterProduct(string product, int price)
        {
            //TODO: vizsgálatok + van-e már, érték > 0 stb.

            productsList.Add(new Shop(product, price));
        }

        public static int GetPrice(string products)
        {
            //TODO: van-e diszkountos termék? (method) ha van akkor: számolja ki mennyi az ára (method)
            
            var result = ShopStringToChar(products).GroupBy(x => x).ToDictionary(y => y.Key, y => y.Count()).OrderByDescending(z => z.Value);
            int price = 0;

            foreach (var product in result)
            {
                Console.WriteLine("Value: " + product.Key + " Count: " + product.Value);
                var p = new Shop(product.Key, 0);
                productsList.Find(p);
            }
            return price;
        }

        private static int Find<Shop(this List<Shop> productsList, Shop p)
        {
            this.productsList = productsList;

            for(int i = 0; i < productsList.Count; i++)
            {
                if(productsList[i].product.Equals(p.product)) { return productsList[i].getPrice(); }
            }
            return 0;
        }

        private static List<string> ShopStringToChar(string products)
        {
            char[] ProductsOneByOne = products.ToCharArray();

            var productsLocalList = new List<string>();
            for (int i = 0; i < ProductsOneByOne.Length; i++)
            {
                productsLocalList.Add(ProductsOneByOne[i].ToString());
            }

            return productsLocalList;
        }

        #endregion

        #region __2_FELADAT__

        public static void RegisterProductWithDiscount(string product, int price, int discountFrom, int discountValue)
        {
            productsList.Add(new Shop(product, price, discountFrom, discountValue));
        }

        #endregion
    }
}
