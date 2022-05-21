using Google.Cloud.Firestore;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.User.UpdateUser;

public class UpdateUserModelHandler : IRequestHandler<UpdateUserModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.User> _firestoreService;
    private readonly IJWTParser _jwtParser;
    private readonly CollectionReference _collectionReference;

    public UpdateUserModelHandler(IFirestoreService<Core.DataModel.User> firestoreService,
        IJWTParser jwtParser,
        IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _jwtParser = jwtParser;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.UsersColection);
    }
    public async Task<bool> Handle(UpdateUserModelRequest request, CancellationToken cancellationToken)
    {
        string uid = await _jwtParser.GetUserIdFromJWT();

        if(string.IsNullOrEmpty(uid) || request.UserData.DocumentId != uid)
            return false;

        return await _firestoreService.UpdateDocumentAsync(request.UserData, _collectionReference);
    }
}
