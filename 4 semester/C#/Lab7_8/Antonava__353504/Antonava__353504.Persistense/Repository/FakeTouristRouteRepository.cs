using System.Linq.Expressions;

namespace Antonava__353504.Persistense.Repository
{
    public class FakeTouristRouteRepository : IRepository<TouristRoute>
    {
        private readonly List<TouristRoute> _routes;

        public FakeTouristRouteRepository()
        {
            _routes = new List<TouristRoute>();

            var route = new TouristRoute("Экскурсия по историческому центру", DateTime.Now.AddDays(5), 3);
            route.Id = 1;
            _routes.Add(route);

            route = new TouristRoute("Приключение в заповедном лесу", DateTime.Now.AddDays(10), 2);
            route.Id = 2;
            _routes.Add(route);
        }

        public Task AddAsync(TouristRoute entity, CancellationToken cancellationToken = default)
        {
            _routes.Add(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TouristRoute entity, CancellationToken cancellationToken = default)
        {
            _routes.Remove(entity);
            return Task.CompletedTask;
        }

        public Task<TouristRoute> FirstOrDefaultAsync(Expression<Func<TouristRoute, bool>> filter, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_routes.AsQueryable().FirstOrDefault(filter));
        }

        public Task<TouristRoute> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<TouristRoute, object>>[] includesProperties)
        {
            return Task.FromResult(_routes.FirstOrDefault(r => r.Id == id));
        }

        public Task<IReadOnlyList<TouristRoute>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult((IReadOnlyList<TouristRoute>)_routes);
        }

        public Task<IReadOnlyList<TouristRoute>> ListAsync(Expression<Func<TouristRoute, bool>> filter, CancellationToken cancellationToken = default, params Expression<Func<TouristRoute, object>>[] includesProperties)
        {
            var query = _routes.AsQueryable();
            var list = query.Where(filter).ToList();
            return Task.FromResult((IReadOnlyList<TouristRoute>)list);
        }

        public Task UpdateAsync(TouristRoute entity, CancellationToken cancellationToken = default)
        {
            var existing = _routes.FirstOrDefault(r => r.Id == entity.Id);
            if (existing != null)
            {
                var index = _routes.IndexOf(existing);
                _routes[index] = entity;
            }
            return Task.CompletedTask;
        }
    }
}
