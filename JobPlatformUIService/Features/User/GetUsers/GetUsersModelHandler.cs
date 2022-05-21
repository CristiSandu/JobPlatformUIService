using Google.Cloud.Firestore;
using JobPlatformUIService.Core.Helpers;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.User.GetUsers;

public class GetUsersModelHandler : IRequestHandler<GetUsersModelRequest, List<Core.DataModel.User>>
{
    private readonly IFirestoreService<Core.DataModel.User> _firestoreService;
    private readonly CollectionReference _collectionReference;
    private readonly IJWTParser _jwtParser;

    public GetUsersModelHandler(IFirestoreService<Core.DataModel.User> firestoreService,
        IJWTParser jwtParser,
        IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _jwtParser = jwtParser;

        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.UsersColection);
    }

    public async Task<List<Core.DataModel.User>> Handle(GetUsersModelRequest request, CancellationToken cancellationToken)
    {
        if (request.UserId == "All" && await _jwtParser.VerifyUserRole(Constants.AdminRole))
            return await _firestoreService.GetDocumentsInACollection(_collectionReference);

        return await _firestoreService.GetDocumentByIds(request.UserId, _collectionReference);
    }
}
