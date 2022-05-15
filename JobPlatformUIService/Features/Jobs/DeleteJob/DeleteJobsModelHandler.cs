using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.DeleteJob;

public class DeleteJobsModelHandler : IRequestHandler<DeleteJobsModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.Job> _firestoreService;
    private readonly CollectionReference _collectionReference;
    public DeleteJobsModelHandler(IFirestoreService<Core.DataModel.Job> firestoreService,
           IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.JobsColection);
    }

    public async Task<bool> Handle(DeleteJobsModelRequest request, CancellationToken cancellationToken)
    {
        var userReqData = await _firestoreService.GetDocumentByIds(request.JobId, _collectionReference);
        if (userReqData.Count == 0)
        {
            return false;
        }

        return await _firestoreService.DeleteDocumentByIdAsync(request.JobId, _collectionReference);
    }
}
