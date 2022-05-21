using JobPlatformUIService.Core.Helpers;
using JobPlatformUIService.Helper;
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
        private readonly List<string> _posibleRoles = new() { "admin", "recruiter", "candidate" };

        public MakeUserAdminModelHandler(IJWTParser jwtParser)
        {
            _jwtParser = jwtParser;
        }

        public async Task<bool> Handle(MakeUserAdminModelRequest request, CancellationToken cancellationToken)
        {
            if (!await _jwtParser.VerifyUserRole(Constants.AdminRole))
                return false;

            if (!_posibleRoles.Contains(request.Role.ToLower()))
                return false;
            
            try
            {
                await _jwtParser.AssignARoleToUser(request.UserID, request.Role);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
