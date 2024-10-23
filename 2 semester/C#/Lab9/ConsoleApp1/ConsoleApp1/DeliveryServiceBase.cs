using System;

namespace ConsoleApp1
{
	internal abstract class DeliveryServiceBase
	{
		protected string name;
		protected DeliveryType type;
		protected IService service;

		public string Name { get; set; }

		public DeliveryServiceBase(string name, DeliveryType type, IService service)
		{
			this.name = name;
			this.type = type;
			this.service = service;
		}
		public void Comment(string comment)
		{
			Console.WriteLine(name + comment);
		}

		public void SetService(IService service)
		{
			this.service = service;
		}

		public IService UseService()
		{
			return service;
		}

		public abstract string GetInfo();

		public override string ToString()
		{
			return GetInfo() + " service: " + service.GetInfo();
		}
	}
}
