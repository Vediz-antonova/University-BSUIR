using _353504_Antonava_Lab2.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace _353504_Antonava_Lab2.Collections
{
	public class ItemNotFoundException : Exception
	{
		public ItemNotFoundException(string message) : base(message) { }
	}

	public class MyCustomCollection<T> : ICustomCollection<T>, IEnumerable<T>
	{
		private class Node
		{
			public T Data;
			public Node Next;
		}

		private Node head;
		private Node current;
		private int count;

		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= count) throw new IndexOutOfRangeException();
				Node node = head;
				for (int i = 0; i < index; i++)
				{
					node = node.Next;
				}
				return node.Data;
			}
			set
			{
				if (index < 0 || index >= count) throw new IndexOutOfRangeException();
				Node node = head;
				for (int i = 0; i < index; i++)
				{
					node = node.Next;
				}
				node.Data = value;
			}
		}

		public int Count => count;

		public void Add(T item)
		{
			Node newNode = new Node { Data = item };
			if (head == null)
			{
				head = newNode;
				current = head;
			}
			else
			{
				Node node = head;
				while (node.Next != null)
				{
					node = node.Next;
				}
				node.Next = newNode;
			}
			count++;
		}

		public void Remove(T item)
		{
			Node node = head;
			Node previous = null;
			while (node != null)
			{
				if (node.Data.Equals(item))
				{
					if (previous == null)
					{
						head = node.Next;
					}
					else
					{
						previous.Next = node.Next;
					}
					count--;
					return;
				}
				previous = node;
				node = node.Next;
			}
			throw new ItemNotFoundException("Item not found in the collection."); 
		}

		public T RemoveCurrent()
		{
			if (current == null) throw new InvalidOperationException();
			T data = current.Data;
			Remove(data);
			return data;
		}

		public void Reset()
		{
			current = head;
		}

		public void Next()
		{
			if (current != null)
			{
				current = current.Next;
			}
		}

		public T Current()
		{
			if (current == null) throw new InvalidOperationException();
			return current.Data;
		}

		public IEnumerator<T> GetEnumerator()
		{
			Node node = head;
			while (node != null)
			{
				yield return node.Data;
				node = node.Next;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}