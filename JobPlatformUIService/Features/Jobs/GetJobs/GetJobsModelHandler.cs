using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using JobPlatformUIService.Core.Domain.Jobs;
using JobPlatformUIService.Core.Helpers;
using JobPlatformUIService.Features.Jobs.GetJobs.ModelRequests;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.GetJobs;

public class GetJobsModelHandler : IRequestHandler<GetJobsModelRequest, List<JobExtendedModel>>
{
    private readonly IFirestoreService<Job> _firestoreService;
    private readonly IFirestoreService<Core.DataModel.User> _firestoreServiceU;
    private readonly IFirestoreService<CandidateJobsExtendedModel> _firestoreServiceC;
    private readonly IJWTParser _jwtParser;


    private readonly CollectionReference _collectionReference;
    private readonly IFirestoreContext _firestoreContext;
    public GetJobsModelHandler(IFirestoreService<Job> firestoreService,
        IFirestoreService<Core.DataModel.User> firestoreServiceU,
        IJWTParser jwtParser,
        IFirestoreService<CandidateJobsExtendedModel> firestoreServiceC,
        IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _firestoreServiceC = firestoreServiceC;
        _firestoreServiceU = firestoreServiceU;
        _firestoreContext = firestoreContext;
        _jwtParser = jwtParser;

        _collectionReference = firestoreContext.FirestoreDB.Collection(Constants.JobsColection);
    }

    public async Task<List<JobExtendedModel>> Handle(GetJobsModelRequest request, CancellationToken cancellationToken)
    {
        bool isAdmin = await _jwtParser.VerifyUserRole(Constants.AdminRole);
        bool isRecruter = await _jwtParser.VerifyUserRole(Constants.RecruiterRole);
        string uid = await _jwtParser.GetUserIdFromJWT();

        var jobs = await _firestoreService.GetDocumentsInACollection(_collectionReference);
        var users = await _firestoreServiceU.GetDocumentsInACollection(_firestoreContext.FirestoreDB.Collection(Constants.UsersColection));

        List<CandidateJobsExtendedModel> candidateJobList = new();

        List<JobExtendedModel> result = new();

        if (!isRecruter && !isAdmin)
        {
            CollectionReference collectionReferenceC = _firestoreContext.FirestoreDB.Collection(Constants.CandidateJobsColection);
            candidateJobList = await _firestoreServiceC.GetFilteredDocumentsByAField("CandidateID", uid, collectionReferenceC);
        }

        jobs.ForEach(job =>
        {
            JobExtendedModel value = new JobExtendedModel
            {
                Address = job.Address,
                Domain = job.Domain,
                Name = job.Name,
                Date = job.Date,
                Description = job.Description,
                DocID = job.DocumentId,
                IsCheck = job.IsCheck,
                IsExpired = job.IsExpired,
                NumberApplicants = job.NumberApplicants,
                NumberEmp = job.NumberEmp,
                RecruterID = job.RecruterID,
                RecruterName = job.RecruterName,
            };

            value.IsMine = (isRecruter && !isAdmin) ? job.RecruterID == uid : null;

            if (!isRecruter && !isAdmin)
            {
                try
                {
                    var val = candidateJobList.Where(x => x.JobID == job.DocumentId).ToList();
                    value.IsApplied = candidateJobList.Any() && val.Count > 0;
                }
                catch (System.InvalidOperationException ex)
                {
                    value.IsApplied = false;
                }
            }
            else
            {
                value.IsApplied = null;
            }

            if (isAdmin || (!isAdmin && job.IsCheck))
            {
                result.Add(value);
            }

        });

        return result;
    }
}
