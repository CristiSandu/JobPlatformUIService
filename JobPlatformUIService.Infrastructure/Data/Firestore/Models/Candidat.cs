using Google.Cloud.Firestore;

namespace JobPlatformUIService.Core.DataModel
{
    [FirestoreData]
    public class Candidat : IFirebaseEntity
    {
        public Candidat()
        {

        }

        public Candidat(string email, string name, string phoneNumber, string address, 
            string gen, string description, string domain, string type, int age, 
            string lastLvlGrad, string lastJobDesc, bool isAdmin)
        {
            //IdentityGUID = Guid.NewGuid().ToString("N");
            ID = Guid.NewGuid().ToString("N");
            Email = email;
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            Gen = gen;
            Description = description;
            Domain = domain;
            Type = type;
            Age = age;
            LastLvlGrad = lastLvlGrad;
            LastJobDesc = lastJobDesc;
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
        public string Gen { get; set; }
        public string Description { get; set; }
        public string Domain { get; set; }
        public string Type { get; set; }
        public int Age { get; set; }
        public string LastLvlGrad { get; set; }
        public string LastJobDesc { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<AnunturiCandidat> AnuntList { get; set; } = new List<AnunturiCandidat>();

    }
}