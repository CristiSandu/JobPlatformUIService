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

public class GetRecruiterJobsModelHandler : IRequestHandler<GetRecruiterJobsModelRequest, List<RecruterJobs>>
{
    private readonly IFirestoreService<RecruterJobs> _firestoreService;
    private readonly IJWTParser _jwtParser;
    private readonly CollectionReference _collectionReference;
    public GetRecruiterJobsModelHandler(IFirestoreService<RecruterJobs> firestoreServiceC,
        IJWTParser jwtParser,
           IFirestoreContext firestoreContext)
    {
        _jwtParser = jwtParser;
        _firestoreService = firestoreServiceC;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Constants.RecruterJobsColection);
    }

    public async Task<List<RecruterJobs>> Handle(GetRecruiterJobsModelRequest request, CancellationToken cancellationToken)
    {
        if (!await _jwtParser.VerifyUserRole(Constants.RecruiterRole))
            throw new ApiException(HttpStatusCode.Unauthorized, $"This is not a Recruiter");

        string uid = await _jwtParser.GetUserIdFromJWT();
        var recruiterJobList = await _firestoreService.GetFilteredDocumentsByAField("AngajatorID", uid, _collectionReference);

        if (recruiterJobList == null)
        {
            throw new ApiException(HttpStatusCode.NoContent, $"This is not a No Data");
        }

        return recruiterJobList;
    }
}
