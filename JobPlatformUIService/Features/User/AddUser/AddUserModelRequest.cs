using MediatR;

namespace JobPlatformUIService.Features.User.AddUser;

public class AddUserModelRequest : IRequest<bool>
{
    public Core.DataModel.User UserData { get; set; }
}
