using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.ChangeJobStatus;

public class ValidateJobModelHandler : IRequestHandler<ValidateJobModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.Job> _firestoreService;
    private readonly CollectionReference _collectionReference;
    public ValidateJobModelHandler(IFirestoreService<Core.DataModel.Job> firestoreService,
           IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.JobsColection);
    }

    public async Task<bool> Handle(ValidateJobModelRequest request, CancellationToken cancellationToken)
    {
        return await _firestoreService.UpdateDocumentFieldAsync("IsCheck", request.JobId, request.IsCheck, _collectionReference);
    }
}
