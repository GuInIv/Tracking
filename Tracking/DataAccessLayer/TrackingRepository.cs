using System;
using System.Collections.Generic;
using System.Linq;
using Tracking.DataAccessLayer;

namespace Tracking
{
    public class TrackingRepository : IRepository<TrackingData>
    {
        private readonly DataContext context;

        public TrackingRepository(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<TrackingData> Read()
        {
            return context.TrackingData;
        }

        public void Add(TrackingData trackingData)
        {
            if (trackingData.User != null && trackingData.User.Id != 0)
                context.Attach(trackingData.User);
            context.TrackingData.Add(trackingData);
        }

        public IEnumerable<TrackingData> Find(Func<TrackingData, bool> predicate)
        {
            return context.TrackingData.Where(predicate).ToList();
        }
    }
}
