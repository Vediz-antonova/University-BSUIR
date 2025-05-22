namespace Antonava__353504.Application.TouristRouteUseCases.Queries
{
    public sealed record GetTouristRoutesQuery() : IRequest<IEnumerable<TouristRoute>>;
}