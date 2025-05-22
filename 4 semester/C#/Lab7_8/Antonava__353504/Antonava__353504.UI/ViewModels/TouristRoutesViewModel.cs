using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Antonava__353504.Application.TouristRouteUseCases.Queries;
using Antonava__353504.Application.AttractionsUseCases.Queries;
using Antonava__353504.UI.Pages;
using Antonava__353504.Application.TouristRouteUseCases.Commands;

namespace Antonava__353504.UI.ViewModels
{
    public partial class TouristRoutesViewModel : ObservableObject
    {
        private readonly IMediator _mediator;

        public TouristRoutesViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ObservableCollection<TouristRoute> TouristRoutes { get; set; } = new();
        public ObservableCollection<Attraction> Attractions { get; set; } = new();

        [ObservableProperty]
        private TouristRoute selectedTouristRoute;

        [RelayCommand]
        async Task UpdateGroupList() => await GetTouristRoutes();

        [RelayCommand]
        async Task UpdateMembersList() => await GetAttractions();

        public async Task GetTouristRoutes()
        {
            System.Diagnostics.Debug.WriteLine("Команда UpdateGroupList вызвана");
            var routes = await _mediator.Send(new GetTouristRoutesQuery());
            System.Diagnostics.Debug.WriteLine($"Получено {routes.Count()} маршрутов");

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                TouristRoutes.Clear();
                foreach (var route in routes)
                    TouristRoutes.Add(route);
            });
        }

        public async Task GetAttractions()
        {
            if (SelectedTouristRoute == null)
                return;

            var attractions = await _mediator.Send(new GetAttractionsByTouristRouteQuery(SelectedTouristRoute.Id));
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Attractions.Clear();
                foreach (var attraction in attractions)
                    Attractions.Add(attraction);
            });
        }

        [RelayCommand]
        async Task ShowDetails(Attraction attraction)
        {
            await GotoDetailsPage(attraction);
        }

        private async Task GotoDetailsPage(Attraction attraction)
        {
            var parameters = new Dictionary<string, object>
            {
                { "Attraction", attraction }
            };

            await Shell.Current.GoToAsync(nameof(AttractionDetailsPage), parameters);
        }

        // Команда для перехода к странице создания нового маршрута
        [RelayCommand]
        async Task CreateNewTouristRoute()
        {
            await Shell.Current.GoToAsync("NewTouristRoutePage");
        }

        // Команда для перехода к странице создания новой достопримечательности с передачей Id текущего маршрута
        [RelayCommand]
        async Task CreateNewAttraction()
        {
            if (SelectedTouristRoute == null)
                return;

            var parameters = new Dictionary<string, object>
            {
                { "TouristRouteId", SelectedTouristRoute.Id }
            };
            await Shell.Current.GoToAsync("NewAttractionPage", parameters);
        }

        [RelayCommand]
        async Task DeleteTouristRoute()
        {
            bool confirm = await Shell.Current.DisplayAlert("Подтверждение", "Вы уверены, что хотите удалить данный маршрут?", "Да", "Нет");
            if (!confirm) return;

            await _mediator.Send(new DeleteTouristRouteCommand(selectedTouristRoute.Id));
            await Shell.Current.GoToAsync("..");
        }
    }
}
