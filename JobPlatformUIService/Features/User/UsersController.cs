using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobPlatformUIService.Features.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<UsersController>  
        [HttpGet("{userId}")]

        public async Task<List<Core.DataModel.User>> GetUsers([FromRoute] string? userId = null) => await _mediator.Send(new GetUsers.GetUsersModelRequest { UserId = userId });

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<bool>> AddNewUser([FromBody] AddUser.AddUserModelRequest userData)
        {
            var respons = await _mediator.Send(userData);
            return !respons ? BadRequest(respons) : Ok(respons);
        }

        [HttpPost("MakeUserAdmin")]
        public async Task<ActionResult<bool>> MakeUserAdmin([FromBody] AddUser.MakeUserAdminModelRequest userId)
        {
            var respons = await _mediator.Send(userId);
            return !respons ? StatusCode(403) : Ok(respons);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateUser(string id, [FromBody] UpdateUser.UpdateUserModelRequest userData)
        {
            var respons = await _mediator.Send(userData);
            return !respons ? BadRequest(respons) : Ok(respons);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeletUser(string id)
        {
            var respons = await _mediator.Send(new DeleteUser.DeleteUsersModelRequest { UserID = id });
            return !respons ? BadRequest("Error to delete user") : Ok(respons);
        }
    }
}
