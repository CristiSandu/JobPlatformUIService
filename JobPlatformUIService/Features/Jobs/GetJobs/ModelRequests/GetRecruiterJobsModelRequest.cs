using MediatR;
using JobPlatformUIService.Core.DataModel;

namespace JobPlatformUIService.Features.Jobs.GetJobs.ModelRequests;

public class GetRecruiterJobsModelRequest : IRequest<List<RecruterJobs>>
{
    public string UserID { get; set; }
}