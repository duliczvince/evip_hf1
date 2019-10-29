using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace evip_hf1_dv_fdzk2z
{
    public class Shop
    {
        #region _VARIABLES_

        private static List<Shop> productsList = new List<Shop>();
        private static List<Shop> comboDiscountProductsList = new List<Shop>();
        private string product;
        private double price;
        private int discount;
        private int discountValue;
        private int amount;
        private double amountValue;
        private bool onlyForClubMembers;
        #endregion

        private static double RenewRegister(string product, double price)
        {
            double oldPrice = price;
            
            foreach(Shop v in productsList)
            {
                if (v.product.Equals(product))
                {
                    oldPrice = v.getPrice();
                    if (oldPrice > price) {
                        productsList.Remove(v);
                        return price; 
                    }
                    return oldPrice;
                }
            }

            return oldPrice;
        }

        #region _CONSTRUCTORS_

        public Shop(string product, double price)
        {
            this.product = product;
            this.price = price;
        }

        public Shop(string product, double price, int discount, int discountValue)
        {
            this.product = product;
            this.price = price;
            this.discount = discount;
            this.discountValue = discountValue;
        }

        public Shop(string product, double price, int amount, double amountValue)
        {
            this.product = product;
            this.price = price;
            this.amount = amount;
            this.amountValue = amountValue;
        }

        public Shop(string product, double price, bool onlyForClubMembers)
        {
            this.product = product;
            this.price = price;
            this.onlyForClubMembers = onlyForClubMembers;
        }

        public double getPrice() { return price; }

        #endregion

        #region _1_FELADAT_

        public static void RegisterProduct(string product, double price)
        {
            ExtensionMethods.CheckRegisteredProductIsValid(product, price);;
            price = RenewRegister(product, price);
            productsList.Add(new Shop(product, price));
        }

        public static double GetPrice(string products)
        {
            double clubMemberDiscount = 1;
            double price = 0;
            var res = isComboDiscountInCart(ref products, ref price);
            products = res.Item1;
            price = res.Item2;

            (string outProduct, double outClubMemberDiscount) = ExtensionMethods.ClubMemberInCart(products, clubMemberDiscount);
            products = outProduct;
            clubMemberDiscount = outClubMemberDiscount;

            var result = ExtensionMethods.ShopStringToChar(products).GroupBy(x => x).ToDictionary(y => y.Key, y => y.Count()).OrderByDescending(z => z.Value);
            foreach (var product in result)
            {
                var p = new Shop(product.Key, 0);
                price += FindElementThenReturnWithItsPrice(p, Convert.ToDouble(product.Value)); //product.Value is int, make it double
            }

            return price * clubMemberDiscount;
        }

        private static double FindElementThenReturnWithItsPrice(Shop p, double countProduct)
        {
            foreach (Shop v in productsList)
            {
                if (v.product.Equals(p.product))
                {
                    var discount = GetPriceForDiscountValue(v, countProduct);
                    if (discount != 0) { return discount; }

                    var amount = GetPriceForAmountValue(v, countProduct);
                    if (amount != 0) { return amount; }

                    return v.getPrice() * countProduct;
                }
            }
            return 0;
        }

        #endregion

        #region _2_FELADAT_

        public static void RegisterProductWithDiscount(string product, double price, int discountFrom, int discountValue)
        {
            productsList.Add(new Shop(product, price, discountFrom, discountValue));
        }

        private static double GetPriceForDiscountValue(Shop v, double countProduct)
        {
            if (countProduct >= v.discountValue && v.discountValue != 0)
            {
                countProduct -= v.discountValue - v.discount;
                return v.getPrice() * countProduct;
            }
            return 0;
        }

        #endregion

        #region _3_FELADAT_

        public static void RegisterAmountDiscount(string products, double price, int amount, double amountValue)
        {
            productsList.Add(new Shop(products, price, amount, amountValue));
        }

        private static double GetPriceForAmountValue(Shop v, double countProduct)
        {
            if (countProduct > v.amount && v.amount != 0)
            {
                return v.amountValue * v.getPrice() * countProduct;
            }
            return 0;
        }

        #endregion

        #region _4_and_6_FELADAT_

        public static void RegisterComboDiscount(string products, double price, bool onlyForClubMembers)
        {
            comboDiscountProductsList.Add(new Shop(products, price, onlyForClubMembers));
        }

        public static (string, double) isComboDiscountInCart(ref string products, ref double price)
        {
            for (int i = 0; i < comboDiscountProductsList.Count; i++)
            {
                var firstElementOfCDPL = comboDiscountProductsList[i].product;
                bool isOnlyForClubMembers = comboDiscountProductsList[i].onlyForClubMembers;
                bool isClubMemberIncart = ExtensionMethods.isClubMemberIncart(products);

                if (!isClubMemberIncart && isOnlyForClubMembers){ continue; }

                var acronyms = FillAcronymsInList(firstElementOfCDPL, i);
                var check = CheckThatProductsContainsDiscount(acronyms, products, i);
                products = check.Item1;

                if (check.Item2 != 0) { price += check.Item2; }
            }
            return (products, price);
        }

        private static List<string> FillAcronymsInList(string firstElementOfCDPL, int i)
        {
            firstElementOfCDPL = comboDiscountProductsList[i].product;
            char[] tmp = firstElementOfCDPL.ToCharArray();

            List<string> acronyms = new List<string>();
            for (int j = 0; j < tmp.Length; j++)
            {
                acronyms.Add(Convert.ToString(tmp[j]));
            }

            return acronyms;
        }

        private static (string, double) CheckThatProductsContainsDiscount(List<string> acronyms, string products, int i)
        {
            var regex = new Regex(string.Join("|", acronyms), RegexOptions.Compiled);
            int acronymsCount = acronyms.Count;

            int runs = 0;
            string productsIsInList = products;
            for (var match = regex.Match(productsIsInList); match.Success; match = match.NextMatch())
            {
                productsIsInList = productsIsInList.Replace(match.Value, "");
                acronyms.Remove(match.Value);
                runs++;
            }

            if (runs == acronymsCount) { return (productsIsInList, comboDiscountProductsList[i].price); }

            return (products, 0);
        }

        #endregion

        #region _7_FELADAT_
        public static void UserID()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}