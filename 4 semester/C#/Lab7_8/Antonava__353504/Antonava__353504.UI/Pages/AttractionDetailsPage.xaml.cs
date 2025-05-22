using Antonava__353504.UI.ViewModels;

namespace Antonava__353504.UI.Pages;

public partial class AttractionDetailsPage : ContentPage
{
    public AttractionDetailsPage(AttractionDetailsViewModel viewModel)
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