using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel.DropdownsModels;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace JobPlatformUIService.Features.Dropdowns.GetValues;

public class GetValuesModelHandler : IRequestHandler<GetValuesModelRequest, List<DomainModel>>
{
    private readonly IFirestoreService<DomainModel> _firestoreService;
    private readonly IJWTParser _jwtParser;

    private readonly CollectionReference _collectionReference;
    public GetValuesModelHandler(IFirestoreService<DomainModel> firestoreService,
        IJWTParser jwtParser,
        IFirestoreContext firestoreContext)
    {
        _firestoreService = firestoreService;
        _jwtParser = jwtParser;
        _collectionReference = firestoreContext.FirestoreDB.Collection(Core.Helpers.Constats.DomainsColection);
    }

    public async Task<List<DomainModel>> Handle(GetValuesModelRequest request, CancellationToken cancellationToken)
    {
        string userId = await _jwtParser.GetUserIdFromJWT();
        return await _firestoreService.GetDocumentsInACollection(_collectionReference);
    }
}
