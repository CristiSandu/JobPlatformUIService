using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces
{
    public interface IFirestoreService<T> where T : IFirestoreDocument
    {
        IFirestoreContext GetFirestoreContext();
        Task<bool> InsertDocumentAsync(T document, CollectionReference collectionReference);
        Task<bool> UpdateDocumentAsync(T document, CollectionReference collectionReference, bool mergeAll = true);
        Task<bool> UpdateDocumentFieldAsync<V>(string fild, string documentId, V value, CollectionReference collectionReference, bool mergeAll = true);
        Task<List<T>> GetAllValuesWithCertificateId<T>(string certificateId, CollectionReference collectionReference);
        Task<bool> CheckIfPhoneIdExistInDatabase<T>(string phoneId, CollectionReference collectionReference);
        Task<List<T>> GetDocumentsInACollection(CollectionReference collectionReference);
        Task<List<T>> GetFilteredDocumentsByAField<V>(string fieldPath, V value, CollectionReference collectionReference);
        Task<List<T>> GetDocumentByIds(string documentId, CollectionReference collectionReference);
        Task<bool> DeleteDocumentByIdAsync(string documentId, CollectionReference collectionReference);
    }
}
