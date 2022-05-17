using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.User.DeleteUser;


public class DeleteUsersModelHandler : IRequestHandler<DeleteUsersModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.User> _firestoreService;
    private readonly CollectionReference _collectionReference;
    public DeleteUsersModelHandler(IFirestoreService<Core.DataModel.User> firestoreService,
           IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.UsersColection);
    }

    public async Task<bool> Handle(DeleteUsersModelRequest request, CancellationToken cancellationToken)
    {
       return  await _firestoreService.DeleteDocumentByIdAsync(request.UserID, _collectionReference);
    }
}
