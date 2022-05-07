using JobPlatformUIService.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformUIService.Core.Domain.Jobs
{
    public class JobsDomain : DomainOfAggregate<DataModel.Job>
    {
        public JobsDomain(DataModel.Job aggregate) : base(aggregate)
        {
        }

        public void UpdateJob(DataModel.Job job)
        {
            aggregate.Name = job.Name;
            aggregate.NumberEmp = job.NumberEmp;
            aggregate.Address = job.Address;
            aggregate.Description = job.Description;
            aggregate.Domain = job.Domain;
            aggregate.Date = job.Date;
            aggregate.IsExpired = job.IsExpired;
        }
        public void UpdateCheck(bool check)
        {
            aggregate.IsCheck = check;
        }
        public void UpdateExpiration(bool expiration)
        {
            aggregate.IsExpired = expiration;
        }
    }
}
