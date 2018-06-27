using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Exam.Test
{
    public class ItemTest
    {

        const int VALID_KEY = 3;
        const string VALID_NAME = "Fries";
        const decimal VALID_PRICE = 2;

        [Theory]
        [InlineData(1)]
        [InlineData(VALID_KEY)]
        [InlineData(int.MaxValue)]
        public void ItemConstructor_Keys_Valid(int key)
        {
            new Item(key, VALID_NAME, VALID_PRICE);
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public void ItemConstructor_Keys_Invalid(int key)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Item(key, VALID_NAME, VALID_PRICE));
        }

        [Theory]
        [InlineData("Burger")]
        [InlineData(VALID_NAME)]
        [InlineData("Chicken Nuggets")]
        public void ItemConstructor_Name_Valid(string name)
        {
            new Item(VALID_KEY, name, VALID_PRICE);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ItemConstructor_Name_Invalid(string name)
        {
            Assert.Throws<ArgumentNullException>(() => new Item(VALID_KEY, name, VALID_PRICE));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        public void ItemConstructor_Price_Valid(decimal price)
        {
            new Item(VALID_KEY, VALID_NAME, price);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void ItemConstructor_Price_Invalid(decimal price)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Item(VALID_KEY, VALID_NAME, price));
        }

        [Fact]
        public void Item_Hashtable()
        {
            Item item1 = new Item(1, "Fries", 2.5m);
            Item item2 = new Item(2, "Burger", 5m);

            Hashtable hashTable = new Hashtable();

            hashTable.Add(item1, item1.GetName());
            hashTable.Add(item2, item2.GetName());

            string expectedName = "Fries";

            Assert.Equal(expectedName, hashTable[item1]);
        }

    }
}
