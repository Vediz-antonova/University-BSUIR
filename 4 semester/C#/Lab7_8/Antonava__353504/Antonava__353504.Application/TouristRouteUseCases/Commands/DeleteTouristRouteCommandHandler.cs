namespace Antonava__353504.Application.TouristRouteUseCases.Commands
{
    internal class DeleteTouristRouteCommandHandler : IRequestHandler<DeleteTouristRouteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTouristRouteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteTouristRouteCommand request, CancellationToken cancellationToken)
        {
            var route = await _unitOfWork.TouristRouteRepository.GetByIdAsync(request.Id, cancellationToken);
            if (route is null)
            {
                throw new ArgumentException($"TouristRoute with Id {request.Id} was not found.");
            }

            await _unitOfWork.TouristRouteRepository.DeleteAsync(route, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return Unit.Value;
        }

        Task IRequestHandler<DeleteTouristRouteCommand>.Handle(DeleteTouristRouteCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}