using MediatR;
using JobPlatformUIService.Core.DataModel;

namespace JobPlatformUIService.Features.Jobs.GetJobs.ModelRequests;

public class GetRecruiterJobsByIdModelRequest : IRequest<List<RecruterJobs>>
{
    public string AngajatorID { get; set; }
    public string JobID { get; set; }

    public string DocumentID => $"{AngajatorID}-{JobID}";
}
