using System.Collections.Generic;
using Tracking.Models;

namespace Tracking
{
    interface IExecuteResult
    {
        IEnumerable<TrackingDataView> TrackingResult { get; set; }
    }
}