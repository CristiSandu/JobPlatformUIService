using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;

namespace JobPlatformUIService.Core.DataModel
{
    [FirestoreData]
    public class CandidatiAnunt : IFirestoreDocument
    {
        public CandidatiAnunt()
        {

        }

        public CandidatiAnunt(int status, DateTime applyDate)
        {
            //CandidatID = Guid.NewGuid().ToString("N");
            ID = Guid.NewGuid().ToString("N");
            Status = status;
            ApplyDate = applyDate;
        }

        [FirestoreProperty]
        //public string CandidatID { get; set; }
        public string ID { get; set; }
        public int Status { get; set; }
        public DateTime ApplyDate { get; set; }

        public string DocumentId => throw new NotImplementedException();
    }
}