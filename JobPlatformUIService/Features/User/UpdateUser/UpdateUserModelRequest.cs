using MediatR;

namespace JobPlatformUIService.Features.User.UpdateUser;

public class UpdateUserModelRequest : IRequest<bool>
{
    public Core.DataModel.User UserData { get; set; }
    public string UserID { get; set; }
}
