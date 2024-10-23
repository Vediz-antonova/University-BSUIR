namespace _353504_Antonava_Lab4
{
	public class Car
	{
		public int Id { get; set; }
		public bool IsAutomaticTransmission { get; set; }
		public string Name { get; set; }

		public Car(int id, bool isAutomaticTransmission, string name)
		{
			Id = id;
			IsAutomaticTransmission = isAutomaticTransmission;
			Name = name;
		}
	}
}
