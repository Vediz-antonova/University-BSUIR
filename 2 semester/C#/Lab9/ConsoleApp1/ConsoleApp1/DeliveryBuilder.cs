namespace ConsoleApp1
{
	internal abstract class DeliveryBuilder
	{
		protected string name;
		protected DeliveryType type;
		protected IService service;

		public DeliveryBuilder SetName(string name)
		{
			this.name = name;
			return this;
		}

		public DeliveryBuilder SetType(DeliveryType type)
		{
			this.type = type;
			return this;
		}

		public DeliveryBuilder SetService(IService service)
		{
			this.service = service;
			return this;
		}

		public abstract DeliveryServiceBase Build();
	}
}
