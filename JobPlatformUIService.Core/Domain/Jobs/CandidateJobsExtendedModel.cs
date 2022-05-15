using Google.Cloud.Firestore;
using JobPlatformUIService.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformUIService.Core.Domain.Jobs;

[FirestoreData]

public class CandidateJobsExtendedModel : CandidateJobs
{
    [FirestoreProperty]
    public Job JobDetails { get; set; }
}
