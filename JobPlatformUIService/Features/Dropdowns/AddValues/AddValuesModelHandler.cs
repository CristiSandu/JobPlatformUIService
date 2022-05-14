using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Dropdowns.AddValues;


public class AddValuesModelHandler : IRequestHandler<AddValuesModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.DropdownsModels.DomainModel> _firestoreService;
    private readonly CollectionReference _collectionReference;
    public AddValuesModelHandler(IFirestoreService<Core.DataModel.DropdownsModels.DomainModel> firestoreService,
           IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.DomainsColection);
    }

    public async Task<bool> Handle(AddValuesModelRequest request, CancellationToken cancellationToken)
    {
        return await _firestoreService.InsertDocumentAsync(new Core.DataModel.DropdownsModels.DomainModel { Name = request.Value}, _collectionReference);
    }
}
