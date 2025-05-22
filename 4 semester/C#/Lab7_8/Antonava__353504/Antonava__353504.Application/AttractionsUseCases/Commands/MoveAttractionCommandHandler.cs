namespace Antonava__353504.Application.AttractionsUseCases.Commands
{
    internal class MoveAttractionCommandHandler : IRequestHandler<MoveAttractionCommand, Attraction>
    {
        private readonly IUnitOfWork _unitOfWork;
        public MoveAttractionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Attraction> Handle(MoveAttractionCommand request, CancellationToken cancellationToken)
        {
            var attraction = await _unitOfWork.AttractionRepository.FirstOrDefaultAsync(a => a.Name == request.AttractionName, cancellationToken);
            if (attraction is null)
                throw new ArgumentException($"Attraction with Name {request.AttractionName} not found.");

            attraction.RemoveFromTouristRoute();
            attraction.AddToTouristRoute(request.NewTouristRouteId);
            await _unitOfWork.AttractionRepository.UpdateAsync(attraction, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return attraction;
        }
    }
}
