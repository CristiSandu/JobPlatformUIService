using MediatR;

namespace JobPlatformUIService.Features.User.DeleteUser;

public class DeleteUsersModelRequest : IRequest<bool>
{
    public string UserID { get; set; }
    public string UserReqID { get; set; }


}
