using MediatR;

namespace JobPlatformUIService.Features.Dropdowns.AddValues;

public class AddValuesModelRequest : IRequest<bool>
{
    public int Type { get; set; }
    public string Value { get; set; }
}
