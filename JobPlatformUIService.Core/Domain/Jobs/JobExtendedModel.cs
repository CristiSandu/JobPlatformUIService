using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformUIService.Core.Domain.Jobs
{
    public class JobExtendedModel : DataModel.Job
    {
        public string RecruterName { get; set; }
        public bool IsMine { get; set; }
        public bool IsApplied { get; set; }
        public string DocID { get; set; }

    }
}
