using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.User.GetUsers;

public class GetUsersModelHandler : IRequestHandler<GetUsersModelRequest, List<Core.DataModel.User>>
{
    private readonly IFirestoreService<Core.DataModel.User> _firestoreService;
    private readonly CollectionReference _collectionReference;
    public GetUsersModelHandler(IFirestoreService<Core.DataModel.User> firestoreService,
           IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.UsersColection);
    }

    public async Task<List<Core.DataModel.User>> Handle(GetUsersModelRequest request, CancellationToken cancellationToken)
    {
        if (request.UserId == "All")
            return await _firestoreService.GetDocumentsInACollection(_collectionReference);
        return await _firestoreService.GetDocumentByIds(request.UserId, _collectionReference);
    }
}
