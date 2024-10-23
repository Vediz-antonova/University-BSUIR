using System;

namespace ConsoleApp1
{
	internal class AutoLightExpressDelivery : DeliveryServiceBase, IDelivery, IInsurance
	{
		public AutoLightExpressDelivery(string name, DeliveryType type, IService service)
			: base(name, type, service)
		{
		}

		public void DeliveryMethod()
		{
			Console.WriteLine("Способ доставки: AutoLight Express.");
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
