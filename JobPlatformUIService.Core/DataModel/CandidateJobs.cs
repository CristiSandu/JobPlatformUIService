
using Google.Cloud.Firestore;

namespace JobPlatformUIService.Core.DataModel
{
    [FirestoreData]
    public class CandidateJobs : IFirestoreDocument
    {
        public CandidateJobs()
        {

        }

        public CandidateJobs(string candidateID, string jobId, int status, User user, DateTime applyDate)
        {
            CandidateID = candidateID;
            JobID = jobId;
            Status = status;
            ApplyDate = applyDate;
            Candidate = user;
        }

        public string DocumentId => $"{CandidateID}-{JobID}";

        [FirestoreProperty]
        public string CandidateID { get; set; }

        [FirestoreProperty]
        public string JobID { get; set; }

        private int _status;
        [FirestoreProperty]
        public int Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    LastStatusDate = DateTime.Now;
                }
            }
        }

        [FirestoreProperty]
        public User Candidate { get; set; }

        [FirestoreProperty]
        public DateTime ApplyDate { get; set; }

        [FirestoreProperty]
        public DateTime LastStatusDate { get; set; }
    }
}