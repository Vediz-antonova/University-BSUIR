namespace Antonava__353504.Application.AttractionsUseCases.Commands
{
    public sealed record AddAttractionCommand(
        string Name,
        double EntranceFee,
        int? TouristRouteId) : IRequest<Attraction>;
}