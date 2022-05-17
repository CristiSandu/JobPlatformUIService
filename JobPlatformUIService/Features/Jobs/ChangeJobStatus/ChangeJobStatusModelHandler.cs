using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.ChangeJobStatus;

public class ChangeJobStatusModelHandler : IRequestHandler<ChangeJobStatusModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.CandidateJobs> _firestoreServiceC;
    private readonly IFirestoreService<Core.DataModel.RecruterJobs> _firestoreServiceR;

    private readonly IFirestoreContext _firestoreContext;

    public ChangeJobStatusModelHandler(IFirestoreService<Core.DataModel.CandidateJobs> firestoreServiceC,
        IFirestoreService<Core.DataModel.RecruterJobs> firestoreServiceR,
        IFirestoreContext firestoreContext)
    {
        _firestoreServiceC = firestoreServiceC;
        _firestoreServiceR = firestoreServiceR;
        _firestoreContext = firestoreContext;
    }

    public async Task<bool> Handle(ChangeJobStatusModelRequest request, CancellationToken cancellationToken)
    {
        CollectionReference collectionReferenceC = _firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.CandidateJobsColection);
        CollectionReference collectionReferenceR = _firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.RecruterJobsColection);

        var candidateJobList = await _firestoreServiceC.GetDocumentByIds(request.CandidateJobId, collectionReferenceC);
        var recruterJobList = await _firestoreServiceR.GetDocumentByIds(request.RecruterJobId, collectionReferenceR);

        if (request.JobStatus != 0 || request.JobStatus != 1 || request.JobStatus != 2)
        {
            return false;
        }

        var isCandidateJobsStatusOk = await _firestoreServiceC.UpdateDocumentFieldAsync("Status", request.CandidateJobId, request.JobStatus, collectionReferenceC);

        var index = recruterJobList[0].CandidateList.FindIndex(x => x.CandidateID == request.CandidateId);
        if (index == -1)
        {
            return false;
        }

        recruterJobList[0].CandidateList[index].Status = request.JobStatus;
        var isRecruiterJobsStatusOk = isCandidateJobsStatusOk && await _firestoreServiceR.UpdateDocumentFieldAsync("CandidateList", request.RecruterJobId, recruterJobList[0].CandidateList, collectionReferenceR);


        return isRecruiterJobsStatusOk;
    }
}
