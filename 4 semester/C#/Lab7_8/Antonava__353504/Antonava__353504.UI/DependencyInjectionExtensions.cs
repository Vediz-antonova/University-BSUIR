namespace Antonava__353504.UI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterPages(this IServiceCollection services)
        {
            services.AddSingleton<Pages.TouristRoutesPage>();
            services.AddTransient<Pages.AttractionDetailsPage>();
            services.AddTransient<Pages.NewTouristRoutePage>();
            services.AddTransient<Pages.NewAttractionPage>();
            services.AddTransient<Pages.EditAttractionPage>();
            services.AddTransient<Pages.MoveAttractionPage>();
            return services;
        }

        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddSingleton<ViewModels.TouristRoutesViewModel>(); 
            services.AddTransient<ViewModels.AttractionDetailsViewModel>();
            services.AddTransient<ViewModels.NewTouristRouteViewModel>();
            services.AddTransient<ViewModels.NewAttractionViewModel>();
            services.AddTransient<ViewModels.EditAttractionViewModel>();
            services.AddTransient<ViewModels.MoveAttractionViewModel>();
            return services;
        }
    }
}
