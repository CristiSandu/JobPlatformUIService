using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.User.UpdateUser;

public class UpdateUserModelHandler : IRequestHandler<UpdateUserModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.User> _firestoreService;
    private readonly CollectionReference _collectionReference;
    public UpdateUserModelHandler(IFirestoreService<Core.DataModel.User> firestoreService,
           IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.UsersColection);
    }
    public async Task<bool> Handle(UpdateUserModelRequest request, CancellationToken cancellationToken)
    {
        if (request.UserData.DocumentId != request.UserID)
            return false;

        return await _firestoreService.UpdateDocumentAsync(request.UserData, _collectionReference);
    }
}
