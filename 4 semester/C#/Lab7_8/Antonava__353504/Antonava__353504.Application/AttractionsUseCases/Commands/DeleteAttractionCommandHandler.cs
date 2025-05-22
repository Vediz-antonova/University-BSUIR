namespace Antonava__353504.Application.AttractionsUseCases.Commands
{
    internal class DeleteAttractionCommandHandler : IRequestHandler<DeleteAttractionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAttractionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteAttractionCommand request, CancellationToken cancellationToken)
        {
            var attraction = await _unitOfWork.AttractionRepository.GetByIdAsync(request.Id, cancellationToken);
            if (attraction == null)
                throw new ArgumentException($"Attraction with Id {request.Id} not found.");

            await _unitOfWork.AttractionRepository.DeleteAsync(attraction, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return Unit.Value;
        }

        Task IRequestHandler<DeleteAttractionCommand>.Handle(DeleteAttractionCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}