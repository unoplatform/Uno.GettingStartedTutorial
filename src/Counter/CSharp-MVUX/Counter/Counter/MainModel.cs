using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counter
{
    public partial record MainModel
    {
        public IState<int> StepSize => State.Value(this, () => 1);

        public IState<int> CounterValue => State.Value(this, () => 0);

        public async ValueTask IncrementCommand(CancellationToken ct = default)
        {
            var step = await StepSize.Value(ct);

            await CounterValue.Update(c => c + step, ct);
        }
    }
}
