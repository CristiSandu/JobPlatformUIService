using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.User.AddUser;

public class AddUserModelHandler : IRequestHandler<AddUserModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.User> _firestoreService;
    private readonly CollectionReference _collectionReference;
    public AddUserModelHandler(IFirestoreService<Core.DataModel.User> firestoreService,
           IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.UsersColection);
    }
    public async Task<bool> Handle(AddUserModelRequest request, CancellationToken cancellationToken)
    {
        return await _firestoreService.InsertDocumentAsync(request.UserData, _collectionReference);
    }
}
