using MediatR;

namespace JobPlatformUIService.Features.Jobs.GetJobs.ModelRequests;

public class GetJobsModelRequest : IRequest<List<Core.Domain.Jobs.JobExtendedModel>>
{
    public bool IsRecruter { get; set; }
    public bool IsAdmin { get; set; }
    public string UserID { get; set; }
}
