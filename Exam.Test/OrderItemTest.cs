using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Exam.Test
{
    public class OrderItemTest
    {

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public void OrderItemConstructor_QuantityBounds_Exception(int quantity)
        {
            Item item = new Item(1, "Bob", 100);
            OrderItemType orderItemType = OrderItemType.Material;

            Assert.Throws<ArgumentOutOfRangeException>(() => new OrderItem(quantity, item, orderItemType));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(int.MaxValue)]
        public void OrderItemConstructor_QuantityBounds_Valid(int quantity)
        {
            Item item = new Item(1, "Bob", 100);
            OrderItemType orderItemType = OrderItemType.Material;

            new OrderItem(quantity, item, orderItemType);
        }

        [Fact]
        public void OrderItemConstructor_ItemCannotBeNull()
        {
            int quantity = 1;
            Item item = null;
            OrderItemType orderItemType = OrderItemType.Material;

            Assert.Throws<ArgumentNullException>(() => new OrderItem(quantity, item, orderItemType));
        }

        [Theory]
        [InlineData(OrderItemType.Material)]
        [InlineData(OrderItemType.Service)]
        public void OrderItemConstructor_OrderItemType_Valid(OrderItemType orderItemType)
        {
            int quantity = 1;
            Item item = new Item(1, "Fries", 5);

            new OrderItem(quantity, item, orderItemType);
        }
        
        [Fact]
        public void OrderItemConstructor_OrderItemType_Invalid()
        {
            int quantity = 1;
            Item item = new Item(1, "Fries", 5);
            OrderItemType orderItemType = (OrderItemType)100;

            Assert.Throws<ArgumentOutOfRangeException>(() => new OrderItem(quantity, item, orderItemType));
        }

        [Theory]
        [InlineData(5, 1, 5)]
        [InlineData(2.5, 2, 5)]
        [InlineData(10, 3, 30)]
        [InlineData(3.3, 3, 9.9)]
        [InlineData(0.5, 2, 1)]
        public void GetTotal(decimal itemPrice, int quantity, decimal expected)
        {
            Item item = new Item(1, "Fries", itemPrice);
            OrderItem orderItem = new OrderItem(quantity, item, OrderItemType.Service);

            decimal actual = orderItem.GetTotal();

            Assert.Equal(expected, actual);
        }

    }
}
