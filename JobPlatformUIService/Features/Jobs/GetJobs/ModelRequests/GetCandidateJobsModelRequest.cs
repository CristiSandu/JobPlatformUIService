using MediatR;
using JobPlatformUIService.Core.Domain.Jobs;

namespace JobPlatformUIService.Features.Jobs.GetJobs.ModelRequests;

public class GetCandidateJobsModelRequest : IRequest<List<CandidateJobsExtendedModel>>
{

}
