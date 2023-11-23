namespace Counter;

public partial record MainModel
{
    public IState<int> Count => State.Value(this, () => 0);

    public IState<int> Step => State.Value(this, () => 1);
    
    public ValueTask IncrementCommand(int Step, CancellationToken ct)
            => Count.Update(c => c + Step, ct);
}
