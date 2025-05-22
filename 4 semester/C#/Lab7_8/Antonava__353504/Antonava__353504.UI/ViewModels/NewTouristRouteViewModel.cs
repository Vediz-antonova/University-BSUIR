using Antonava__353504.Application.TouristRouteUseCases.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Antonava__353504.UI.ViewModels
{
    public partial class NewTouristRouteViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        public NewTouristRouteViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ObservableProperty]
        private string routeName;

        [ObservableProperty]
        private DateTime commenceFrom = DateTime.Now;

        [ObservableProperty]
        private int duration;

        [RelayCommand]
        async Task SaveNewTouristRoute()
        {
            var command = new AddTouristRouteCommand(RouteName, CommenceFrom, Duration);
            var result = await _mediator.Send(command);
            await Shell.Current.GoToAsync("..");
        }
    }
}
