using System;

namespace Exam
{
    /// <summary>
    /// Represents a part or service that can be sold.
    ///
    /// Care should be taken to ensure that this class is immutable since it
    /// is sent to other systems for processing that should not be able to
    /// change it in any way.
    ///
    /// </summary>
    [Serializable]
    public class Item
    {

        const int PRIME_NUMBER = 397;

        private readonly int _key;
        private readonly string _name;
        private readonly decimal _price;

        public Item(int key, string name, decimal price)
        {
            if(key <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(key), key, "Key must be greater than zero");
            }
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Name cannot be null, empty, or whitespace.");
            }
            if(price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), price, "Price cannot be less than zero");
            }

            _key = key;
            _name = name;
            _price = price;
        }

        public int GetKey()
        {
            return _key;
        }

        public string GetName()
        {
            return _name;
        }

        public decimal GetPrice()
        {
            return _price;
        }

        public override int GetHashCode()
        {
            return (_key.GetHashCode() * PRIME_NUMBER) ^ _price.GetHashCode();
        }

    }
}
