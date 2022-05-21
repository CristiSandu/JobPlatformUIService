using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using JobPlatformUIService.Core.Domain.Jobs;
using JobPlatformUIService.Core.Helpers;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.ApplyToJobs;

public class ApplyToJobsModelHandler : IRequestHandler<ApplyToJobsModelRequest, bool>
{
    private readonly IFirestoreService<CandidateJobs> _firestoreServiceC;
    private readonly IFirestoreService<CandidateJobsExtendedModel> _firestoreServiceCExt;
    private readonly IFirestoreService<RecruterJobs> _firestoreServiceR;
    private readonly IFirestoreService<Core.DataModel.User> _firestoreServiceU;
    private readonly IFirestoreService<Job> _firestoreServiceJob;

    private readonly IFirestoreContext _firestoreContext;
    private readonly IJWTParser _jwtParser;

    public ApplyToJobsModelHandler(IFirestoreService<CandidateJobs> firestoreServiceC,
        IFirestoreService<RecruterJobs> firestoreServiceR,
        IFirestoreService<Core.DataModel.User> firestoreServiceU,
        IFirestoreService<CandidateJobsExtendedModel> firestoreServiceCExt,
        IFirestoreService<Job> firestoreServiceJob,
        IJWTParser jwtParser,
        IFirestoreContext firestoreContext)
    {
        _firestoreServiceC = firestoreServiceC;
        _firestoreServiceR = firestoreServiceR;
        _firestoreServiceU = firestoreServiceU;
        _firestoreServiceCExt = firestoreServiceCExt;
        _firestoreServiceJob = firestoreServiceJob;
        _firestoreContext = firestoreContext;
        _jwtParser = jwtParser;

    }

    public async Task<bool> Handle(ApplyToJobsModelRequest request, CancellationToken cancellationToken)
    {
        if (!await _jwtParser.VerifyUserRole(Constants.CandidateRole))
            return false;

        string uid = await _jwtParser.GetUserIdFromJWT();

        CollectionReference collectionReferenceC = _firestoreContext.FirestoreDB.Collection(Constants.CandidateJobsColection);
        CollectionReference collectionReferenceCExt = _firestoreContext.FirestoreDB.Collection(Constants.CandidateJobsColection);

        CollectionReference collectionReferenceR = _firestoreContext.FirestoreDB.Collection(Constants.RecruterJobsColection);
        CollectionReference collectionReferenceU = _firestoreContext.FirestoreDB.Collection(Constants.UsersColection);
        
        CollectionReference collectionReferenceJob = _firestoreContext.FirestoreDB.Collection(Constants.JobsColection);

        var recruterJobList = await _firestoreServiceR.GetDocumentByIds(request.RecruterJobId, collectionReferenceR);
        var usersList = await _firestoreServiceU.GetDocumentByIds(uid, collectionReferenceU);
        var jobList = await _firestoreServiceJob.GetDocumentByIds(request.JobId, collectionReferenceJob);

        CandidateJobs candidate = new CandidateJobs
        {
            CandidateID = uid,
            JobID = request.JobId,
            Candidate = usersList[0],
            Status = 0,
            LastStatusDate = DateTime.UtcNow,
            ApplyDate = DateTime.UtcNow
        };

        recruterJobList[0].CandidateList.Add(candidate);
        recruterJobList[0].Job.NumberApplicants = jobList[0].NumberApplicants + 1;


        CandidateJobsExtendedModel candidateData = new CandidateJobsExtendedModel
        {
            CandidateID = uid,
            JobID = request.JobId,
            Status = 0,
            JobDetails = recruterJobList[0].Job,
            LastStatusDate = DateTime.UtcNow,
            ApplyDate = DateTime.UtcNow
        };


        var isCandidatInsertedC = await _firestoreServiceCExt.InsertDocumentAsync(candidateData, collectionReferenceCExt);
        var isListUpdated = await _firestoreServiceR.UpdateDocumentListAsync(recruterJobList, collectionReferenceR);


        if (!isCandidatInsertedC)
        {
            await _firestoreServiceC.DeleteDocumentByIdAsync(candidate.DocumentId, collectionReferenceC);
        }
        else
        {
            int increment = jobList[0].NumberApplicants + 1;
            await _firestoreServiceJob.UpdateDocumentFieldAsync("NumberApplicants", jobList[0].DocumentId, increment, collectionReferenceJob);
        }

        return isListUpdated > 0;
    }
}
