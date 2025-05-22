using Antonava__353504.UI.ViewModels;

namespace Antonava__353504.UI.Pages;

public partial class NewTouristRoutePage : ContentPage
{
    public NewTouristRoutePage(NewTouristRouteViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}