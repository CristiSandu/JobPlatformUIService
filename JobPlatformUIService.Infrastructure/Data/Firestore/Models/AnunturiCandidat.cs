
using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;

namespace JobPlatformUIService.Core.DataModel
{
    [FirestoreData]
    public class AnunturiCandidat : IFirestoreDocument
    {
        public AnunturiCandidat()
        {

        }

        public AnunturiCandidat(string candidateID, int status, DateTime applyDate)
        {
            //AnuntID = Guid.NewGuid().ToString("N");
            ID = Guid.NewGuid().ToString("N");
            CandidateID = candidateID;
            Status = status;
            ApplyDate = applyDate;
        }

        [FirestoreProperty]
        //public string AnuntID { get; set; }
        public string ID { get; set; }
        public string CandidateID { get; set; }
        public int Status { get; set; }
        public DateTime ApplyDate { get; set; }

        public string DocumentId => throw new NotImplementedException();
    }
}