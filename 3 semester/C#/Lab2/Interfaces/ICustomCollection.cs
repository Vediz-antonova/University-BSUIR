using System;

namespace _353504_Antonava_Lab2.Interfaces
{
	public interface ICustomCollection<T>
	{
		T this[int index] { get; set; }
		void Reset();
		void Next();
		T Current();
		int Count { get; }
		void Add(T item);
		void Remove(T item);
		T RemoveCurrent();
	}
}
