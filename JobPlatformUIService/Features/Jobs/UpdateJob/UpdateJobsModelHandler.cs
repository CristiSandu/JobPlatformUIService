using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.UpdateJob;

public class UpdateJobsModelHandler : IRequestHandler<UpdateJobsModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.Job> _firestoreService;
    private readonly CollectionReference _collectionReference;
    public UpdateJobsModelHandler(IFirestoreService<Core.DataModel.Job> firestoreService,
           IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.JobsColection);
    }

    public async Task<bool> Handle(UpdateJobsModelRequest request, CancellationToken cancellationToken)
    {
        if (request.JobData.DocumentId != request.JobId)
            return false;

        return await _firestoreService.UpdateDocumentAsync(request.JobData, _collectionReference); 
    }
}
