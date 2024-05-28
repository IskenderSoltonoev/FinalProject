using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            MyHashTable hs = new MyHashTable(3);
        }
    }

    public class MyHashTable
    {
        LinkedList<Item>[] _buckets;

        int _capacity;
        int _count;

        public MyHashTable(int size)
        {

            for (int i = 0; i < _capacity; i++)
            {
                _buckets[i] = new LinkedList<Item>();
            }
        }

        public string this[string key]
        {
            get
            {
                return GetByKey(key);
            }
            set
            {
                SetByKey(key, value);
            }
        }

        public void Add(string key, string value)
        {
            IncreaseCapacity();
            if (ContainsKey(key))
            {
                throw new ArgumentException("key already exists");
            }
            var item = new Item(key, value);
            int index = GetBucketIndex(key);
            _buckets[index].AddLast(item);
            _count++;
        }

        public void SetByKey(string key, string value)
        {
            int index = GetBucketIndex(key);
            foreach (var item in _buckets[index])
            {
                if (item.key == key)
                {
                    item.value = value;
                }
            }
        }

        public bool ContainsKey(string key)
        {
            int index = GetBucketIndex(key);
            foreach (var item in _buckets[index])
            {
                if (item.key == key)
                {
                    return true;
                }
            }
            return false;
        }

        public string GetByKey(string key)
        {
            int index = GetBucketIndex(key);
            foreach (var item in _buckets[index])
            {
                if (item.key == key)
                {
                    return item.value;
                }
            }
            return null;
        }

        private void IncreaseCapacity()
        {
            if (_count >= _capacity)
            {
                _capacity *= 2;
                var temp = new LinkedList<Item>[_capacity];
                foreach (var bucket in _buckets)
                {
                    foreach (var item in bucket)
                    {
                        int index = GetBucketIndex(item.key);
                        if (temp[index] == null)
                        {
                            temp[index] = new LinkedList<Item>();
                        }
                        temp[index].AddLast(item);
                    }
                }
            }
        }

        public int GetBucketIndex(string key)
        {
            return HashFunc(key) % _capacity;
        }

        public int HashFunc(string key)
        {
            int hashCode = 0;
            foreach (char c in key)
            {
                hashCode += c;
            }
            return hashCode;
        }
    }

    class Item
    {
        public string key;
        public string value;

        public Item(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }
}