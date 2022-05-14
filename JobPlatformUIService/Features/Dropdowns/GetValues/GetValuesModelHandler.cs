using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Dropdowns.GetValues;

public class GetValuesModelHandler : IRequestHandler<GetValuesModelRequest, List<Core.DataModel.DropdownsModels.DomainModel>>
{
    private readonly IFirestoreService<Core.DataModel.DropdownsModels.DomainModel> _firestoreService;
    private readonly CollectionReference _collectionReference;
    public GetValuesModelHandler(IFirestoreService<Core.DataModel.DropdownsModels.DomainModel> firestoreService,
           IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.DomainsColection);
    }

    public async Task<List<Core.DataModel.DropdownsModels.DomainModel>> Handle(GetValuesModelRequest request, CancellationToken cancellationToken)
    {
        return await _firestoreService.GetDocumentsInACollection(_collectionReference);
    }
}
