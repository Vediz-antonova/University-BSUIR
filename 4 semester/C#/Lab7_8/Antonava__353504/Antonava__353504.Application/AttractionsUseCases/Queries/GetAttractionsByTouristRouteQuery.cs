namespace Antonava__353504.Application.AttractionsUseCases.Queries
{
    public sealed record GetAttractionsByTouristRouteQuery(int TouristRouteId) : IRequest<IEnumerable<Attraction>>;
}