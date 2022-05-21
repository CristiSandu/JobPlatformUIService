using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using JobPlatformUIService.Core.Domain.Jobs;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.ApplyToJobs;

public class ApplyToJobsModelHandler : IRequestHandler<ApplyToJobsModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.CandidateJobs> _firestoreServiceC;
    private readonly IFirestoreService<Core.Domain.Jobs.CandidateJobsExtendedModel> _firestoreServiceCExt;

    private readonly IFirestoreService<Core.DataModel.RecruterJobs> _firestoreServiceR;
    private readonly IFirestoreService<Core.DataModel.User> _firestoreServiceU;
    private readonly IFirestoreService<Core.DataModel.Job> _firestoreServiceJob;


    private readonly IFirestoreContext _firestoreContext;

    public ApplyToJobsModelHandler(IFirestoreService<Core.DataModel.CandidateJobs> firestoreServiceC,
        IFirestoreService<Core.DataModel.RecruterJobs> firestoreServiceR,
        IFirestoreService<Core.DataModel.User> firestoreServiceU,
        IFirestoreService<Core.Domain.Jobs.CandidateJobsExtendedModel> firestoreServiceCExt,
        IFirestoreService<Core.DataModel.Job> firestoreServiceJob,
        IFirestoreContext firestoreContext)
    {
        _firestoreServiceC = firestoreServiceC;
        _firestoreServiceR = firestoreServiceR;
        _firestoreServiceU = firestoreServiceU;
        _firestoreServiceCExt = firestoreServiceCExt;
        _firestoreServiceJob = firestoreServiceJob;
        _firestoreContext = firestoreContext;
    }

    public async Task<bool> Handle(ApplyToJobsModelRequest request, CancellationToken cancellationToken)
    {
        CollectionReference collectionReferenceC = _firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.CandidateJobsColection);
        CollectionReference collectionReferenceCExt = _firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.CandidateJobsColection);

        CollectionReference collectionReferenceR = _firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.RecruterJobsColection);
        CollectionReference collectionReferenceU = _firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.UsersColection);

        CollectionReference collectionReferenceJob = _firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.JobsColection);



        var recruterJobList = await _firestoreServiceR.GetDocumentByIds(request.RecruterJobId, collectionReferenceR);
        var usersList = await _firestoreServiceU.GetDocumentByIds(request.CandidateId, collectionReferenceU);

        var jobList = await _firestoreServiceJob.GetDocumentByIds(request.JobId, collectionReferenceJob);


        CandidateJobs candidate = new CandidateJobs
        {
            CandidateID = request.CandidateId,
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
            CandidateID = request.CandidateId,
            JobID = request.JobId,
            Status = 0,
            JobDetails = recruterJobList[0].Job,
            LastStatusDate = DateTime.UtcNow,
            ApplyDate = DateTime.UtcNow
        };

        candidateData.JobDetails.NumberApplicants = jobList[0].NumberApplicants + 1;


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
