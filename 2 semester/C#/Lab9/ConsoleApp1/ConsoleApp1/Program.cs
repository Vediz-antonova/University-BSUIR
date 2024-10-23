using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var services = new List<DeliveryServiceBase>();
			var carDeliveryBuilder = new CarDeliveryBuilder();
			var dhlDeliveryBuilder = new DHLDeliveryBuilder();
			var autoLightExpressDeliveryBuilder = new AutoLightExpressDeliveryBuilder();

			services.AddRange(new DeliveryServiceBase[] {
				Director.GetCarDelivery("Service №1", carDeliveryBuilder),
				Director.GetDHLDelivery("Service №2", dhlDeliveryBuilder),
				Director.GetAutoLightExpressDelivery("Service №3", autoLightExpressDeliveryBuilder),
			});
			foreach (var service in services)
			{
				Console.WriteLine(service.ToString());
			}

			Console.ReadLine();
		}
	}
}
