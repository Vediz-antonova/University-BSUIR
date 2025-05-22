using Antonava__353504.Application.AttractionsUseCases.Commands;
using Antonava__353504.UI.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Antonava__353504.UI.ViewModels
{
    [QueryProperty(nameof(TouristRouteId), "TouristRouteId")]
    public partial class NewAttractionViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        public NewAttractionViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ObservableProperty]
        private string attractionName;

        [ObservableProperty]
        private double entranceFee;

        [ObservableProperty]
        private int touristRouteId;

        [RelayCommand]
        async Task SaveNewAttraction()
        {
            var command = new AddAttractionCommand(AttractionName, EntranceFee, TouristRouteId);
            var result = await _mediator.Send(command);
            await Shell.Current.GoToAsync("..");
        }
    }
}
