using System.Linq.Expressions;

namespace Antonava__353504.Persistense.Repository
{
    public class FakeAttractionRepository : IRepository<Attraction>
    {
        private readonly List<Attraction> _attractions;

        public FakeAttractionRepository()
        {
            _attractions = new List<Attraction>();
            int k = 1;
            for (int routeId = 1; routeId <= 2; routeId++)
            {
                for (int i = 0; i < 5; i++)
                {
                    var attraction = new Attraction($"Достопримечательность {k}", k * 10.0);
                    attraction.AddToTouristRoute(routeId);
                    attraction.Id = k;
                    _attractions.Add(attraction);
                    k++;
                }
            }
        }

        public Task AddAsync(Attraction entity, CancellationToken cancellationToken = default)
        {
            _attractions.Add(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Attraction entity, CancellationToken cancellationToken = default)
        {
            _attractions.Remove(entity);
            return Task.CompletedTask;
        }

        public Task<Attraction> FirstOrDefaultAsync(Expression<Func<Attraction, bool>> filter, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_attractions.AsQueryable().FirstOrDefault(filter));
        }

        public Task<Attraction> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Attraction, object>>[] includesProperties)
        {
            return Task.FromResult(_attractions.FirstOrDefault(a => a.Id == id));
        }

        public Task<IReadOnlyList<Attraction>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult((IReadOnlyList<Attraction>)_attractions);
        }

        public Task<IReadOnlyList<Attraction>> ListAsync(Expression<Func<Attraction, bool>> filter, CancellationToken cancellationToken = default, params Expression<Func<Attraction, object>>[] includesProperties)
        {
            var query = _attractions.AsQueryable();
            var list = query.Where(filter).ToList();
            return Task.FromResult((IReadOnlyList<Attraction>)list);
        }

        public Task UpdateAsync(Attraction entity, CancellationToken cancellationToken = default)
        {
            var index = _attractions.FindIndex(a => a.Id == entity.Id);
            if (index != -1)
            {
                _attractions[index] = entity;
            }
            return Task.CompletedTask;
        }
    }
}
