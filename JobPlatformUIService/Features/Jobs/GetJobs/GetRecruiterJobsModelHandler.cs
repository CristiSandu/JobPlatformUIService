using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using JobPlatformUIService.Features.Jobs.GetJobs.ModelRequests;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.GetJobs;

public class GetRecruiterJobsModelHandler : IRequestHandler<GetRecruiterJobsModelRequest, List<RecruterJobs>>
{
    private readonly IFirestoreService<RecruterJobs> _firestoreService;

    private readonly IFirestoreContext _firestoreContext;
    public GetRecruiterJobsModelHandler(IFirestoreService<RecruterJobs> firestoreServiceC,
           IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreServiceC;
        _firestoreContext = firestoreContext;
    }

    public async Task<List<RecruterJobs>> Handle(GetRecruiterJobsModelRequest request, CancellationToken cancellationToken)
    {
        CollectionReference collectionReference = _firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.RecruterJobsColection);

        var recruiterJobList = await _firestoreService.GetFilteredDocumentsByAField("AngajatorID", request.UserID, collectionReference);

        if (recruiterJobList == null)
        {
            return new();
        }

        return recruiterJobList;
    }
}
