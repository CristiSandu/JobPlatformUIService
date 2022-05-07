using JobPlatformUIService.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformUIService.Core.Domain.User
{
    public class UsersDomain : DomainOfAggregate<DataModel.User>
    {
        public UsersDomain(DataModel.User aggregate) : base(aggregate)
        {
        }

        public void UpdateUser(DataModel.User user)
        {
            aggregate.Name = user.Name;
            aggregate.Email = user.Email;
            aggregate.Age = user.Age;
            aggregate.Last_level_grad = user.Last_level_grad;
            aggregate.Phone = user.Phone;
            aggregate.Location = user.Location;
            aggregate.Description = user.Description;
            aggregate.Domain = user.Domain;
            aggregate.Gender = user.Gender;
            aggregate.Description_last_job = user.Description_last_job;
        }
        public bool UserIsAdmin() => aggregate.IsAdmin ;
        public bool IsRecruter() => aggregate.Type == "Recruiter";
    }
}
