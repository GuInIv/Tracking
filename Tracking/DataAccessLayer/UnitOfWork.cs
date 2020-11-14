using System;
using Tracking.DataAccessLayer;

namespace Tracking
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext context;
        private TrackingRepository trackingDataRepository;
        private UserRepository userRepository;

        public UnitOfWork()
        {
            context = new DataContext();
        }

        public IRepository<TrackingData> TrackingData
        {
            get
            {
                if (trackingDataRepository == null)
                    trackingDataRepository = new TrackingRepository(context);
                return trackingDataRepository;
            }
        }

        public IRepository<User> User
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(context);
                return userRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
