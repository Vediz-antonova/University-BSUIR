namespace ConsoleApp1
{
	internal class DHLDeliveryBuilder : DeliveryBuilder
	{
		public override DeliveryServiceBase Build()
		{
			return new DHLDelivery(name, type, service);
		}
	}
}
