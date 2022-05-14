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

        public async Task<bool> UpdateDocumentFieldAsync<V>(string fild, string documentId, V value ,CollectionReference collectionReference, bool mergeAll = true)
        {
            var docRef = collectionReference.Document(documentId);

            try
            {
                var response = await docRef.UpdateAsync(fild, value);
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

        public async Task<List<T>> GetDocumentsInACollection(CollectionReference collectionReference)
        {
            try
            {
                QuerySnapshot allDataQuerySnapshot = await collectionReference.GetSnapshotAsync();
                return allDataQuerySnapshot.Documents.Select(s => s.ConvertTo<T>()).ToList();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error on GetAsync: {ex.Message} - {ex.StackTrace}");
            }

            return new List<T>();
        }

        public async Task<List<T>> GetDocumentByIds(string documentId, CollectionReference collectionReference)
        {
            try
            {
                DocumentReference docRef = collectionReference.Document(documentId);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
                T value = snapshot.ConvertTo<T>();

                List<T> result = new();
                result.Add(value);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error on GetAsync: {ex.Message} - {ex.StackTrace}");
            }

            return new List<T>();
        }

        public async Task<bool> DeleteDocumentAsync(T document, CollectionReference collectionReference)
        {
            return await DeleteDocumentByIdAsync(document.DocumentId, collectionReference);
        }

        public async Task<bool> DeleteDocumentByIdAsync(string documentId, CollectionReference collectionReference)
        {

            var docRef = collectionReference.Document(documentId);

            try
            {
                var response = await docRef.DeleteAsync();
                return response != null;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error on DeleteDocumentAsync: {ex.Message} - {ex.StackTrace}");
            }

            return false;
        }
    }
}
