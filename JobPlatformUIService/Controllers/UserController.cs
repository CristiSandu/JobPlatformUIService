using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using JobPlatformUIService.Infrastructure.Data.Firestore.Models;
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


        public UserController(IFirestoreService<User> firestoreService,
           IFirestoreContext firestoreContext)
        {
            _firestoreService = firestoreService;
            _collectionReference = firestoreContext.FirestoreDB.Collection("Users");
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<User> GetUser(string id)
        {
            return await _firestoreService.GetDocumentByIds("Users", id);
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
