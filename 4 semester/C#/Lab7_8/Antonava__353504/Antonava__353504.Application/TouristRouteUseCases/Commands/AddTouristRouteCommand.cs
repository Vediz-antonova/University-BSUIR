namespace Antonava__353504.Application.TouristRouteUseCases.Commands
{
    public sealed record AddTouristRouteCommand(string Name, DateTime CommenceFrom, int Duration) : IRequest<TouristRoute>;
}