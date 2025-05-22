using Antonava__353504.UI.ViewModels;

namespace Antonava__353504.UI.Pages;

public partial class EditAttractionPage : ContentPage
{
	public EditAttractionPage(EditAttractionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}