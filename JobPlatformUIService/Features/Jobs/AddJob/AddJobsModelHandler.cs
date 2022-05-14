using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.AddJob;

public class AddJobsModelHandler : IRequestHandler<AddJobsModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.Job> _firestoreService;
    private readonly CollectionReference _collectionReference;
    public AddJobsModelHandler(IFirestoreService<Core.DataModel.Job> firestoreService,
           IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.JobsColection);
    }

    public async Task<bool> Handle(AddJobsModelRequest request, CancellationToken cancellationToken)
    {
        return await _firestoreService.InsertDocumentAsync(request.JobData, _collectionReference);
    }
}
