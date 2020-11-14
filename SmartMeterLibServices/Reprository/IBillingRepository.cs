using SmartMeterLibServices.Model;
using System;
using System.Collections.Generic;

namespace SmartMeterLibServices.Reprository
{
    public interface IBillingRepository
    {
        NotificatnModel AddNotificatn(NotificatnModel newNotify);

        //IEnumerable<NotificatnModel> GetAllNotification(int subscriiberid);
        void UpdatePaymentDetail(paymentUpdateInfo UpdatePayment);
        string AddJsonIOTDeviceSmartMeterJson(string Json);
        IOTDeviceSmartMeter AddJsonIOTDeviceSmartMeter(IOTDeviceSmartMeter newIOTDEvice);
        BillingsInfoModel Add(BillingsInfoModel newDevice);
        IEnumerable<DeviceCount> CountByBillings();
        BillingDataS GetSubscriber_IDlogin(string Email);
        IEnumerable<BillingsInfoModel> CountDevices(BillingsInfoModel device);
        BillingsRealData GetBillingBySubscriberIdCurrentMonth(int id);
        BillingsInfoModel Delete(int id);

        IEnumerable<TarrifInfoModel> GetAllTarrifs(string id);

        IEnumerable<BillingsRealData> GetAllBillingsStack(DateTime startDate, DateTime endDate, string STAKEHOLDER_ID);
        IEnumerable<BillingsRealData> GetAllBillings(DateTime startDate, DateTime endDate, int Subscriber_ID);
        IEnumerable<IOTDeviceSmartMeter> GetAllDeviceSmartMeters(DateTime startDate, DateTime endDate, string STAKEHOLDER_ID);
        IEnumerable<IOTUsageSmartMeter> GetAllUsageSmartMeters();
        IEnumerable<BillingsRealData> GetAllTotal_EnergyConsumedMonthly(string id);
        IEnumerable<IOTUsagemontSmartMeter> GetAllMonthlySmartMeters();
        BillingsRealData UpdateSelectedBillings(BillingsRealData UpdateDevice);
        IEnumerable<BillingDataA> UpdateSelectedBillings(string DeviceId, DateTime startDate, DateTime endDate, int Billings_Id);
        //IEnumerable<BillingsRealData> UpdateSelectedBillings(int DeviceId, DateTime startDate, DateTime endDate, int Billings_Id);
        IEnumerable<BillingsRealData> SearchBilling(string searchTerm, DateTime StartDate, DateTime EndDate);
        //IEnumerable<BillingsInfoModel> GetAllBillings(DateTime startDate, DateTime endDate);
        BillingsInfoModel GetBilling(int id);
        IEnumerable<BillingsInfoModel> Search(string searchTerm, DateTime StartDate, DateTime EndDate);
        BillingsInfoModel Update(BillingsInfoModel UpdateDevice);

        BillingsRealData GetBilling_Id(int id);
        IEnumerable<BillingsRealData> GetAllBillings_BySubscriberIdMonth(int id, string billingMonth);
        IEnumerable<BillingsRealData> GetAllBillings_First3Month(int id);
        TarrifInfoModel AddTarrif(TarrifInfoModel newTarrif);
        TarrifInfoModel UpdateTarrif(TarrifInfoModel UpdateTarrif);
        TarrifInfoModel GetTarrifs(int id);
        paymentInfo GetPaymentInfo(int id);
    }
}