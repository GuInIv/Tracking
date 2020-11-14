using AutoMapper;
using System;
using System.Linq;
using Tracking.DataAccessLayer;
using Tracking.Models;

namespace Tracking.BusinessLayer
{
    internal class AddTrackingInfo : IExecute
    {
        IUnitOfWork repository;
        TrackingDataView trackingDataView;

        public AddTrackingInfo(IUnitOfWork repository, TrackingDataView trackingDataView)
        {
            this.repository = repository;
            this.trackingDataView = trackingDataView;
        }

        public void Execute()
        {
            if (trackingDataView.Points == null)
                throw new Exception("Tracking data not found");

            if (string.IsNullOrWhiteSpace(trackingDataView.CipherKey))
                throw new Exception("Cipher key not found");

            var user = repository.User.Find(x => x.FirstName == trackingDataView.FirstName && x.LastName == trackingDataView.LastName)
                .ToList().FirstOrDefault();
            
            if(user == null)
            {
                user = new User() { FirstName = trackingDataView.FirstName, LastName = trackingDataView.LastName, Age = trackingDataView.Age };
                repository.User.Add(user);
                repository.Save();
            }

            if (user == null)
                throw new Exception("User not added");

            foreach (var item in trackingDataView.Points)
            {
                string point = string.Join(" ", item.X, item.Y);
                repository.TrackingData.Add(new TrackingData() { CipherPoints = XorCipher.Encrypt(point, trackingDataView.CipherKey), User = user });
                repository.Save();
            }
        }
    }
}