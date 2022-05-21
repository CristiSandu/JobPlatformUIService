using Google.Cloud.Firestore;
using JobPlatformUIService.Core.Helpers;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.User.DeleteUser;


public class DeleteUsersModelHandler : IRequestHandler<DeleteUsersModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.User> _firestoreService;
    private readonly IJWTParser _jwtParser;

    private readonly CollectionReference _collectionReference;
    public DeleteUsersModelHandler(IFirestoreService<Core.DataModel.User> firestoreService,
        IJWTParser jwtParser,
        IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _jwtParser = jwtParser;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.UsersColection);
    }

    public async Task<bool> Handle(DeleteUsersModelRequest request, CancellationToken cancellationToken)
    {
        if (!await _jwtParser.VerifyUserRole(Constants.AdminRole))
            return false;
        
        return  await _firestoreService.DeleteDocumentByIdAsync(request.UserID, _collectionReference);
    }
}
