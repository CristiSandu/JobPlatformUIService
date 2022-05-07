using Google.Cloud.Firestore;

namespace JobPlatformUIService.Core.DataModel
{
    [FirestoreData]
    public class CandidatiAnunt : IFirebaseEntity
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

    }
}