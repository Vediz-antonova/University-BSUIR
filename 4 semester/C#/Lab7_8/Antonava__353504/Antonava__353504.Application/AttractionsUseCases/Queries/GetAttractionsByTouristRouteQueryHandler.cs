namespace Antonava__353504.Application.AttractionsUseCases.Queries
{
    internal class GetAttractionsByTouristRouteQueryHandler : IRequestHandler<GetAttractionsByTouristRouteQuery, IEnumerable<Attraction>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAttractionsByTouristRouteQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Attraction>> Handle(GetAttractionsByTouristRouteQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AttractionRepository.ListAsync(
                a => a.TouristRouteId == request.TouristRouteId,
                cancellationToken);
        }
    }
}
