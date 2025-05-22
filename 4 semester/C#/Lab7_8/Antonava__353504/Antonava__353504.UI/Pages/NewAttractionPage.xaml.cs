using Antonava__353504.UI.ViewModels;

namespace Antonava__353504.UI.Pages;

public partial class NewAttractionPage : ContentPage
{
    public NewAttractionPage(NewAttractionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}