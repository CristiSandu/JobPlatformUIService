using JobPlatformUIService.Core.Domain.Dropdown;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobPlatformUIService.Features.Dropdowns;

[Route("api/[controller]")]
[ApiController]
public class DropdownController : ControllerBase
{
    private readonly IMediator _mediator;

    public DropdownController(IMediator mediator)
    {
        _mediator = mediator;
    }
    // GET: api/<DropdownController>
    [HttpGet("Domains")]
    public async Task<List<Core.DataModel.DropdownsModels.DomainModel>> GetDropdownDomainsValue() => await _mediator.Send(new GetValues.GetValuesModelRequest());

    [HttpGet("DomainsAndNumbers/{type}")]
    public async Task<List<DomainModelExtended>> GetDropdownDomainsAndNumbersValue(bool type) => await _mediator.Send(new GetValues.GetValuesWithJobNumberModelRequest { isUser = type });

    // POST api/<DropdownController>
    [HttpPost("Domains")]
    public async Task<bool> AddNewDomains([FromBody] AddValues.AddValuesModelRequest request) => await _mediator.Send(request);

    // DELETE api/<DropdownController>/5
    [HttpDelete("Domains/{id}")]
    public async Task<bool> DeletUser(string id)
    {
        DeleteValues.DeleteValuesModelRequest deleteValue = new();
        deleteValue.DocumentId = id;
        return await _mediator.Send(deleteValue);
    }
}
