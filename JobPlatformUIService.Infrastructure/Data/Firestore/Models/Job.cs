using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;

namespace JobPlatformUIService.Core.DataModel
{
    [FirestoreData]
    public class Job : IFirestoreDocument
    {
        public Job()
        {

        }

        public Job(string name, int numberEmp, string address,
            string description, string domain, string recruterID, DateTime date)
        {
            DocumentId = Guid.NewGuid().ToString("N");
            Name = name;
            NumberEmp = numberEmp;
            Address = address;
            Description = description;
            Domain = domain;
            RecruterID = recruterID;
            Date = date;
        }

        public string DocumentId { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }
        
        [FirestoreProperty]
        public int NumberEmp { get; set; }
        
        [FirestoreProperty]
        public string Address { get; set; }
        
        [FirestoreProperty]
        public string Description { get; set; }
        
        [FirestoreProperty]
        public string Domain { get; set; }
        
        [FirestoreProperty]
        public string RecruterID { get; set; }
        
        [FirestoreProperty]
        public DateTime Date { get; set; }
    }
}