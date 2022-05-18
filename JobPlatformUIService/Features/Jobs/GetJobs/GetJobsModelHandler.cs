using Google.Cloud.Firestore;
using JobPlatformUIService.Features.Jobs.GetJobs.ModelRequests;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.GetJobs;

public class GetJobsModelHandler : IRequestHandler<GetJobsModelRequest, List<Core.Domain.Jobs.JobExtendedModel>>
{
    private readonly IFirestoreService<Core.DataModel.Job> _firestoreService;
    private readonly IFirestoreService<Core.DataModel.User> _firestoreServiceU;

    private readonly CollectionReference _collectionReference;
    private readonly IFirestoreContext _firestoreContext;
    public GetJobsModelHandler(IFirestoreService<Core.DataModel.Job> firestoreService,
        IFirestoreService<Core.DataModel.User> firestoreServiceU,
           IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _firestoreServiceU = firestoreServiceU;
        _firestoreContext = firestoreContext;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.JobsColection);
    }

    public async Task<List<Core.Domain.Jobs.JobExtendedModel>> Handle(GetJobsModelRequest request, CancellationToken cancellationToken)
    {
        var jobs = await _firestoreService.GetDocumentsInACollection(_collectionReference);
        var users = await _firestoreServiceU.GetDocumentsInACollection(_firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.UsersColection));

        List<Core.Domain.Jobs.JobExtendedModel> result = new();

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

            if (request.IsAdmin || (!request.IsAdmin && job.IsCheck))
            {
                result.Add(value);
            }

        });

        return result;
    }
}
