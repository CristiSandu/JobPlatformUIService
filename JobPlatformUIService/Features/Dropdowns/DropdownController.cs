using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobPlatformUIService.Features.Dropdowns
{
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

        // POST api/<DropdownController>
        [HttpPost("Domains")]
        public async Task<bool> AddNewDomains([FromBody] AddValues.AddValuesModelRequest request) => await _mediator.Send(request);


        [HttpDelete("Domains/{id}")]
        public async Task<bool> DeletUser(string id)
        {
            DeleteValues.DeleteValuesModelRequest deleteValue = new();
            deleteValue.DocumentId = id;
            return await _mediator.Send(deleteValue);
        }
        // GET api/<DropdownController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // PUT api/<DropdownController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DropdownController>/5

    }
}
