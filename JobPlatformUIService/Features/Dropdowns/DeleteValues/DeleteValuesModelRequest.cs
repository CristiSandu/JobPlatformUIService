using MediatR;

namespace JobPlatformUIService.Features.Dropdowns.DeleteValues;

public class DeleteValuesModelRequest : IRequest<bool>
{
    public string DocumentId { get; set; }
}
