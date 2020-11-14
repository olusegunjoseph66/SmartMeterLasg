using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SmartMeterLibServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartMeterLibServices.Reprository
{
    public class SubscriberRepository :  ISubscriberRepository
    {
        private readonly AppDbContext _context;

        public SubscriberRepository(AppDbContext context)
        {
            _context = context;
        }
        public SubscriberInfoModel Add(SubscriberInfoModel newSubscriber)
        {
            _context.Database.ExecuteSqlRaw("dbo.Insert_Select_AllSubscribers {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                newSubscriber.STAKEHOLDER_ID,newSubscriber.First_Name,
                newSubscriber.Last_Name,newSubscriber.Phone,newSubscriber.Gender,
                newSubscriber.Dofb, newSubscriber.Email, newSubscriber.Address,
                newSubscriber.State,newSubscriber.Country,newSubscriber.UserId);

            return newSubscriber;

        }

        public IEnumerable<SubscriberInfoModel> CountSubscribers(SubscriberInfoModel subscriber)
        {
            throw new NotImplementedException();
        }

        public SubscriberInfoModel Delete(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<SubscriberInfoModel> GetAllSubscribersStack(DateTime startDate, DateTime endDate, string STAKEHOLDER_ID)
        {
            SqlParameter parameter1 = new SqlParameter("@startDate", startDate);
            SqlParameter parameter2 = new SqlParameter("@endDate", endDate);
            SqlParameter parameter3 = new SqlParameter("@STAKEHOLDER_ID", STAKEHOLDER_ID);
            return _context.subscriberInfos
                .FromSqlRaw<SubscriberInfoModel>("dbo.Select_AllSubscriberStack @startDate, @endDate,@STAKEHOLDER_ID", parameter1, parameter2, parameter3)
                .ToList();
            //return _context.subscriberInfos.ToList();
        }

        public IEnumerable<SubscriberInfoModel> GetAllSubscribers()
        {
            //SqlParameter parameter1 = new SqlParameter("@startDate", startDate);
            //SqlParameter parameter2 = new SqlParameter("@endDate", endDate);
            //SqlParameter parameter3 = new SqlParameter("@Subscriber_ID", Subscriber_ID);
            return _context.subscriberInfos
                .FromSqlRaw<SubscriberInfoModel>("dbo.Select_AllSubscriber_UserNotification")
                .ToList();
            //return _context.subscriberInfos.ToList();
        }
        public IEnumerable<SubscriberInfoModel> GetAllSubscribers(DateTime startDate, DateTime endDate, int Subscriber_ID)
        {
            SqlParameter parameter1 = new SqlParameter("@startDate", startDate);
            SqlParameter parameter2 = new SqlParameter("@endDate", endDate);
            SqlParameter parameter3 = new SqlParameter("@Subscriber_ID", Subscriber_ID);
            return _context.subscriberInfos
                .FromSqlRaw<SubscriberInfoModel>("dbo.Select_AllSubscriber @startDate, @endDate,@Subscriber_ID", parameter1,parameter2,parameter3)
                .ToList(); 
            //return _context.subscriberInfos.ToList();
        }

        public SubscriberInfoModel GetSubscriber(int id)
        {
            SqlParameter parameter = new SqlParameter("@Id", id);
            return _context.subscriberInfos
                .FromSqlRaw<SubscriberInfoModel>("dbo.GetSubscriber_ById @Id", parameter)
                .ToList().FirstOrDefault();
            //return _context.subscriberInfos.FromSqlRaw<SubscriberInfoModel>("GetDevices_ById {0}",id)
            //    .ToList().FirstOrDefault();           
        }

        public IEnumerable<SubscriberInfoModel> Search(string searchTerm, DateTime StartDate, DateTime EndDate)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return _context.subscriberInfos;
            }

            return _context.subscriberInfos.Where(e => e.Subscriber_ID.ToString().Contains(searchTerm) ||
                                            (e.First_Name.Contains(searchTerm)) ||
                                            (e.Date_Of_Registration.Equals(StartDate) &&
                                            e.Date_Of_Registration.Equals(EndDate)));
        }

        public SubscriberInfoModel Update(SubscriberInfoModel UpdateSubscriber)
        {
            throw new NotImplementedException();
        }
    }
}
