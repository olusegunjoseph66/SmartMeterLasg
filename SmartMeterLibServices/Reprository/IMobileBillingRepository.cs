using SmartMeterLibServices.Model;
using System;
using System.Collections.Generic;

namespace SmartMeterLibServices.Reprository
{
    public interface IMobileBillingRepository
    {
        IEnumerable<MobileBillingDash> GetAllTotal_EnergyConsumedMonthly(string id);
        IEnumerable<MobileBillingModel> GetAllBillings(DateTime startDate, DateTime endDate);
        IEnumerable<MobileBillingModel> GetAllBillings_BySubscriberIdMonth(int id, string billingMonth);
        //MobileBillingModel GetBillingBySubscriberId(int id);
        BillingDataS GetSubscriber_IDlogin(string Email);
        MobileBillingModel GetBillingBySubscriberIdCurrentMonth(int id);
        MobileBillingModel GetBilling_Id(int id);
        IEnumerable<MobileBillingModel> SearchBilling(string searchTerm, DateTime StartDate, DateTime EndDate);
        IEnumerable<MobileBillingModel> GetAllBillings_First3Month(int id);
    }
}