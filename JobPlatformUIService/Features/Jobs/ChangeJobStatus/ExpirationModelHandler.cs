using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using JobPlatformUIService.Core.Domain.Jobs;
using JobPlatformUIService.Features.Jobs.ChangeJobStatus.ModelRequests;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.ChangeJobStatus;

public class ExpirationModelHandler : IRequestHandler<ExpirationModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.Job> _firestoreService;
    private readonly IFirestoreService<RecruterJobs> _firestoreServicRJ;
    private readonly IFirestoreContext _firestoreContext;

    private readonly CollectionReference _collectionReference;
    public ExpirationModelHandler(IFirestoreService<Core.DataModel.Job> firestoreService,
        IFirestoreService<RecruterJobs> firestoreServicRJ,
        IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _firestoreServicRJ = firestoreServicRJ;
        _firestoreContext = firestoreContext;

        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.JobsColection);
    }

    public async Task<bool> Handle(ExpirationModelRequest request, CancellationToken cancellationToken)
    {
        CollectionReference collectionReferenceRJ = _firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.RecruterJobsColection);

        var updateInJobColection = await _firestoreService.UpdateDocumentFieldAsync("IsExpired", request.JobId, request.IsExpired, _collectionReference);
        var updateInJobColectionJob = await _firestoreServicRJ.GetFilteredDocumentsByAField("JobId", request.JobId, collectionReferenceRJ);

        if (updateInJobColectionJob != null && updateInJobColectionJob.Count == 1)
        {
            updateInJobColectionJob[0].Job.IsExpired = request.IsExpired;
            var responsRecruterJobDoc = await _firestoreServicRJ.UpdateDocumentFieldAsync("Job", updateInJobColectionJob[0].DocumentId, updateInJobColectionJob[0].Job, collectionReferenceRJ);
            return updateInJobColection && responsRecruterJobDoc;
        }

        return updateInJobColection;
    }
}
