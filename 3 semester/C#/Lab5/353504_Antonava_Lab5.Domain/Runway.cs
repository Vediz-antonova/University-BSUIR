namespace _353504_Antonava_Lab5.Domain
{
	public class Runway : IEquatable<Runway>
	{
		public string Name { get; set; }
		public int Length { get; set; } // в метрах
		public int Width { get; set; } // в метрах

		public bool Equals(Runway other)
		{
			if (other is null) return false;
			return Name == other.Name && Length == other.Length && Width == other.Width;
		}

		public override bool Equals(object obj)
		{
			if (obj is Runway runway)
				return Equals(runway);
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Name, Length, Width);
		}
	}
}
