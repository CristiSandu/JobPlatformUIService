using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JobPlatformUIService.Infrastructure.Data.Firestore
{
    public class FirestoreContext : IFirestoreContext
    {
        private FirestoreDb _firestoreContext { get; set; }
        public FirestoreDb FirestoreDB { get => _firestoreContext; }

        public FirestoreContext(IOptions<FirestoreSettings> firestore)
        {
            var firebaseJson = JsonSerializer.Serialize(firestore.Value);

            _firestoreContext = new FirestoreDbBuilder
            {
                ProjectId = "jobplatform-d63d7",
                JsonCredentials = firebaseJson
            }.Build();
        }
    }
}
