using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Exam
{
    /// <summary>
    /// Represents and Order that contains a collection of items.
    ///
    /// Care should be taken to ensure that this class is immutable since it
    /// is sent to other systems for processing that should not be able
    /// to change it in any way.
    /// </summary>
    [Serializable]
    public class Order : ISerializable
    {

        const string ORDER_ITEMS = "orderItems";

        private readonly ReadOnlyCollection<OrderItem> _orderItems;

        public Order(OrderItem[] orderItems)
        {
            if(orderItems == null)
            {
                throw new ArgumentNullException(nameof(orderItems), "OrderItems cannot be equal to null");
            }
            if(orderItems.Length == 0)
            {
                throw new ArgumentException(nameof(orderItems), "OrderItems cannot be an empty list.");
            }

            _orderItems = new ReadOnlyCollection<OrderItem>(new List<OrderItem>(orderItems));
        }

        public Order(SerializationInfo info, StreamingContext context) 
            : this(((ReadOnlyCollection<OrderItem>) info.GetValue(ORDER_ITEMS, typeof(ReadOnlyCollection<OrderItem>))).ToArray()) {}

        /// <summary>
        /// Returns the total order cost after the tax has been applied
        /// </summary>
        /// <param name="taxRate"></param>
        /// <returns>The total </returns>
        public decimal GetOrderTotal(decimal taxRate)
        {
            return _orderItems
                .Aggregate(decimal.Zero, 
                    (total, orderItem) => 
                        total += orderItem.GetTotal()
                            * (orderItem.OrderItemType == OrderItemType.Material ? taxRate+1 : 1m));
        }

        /// <summary>
        /// Returns a Collection of all items sorted by item name
        /// (case-insensitive).
        /// </summary>
        /// <returns>Collection of all the items in the order</returns>
        public ICollection<Item> GetItems()
        {
            return _orderItems
                .Select(orderItem => orderItem.Item)
                .OrderBy(item => item.GetName())
                .ToList();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ORDER_ITEMS, _orderItems, typeof(ReadOnlyCollection<OrderItem>));
        }
    }
}
