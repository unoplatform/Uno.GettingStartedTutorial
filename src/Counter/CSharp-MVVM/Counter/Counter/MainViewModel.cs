namespace Counter;

internal partial class MainViewModel:ObservableObject
{
    [ObservableProperty]
    private int _stepSize = 1;

    [ObservableProperty]
    private int _counterValue = 0;

    [RelayCommand]
    private void Increment()
    {
        CounterValue += StepSize; 
    }
}
