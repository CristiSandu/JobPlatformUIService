using Google.Cloud.Firestore;

namespace JobPlatformUIService
{
    [FirestoreData]
    public class Anunt : IFirebaseEntity
    {
        public Anunt()
        {

        }

        public Anunt(string name, string numberEmp, string address,
            string description, string domain, string recruterID, DateTime date)
        {
            ID = Guid.NewGuid().ToString("N");
            Name = name;
            NumberEmp = numberEmp;
            Address = address;
            Description = description;
            Domain = domain;
            RecruterID = recruterID;
            Date = date;
        }

        [FirestoreProperty]
        public string ID { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        public string NumberEmp { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Domain { get; set; }
        public string RecruterID { get; set; }
        public DateTime Date { get; set; }

    }
}