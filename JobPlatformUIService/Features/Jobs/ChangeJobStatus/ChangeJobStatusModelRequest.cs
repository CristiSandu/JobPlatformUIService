using MediatR;

namespace JobPlatformUIService.Features.Jobs.ChangeJobStatus;

public class ChangeJobStatusModelRequest : IRequest<bool>
{
    public string JobId { get; set; }
    public string AngajatorId { get; set; }
    public string CandidateId { get; set; }

    public string RecruterJobId => $"{AngajatorId}-{JobId}";
    public string CandidateJobId => $"{CandidateId}-{JobId}";
    public int JobStatus { get; set; }
}
