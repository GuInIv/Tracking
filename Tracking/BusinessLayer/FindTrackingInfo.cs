using System.Collections.Generic;
using System.Linq;
using Tracking.DataAccessLayer;
using Tracking.Models;
namespace Tracking.BusinessLayer
{
    internal class FindTrackingInfo : BaseTrackingInfo
    {
        public FindTrackingInfo(IUnitOfWork repository, TrackingDataView trackingDataView) : base(repository, trackingDataView)
        { }

        public override void Execute()
        {
            var findList = new List<User>();
            var firstName = !TrackingDataView.PatternUserFirstName.Contains("*") ? TrackingDataView.PatternUserFirstName : TrackingDataView.PatternUserFirstName.Substring(0, TrackingDataView.PatternUserFirstName.Length - 1);
            if (!TrackingDataView.PatternUserFirstName.Contains("*"))
                findList = Repository.User.Find(x => x.FirstName.Contains(TrackingDataView.PatternUserFirstName)).ToList();
            else
                findList = Repository.User.Find(x => x.FirstName.Contains(firstName)).ToList();

            Users = findList;
            base.Execute();
        }
    }
}