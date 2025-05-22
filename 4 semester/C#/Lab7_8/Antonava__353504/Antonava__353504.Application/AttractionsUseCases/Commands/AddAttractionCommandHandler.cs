namespace Antonava__353504.Application.AttractionsUseCases.Commands
{
    internal class AddAttractionCommandHandler : IRequestHandler<AddAttractionCommand, Attraction>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddAttractionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Attraction> Handle(AddAttractionCommand request, CancellationToken cancellationToken)
        {
            var newAttraction = new Attraction(request.Name, request.EntranceFee);
            if (request.TouristRouteId.HasValue)
            {
                newAttraction.AddToTouristRoute(request.TouristRouteId.Value);
            }
            await _unitOfWork.AttractionRepository.AddAsync(newAttraction, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return newAttraction;
        }
    }
}