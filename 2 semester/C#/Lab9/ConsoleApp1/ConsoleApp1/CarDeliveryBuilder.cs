namespace ConsoleApp1
{
	internal class CarDeliveryBuilder : DeliveryBuilder
	{
		public override DeliveryServiceBase Build()
		{
			return new CarDelivery(name, type, service);
		}
	}
}
