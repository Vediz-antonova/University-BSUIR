namespace Antonava__353504.Application.TouristRouteUseCases.Commands
{
    internal class AddTouristRouteCommandHandler : IRequestHandler<AddTouristRouteCommand, TouristRoute>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddTouristRouteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TouristRoute> Handle(AddTouristRouteCommand request, CancellationToken cancellationToken)
        {
            var route = new TouristRoute(request.Name, request.CommenceFrom, request.Duration);
            await _unitOfWork.TouristRouteRepository.AddAsync(route, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return route;
        }
    }
}