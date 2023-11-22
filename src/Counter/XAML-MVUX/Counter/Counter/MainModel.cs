namespace Counter;

public partial record MainModel
{
    public IState<int> StepSize => State.Value(this, () => 1);

    public IState<int> CounterValue => State.Value(this, () => 0);

    public ValueTask IncrementCommand(int stepSize, CancellationToken ct)
            => CounterValue.Update(c => c + stepSize, ct);
}
