using MediatR;

namespace JobPlatformUIService.Features.Jobs.ChangeJobStatus;

public class ExpirationModelRequest : IRequest<bool>
{
    public bool IsExpired { get; set; }
    public string JobId { get; set; }

}
