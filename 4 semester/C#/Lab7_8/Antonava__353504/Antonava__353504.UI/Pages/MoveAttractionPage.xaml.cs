using Antonava__353504.UI.ViewModels;

namespace Antonava__353504.UI.Pages;

public partial class MoveAttractionPage : ContentPage
{
	public MoveAttractionPage(MoveAttractionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}