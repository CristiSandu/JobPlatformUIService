using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformUIService.Core.SeedWork
{
    public abstract class DomainOfAggregate<TAggregate> where TAggregate :  IAggregateRoot
    {
        private protected readonly TAggregate aggregate;

        public DomainOfAggregate(TAggregate aggregate)
        {
            this.aggregate = aggregate;
        }
    }
}
