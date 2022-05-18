using Google.Cloud.Firestore;
using JobPlatformUIService.Core.Domain.Jobs;
using JobPlatformUIService.Features.Jobs.GetJobs.ModelRequests;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.GetJobs;

public class GetJobsModelHandler : IRequestHandler<GetJobsModelRequest, List<Core.Domain.Jobs.JobExtendedModel>>
{
    private readonly IFirestoreService<Core.DataModel.Job> _firestoreService;
    private readonly IFirestoreService<Core.DataModel.User> _firestoreServiceU;
    private readonly IFirestoreService<CandidateJobsExtendedModel> _firestoreServiceC;

    private readonly CollectionReference _collectionReference;
    private readonly IFirestoreContext _firestoreContext;
    public GetJobsModelHandler(IFirestoreService<Core.DataModel.Job> firestoreService,
        IFirestoreService<Core.DataModel.User> firestoreServiceU,
        IFirestoreService<CandidateJobsExtendedModel> firestoreServiceC,
        IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _firestoreServiceC = firestoreServiceC;
        _firestoreServiceU = firestoreServiceU;
        _firestoreContext = firestoreContext;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.JobsColection);
    }

    public async Task<List<Core.Domain.Jobs.JobExtendedModel>> Handle(GetJobsModelRequest request, CancellationToken cancellationToken)
    {
        var jobs = await _firestoreService.GetDocumentsInACollection(_collectionReference);
        var users = await _firestoreServiceU.GetDocumentsInACollection(_firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.UsersColection));

        List<CandidateJobsExtendedModel> candidateJobList = new();

        List<Core.Domain.Jobs.JobExtendedModel> result = new();

        if (!request.IsRecruter && !request.IsAdmin)
        {
            CollectionReference collectionReferenceC = _firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.CandidateJobsColection);
            candidateJobList = await _firestoreServiceC.GetFilteredDocumentsByAField("CandidateID", request.UserID, collectionReferenceC);

        }

        jobs.ForEach(job =>
        { 
            Core.Domain.Jobs.JobExtendedModel value = new Core.Domain.Jobs.JobExtendedModel
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
            };

            value.IsMine = job.RecruterID == request.UserID;
            value.RecruterName = users.FirstOrDefault(x => x.DocumentId == job.RecruterID)?.Name;

            if (!request.IsRecruter && !request.IsAdmin)
            {
                if (!candidateJobList.Any())
                {
                    value.IsApplied = false;
                }
                else
                {
                    try
                    {
                        var val = candidateJobList.First(x => x.JobID == job.DocumentId);
                        value.IsApplied = val != null;
                    }
                    catch (System.InvalidOperationException ex)
                    {
                        value.IsApplied = false;
                    }
                }
            }

            if (request.IsAdmin || (!request.IsAdmin && job.IsCheck))
            {
                result.Add(value);
            }

        });

        return result;
    }
}
