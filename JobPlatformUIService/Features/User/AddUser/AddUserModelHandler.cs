using Google.Cloud.Firestore;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.User.AddUser;

public class AddUserModelHandler : IRequestHandler<AddUserModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.User> _firestoreService;
    private readonly IJWTParser _jwtParser;

    private readonly CollectionReference _collectionReference;
    public AddUserModelHandler(IFirestoreService<Core.DataModel.User> firestoreService,
        IJWTParser jwtParser,
        IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _jwtParser = jwtParser;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.UsersColection);
    }
    public async Task<bool> Handle(AddUserModelRequest request, CancellationToken cancellationToken)
    {
        string? uid = await _jwtParser.GetUserIdFromJWT();
        if (string.IsNullOrEmpty(uid))
            return false;

        await _jwtParser.AssignARoleToUser(uid, request.UserData.Type.ToLower());
        return await _firestoreService.InsertDocumentAsync(request.UserData, _collectionReference);
    }
}
