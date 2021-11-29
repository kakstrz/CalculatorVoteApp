namespace Calculator.UI.ViewModels.Factories
{
    public interface ICalculatorViewModelFactory<T> where T : ViewModelBase
    {
        T CreateViewModel();
    }
}
