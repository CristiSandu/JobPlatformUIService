using MediatR;

namespace JobPlatformUIService.Features.Jobs.DeleteJob;

public class DeleteJobsModelRequest : IRequest<bool>
{
    public string JobId { get; set; }
}
