using System;

namespace ConsoleApp1
{
	internal class CarDelivery : DeliveryServiceBase, IDelivery, ITracking, IInsurance
	{
		public CarDelivery(string name, DeliveryType type, IService service)
			: base(name, type, service)
		{
		}

		public void DeliveryMethod()
		{
			Console.WriteLine("Способ доставки: автомобиль.");
		}

		public void Track()
		{
			Console.WriteLine("Отслеживание этапов доставки.");
		}

		public void Insure()
		{
			Console.WriteLine("Страхование груза.");
		}

		public override string GetInfo()
		{
			return ($"Название: {name}, Тип: {type}");
		}
	}
}
