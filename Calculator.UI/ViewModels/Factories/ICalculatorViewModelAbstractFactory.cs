using Calculator.UI.Navigation;

namespace Calculator.UI.ViewModels.Factories
{
    public interface ICalculatorViewModelAbstractFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
