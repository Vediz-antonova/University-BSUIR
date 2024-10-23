namespace ConsoleApp1
{
	internal static class Director
	{
		public static DeliveryServiceBase GetCarDelivery(string name, DeliveryBuilder builder)
		{
			return builder.SetName(name).SetType(DeliveryType.City).SetService(new SomeService()).Build();
		}
		public static DeliveryServiceBase GetDHLDelivery(string name, DHLDeliveryBuilder builder)
		{
			return builder.SetName(name).SetType(DeliveryType.InterCity).SetService(new SomeService()).Build();
		}
		public static DeliveryServiceBase GetAutoLightExpressDelivery(string name, AutoLightExpressDeliveryBuilder builder)
		{
			return builder.SetName(name).SetType(DeliveryType.International).SetService(new SomeService()).Build();
		}
	}
}
