using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SmartMeterLibServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartMeterLibServices.Reprository
{
    public class MobileBillingRepository : IMobileBillingRepository
    {

        private readonly AppDbContext _context;

        public MobileBillingRepository(AppDbContext context)
        {
            _context = context;
        }
        public MobileBillingModel GetBilling_Id(int id)
        {
            SqlParameter parameter = new SqlParameter("@BillingsId", id);
            return _context.mobileBillings
                .FromSqlRaw<MobileBillingModel>("dbo.GetBillingswithId @BillingsId", parameter)
                .ToList().FirstOrDefault();

        }
        public IEnumerable<MobileBillingModel> GetAllBillings_First3Month(int id)
        {
            SqlParameter parameter = new SqlParameter("@Subscriber_ID", id);
            var res = _context.mobileBillings
                .FromSqlRaw<MobileBillingModel>("dbo.GetAllBillings_FirstThreeMonth @Subscriber_ID", parameter)
                .ToList();
            return res;
        }
        

        public IEnumerable<MobileBillingModel> GetAllBillings_BySubscriberIdMonth(int id, string billingMonth)
        {
            SqlParameter parameter = new SqlParameter("@Subscriber_ID", id);
            SqlParameter parameter1 = new SqlParameter("@billingMonth", billingMonth);
            var res = _context.mobileBillings
                .FromSqlRaw<MobileBillingModel>("dbo.GetAllBillings_BySubscriberIdMonth @Subscriber_ID,@billingMonth", parameter, parameter1)
                .ToList();
            return res;
        }
        public IEnumerable<MobileBillingDash> GetAllTotal_EnergyConsumedMonthly(string id)
        {
            SqlParameter parameter = new SqlParameter("@Stakeholder_ID", id);
            var res = _context.Dashashbord
                .FromSqlRaw<MobileBillingDash>("dbo.GetAllTotal_EnergyConsumedMonthly @Stakeholder_ID", parameter)
                .ToList();
            return res;
        }
        public MobileBillingModel GetBillingBySubscriberIdCurrentMonth(int id)
        {
            SqlParameter parameter = new SqlParameter("@Subscriber_ID", id);
            return _context.mobileBillings
                .FromSqlRaw<MobileBillingModel>("dbo.GetAllBillings_BySubscriberId_CurrentMonth @Subscriber_ID", parameter)
                .ToList().FirstOrDefault();

        }

        public IEnumerable<MobileBillingModel> GetAllBillings(DateTime startDate, DateTime endDate)
        {
            SqlParameter parameter1 = new SqlParameter("@startDate", startDate);
            SqlParameter parameter2 = new SqlParameter("@endDate", endDate);
            var res = _context.mobileBillings
                .FromSqlRaw<MobileBillingModel>("dbo.Select_AllBillings @startDate, @endDate", parameter1, parameter2)
                .ToList();
            return res;
        }
        public IEnumerable<MobileBillingModel> SearchBilling(string searchTerm, DateTime StartDate, DateTime EndDate)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return _context.mobileBillings;
            }

            return _context.mobileBillings.Where(e => e.DEviceId.ToString().Contains(searchTerm) ||
                                            (e.SUBSCRIBER_ID.Equals(searchTerm)));
        }
        public BillingDataS GetSubscriber_IDlogin(string Email)
        {
            SqlParameter parameter = new SqlParameter("@Email", Email);
            return _context.billingsDataSure
                .FromSqlRaw<BillingDataS>("dbo.GetSubscriber_ByloginId @Email", parameter)
                .ToList().FirstOrDefault();

        }
    }
}
