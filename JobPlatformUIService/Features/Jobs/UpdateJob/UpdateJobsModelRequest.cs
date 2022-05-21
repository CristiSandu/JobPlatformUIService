using MediatR;

namespace JobPlatformUIService.Features.Jobs.UpdateJob;

public class UpdateJobsModelRequest : IRequest<bool>
{
    public Core.DataModel.Job JobData { get; set; }
}
