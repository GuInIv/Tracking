using System;
using System.Collections.Generic;
using Tracking.DataAccessLayer;
using Tracking.Models;
namespace Tracking.BusinessLayer
{
    public class BaseTrackingInfo  : IExecute, IExecuteResult
    {
        protected IUnitOfWork Repository;
        protected TrackingDataView TrackingDataView;
        protected IEnumerable<User> Users;
        public IEnumerable<TrackingDataView> TrackingResult { get; set; }

        public BaseTrackingInfo(IUnitOfWork repository, TrackingDataView trackingDataView)
        {
            this.Repository = repository;
            this.TrackingDataView = trackingDataView;
        }

        public virtual void Execute()
        {
            List<TrackingDataView> result = new List<TrackingDataView>();
            
            try
            {
                foreach (var item in Users)
                {
                    var points = new List<Point>();
                    foreach (var trackingData in item.Tracking)
                    {
                        Point point = new Point();
                        try
                        {
                            string[] nums = XorCipher.Decrypt(trackingData.CipherPoints, TrackingDataView.CipherKey).Split(' ');
                            point.X = Convert.ToDouble(nums[0]);
                            point.Y = Convert.ToDouble(nums[1]);
                        }
                        catch
                        {
                            throw;
                        }
                        points.Add(point);
                    }
                    result.Add(new TrackingDataView() { FirstName = item.FirstName, LastName = item.LastName, Age = item.Age, Points = points });
                }
            }
            catch
            {
                Console.WriteLine("Проверьте правильно ли введен ключ.");
            }

            TrackingResult = result;
        }
    }
}
