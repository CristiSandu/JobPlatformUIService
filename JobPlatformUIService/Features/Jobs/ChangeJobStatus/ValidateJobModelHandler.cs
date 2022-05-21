using Google.Cloud.Firestore;
using JobPlatformUIService.Core.Helpers;
using JobPlatformUIService.Features.Jobs.ChangeJobStatus.ModelRequests;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.ChangeJobStatus;

public class ValidateJobModelHandler : IRequestHandler<ValidateJobModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.Job> _firestoreService;
    private readonly IJWTParser _jwtParser;
    private readonly CollectionReference _collectionReference;

    public ValidateJobModelHandler(IFirestoreService<Core.DataModel.Job> firestoreService,
        IJWTParser jwtParser,
        IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _jwtParser = jwtParser;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.JobsColection);
    }

    public async Task<bool> Handle(ValidateJobModelRequest request, CancellationToken cancellationToken)
    {
        if (!await _jwtParser.VerifyUserRole(Constants.AdminRole))
            return false;

        return await _firestoreService.UpdateDocumentFieldAsync("IsCheck", request.JobId, request.IsCheck, _collectionReference);
    }
}
