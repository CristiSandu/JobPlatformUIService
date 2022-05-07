using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;

namespace JobPlatformUIService.Infrastructure.Data.Firestore.Models
{
    [FirestoreData]
    public class Angajator : IFirestoreDocument
    {
        public Angajator()
        {

        }

        public Angajator(string email, string name, string phoneNumber, string address,
            string description, string domain, string type, bool isAdmin)
        {
            //IdentityGUID = Guid.NewGuid().ToString("N");
            DocumentId = Guid.NewGuid().ToString("N");
            Email = email;
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            Description = description;
            Domain = domain;
            Type = type;
            IsAdmin = isAdmin;
        }

        [FirestoreProperty]
        //public string IdentityGUID { get; set; }
        public string DocumentId { get; set; }
        [FirestoreProperty]
        public string Email { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Domain { get; set; }
        public string Type { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<AnunturiAngajator> AnuntList { get; set; } = new List<AnunturiAngajator>();
    }
}