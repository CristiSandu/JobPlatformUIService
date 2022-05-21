using MediatR;

namespace JobPlatformUIService.Features.Jobs.ApplyToJobs;

public class ApplyToJobsModelRequest : IRequest<bool>
{
    public string JobId { get; set; }
    public string RecruiterId { get; set; }
    public string RecruterJobId => $"{RecruiterId}-{JobId}";
}
