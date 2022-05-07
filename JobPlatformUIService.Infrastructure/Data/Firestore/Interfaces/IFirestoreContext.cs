using Google.Cloud.Firestore;

namespace JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces
{
    public interface IFirestoreContext
    {
        FirestoreDb FirestoreDB { get; }
    }
}