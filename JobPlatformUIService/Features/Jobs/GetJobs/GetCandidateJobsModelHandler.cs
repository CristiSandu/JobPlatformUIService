using Google.Cloud.Firestore;
using JobPlatformUIService.Core.Domain.Jobs;
using JobPlatformUIService.Core.Helpers;
using JobPlatformUIService.Features.Jobs.GetJobs.ModelRequests;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using JobPlatformUIService.Web;
using MediatR;
using System.Net;

namespace JobPlatformUIService.Features.Jobs.GetJobs;

public class GetCandidateJobsModelHandler : IRequestHandler<GetCandidateJobsModelRequest, List<CandidateJobsExtendedModel>>
{
    private readonly IFirestoreService<CandidateJobsExtendedModel> _firestoreServiceC;
    private readonly IJWTParser _jwtParser;
    private readonly IFirestoreContext _firestoreContext;
    
    public GetCandidateJobsModelHandler(IFirestoreService<CandidateJobsExtendedModel> firestoreServiceC,
        IJWTParser jwtParser,
        IFirestoreContext firestoreContext)
    {
        _firestoreServiceC = firestoreServiceC;
        _jwtParser = jwtParser;
        _firestoreContext = firestoreContext;
    }

    public async Task<List<CandidateJobsExtendedModel>> Handle(GetCandidateJobsModelRequest request, CancellationToken cancellationToken)
    {
        if (await _jwtParser.VerifyUserRole(Constants.RecruiterRole) )
            throw new ApiException(HttpStatusCode.Unauthorized, $"This is not a Candidate");

        string uid = await _jwtParser.GetUserIdFromJWT();

        CollectionReference collectionReferenceC = _firestoreContext.FirestoreDB.Collection(Constants.CandidateJobsColection);
        var candidateJobList = await _firestoreServiceC.GetFilteredDocumentsByAField("CandidateID", uid, collectionReferenceC);

        if (candidateJobList == null)
        {
            throw new ApiException(HttpStatusCode.NoContent, $"No list of data");
        }

        return candidateJobList;
    }
}
