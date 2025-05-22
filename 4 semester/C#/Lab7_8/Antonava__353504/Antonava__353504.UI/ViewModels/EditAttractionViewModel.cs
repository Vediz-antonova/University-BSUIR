using Antonava__353504.Application.AttractionsUseCases.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Antonava__353504.UI.ViewModels
{
    [QueryProperty(nameof(Attraction.Name), "Name")]
    public partial class EditAttractionViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        public EditAttractionViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public EditAttractionViewModel()
        {
            this.Name = Name;
        }

        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private double entranceFee;
        [ObservableProperty]
        private int touristRouteId;

        [RelayCommand]
        async Task SaveEdit()
        {
            var editCommand = new EditAttractionCommand(Name, EntranceFee, TouristRouteId);
            await _mediator.Send(editCommand);
            await Shell.Current.GoToAsync("..");
        }
    }
}
