using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces
{
    public interface IFirestoreService<T> where T : IFirestoreDocument
    {
        Task<T> GetDocumentByIds(string collectionName, string documentId);
        IFirestoreContext GetFirestoreContext();
        Task<bool> InsertDocumentAsync(T document, CollectionReference collectionReference);
        Task<bool> UpdateDocumentAsync(T document, CollectionReference collectionReference, bool mergeAll = true);
        Task<List<T>> GetAllValuesWithCertificateId<T>(string certificateId, CollectionReference collectionReference);
        Task<bool> CheckIfPhoneIdExistInDatabase<T>(string phoneId, CollectionReference collectionReference);
    }
}
