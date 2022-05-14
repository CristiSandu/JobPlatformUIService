using MediatR;

namespace JobPlatformUIService.Features.User.GetUsers;

public class GetUsersModelRequest : IRequest<List<Core.DataModel.User>>
{
    public string? UserId { get; set; }
}
