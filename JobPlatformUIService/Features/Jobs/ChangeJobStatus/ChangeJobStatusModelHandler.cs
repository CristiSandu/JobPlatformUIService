using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using JobPlatformUIService.Core.Helpers;
using JobPlatformUIService.Features.Jobs.ChangeJobStatus.ModelRequests;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;
using System.Text.Json;

namespace JobPlatformUIService.Features.Jobs.ChangeJobStatus;

public class ChangeJobStatusModelHandler : IRequestHandler<ChangeJobStatusModelRequest, bool>
{
    private readonly IFirestoreService<CandidateJobs> _firestoreServiceC;
    private readonly IFirestoreService<RecruterJobs> _firestoreServiceR;
    private readonly IRabbitMQService _rabbitMQService;
    private readonly IJWTParser _jwtParser;


    private readonly IFirestoreContext _firestoreContext;

    public ChangeJobStatusModelHandler(IFirestoreService<CandidateJobs> firestoreServiceC,
        IJWTParser jwtParser,
        IRabbitMQService rabbitMQService,
        IFirestoreService<RecruterJobs> firestoreServiceR,
        IFirestoreContext firestoreContext)
    {
        _jwtParser = jwtParser;
        _rabbitMQService = rabbitMQService;
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

        var recruterJobList = await _firestoreServiceR.GetDocumentByIds(request.RecruterJobId, collectionReferenceR);

        var isCandidateJobsStatusOk = await _firestoreServiceC.UpdateDocumentFieldAsync("Status", request.CandidateJobId, request.JobStatus, collectionReferenceC);

        var index = recruterJobList[0].CandidateList.FindIndex(x => x.CandidateID == request.CandidateId);
        string stausMsg = request.JobStatus == 1 ? "Accespted" : "Rejected";
       

        if (index == -1)
        {
            return false;
        }

        recruterJobList[0].CandidateList[index].Status = request.JobStatus;
        var isRecruiterJobsStatusOk = isCandidateJobsStatusOk && await _firestoreServiceR.UpdateDocumentFieldAsync("CandidateList", request.RecruterJobId, recruterJobList[0].CandidateList, collectionReferenceR);

        if (isRecruiterJobsStatusOk)
        {
            EmailData email = new()
            {
                EmailAddress = recruterJobList[0].CandidateList[index].Candidate.Email,
                EmailBody = $"Your job application to {recruterJobList[0].Job.Name} status change to {stausMsg}",
                Subject = $"Status Change to {recruterJobList[0].Job.Name}",
                Username = recruterJobList[0].CandidateList[index].Candidate.Name
            };

            string jsonString = JsonSerializer.Serialize<EmailData>(email);
            _rabbitMQService.SendMesage(jsonString);
        }

        return isRecruiterJobsStatusOk;
    }
}
