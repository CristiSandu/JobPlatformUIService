using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using JobPlatformUIService.Core.DataModel;

namespace JobPlatformUIService.Infrastructure.Data.Firestore
{
    public class FirestoreService<T> : IFirestoreService<T>
        where T : IFirestoreDocument
    {
        private IFirestoreContext _firestoreContext { get; set; }
        private ILogger _logger { get; set; }

        public FirestoreService(IFirestoreContext firestoreContext,
                                ILogger<FirestoreService<T>> logger)
        {
            _firestoreContext = firestoreContext;
            _logger = logger;
        }

        public IFirestoreContext GetFirestoreContext()
        {
            return _firestoreContext;
        }

        public async Task<bool> InsertDocumentAsync(T document, CollectionReference collectionReference)
        {
            var docRef = collectionReference.Document(document.DocumentId);

            try
            {
                var response = await docRef.SetAsync(document);
                return response != null;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error on InsertDocumentAsync: {ex.Message} - {ex.StackTrace}");
            }

            return false;
        }

        public async Task<bool> UpdateDocumentAsync(T document, CollectionReference collectionReference, bool mergeAll = true)
        {

            var docRef = collectionReference.Document(document.DocumentId);

            try
            {
                var response = mergeAll ? await docRef.SetAsync(document, SetOptions.MergeAll) : await docRef.SetAsync(document);
                return response != null;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error on UpdateDocumentAsync: {ex.Message} - {ex.StackTrace}");
            }

            return false;
        }

        public async Task<List<T>> GetAllValuesWithCertificateId<T>(string certificateId, CollectionReference collectionReference)
        {
            Query query = collectionReference.WhereEqualTo("CertificateId", certificateId);

            try
            {
                QuerySnapshot snapshot = await query.GetSnapshotAsync();
                return snapshot.Select(s => s.ConvertTo<T>()).ToList();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error on GetAsync: {ex.Message} - {ex.StackTrace}");
            }

            return new List<T>();
        }

        public async Task<bool> CheckIfPhoneIdExistInDatabase<T>(string phoneId, CollectionReference collectionReference)
        {
            Query query = collectionReference.WhereEqualTo("PhoneId", phoneId);

            try
            {
                QuerySnapshot snapshot = await query.GetSnapshotAsync();
                return snapshot.Select(s => s.ConvertTo<T>()).ToList().Any();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error on GetAsync: {ex.Message} - {ex.StackTrace}");
            }

            return false;
        }

        public async Task<T> GetDocumentByIds(string collectionName, string documentId)
        {
            DocumentReference docRef = _firestoreContext.FirestoreDB.Collection(collectionName).Document(documentId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            T document = snapshot.ConvertTo<T>();

            return document;
        }

    }
}
