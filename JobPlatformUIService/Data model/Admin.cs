using Google.Cloud.Firestore;

namespace JobPlatformUIService
{
    [FirestoreData]
    public class Admin : IFirebaseEntity
    {
        public Admin()
        {

        }

        public Admin(string email, string name, string phoneNumber, string address, 
            string description, string domain, string type, bool isAdmin)
        {
            //IdentityGUID = Guid.NewGuid().ToString("N");
            ID = Guid.NewGuid().ToString("N");
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
        public string ID { get; set; }
        [FirestoreProperty]
        public string Email { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Domain { get; set; }
        public string Type { get; set; }
        public bool IsAdmin { get; set; }
        
    }
}