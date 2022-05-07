using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;

namespace JobPlatformUIService.Infrastructure.Data.Firestore.Models
{
    [FirestoreData]
    public class RecruterJobs : IFirestoreDocument
    {
        public RecruterJobs()
        {

        }

        public RecruterJobs(string angajatorID, string jobId, Job job)
        {
            //AnuntID = Guid.NewGuid().ToString("N");
            AngajatorID = angajatorID;
            JobId = jobId;
            Job = job;
        }

        public string DocumentId => $"{AngajatorID}-{JobId}";
        
        [FirestoreProperty]
        public string JobId { get; set; }
        
        [FirestoreProperty]
        public string AngajatorID { get; set; }

        [FirestoreProperty]
        public Job Job { get; set; }
        
        [FirestoreProperty]
        public List<CandidateJobs> CandidateList { get; set; }
    }
}