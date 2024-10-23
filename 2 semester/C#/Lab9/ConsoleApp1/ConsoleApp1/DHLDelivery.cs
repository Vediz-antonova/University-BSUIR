using System;

namespace ConsoleApp1
{
	internal class DHLDelivery : DeliveryServiceBase, IDelivery, ITracking
	{
		public DHLDelivery(string name, DeliveryType type, IService service)
			: base(name, type, service)
		{
		}

		public void DeliveryMethod()
		{
			Console.WriteLine("Способ доставки: DHL.");
		}

		public void Track()
		{
			Console.WriteLine("Отслеживание этапов доставки.");
		}

		public override string GetInfo()
		{
			return ($"Название: {name}, Тип: {type}");
		}
	}
}
