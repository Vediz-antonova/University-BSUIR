using Antonava__353504.UI.Pages;

namespace Antonava__353504.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AttractionDetailsPage), typeof(AttractionDetailsPage));
            Routing.RegisterRoute(nameof(NewTouristRoutePage), typeof(NewTouristRoutePage));
            Routing.RegisterRoute(nameof(NewAttractionPage), typeof(NewAttractionPage));
            Routing.RegisterRoute(nameof(EditAttractionPage), typeof(EditAttractionPage));
            Routing.RegisterRoute(nameof(MoveAttractionPage), typeof(MoveAttractionPage));
        }
    }
}
