namespace Antonava__353504.Domain.Entities
{
    public class Attraction : Entity
    {
        public Attraction(string name, double entranceFee)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название не может быть пустым", nameof(name));
            if (entranceFee < 0)
                throw new ArgumentOutOfRangeException(nameof(entranceFee), "Стоимость билета не может быть отрицательной");

            Name = name;
            EntranceFee = entranceFee;
        }

        public string Name { get; private set; }

        public double EntranceFee { get; set; }

        public int? TouristRouteId { get; private set; }

        public void AddToTouristRoute(int touristRouteId)
        {
            if (touristRouteId <= 0)
                throw new ArgumentException("Некорректный идентификатор туристического маршрута", nameof(touristRouteId));
            TouristRouteId = touristRouteId;
        }

        public void RemoveFromTouristRoute()
        {
            TouristRouteId = null;
        }

        public void ChangeEntranceFee(double newFee)
        {
            if (newFee < 0)
                throw new ArgumentOutOfRangeException(nameof(newFee), "Стоимость билета не может быть отрицательной");
            EntranceFee = newFee;
        }
    }
}