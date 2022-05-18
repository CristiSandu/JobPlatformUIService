using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using JobPlatformUIService.Features.Jobs.GetJobs.ModelRequests;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.GetJobs;

public class GetRecruiterJobsByIdModelHandler : IRequestHandler<GetRecruiterJobsByIdModelRequest, List<RecruterJobs>>
{
    private readonly IFirestoreService<RecruterJobs> _firestoreService;

    private readonly IFirestoreContext _firestoreContext;
    public GetRecruiterJobsByIdModelHandler(IFirestoreService<RecruterJobs> firestoreServiceC,
           IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreServiceC;
        _firestoreContext = firestoreContext;
    }

    public async Task<List<RecruterJobs>> Handle(GetRecruiterJobsByIdModelRequest request, CancellationToken cancellationToken)
    {
        CollectionReference collectionReference = _firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.RecruterJobsColection);

        var recruiterJobList = await _firestoreService.GetDocumentByIds(request.DocumentID, collectionReference);

        if (recruiterJobList == null)
        {
            return new();
        }

        return recruiterJobList;
    }
}
