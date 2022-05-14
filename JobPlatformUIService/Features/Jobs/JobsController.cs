using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobPlatformUIService.Features.Jobs
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {

        private readonly IMediator _mediator;
        public JobsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<JobsController>
        [HttpPost("GetJobs")]
        public async Task<List<Core.Domain.Jobs.JobExtendedModel>> GetJobs([FromBody] GetJobs.GetJobsModelRequest value) => await _mediator.Send(value);

        // POST api/<JobsController>
        [HttpPost]
        public async Task<bool> AddJob([FromBody] AddJob.AddJobsModelRequest jobData) => await _mediator.Send(jobData);

        [HttpPost("ValidateJob")]
        public async Task<bool> ValidateOffert([FromBody] ChangeJobStatus.ValidateJobModelRequest validateJob) => await _mediator.Send(validateJob);
        
        [HttpPost("ExpireJob")]
        public async Task<bool> MakeExpiredJob([FromBody] ChangeJobStatus.ExpirationModelRequest expireJob) => await _mediator.Send(expireJob);

        // PUT api/<JobsController>/5
        [HttpPut("{id}")]
        public async Task<bool> UpdateUser(string id, [FromBody] UpdateJob.UpdateJobsModelRequest jobData)
        {
            jobData.JobId = id;
            return await _mediator.Send(jobData);
        }
    }
}
