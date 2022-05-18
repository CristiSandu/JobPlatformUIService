using MediatR;
using Microsoft.AspNetCore.Mvc;
using JobPlatformUIService.Core.Domain.Jobs;
using JobPlatformUIService.Core.DataModel;

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
        public async Task<List<Core.Domain.Jobs.JobExtendedModel>> GetJobs([FromBody] GetJobs.ModelRequests.GetJobsModelRequest value) => await _mediator.Send(value);

        // GET: api/<JobsController>
        [HttpPost("GetCandidateJobs")]
        public async Task<List<CandidateJobsExtendedModel>> GetCandidateJobs([FromBody] GetJobs.ModelRequests.GetCandidateJobsModelRequest value) => await _mediator.Send(value);

        // GET: api/<JobsController>
        [HttpPost("GetRecruiterJobs")]
        public async Task<List<RecruterJobs>> GetRecruiterJobs([FromBody] GetJobs.ModelRequests.GetRecruiterJobsModelRequest value) => await _mediator.Send(value);

        // POST api/<JobsController>
        [HttpPost]
        public async Task<bool> AddJob([FromBody] AddJob.AddJobsModelRequest jobData) => await _mediator.Send(jobData);

        // POST api/<JobsController>
        [HttpPost("ApplyToAJob")]
        public async Task<bool> ApplyToAJob([FromBody] ApplyToJobs.ApplyToJobsModelRequest applyRequest) => await _mediator.Send(applyRequest);

        [HttpPost("ValidateJob")]
        public async Task<bool> ValidateOffert([FromBody] ChangeJobStatus.ModelRequests.ValidateJobModelRequest validateJob) => await _mediator.Send(validateJob);
        
        [HttpPost("ExpireJob")]
        public async Task<bool> MakeExpiredJob([FromBody] ChangeJobStatus.ModelRequests.ExpirationModelRequest expireJob) => await _mediator.Send(expireJob);

        // PUT api/<JobsController>/5
        [HttpPut("{id}")]
        public async Task<bool> UpdateJob(string id, [FromBody] UpdateJob.UpdateJobsModelRequest jobData)
        {
            jobData.JobId = id;
            return await _mediator.Send(jobData);
        }

        // POST api/<JobsController>/5
        [HttpPost("JobStatus")]
        public async Task<bool> ChangeJobStatus([FromBody] ChangeJobStatus.ModelRequests.ChangeJobStatusModelRequest jobData) => await _mediator.Send(jobData);

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<bool> DeletJob(string id)
        {
            DeleteJob.DeleteJobsModelRequest deleteJobs = new();
            deleteJobs.JobId = id;
            return (bool)await _mediator.Send(deleteJobs);
        }
    }
}
