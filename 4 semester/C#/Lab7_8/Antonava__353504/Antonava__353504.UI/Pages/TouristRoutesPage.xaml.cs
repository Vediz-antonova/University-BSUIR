using Antonava__353504.UI.ViewModels;
using System.Diagnostics;
namespace Antonava__353504.UI.Pages;

public partial class TouristRoutesPage : ContentPage
{
    public TouristRoutesPage(TouristRoutesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is TouristRoutesViewModel vm)
        {
            await vm.UpdateGroupListCommand.ExecuteAsync(null);
            await vm.UpdateMembersListCommand.ExecuteAsync(null);
        }
    }
}