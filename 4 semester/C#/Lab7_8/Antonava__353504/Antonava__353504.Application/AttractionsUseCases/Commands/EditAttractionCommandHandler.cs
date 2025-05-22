namespace Antonava__353504.Application.AttractionsUseCases.Commands
{
    internal class EditAttractionCommandHandler : IRequestHandler<EditAttractionCommand, Attraction>
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditAttractionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Attraction> Handle(EditAttractionCommand request, CancellationToken cancellationToken)
        {
            var attraction = await _unitOfWork.AttractionRepository.FirstOrDefaultAsync(a => a.Name == request.Name, cancellationToken);
            if (attraction is null)
                throw new ArgumentException($"Attraction with Name {request.Name} not found.");

            attraction.EntranceFee = request.EntranceFee;
            attraction.AddToTouristRoute(request.TouristRouteId);
            await _unitOfWork.AttractionRepository.UpdateAsync(attraction, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return attraction;
        }
    }
}
