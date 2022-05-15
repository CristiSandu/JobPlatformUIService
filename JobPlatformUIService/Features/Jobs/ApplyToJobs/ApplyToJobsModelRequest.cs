using MediatR;

namespace JobPlatformUIService.Features.Jobs.ApplyToJobs;

public class ApplyToJobsModelRequest : IRequest<bool>
{
    public string JobId { get; set; }
    public string AngajatorId { get; set; }
    public string CandidateId { get; set; }

    public string RecruterJobId => $"{AngajatorId}-{JobId}";
}
