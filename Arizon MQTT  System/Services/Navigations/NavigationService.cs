using Arizon_MQTT__System.Stores;
using Arizon_MQTT__System.ViewModels;
using Arizon_MQTT_System.Domain.Services.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT__System.Services.Navigations
{
    public class NavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {

        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(
            NavigationStore navigationStore,
            Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
