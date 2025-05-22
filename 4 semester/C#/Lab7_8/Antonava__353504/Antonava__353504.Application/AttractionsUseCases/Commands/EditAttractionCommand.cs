namespace Antonava__353504.Application.AttractionsUseCases.Commands
{
    public sealed record EditAttractionCommand(
        string Name,
        double EntranceFee,
        int TouristRouteId) : IRequest<Attraction>;
}
