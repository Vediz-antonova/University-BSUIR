namespace Antonava__353504.Application.AttractionsUseCases.Commands
{
    public sealed record MoveAttractionCommand(
        string AttractionName,
        int NewTouristRouteId) : IRequest<Attraction>;
}
