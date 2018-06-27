using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Xunit;

namespace Exam.Test
{
    public class OrderTest
    {
        
        /// <summary>
        /// An order without orderItems is invalid state
        /// </summary>
        [Fact]
        public void OrderConstructor_CannotBeNull()
        {
            OrderItem[] orderItems = null;
            Assert.Throws<ArgumentNullException>(() => new Order(orderItems));
        }

        /// <summary>
        /// An order without orderItems is invalid state
        /// </summary>
        [Fact]
        public void OrderConstructor_CannotHaveAnEmptyList()
        {
            OrderItem[] orderItems = new OrderItem[0];
            Assert.Throws<ArgumentException>(() => new Order(orderItems));
        }

        [Fact]
        public void OrderItems_AreImmutable()
        {
            OrderItem[] orderItems = new OrderItem[1];

            Item firstItem = new Item(1, "Fries", 3);
            OrderItem firstOrderItem = new OrderItem(1, firstItem, OrderItemType.Material);
            orderItems[0] = firstOrderItem;

            Item secondItem = new Item(2, "Burger", 5);
            OrderItem secondOrderItem = new OrderItem(3, secondItem, OrderItemType.Material);

            Order order = new Order(orderItems);
            decimal firstTotal = order.GetOrderTotal(1);
            
            orderItems[0] = secondOrderItem;
            decimal secondTotal = order.GetOrderTotal(1);
            
            // Confirm total calculation behavior does not change
            Assert.Equal(firstTotal, secondTotal);
            
            // Confirm item collection behavior does not change
            Assert.Contains(firstItem, order.GetItems());
            Assert.DoesNotContain(secondItem, order.GetItems());
        }

        [Fact]
        public void GetItems()
        {
            OrderItem[] orderItems = new OrderItem[2];

            Item firstItem = new Item(1, "Fries", 3);
            OrderItem firstOrderItem = new OrderItem(1, firstItem, OrderItemType.Material);
            orderItems[0] = firstOrderItem;

            Item secondItem = new Item(2, "Burger", 5);
            OrderItem secondOrderItem = new OrderItem(3, secondItem, OrderItemType.Material);
            orderItems[1] = secondOrderItem;

            Order order = new Order(orderItems);
            
            // Confirm item collection behavior does not change
            Assert.Contains(firstItem, order.GetItems());
            Assert.Contains(secondItem, order.GetItems());
        }

        [Fact]
        public void GetTotal()
        {
            OrderItem[] orderItems = new OrderItem[2];

            // Total = (3 * 1 * 0.10) + (3 * 1) = 3.30
            Item firstItem = new Item(1, "Fries", 3);
            OrderItem firstOrderItem = new OrderItem(1, firstItem, OrderItemType.Material);
            orderItems[0] = firstOrderItem;

            // Total = (5 * 3 * 0.1) + (5 * 3) = 16.50
            Item secondItem = new Item(2, "Burger", 5);
            OrderItem secondOrderItem = new OrderItem(3, secondItem, OrderItemType.Material);
            orderItems[1] = secondOrderItem;

            Order order = new Order(orderItems);

            decimal taxRate = 0.1m;
            decimal expected = 19.8m;
            decimal actual = order.GetOrderTotal(taxRate);

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Order_Serializable()
        {
            OrderItem[] orderItems = new OrderItem[2];

            // Total = (3 * 1 * 0.10) + (3 * 1) = 3.30
            Item firstItem = new Item(1, "Fries", 3);
            OrderItem firstOrderItem = new OrderItem(1, firstItem, OrderItemType.Material);
            orderItems[0] = firstOrderItem;

            // Total = (5 * 3 * 0.1) + (5 * 3) = 16.50
            Item secondItem = new Item(2, "Burger", 5);
            OrderItem secondOrderItem = new OrderItem(3, secondItem, OrderItemType.Material);
            orderItems[1] = secondOrderItem;

            Order order = new Order(orderItems);
            decimal expectedTotal = order.GetOrderTotal(0.1m);

            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, order);
            memoryStream.Position = (long)SeekOrigin.Begin;

            Order deserializedOrder = (Order) formatter.Deserialize(memoryStream);
            decimal actualTotal = deserializedOrder.GetOrderTotal(0.1m);

            Assert.Equal(expectedTotal, actualTotal);
            Assert.Contains(firstItem, order.GetItems());
            Assert.Contains(secondItem, order.GetItems());
        }

    }
}
