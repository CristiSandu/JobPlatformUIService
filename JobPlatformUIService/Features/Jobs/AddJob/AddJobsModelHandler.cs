using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using JobPlatformUIService.Core.Helpers;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.AddJob;

public class AddJobsModelHandler : IRequestHandler<AddJobsModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.Job> _firestoreServiceJ;
    private readonly IFirestoreService<Core.DataModel.RecruterJobs> _firestoreServiceR;
    private readonly IFirestoreContext _firestoreContext;
    private readonly IJWTParser _jwtParser;

    private readonly CollectionReference _collectionReference;
    private readonly CollectionReference _collectionReferenceR;

    public AddJobsModelHandler(IFirestoreService<Job> firestoreServiceJ,
        IFirestoreService<RecruterJobs> firestoreServiceR,
        IJWTParser jwtParser,
        IFirestoreContext firestoreContext)
    {
        _firestoreServiceJ = firestoreServiceJ;
        _firestoreServiceR = firestoreServiceR;

        _firestoreContext = firestoreContext;
        _jwtParser = jwtParser;

        _collectionReference = firestoreContext.FirestoreDB.Collection(Constants.JobsColection);
        _collectionReferenceR = firestoreContext.FirestoreDB.Collection(Constants.RecruterJobsColection);
    }

    public async Task<bool> Handle(AddJobsModelRequest request, CancellationToken cancellationToken)
    {
        if (await _jwtParser.VerifyUserRole(Constants.CandidateRole))
            return false;

        request.JobData.DocumentId = Guid.NewGuid().ToString("N");
        RecruterJobs recruterJobs = new()
        {
            JobId = request.JobData.DocumentId,
            AngajatorID = request.JobData.RecruterID,
            Job = request.JobData,
            CandidateList = new List<CandidateJobs>()
        };

        var isJobInsertedJ = await _firestoreServiceJ.InsertDocumentAsync(request.JobData, _collectionReference);
        var isJobInsertedR = isJobInsertedJ && await _firestoreServiceR.InsertDocumentAsync(recruterJobs, _collectionReferenceR);

        if (!isJobInsertedR)
        {
            await _firestoreServiceJ.DeleteDocumentByIdAsync(request.JobData.DocumentId, _collectionReference);
        }

        return isJobInsertedR;
    }
}
