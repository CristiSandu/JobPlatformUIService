using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Dropdowns.DeleteValues;

public class DeleteValuesModelHandler : IRequestHandler<DeleteValuesModelRequest , bool>
{
    private readonly IFirestoreService<Core.DataModel.DropdownsModels.DomainModel> _firestoreService;
    private readonly CollectionReference _collectionReference;
    public DeleteValuesModelHandler(IFirestoreService<Core.DataModel.DropdownsModels.DomainModel> firestoreService,
           IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.JobsColection);
    }

    public async Task<bool> Handle(DeleteValuesModelRequest request, CancellationToken cancellationToken)
    {
        return await _firestoreService.DeleteDocumentByIdAsync(request.DocumentId.ToLower(), _collectionReference); 
    }
}
