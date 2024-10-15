using MediatR;

namespace AspNetCoreUseHangfire.Models
{
    public class RecurJobModel : IRequest
    {
        public string Id { get; set; }
    }

    public class RecurJobModelHandler : IRequestHandler<RecurJobModel>
    {
        public async Task Handle(RecurJobModel request, CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync("RecurJobModel");
        }
    }
}
