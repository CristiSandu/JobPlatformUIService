using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using JobPlatformUIService.Core.Domain.Jobs;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.DeleteJob;

public class DeleteJobsModelHandler : IRequestHandler<DeleteJobsModelRequest, bool>
{
    private readonly IFirestoreService<Job> _firestoreService;
    private readonly IFirestoreService<RecruterJobs> _firestoreServicRJ;
    private readonly IFirestoreService<CandidateJobsExtendedModel> _firestoreServicCJ;
    private readonly IFirestoreContext _firestoreContext;
    private readonly CollectionReference _collectionReference;

    public DeleteJobsModelHandler(IFirestoreService<Job> firestoreService,
        IFirestoreService<RecruterJobs> firestoreServicRJ,
        IFirestoreService<CandidateJobsExtendedModel> firestoreServicCJ,
        IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _firestoreServicRJ = firestoreServicRJ;
        _firestoreServicCJ = firestoreServicCJ;
        _firestoreContext = firestoreContext;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.JobsColection);
    }

    public async Task<bool> Handle(DeleteJobsModelRequest request, CancellationToken cancellationToken)
    {
        CollectionReference collectionReferenceCJ = _firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.CandidateJobsColection);
        CollectionReference collectionReferenceRJ = _firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.RecruterJobsColection);

        var getDocsForRJ = await _firestoreServicRJ.GetFilteredDocumentsByAField("JobId", request.JobId, collectionReferenceRJ);
        var getDocsForCJ = await _firestoreServicCJ.GetFilteredDocumentsByAField("JobID", request.JobId, collectionReferenceCJ);

        var responsdeleteJob = await _firestoreService.DeleteDocumentByIdAsync(request.JobId, _collectionReference);
        var deletRJRespons = getDocsForRJ.Count >= 1 && await _firestoreServicRJ.DeleteDocumentListAsync(getDocsForRJ, collectionReferenceRJ) > 0;
        var deletCJRespons = getDocsForCJ.Count >= 1 && await _firestoreServicCJ.DeleteDocumentListAsync(getDocsForCJ, collectionReferenceCJ) > 0;

        return responsdeleteJob && (deletRJRespons || deletCJRespons);
    }
}
