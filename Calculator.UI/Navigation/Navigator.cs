using Calculator.UI.Commands;
using Calculator.UI.Models;
using Calculator.UI.ViewModels;
using Calculator.UI.ViewModels.Factories;
using System.Windows.Input;

namespace Calculator.UI.Navigation
{
    public class Navigator : ObservableObject, INavigator
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel 
        {
            get 
            {
                return _currentViewModel;
            }
            set 
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
    }
}
