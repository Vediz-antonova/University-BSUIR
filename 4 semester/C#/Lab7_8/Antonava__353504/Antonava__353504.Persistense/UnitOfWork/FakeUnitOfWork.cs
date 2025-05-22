using Antonava__353504.Persistense.Repository;

namespace Antonava__353504.Persistense.UnitOfWork
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        private readonly IRepository<TouristRoute> _touristRouteRepository;
        private readonly IRepository<Attraction> _attractionRepository;

        public FakeUnitOfWork()
        {
            _touristRouteRepository = new FakeTouristRouteRepository();
            _attractionRepository = new FakeAttractionRepository();
        }

        public IRepository<TouristRoute> TouristRouteRepository => _touristRouteRepository;

        public IRepository<Attraction> AttractionRepository => _attractionRepository;

        public Task CreateDataBaseAsync() => Task.CompletedTask;

        public Task DeleteDataBaseAsync() => Task.CompletedTask;

        public Task SaveAllAsync() => Task.CompletedTask;
    }
}
