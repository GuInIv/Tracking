using System;
using Tracking.DataAccessLayer;

namespace Tracking
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TrackingData> TrackingData { get; }
        IRepository<User> User { get; }
        void Save();
    }
}
