using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;

namespace JobPlatformUIService.Core.DataModel
{
    [FirestoreData]
    public class AnunturiAngajator : IFirestoreDocument
    {
        public AnunturiAngajator()
        {

        }

        public AnunturiAngajator(string angajatorID)
        {
            //AnuntID = Guid.NewGuid().ToString("N");
            ID = Guid.NewGuid().ToString("N");
            AngajatorID = angajatorID;
        }

        [FirestoreProperty]
        //public string AnuntID { get; set; }
        public string ID { get; set; }
        public string AngajatorID { get; set; }
        public virtual ICollection<CandidatiAnunt> AnuntList { get; set; } = new List<CandidatiAnunt>();

        public string DocumentId => throw new NotImplementedException();
    }
}