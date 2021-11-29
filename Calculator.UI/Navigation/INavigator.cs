using Calculator.UI.ViewModels;
using System.Windows.Input;

namespace Calculator.UI.Navigation
{
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
    }
}
