using Google.Cloud.Firestore;
using JobPlatformUIService.Core.SeedWork;

namespace JobPlatformUIService.Core.DataModel;

[FirestoreData]
public class Job : IFirestoreDocument, IAggregateRoot
{
    private string documentId;

    public Job()
    {
        documentId = Guid.NewGuid().ToString("N");
    }

    public Job(string name, int numberEmp, string address,
        string description, string domain, string recruterID, DateTime date)
    {
        documentId = Guid.NewGuid().ToString("N");
        Name = name;
        NumberEmp = numberEmp;
        Address = address;
        Description = description;
        Domain = domain;
        RecruterID = recruterID;
        Date = date;
        IsCheck = false;
    }

    [FirestoreProperty]
    public string DocumentId { get => documentId; }

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
    public bool IsCheck { get; set; }

    [FirestoreProperty]
    public bool IsExpired { get; set; }

    [FirestoreProperty]
    public string RecruterID { get; set; }

    [FirestoreProperty]
    public DateTime Date { get; set; }
}
