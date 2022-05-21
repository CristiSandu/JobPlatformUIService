using Google.Cloud.Firestore;
using JobPlatformUIService.Core.Domain.Jobs;
using JobPlatformUIService.Features.Jobs.GetJobs.ModelRequests;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.GetJobs;

public class GetCandidateJobsModelHandler : IRequestHandler<GetCandidateJobsModelRequest, List<CandidateJobsExtendedModel>>
{
    private readonly IFirestoreService<CandidateJobsExtendedModel> _firestoreServiceC;

    private readonly IFirestoreContext _firestoreContext;
    public GetCandidateJobsModelHandler(IFirestoreService<CandidateJobsExtendedModel> firestoreServiceC,
           IFirestoreContext firestoreContext)
    {
        _firestoreServiceC = firestoreServiceC;
        _firestoreContext = firestoreContext;
    }

    public async Task<List<CandidateJobsExtendedModel>> Handle(GetCandidateJobsModelRequest request, CancellationToken cancellationToken)
    {
        CollectionReference collectionReferenceC = _firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.CandidateJobsColection);

        var candidateJobList = await _firestoreServiceC.GetFilteredDocumentsByAField("CandidateID", request.UserID, collectionReferenceC);

        if (candidateJobList == null)
        {
            return new();
        }

        return candidateJobList;
    }
}
