using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobPlatformUIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IFirestoreService<User> _firestoreService;
        private CollectionReference _collectionReference { get; set; }

        private readonly IMediator _mediator;



        public UserController(IFirestoreService<User> firestoreService,
           IFirestoreContext firestoreContext,
           IMediator mediator)
        {
            _firestoreService = firestoreService;
            _collectionReference = firestoreContext.FirestoreDB.Collection("Users");
            _mediator = mediator;

        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<List<User>> GetUser(string id)
        {
            return await _firestoreService.GetDocumentByIds( id,_collectionReference);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<bool> Post([FromBody] User user)
        {
            return await _firestoreService.InsertDocumentAsync(user, _collectionReference);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
