namespace CarCenter
{
	public class Car
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime InspectionDate { get; set; }

		public Car(int id, string name, DateTime inspectionDate)
		{
			Id = id;
			Name = name;
			InspectionDate = inspectionDate;
		}
	}
}
