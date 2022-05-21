using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using JobPlatformUIService.Core.Helpers;
using JobPlatformUIService.Features.Jobs.ChangeJobStatus.ModelRequests;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.ChangeJobStatus;

public class ChangeJobStatusModelHandler : IRequestHandler<ChangeJobStatusModelRequest, bool>
{
    private readonly IFirestoreService<CandidateJobs> _firestoreServiceC;
    private readonly IFirestoreService<RecruterJobs> _firestoreServiceR;
    private readonly IJWTParser _jwtParser;

    private readonly IFirestoreContext _firestoreContext;

    public ChangeJobStatusModelHandler(IFirestoreService<CandidateJobs> firestoreServiceC,
        IJWTParser jwtParser,

        IFirestoreService<RecruterJobs> firestoreServiceR,
        IFirestoreContext firestoreContext)
    {
        _jwtParser = jwtParser;
        _firestoreServiceC = firestoreServiceC;
        _firestoreServiceR = firestoreServiceR;
        _firestoreContext = firestoreContext;
    }

    public async Task<bool> Handle(ChangeJobStatusModelRequest request, CancellationToken cancellationToken)
    {
        if (!await _jwtParser.VerifyUserRole(Constants.RecruiterRole))
            return false;
        
        CollectionReference collectionReferenceC = _firestoreContext.FirestoreDB.Collection(Constants.CandidateJobsColection);
        CollectionReference collectionReferenceR = _firestoreContext.FirestoreDB.Collection(Constants.RecruterJobsColection);

        var candidateJobList = await _firestoreServiceC.GetDocumentByIds(request.CandidateJobId, collectionReferenceC);
        var recruterJobList = await _firestoreServiceR.GetDocumentByIds(request.RecruterJobId, collectionReferenceR);

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
