using Antonava__353504.Domain.Entities;

namespace Antonava__353504.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<TouristRoute> TouristRouteRepository { get; }
        IRepository<Attraction> AttractionRepository { get; }
        public Task SaveAllAsync();
        public Task DeleteDataBaseAsync();
        public Task CreateDataBaseAsync();
    }
}
