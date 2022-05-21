using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using JobPlatformUIService.Core.Domain.Jobs;
using JobPlatformUIService.Core.Helpers;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using JobPlatformUIService.Web;
using MediatR;
using System.Net;

namespace JobPlatformUIService.Features.Jobs.UpdateJob;

public class UpdateJobsModelHandler : IRequestHandler<UpdateJobsModelRequest, bool>
{
    private readonly IFirestoreService<Core.DataModel.Job> _firestoreService;
    private readonly IFirestoreService<RecruterJobs> _firestoreServiceRJ;
    private readonly IFirestoreService<CandidateJobsExtendedModel> _firestoreServiceCJ;
    private readonly IJWTParser _jwtParser;

    private readonly CollectionReference _collectionReference;
    private readonly CollectionReference _collectionReferenceRJ;
    private readonly CollectionReference _collectionReferenceCJ;

    public UpdateJobsModelHandler(IFirestoreService<Job> firestoreService,
        IFirestoreService<RecruterJobs> firestoreServiceRJ,
        IFirestoreService<CandidateJobsExtendedModel> firestoreServiceCJ,
        IJWTParser jwtParser,
        IFirestoreContext firestoreContext)
    {
        _jwtParser = jwtParser;
        _firestoreService = firestoreService;
        _firestoreServiceRJ = firestoreServiceRJ;
        _firestoreServiceCJ = firestoreServiceCJ;

        _collectionReference = firestoreContext.FirestoreDB.Collection(Constants.JobsColection);
        _collectionReferenceRJ = firestoreContext.FirestoreDB.Collection(Constants.RecruterJobsColection);
        _collectionReferenceCJ = firestoreContext.FirestoreDB.Collection(Constants.CandidateJobsColection);
    }

    public async Task<bool> Handle(UpdateJobsModelRequest request, CancellationToken cancellationToken)
    {
        if (!await _jwtParser.VerifyUserRole(Constants.RecruiterRole))
            throw new ApiException(HttpStatusCode.Unauthorized, $"This is not a Recruiter");

        string uid = await _jwtParser.GetUserIdFromJWT();

        var isJobUpdated = await _firestoreService.UpdateDocumentAsync(request.JobData, _collectionReference);

        if (!isJobUpdated)
            throw new ApiException(HttpStatusCode.InternalServerError, $"Error on user update");

        var isRecruterJobUpdated = await _firestoreServiceRJ.UpdateDocumentFieldAsync("Job", $"{uid}-{request.JobData.DocumentId}", request.JobData, _collectionReferenceRJ);
        var userJobsList = await _firestoreServiceCJ.GetFilteredDocumentsByAField("JobId", request.JobData.DocumentId, _collectionReferenceCJ);
        
        userJobsList.ForEach(aplication => aplication.JobDetails = request.JobData);

        var udatedDocs = await _firestoreServiceCJ.UpdateDocumentListAsync(userJobsList, _collectionReferenceCJ);

        return true;
    }
}
