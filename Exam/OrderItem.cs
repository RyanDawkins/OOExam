using System;
using System.Collections.Generic;
using System.Text;

namespace Exam
{
    [Serializable]
    public class OrderItem
    {

        private readonly Item _item;
        private readonly int _quantity;
        private readonly OrderItemType _orderItemType;

        public OrderItem(int quantity, Item item, OrderItemType orderItemType)
        {
            if(quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), quantity, "Quantity must be greater than or equal to 1");
            }

            if (!Enum.IsDefined(typeof(OrderItemType), orderItemType))
            {
                throw new ArgumentOutOfRangeException(nameof(orderItemType), orderItemType, "Invalid enum, does not exist within the defined enum values");
            }

            _quantity = quantity;
            _item = item ?? throw new ArgumentNullException(nameof(item), "Item cannot be null");
            _orderItemType = orderItemType;
        }

        public Item Item { get => _item; }

        public int Quantity { get => _quantity; }

        public OrderItemType OrderItemType { get => _orderItemType; }

        public decimal GetTotal()
        {
            return _item.GetPrice() * _quantity;
        }

    }
}
