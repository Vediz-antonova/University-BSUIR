using System.Collections.Generic;

namespace _353504_Antonava_Lab4
{
	public class MyCustomComparer : IComparer<Car>
	{
		public int Compare(Car x, Car y)
		{
			return string.Compare(x.Name, y.Name);
		}
	}
}