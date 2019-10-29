using System.Collections.Generic;
using System.Linq;

namespace evip_hf1_dv_fdzk2z
{
    class ExtensionMethods
    {
        public static List<string> ShopStringToChar(string products)
        {
            char[] ProductsOneByOne = products.ToCharArray();

            var productsLocalList = new List<string>();
            for (int i = 0; i < ProductsOneByOne.Length; i++)
            {
                productsLocalList.Add(ProductsOneByOne[i].ToString());
            }

            return productsLocalList;
        }

        // _5_FELADAT
        public static (string outProduct, double outClubMemberDiscount) ClubMemberInCart(string products, double clubMemberDiscount)
        {
            if (isClubMemberIncart(products)) { return (products.Replace("t", ""), clubMemberDiscount = 0.9); }

            return (products, clubMemberDiscount);
        }

        public static bool isClubMemberIncart(string products)
        {
            if (products.Contains("t")) { return true; }
            return false;
        }

        public static void CheckRegisteredProductIsValid(string products, double price)
        {
            bool isProductsZero = products == "";
            bool isDigitInProducts = products.Any(c => char.IsDigit(c));
            bool isProductsNull = products == null;
            bool isPriceLowerThenOne = price < 1;

            if (isProductsZero || isDigitInProducts || isProductsNull || isPriceLowerThenOne) { throw new System.Exception(); }
        }
    }
}