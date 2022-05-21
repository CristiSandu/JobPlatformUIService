using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using JobPlatformUIService.Core.DataModel.DropdownsModels;
using JobPlatformUIService.Core.Domain.Dropdown;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.Dropdowns.GetValues;

public class GetValuesWithJobNumberModelRequest : IRequest<List<DomainModelExtended>>
{
    public bool isUser { get; set; }
}
public class GetValuesWithJobNumberModelHandler : IRequestHandler<GetValuesWithJobNumberModelRequest, List<DomainModelExtended>>
{
    private readonly IFirestoreService<DomainModel> _firestoreServiceD;
    private readonly IFirestoreService<Job> _firestoreServiceJ;
    private readonly IFirestoreService<Core.DataModel.User> _firestoreServiceU;

    private readonly CollectionReference _collectionReferenceD;
    private readonly CollectionReference _collectionReferenceJ;
    private readonly CollectionReference _collectionReferenceU;


    public GetValuesWithJobNumberModelHandler(IFirestoreService<DomainModel> firestoreServiceD,
        IFirestoreService<Job> firestoreServiceJ,
        IFirestoreService<Core.DataModel.User> firestoreServiceU,
        IFirestoreContext firestoreContext)
    {
        _firestoreServiceD = firestoreServiceD;
        _firestoreServiceJ = firestoreServiceJ;
        _firestoreServiceU = firestoreServiceU;

        _collectionReferenceD = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.DomainsColection);
        _collectionReferenceJ = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.JobsColection);
        _collectionReferenceU = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constants.UsersColection);
    }

    public async Task<List<DomainModelExtended>> Handle(GetValuesWithJobNumberModelRequest request, CancellationToken cancellationToken)
    {
        var domainsList = await _firestoreServiceD.GetDocumentsInACollection(_collectionReferenceD);
        List<DomainModelExtended> domainModelExtendeds = new List<DomainModelExtended>();

        if (!request.isUser)
        {
            var jobsList = await _firestoreServiceJ.GetDocumentsInACollection(_collectionReferenceJ);
            domainModelExtendeds = jobsList.GroupBy(p => p.Domain).Select(p => new DomainModelExtended { Count = p.ToList().Count, Name = p.Key }).ToList();
        }
        else
        {
            var usersList = await _firestoreServiceU.GetDocumentsInACollection(_collectionReferenceU);
            domainModelExtendeds = usersList.GroupBy(p => p.Domain).Select(p => new DomainModelExtended { Count = p.ToList().Count, Name = p.Key }).ToList();
        }


        domainsList.ForEach(domain =>
        {
            if (!domainModelExtendeds.Where(x => x.Name == domain.Name).Any())
            {
                domainModelExtendeds.Add(new DomainModelExtended { Count = 0, Name = domain.Name });
            }
        });

        return domainModelExtendeds;
    }
}
