using MediatR;

namespace JobPlatformUIService.Features.Jobs.DeleteJob;

public class DeleteJobsModelRequest : IRequest<bool>
{
    public Core.DataModel.Job JobData { get; set; }
    public string JobId { get; set; }
}
