using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.AddJob;

public class AddJobsModelHandler : IRequestHandler<AddJobsModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.Job> _firestoreServiceJ;
    private readonly IFirestoreService<Core.DataModel.RecruterJobs> _firestoreServiceR;
    private readonly IFirestoreContext _firestoreContext;

    private readonly CollectionReference _collectionReference;
    public AddJobsModelHandler(IFirestoreService<Core.DataModel.Job> firestoreServiceJ,
        IFirestoreService<Core.DataModel.RecruterJobs> firestoreServiceR,
           IFirestoreContext firestoreContext)
    {
        _firestoreServiceJ = firestoreServiceJ;
        _firestoreServiceR = firestoreServiceR;
        _firestoreContext = firestoreContext;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.JobsColection);
    }

    public async Task<bool> Handle(AddJobsModelRequest request, CancellationToken cancellationToken)
    {
        CollectionReference collectionReferenceR = _firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.RecruterJobsColection);
        Core.DataModel.RecruterJobs recruterJobs = new Core.DataModel.RecruterJobs
        {
            JobId = request.JobData.DocumentId,
            AngajatorID = request.JobData.RecruterID,
            Job = request.JobData,
            CandidateList = new List<Core.DataModel.CandidateJobs>()
        };

        request.JobData.DocumentId = Guid.NewGuid().ToString("N");

        var isJobInsertedJ = await _firestoreServiceJ.InsertDocumentAsync(request.JobData, _collectionReference);
        var isJobInsertedR = isJobInsertedJ && await _firestoreServiceR.InsertDocumentAsync(recruterJobs, collectionReferenceR);

        if (!isJobInsertedR)
        {
            await _firestoreServiceJ.DeleteDocumentByIdAsync(request.JobData.DocumentId, _collectionReference);
        }

        return isJobInsertedR;
    }
}
