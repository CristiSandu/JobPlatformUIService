using Google.Cloud.Firestore;
using JobPlatformUIService.Core.Domain.Jobs;
using JobPlatformUIService.Core.Helpers;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.User.DeleteUser;


public class DeleteUsersModelHandler : IRequestHandler<DeleteUsersModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.User> _firestoreService;
    private readonly IFirestoreService<CandidateJobsExtendedModel> _firestoreServiceCJ;
    private readonly IJWTParser _jwtParser;

    private readonly CollectionReference _collectionReference;
    private readonly CollectionReference _collectionReferenceCJ;

    public DeleteUsersModelHandler(IFirestoreService<Core.DataModel.User> firestoreService,
        IJWTParser jwtParser,
        IFirestoreService<CandidateJobsExtendedModel> firestoreServiceCJ,
        IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _jwtParser = jwtParser;
        _firestoreServiceCJ = firestoreServiceCJ;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.UsersColection);
        _collectionReferenceCJ = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.CandidateJobsColection);
    }

    public async Task<bool> Handle(DeleteUsersModelRequest request, CancellationToken cancellationToken)
    {
        if (!await _jwtParser.VerifyUserRole(Constants.AdminRole))
            return false;

        var deleteUserRespons = await _firestoreService.DeleteDocumentByIdAsync(request.UserID, _collectionReference);
        if (deleteUserRespons)
        {
            var getCandidatJob = await _firestoreServiceCJ.GetFilteredDocumentsByAField("CandidateID", request.UserID, _collectionReferenceCJ);
            await _firestoreServiceCJ.DeleteDocumentListAsync(getCandidatJob, _collectionReferenceCJ);
        }

        return deleteUserRespons ;
    }
}
