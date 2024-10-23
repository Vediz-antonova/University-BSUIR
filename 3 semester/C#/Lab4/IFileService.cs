using System.Collections.Generic;

namespace _353504_Antonava_Lab4
{
	public interface IFileService<T>
	{
		IEnumerable<T> ReadFile(string fileName);
		void SaveData(IEnumerable<T> data, string fileName);
	}
}