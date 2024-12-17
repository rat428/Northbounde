using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Helpers
{
    public static class ViewLocator
    {
        static public Type GetViewType<TViewModel>()
        {
            var result = GetType<TViewModel>("View");
            if (result == null)
                result = GetType<TViewModel>("Page");
            if (result == null)
                result = GetType<TViewModel>(String.Empty);

            return result;
        }

        static public Type GetType<T>(string type)
        {
            var viewModelType = typeof(T);
            var name = viewModelType.Name.Replace("ViewModel", type);
            var resultType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name == name);

            return resultType;
        }
    }
    public static class ViewModelFactory
    {
        public static T CreateViewModel<T>(IServiceProvider serviceProvider) where T : class
        {
            return ActivatorUtilities.CreateInstance<T>(serviceProvider);
        }
    }
}
