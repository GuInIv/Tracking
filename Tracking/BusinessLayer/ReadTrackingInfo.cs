using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Tracking.Models;

namespace Tracking.BusinessLayer
{
    internal class ReadTrackingInfo : BaseTrackingInfo
    {
        public ReadTrackingInfo(IUnitOfWork repository, TrackingDataView trackingDataView) : base(repository, trackingDataView)
        { }

        public override void Execute()
        {
            List<TrackingDataView> result = new List<TrackingDataView>();
            var readList = Repository.User.Read();
            readList = readList.Include(x => x.Tracking);
            Users = readList;
            base.Execute();
        }
    }
}