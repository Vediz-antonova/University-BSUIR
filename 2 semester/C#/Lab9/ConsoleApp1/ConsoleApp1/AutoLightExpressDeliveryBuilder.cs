namespace ConsoleApp1
{
	internal class AutoLightExpressDeliveryBuilder : DeliveryBuilder
	{
		public override DeliveryServiceBase Build()
		{
			return new AutoLightExpressDelivery(name, type, service);
		}
	}
}