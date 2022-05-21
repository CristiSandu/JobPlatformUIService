using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using JobPlatformUIService.Core.Helpers;
using JobPlatformUIService.Features.Jobs.GetJobs.ModelRequests;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using JobPlatformUIService.Web;
using MediatR;
using System.Net;

namespace JobPlatformUIService.Features.Jobs.GetJobs;

public class GetRecruiterJobsByIdModelHandler : IRequestHandler<GetRecruiterJobsByIdModelRequest, List<RecruterJobs>>
{
    private readonly IFirestoreService<RecruterJobs> _firestoreService;
    private readonly IJWTParser _jwtParser;

    private readonly CollectionReference _collectionReference;
    public GetRecruiterJobsByIdModelHandler(IFirestoreService<RecruterJobs> firestoreServiceC,
        IJWTParser jwtParser,
        IFirestoreContext firestoreContext)
    {
        _jwtParser = jwtParser;
        _firestoreService = firestoreServiceC;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Constants.RecruterJobsColection);
    }

    public async Task<List<RecruterJobs>> Handle(GetRecruiterJobsByIdModelRequest request, CancellationToken cancellationToken)
    {
        if (!await _jwtParser.VerifyUserRole(Constants.RecruiterRole))
            throw new ApiException(HttpStatusCode.Unauthorized, $"This is not a Recruiter");

        var recruiterJobList = await _firestoreService.GetDocumentByIds(request.DocumentID, _collectionReference);

        if (recruiterJobList == null)
        {
            throw new ApiException(HttpStatusCode.NoContent, $"This is not a No Data");
        }

        return recruiterJobList;
    }
}
