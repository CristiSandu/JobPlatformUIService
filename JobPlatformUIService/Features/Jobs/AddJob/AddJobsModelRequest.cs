using MediatR;

namespace JobPlatformUIService.Features.Jobs.AddJob;

public class AddJobsModelRequest : IRequest<bool>
{
    public Core.DataModel.Job JobData { get; set; }
}
