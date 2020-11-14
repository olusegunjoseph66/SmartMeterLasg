using SmartMeterLibServices.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartMeterLibServices.Reprository
{
    public interface ISubscriberRepository
    {
        IEnumerable<SubscriberInfoModel> GetAllSubscribersStack(DateTime startDate, DateTime endDate, string STAKEHOLDER_ID);
        SubscriberInfoModel Add(SubscriberInfoModel newSubscriber);
        IEnumerable<SubscriberInfoModel> CountSubscribers(SubscriberInfoModel subscriber);
        SubscriberInfoModel Delete(int id);
        IEnumerable<SubscriberInfoModel> GetAllSubscribers(DateTime startDate, DateTime endDate, int Subscriber_ID);
        SubscriberInfoModel GetSubscriber(int id);
        IEnumerable<SubscriberInfoModel> Search(string searchTerm, DateTime StartDate, DateTime EndDate);
        SubscriberInfoModel Update(SubscriberInfoModel UpdateSubscriber);
        IEnumerable<SubscriberInfoModel> GetAllSubscribers();
    }
}