using System;

namespace ConsoleApp1
{
	internal class SomeService : IService
	{
		public string GetInfo()
		{
			return ("Информация о сервисе: SomeService");
		}
	}
}