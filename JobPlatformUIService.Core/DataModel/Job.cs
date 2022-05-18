using Google.Cloud.Firestore;
using JobPlatformUIService.Core.SeedWork;

namespace JobPlatformUIService.Core.DataModel;

[FirestoreData]
public class Job : IFirestoreDocument, IAggregateRoot
{
    [FirestoreProperty]
    public string DocumentId { get; set; }

    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public int NumberEmp { get; set; }

    [FirestoreProperty]
    public int NumberApplicants { get; set; }

    [FirestoreProperty]
    public string Address { get; set; }

    [FirestoreProperty]
    public string Description { get; set; }

    [FirestoreProperty]
    public string Domain { get; set; }

    [FirestoreProperty]
    public bool IsCheck { get; set; }

    [FirestoreProperty]
    public bool IsExpired { get; set; }

    [FirestoreProperty]
    public string RecruterID { get; set; }

    [FirestoreProperty]
    public string RecruterName { get; set; }

    [FirestoreProperty]
    public DateTime Date { get; set; }
}
