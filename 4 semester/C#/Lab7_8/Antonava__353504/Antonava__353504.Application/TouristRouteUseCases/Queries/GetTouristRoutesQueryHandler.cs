namespace Antonava__353504.Application.TouristRouteUseCases.Queries
{
    internal class GetTouristRoutesQueryHandler : IRequestHandler<GetTouristRoutesQuery, IEnumerable<TouristRoute>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTouristRoutesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TouristRoute>> Handle(GetTouristRoutesQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TouristRouteRepository.ListAllAsync(cancellationToken);
        }
    }
}