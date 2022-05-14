﻿using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Jobs.GetJobs;

public class GetJobsModelHandler : IRequestHandler<GetJobsModelRequest, List<Core.Domain.Jobs.JobExtendedModel>>
{
    private readonly IFirestoreService<Core.DataModel.Job> _firestoreService;
    private readonly CollectionReference _collectionReference;
    private readonly IFirestoreContext _firestoreContext;
    public GetJobsModelHandler(IFirestoreService<Core.DataModel.Job> firestoreService,
           IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _firestoreContext = firestoreContext;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.JobsColection);
    }

    public async Task<List<Core.Domain.Jobs.JobExtendedModel>> Handle(GetJobsModelRequest request, CancellationToken cancellationToken)
    {
        var jobs = await _firestoreService.GetDocumentsInACollection(_collectionReference);
        var users = await _firestoreService.GetDocumentsInACollection(_firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.UsersColection));

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
                DocumentId = job.DocumentId,
                IsCheck = job.IsCheck,
                IsExpired = job.IsExpired,
                NumberEmp = job.NumberEmp,
                RecruterID = job.RecruterID,
            };

            value.IsMine = job.RecruterID == request.UserID;
            value.RecruterName = users.FirstOrDefault(x => x.DocumentId == job.RecruterID)?.Name ;
            result.Add(value);
        });

        return result;
    }
}