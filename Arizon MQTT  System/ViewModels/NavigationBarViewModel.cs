using Arizon_MQTT__System.Commands;
using Arizon_MQTT__System.Stores;
using Arizon_MQTT_System.Domain.Services.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Arizon_MQTT__System.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateSendTemperatureCommand { get; }
        private readonly NavigationStore _navigationStore;

        public NavigationBarViewModel(
            NavigationStore navigationStore, 
            INavigationService navigateHomeService,
            INavigationService navigateSendTemperatureService)
        {
            NavigateHomeCommand = new NavigateCommand(navigateHomeService);
            NavigateSendTemperatureCommand = new NavigateCommand(navigateSendTemperatureService);
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += _navigationStore_CurrentViewModelChanged;
        }

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        private void _navigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
