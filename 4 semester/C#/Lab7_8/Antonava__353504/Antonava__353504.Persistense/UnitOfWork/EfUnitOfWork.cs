using Antonava__353504.Persistense.Data;
using Antonava__353504.Persistense.Repository;

namespace Antonava__353504.Persistense.UnitOfWork
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Lazy<IRepository<TouristRoute>> _touristRouteRepository;
        private readonly Lazy<IRepository<Attraction>> _attractionRepository;

        public EfUnitOfWork(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "AppDbContext не может быть null.");
            _touristRouteRepository = new Lazy<IRepository<TouristRoute>>(() => new EfRepository<TouristRoute>(_context));
            _attractionRepository = new Lazy<IRepository<Attraction>>(() => new EfRepository<Attraction>(_context));
        }

        public IRepository<TouristRoute> TouristRouteRepository => _touristRouteRepository.Value;

        public IRepository<Attraction> AttractionRepository => _attractionRepository.Value;

        public async Task CreateDataBaseAsync() =>
            await _context.Database.EnsureCreatedAsync();

        public async Task DeleteDataBaseAsync() =>
            await _context.Database.EnsureDeletedAsync();

        public async Task SaveAllAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    var databaseValues = await entry.GetDatabaseValuesAsync();
                    if (databaseValues is null)
                    {
                        throw new InvalidOperationException("Данные были удалены другим процессом.");
                    }

                    entry.OriginalValues.SetValues(databaseValues);
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
