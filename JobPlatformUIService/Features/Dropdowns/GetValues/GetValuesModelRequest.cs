using MediatR;

namespace JobPlatformUIService.Features.Dropdowns.GetValues;

public class GetValuesModelRequest : IRequest<List<Core.DataModel.DropdownsModels.DomainModel>>
{
}
