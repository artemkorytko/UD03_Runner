using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Collections : MonoBehaviour
    {
        private const int COUNT = 10;
        private MyClass[] _array;
        private readonly List<MyClass> _list = new List<MyClass>(COUNT);
        private readonly Dictionary<string, MyClass> _dictionary = new Dictionary<string, MyClass>();
        private readonly Queue<MyClass> _queue = new Queue<MyClass>();
        private readonly Stack<MyClass> _stack = new Stack<MyClass>();
        private readonly LinkedList<MyClass> _linkedList = new LinkedList<MyClass>();

        //FIFO - first in first out
        //LIFO - last in first out
        private void Start()
        {
            #region Array

            _array = new MyClass[COUNT];
            for (int i = 0; i < COUNT; i++)
            {
                _array[i] = new MyClass(i, $"Name {i}");
            }

            // Debug.Log(_array[7].Name);

            #endregion

            #region List

            for (int i = 0; i < COUNT; i++)
            {
                _list.Add(new MyClass(i, $"Name {i}"));
            }

            var item = _list[7];
            //Debug.Log(item.Name);
            _list.Remove(item);
            // Debug.Log(_list[7].Name);
            _list.AddRange(_array);
            _list.RemoveAt(7);
            //Debug.Log(_list[7].Name);
            List<MyClass> newList = _list.FindAll(x => x.Index == 2);
            _list.Sort((x, y) => x.Index - y.Index);
            // Debug.Log(_list[7].Name);

            #endregion

            #region Dictionary

            for (int i = 0; i < COUNT; i++)
            {
                var key = i.ToString();
                if (!_dictionary.ContainsKey(key))
                    _dictionary.Add(key, new MyClass(i, $"Name {i}"));
            }

            var dictItem = _dictionary["7"];
            // Debug.Log(dictItem.Name);
            foreach (KeyValuePair<string, MyClass> tempItem in _dictionary)
            {
                if (tempItem.Value.Index == 7)
                {
                    // Debug.Log(tempItem.Value.Name);
                }
            }

            _dictionary.Remove("7");
            MyClass myClass;

            if (_dictionary.ContainsKey("7"))
                _dictionary.Remove("7", out myClass);
            else _dictionary.Remove("8", out myClass);

            // Debug.Log(myClass.Name);

            #endregion

            #region Queue

            for (int i = 0; i < COUNT; i++)
            {
                _queue.Enqueue(new MyClass(i, $"Name {i}"));
            }

            // Debug.Log(_queue.Peek().Name);
            // Debug.Log(_queue.Dequeue().Name);
            // Debug.Log(_queue.Peek().Name);
            // Debug.Log(_queue.Dequeue().Name);
            // Debug.Log(_queue.Dequeue().Name);
            // Debug.Log(_queue.Dequeue().Name);
            // Debug.Log(_queue.Dequeue().Name);
            // Debug.Log(_queue.Dequeue().Name);
            if (_queue.Count != 0)
            {
                _queue.TryDequeue(out myClass);
                //if (myClass != null)
                //  Debug.Log(myClass.Name);
            }

            #endregion

            #region Stack

            for (int i = 0; i < COUNT; i++)
            {
                _stack.Push(new MyClass(i, $"Name {i}"));
            }

            Debug.Log(_stack.Peek().Name);
            Debug.Log(_stack.Pop().Name);
            Debug.Log(_stack.Peek().Name);
            Debug.Log(_stack.Pop().Name);

            #endregion

            #region LinkedList

            for (int i = 0; i < COUNT; i++)
            {
                _linkedList.AddFirst(new MyClass(i, $"Name {i}"));
            }

            for (int i = 0; i < COUNT; i++)
            {
                _linkedList.AddLast(new MyClass(i, $"Name {i}"));
            }

            _linkedList.AddAfter(_linkedList.First, new MyClass(1, $"Name {1}"));

            var node = _linkedList.Find(_linkedList.Last.Value);
            

            #endregion
        }
    }

    [System.Serializable]
    public class MyClass
    {
        public readonly int Index;
        public readonly string Name;

        public MyClass(int index, string name)
        {
            Index = index;
            Name = name;
        }
    }
}