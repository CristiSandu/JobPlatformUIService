using MediatR;

namespace JobPlatformUIService.Features.Jobs.GetJobs.ModelRequests;

public class GetJobsModelRequest : IRequest<List<Core.Domain.Jobs.JobExtendedModel>>
{
 
}
