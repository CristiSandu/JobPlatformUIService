using Google.Cloud.Firestore;
using JobPlatformUIService.Core.Helpers;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

namespace JobPlatformUIService.Features.User.AddUser
{
    public class MakeUserAdminModelRequest : IRequest<bool>
    {
        public string UserID { get; set; }
        public string Role { get; set; }
    }

    public class MakeUserAdminModelHandler : IRequestHandler<MakeUserAdminModelRequest, bool>
    {
        private readonly IJWTParser _jwtParser;
        private readonly IFirestoreService<Core.DataModel.User> _firestoreService;
        private readonly CollectionReference _collectionReference;

        private readonly List<string> _posibleRoles = new() { "admin", "recruiter", "candidate" };

        public MakeUserAdminModelHandler(IJWTParser jwtParser,
            IFirestoreService<Core.DataModel.User> firestoreService,
            IFirestoreContext firestoreContext)
        {
            _jwtParser = jwtParser;
            _firestoreService = firestoreService;
            _collectionReference = firestoreContext.FirestoreDB.Collection(Constants.JobsColection);
        }

        public async Task<bool> Handle(MakeUserAdminModelRequest request, CancellationToken cancellationToken)
        {
            if (!await _jwtParser.VerifyUserRole(Constants.AdminRole))
                return false;

            if (!_posibleRoles.Contains(request.Role.ToLower()))
                return false;

            var isUpdated = request.Role.ToLower() == "admin" && await _firestoreService.UpdateDocumentFieldAsync("IsAdmin", request.UserID, true, _collectionReference);

            try
            {
                    await _jwtParser.AssignARoleToUser(request.UserID, request.Role.ToLower());
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
