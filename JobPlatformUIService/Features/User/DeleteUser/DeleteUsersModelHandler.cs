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
        var userReqData = await _firestoreService.GetDocumentByIds(request.UserReqID, _collectionReference);
        if (userReqData.Count == 0)
        {
            return false;
        }
        
        return userReqData[0].IsAdmin && await _firestoreService.DeleteDocumentByIdAsync(request.UserID, _collectionReference);
    }
}
