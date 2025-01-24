using Northboundei.Mobile.Mvvm.ViewModels;
namespace Northboundei.Mobile.IServices
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task GoBackAsync();
    }
}
