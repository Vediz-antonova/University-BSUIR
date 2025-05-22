using System.Xml.Linq;

namespace Antonava__353504.Domain.Entities
{
    public class TouristRoute : Entity
    {
        private readonly List<Attraction> _attractions = new List<Attraction>();

        public TouristRoute(string name, DateTime commenceFrom, int duration)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название маршрута не может быть пустым", nameof(name));
            if (duration <= 0)
                throw new ArgumentException("Продолжительность должна быть положительной", nameof(duration));

            Name = name;
            CommenceFrom = commenceFrom;
            Duration = duration;
        }

        public string Name { get; private set; }

        public DateTime CommenceFrom { get; private set; }

        public int Duration { get; private set; }

        public IReadOnlyList<Attraction> Attractions => _attractions.AsReadOnly();

        public void AddAttraction(Attraction attraction)
        {
            if (attraction == null)
                throw new ArgumentNullException(nameof(attraction));
            _attractions.Add(attraction);
            attraction.AddToTouristRoute(Id);
        }

        public void RemoveAttraction(Attraction attraction)
        {
            if (attraction == null)
                throw new ArgumentNullException(nameof(attraction));
            if (_attractions.Remove(attraction))
            {
                attraction.RemoveFromTouristRoute();
            }
        }
    }
}
