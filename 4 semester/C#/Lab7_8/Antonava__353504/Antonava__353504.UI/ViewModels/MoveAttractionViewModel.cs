using Antonava__353504.Application.AttractionsUseCases.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Antonava__353504.UI.ViewModels
{
    [QueryProperty(nameof(Attraction.Name), "AttractionName")]
    public partial class MoveAttractionViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        public MoveAttractionViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public MoveAttractionViewModel()
        {
        }

        [ObservableProperty]
        private string attractionName;
        [ObservableProperty]
        private int newTouristRouteId;

        [RelayCommand]
        async Task MoveAttraction()
        {
            var moveCommand = new MoveAttractionCommand(AttractionName, NewTouristRouteId);
            var result = await _mediator.Send(moveCommand);
            await Shell.Current.GoToAsync("..");
        }
    }
}
