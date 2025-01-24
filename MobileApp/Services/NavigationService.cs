using Microsoft.Extensions.DependencyInjection;
using Northboundei.Mobile.Helpers;
using Northboundei.Mobile.IServices;
using Northboundei.Mobile.Mvvm.ViewModels;


namespace Northboundei.Mobile.Services
{
    public class NavigationService : INavigationService
    {
        IServiceProvider _serviceProvider;
        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            var viewType = ViewLocator.GetViewType<TViewModel>();
            if (viewType != null)
            {
                var viewModel = ViewModelFactory.CreateViewModel<TViewModel>(_serviceProvider);
                var page = (Page)Activator.CreateInstance(viewType, viewModel);

                await InsertPageBeforeAsync(page, Application.Current.MainPage);
            }
        }

        public async Task InsertPageBeforeAsync(Page page, Page beforePage)
        {
            if (Shell.Current.Navigation.NavigationStack.Contains(beforePage))
            {
                Shell.Current.Navigation.InsertPageBefore(page, beforePage);
                await Shell.Current.Navigation.PopAsync();
            }
        }
        public async Task GoBackAsync()
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
