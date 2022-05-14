using Google.Cloud.Firestore;

namespace JobPlatformUIService.Core.DataModel.DropdownsModels;

[FirestoreData]

public class DomainModel : IFirestoreDocument
{
    [FirestoreProperty]
    public string DocumentId => Name.ToLower();
    [FirestoreProperty]
    public string Name { get; set; }
}
