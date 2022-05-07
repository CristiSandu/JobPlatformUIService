using Google.Cloud.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;

namespace JobPlatformUIService.Infrastructure.Data.Firestore.Models
{
    [FirestoreData]
    public class User : IFirestoreDocument
    {
        public User()
        {

        }

        public User(string id, string email, string name, string phoneNumber, string address,
            string description, string domain, string type, bool isAdmin, int? age,
            string? gender, string? last_level_grad, string? description_last_job)
        {
            DocumentId = id;
            Email = email;
            Name = name;
            Phone = phoneNumber;
            Location = address;
            Description = description;
            Domain = domain;
            Type = type;
            IsAdmin = isAdmin;

            if (string.Compare(type, "Recruiter") == 0)
            {
                Gender = null;
                Age = 0;
                Last_level_grad = null;
                Description_last_job = null;
            }
            else if (string.Compare(type, "Candidate") == 0)
            {
                Gender = gender;
                Age = age;
                Last_level_grad = last_level_grad;
                Description_last_job = description_last_job;
            }
        }
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
        public bool IsAdmin { get; set; }

    }
}