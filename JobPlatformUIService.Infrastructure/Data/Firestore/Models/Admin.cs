using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;

namespace JobPlatformUIService.Infrastructure.Data.Firestore.Models
{
    [FirestoreData]
    public class Admin : IFirestoreDocument
    {
        public Admin()
        {

        }

        public Admin(string id, string email, string name, string phoneNumber, string address,
            string description, string domain, string type, bool isAdmin)
        {
            //IdentityGUID = Guid.NewGuid().ToString("N");
            DocumentId = id;
            email = email;
            name = name;
            phone = phoneNumber;
            location = address;
            description = description;
            domain = domain;
            type = type;
            isAdmin = isAdmin;
        }
        [FirestoreProperty]
        public string DocumentId { get; set; }
        
        [FirestoreProperty]
        public int age { get; set; }
        
        [FirestoreProperty]
        public string? description { get; set; }
        
        [FirestoreProperty]
        public string? description_last_job { get; set; }
        
        [FirestoreProperty]
        public string domain { get; set; }
        
        [FirestoreProperty]
        public string email { get; set; }
        
        [FirestoreProperty]
        public string gender { get; set; }
        
        [FirestoreProperty]
        public string last_level_grad { get; set; }
        
        [FirestoreProperty]
        public string location { get; set; }
        
        [FirestoreProperty]
        public string name { get; set; }
        
        [FirestoreProperty]
        public string phone { get; set; }
        
        [FirestoreProperty]
        public string type { get; set; }
        
        [FirestoreProperty]
        public bool isAdmin { get; set; }

    }
}