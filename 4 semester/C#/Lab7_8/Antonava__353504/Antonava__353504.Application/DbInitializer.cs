using Microsoft.Extensions.DependencyInjection;

namespace Antonava__353504.Application
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider services)
        {
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();

            await unitOfWork.DeleteDataBaseAsync();
            await unitOfWork.CreateDataBaseAsync();

            var route1 = new TouristRoute("Исторический маршрут", DateTime.Now.AddDays(3), 2);
            var route2 = new TouristRoute("Природный экспресс", DateTime.Now.AddDays(5), 3);

            await unitOfWork.TouristRouteRepository.AddAsync(route1);
            await unitOfWork.TouristRouteRepository.AddAsync(route2);
            await unitOfWork.SaveAllAsync();

            var attraction1 = new Attraction("Музей", 5.0);
            attraction1.AddToTouristRoute(route1.Id);

            var attraction2 = new Attraction("Старый город", 7.0);
            attraction2.AddToTouristRoute(route1.Id);

            var attraction3 = new Attraction("Национальный парк", 3.0);
            attraction3.AddToTouristRoute(route2.Id);

            await unitOfWork.AttractionRepository.AddAsync(attraction1);
            await unitOfWork.AttractionRepository.AddAsync(attraction2);
            await unitOfWork.AttractionRepository.AddAsync(attraction3);

            await unitOfWork.SaveAllAsync();
        }
    }
}
