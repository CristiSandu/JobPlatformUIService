using Google.Cloud.Firestore;
using JobPlatformUIService.Core.SeedWork;

namespace JobPlatformUIService.Core.DataModel
{
    [FirestoreData]
    public class User : IFirestoreDocument, IAggregateRoot
    {
        
        [FirestoreProperty]
        public string DocumentId { get; set; }

        [FirestoreProperty]
        public int? Age { get; set; }

        [FirestoreProperty]
        public string Description { get; set; }

        [FirestoreProperty]
        public string? Description_last_job { get; set; }

        [FirestoreProperty]
        public string Domain { get; set; }

        [FirestoreProperty]
        public string Email { get; set; }

        [FirestoreProperty]
        public string? Gender { get; set; }

        [FirestoreProperty]
        public string? Last_level_grad { get; set; }

        [FirestoreProperty]
        public string Location { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string Phone { get; set; }

        [FirestoreProperty]
        public string Type { get; set; }

        [FirestoreProperty]
        public bool IsAdmin { get; set; } = false;  

    }
}