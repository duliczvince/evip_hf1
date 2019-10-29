using System;
using System.Collections.Generic;
using System.Linq;
using evip_hf1_dv_fdzk2z;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace hf1_UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        #region _variables_

        private List<Shop> productsList = new List<Shop>();

        #endregion

        [TestMethod]
        public void RenewRegister()
        {
            var v = new Shop("B", 2);
            double oldPrice = 2;
            double newPrice = 1;

            if (oldPrice > newPrice)
            {
                productsList.Remove(v);
                productsList.Add(new Shop("B", newPrice));
            }

            Assert.AreEqual(1, newPrice);
        }

        #region _get_price_

        [TestMethod]
        public void GetPrice()
        {
            Shop a = new Shop("A", 1);
            Shop b = new Shop("B", 2);

            double price = a.getPrice() + b.getPrice();

            Assert.AreEqual(3, price);
        }

        #endregion

        #region _products_and_price_tests_

        [TestMethod]
        public void ProductsHasDigit()
        {
            Assert.ThrowsException<Exception>(() => Shop.RegisterProduct("1", 0));
        }

        [TestMethod]
        public void ProductsIsZero()
        {
            Assert.ThrowsException<Exception>(() => Shop.RegisterProduct("", 0));
        }

        [TestMethod]
        public void PriceIsZero()
        {
            Assert.ThrowsException<Exception>(() => Shop.RegisterProduct("A", 0));
        }

        [TestMethod]
        public void isPriceLowerThenOne()
        {
            Assert.ThrowsException<Exception>(() => Shop.RegisterProduct("A", -1));
        }

        #endregion

        #region _register_tests_

        [TestMethod]
        public void RegisterProduct()
        {
            productsList.Add(new Shop("A", 1));
            productsList.Add(new Shop("B", 2));

            Assert.AreEqual(2, productsList.Count());
        }

        [TestMethod]
        public void RegisterProductWithDiscount()
        {
            productsList.Add(new Shop("X", 1, 2, 3));
            productsList.Add(new Shop("Y", 2, 1, 2));
            productsList.Add(new Shop("Z", 3, 3, 4));

            Assert.AreEqual(3, productsList.Count());
        }


        [TestMethod]
        public void RegisterAmountDiscount()
        {
            productsList.Add(new Shop("A", 10, 1, 0.9));
            productsList.Add(new Shop("B", 20, 3, 0.6));

            Assert.AreEqual(2, productsList.Count());
        }

        [TestMethod]
        public void RegisterComboDiscount()
        {
            productsList.Add(new Shop("ABC", 20, true));
            productsList.Add(new Shop("XYZ", 10, false));

            Assert.AreEqual(2, productsList.Count());
        }

        #endregion

        #region _extension_and_other_methods_

        [TestMethod]
        public void isComboDiscountInCart()
        {
            bool res = false;
            string products = "tABC";

            if (products.Contains("t")) { res = true; }

            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void isClubMemberIncart()
        {
            string products = "tABC";
            bool containsT = false;
            if (products.Contains("t")) { containsT = true; }

            Assert.AreEqual(true, containsT);
        }

        [TestMethod]
        public void RemoveClubMemberFromCart()
        {
            string products = "tABC";
            if (products.Contains("t")) { products = products.Replace("t", ""); }

            Assert.AreEqual(false, products.Contains("t"));
        }

        [TestMethod]
        public void ShopStringToChar()
        {
            string products = "A";
            char[] ProductsOneByOne = products.ToCharArray();

            var productsLocalList = new List<string>();
            productsLocalList.Add(ProductsOneByOne[0].ToString());

            Assert.AreEqual(true, productsLocalList[0] is string);
        }

        [TestMethod]
        public void UserID()
        {
            Assert.ThrowsException<NotImplementedException>(() => Shop.UserID());
        }

        #endregion
    }
}