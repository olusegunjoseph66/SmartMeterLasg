using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SmartMeterLibServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartMeterLibServices.Reprository
{
    public class BillingRepository : IBillingRepository
    {
        private readonly AppDbContext _context;

        public BillingRepository(AppDbContext context)
        {
            _context = context;
        }
        public BillingsInfoModel Add(BillingsInfoModel newDevice)
        {

            SqlParameter Subscriber_ID = new SqlParameter("@Subscriber_ID", newDevice.SUBSCRIBER_ID);
            SqlParameter STAKEHOLDER_ID = new SqlParameter("@STAKEHOLDER_ID", newDevice.STAKEHOLDER_ID);
            SqlParameter Usage_Duration = new SqlParameter("@USAGE_DURATION", newDevice.USAGE_DURATION);
            SqlParameter Monthly_Duration = new SqlParameter("@MONTHLY_DURATION_PER_HR", newDevice.MONTHLY_DURATION_PER_HR);
            SqlParameter Amount_Consumption = new SqlParameter("@AMOUNT_CONSUMPTION_PER_HR", newDevice.AMOUNT_CONSUMPTION_PER_HR);

            _context.Database.ExecuteSqlRaw("dbo.Insert_Select_AllBillings @Subscriber_ID,@STAKEHOLDER_ID, @USAGE_DURATION,@MONTHLY_DURATION_PER_HR,@AMOUNT_CONSUMPTION_PER_HR",
            Subscriber_ID, STAKEHOLDER_ID, Usage_Duration, Monthly_Duration,Amount_Consumption);
            return newDevice;

        }

        public IEnumerable<DeviceCount> CountByBillings()
        {
            return _context.billingsInfos.GroupBy(e => e.DEviceId)
                            .Select(g => new DeviceCount()
                            {
                                //Device_Name = g.Key.Value,
                                Device_Name = g.Key.ToString(),
                                Count = g.Count()
                            }).ToList();
        }

        public IEnumerable<BillingsInfoModel> CountDevices(BillingsInfoModel device)
        {
            throw new NotImplementedException();
        }

        public BillingDataS GetSubscriber_IDlogin(string Email)
        {
            SqlParameter parameter = new SqlParameter("@Email", Email);
            return _context.billingsDataSure
                .FromSqlRaw<BillingDataS>("dbo.GetSubscriber_ByloginId @Email", parameter)
                .ToList().FirstOrDefault();

        }

        public BillingsInfoModel Delete(int id)
        {

            SqlParameter Device_IDs = new SqlParameter("@BillingsId", id);

            BillingsInfoModel DeleteBilling = _context.billingsInfos
                .FromSqlRaw<BillingsInfoModel>("dbo.GetBillings_ById @BillingsId", Device_IDs)
                .ToList().FirstOrDefault();


            if (DeleteBilling != null)
            {
                _context.Database.ExecuteSqlRaw("dbo.Delete_Billings @BillingsId", Device_IDs);
            }
            return DeleteBilling;
        }


        public IEnumerable<IOTDeviceSmartMeter> GetAllDeviceSmartMeters(DateTime startDate, DateTime endDate,string STAKEHOLDER_ID)
        {
            SqlParameter parameter1 = new SqlParameter("@startDate", startDate);
            SqlParameter parameter2 = new SqlParameter("@endDate", endDate);
            SqlParameter parameter3 = new SqlParameter("@STAKEHOLDER_ID", STAKEHOLDER_ID);
            var res = _context.IOTDeviceSmartMeter
                .FromSqlRaw<IOTDeviceSmartMeter>("dbo.Select_AllIOTDeviceSmartMeter @startDate, @endDate,@STAKEHOLDER_ID", parameter1, parameter2,parameter3)
                .ToList();
            return res;
        }
        public string AddJsonIOTDeviceSmartMeterJson(string Json)
        {
            SqlParameter JS = new SqlParameter("@JSON", Json);
            var newIOTDEvice =  _context.Database.ExecuteSqlRaw("dbo.Insert_Select_AllIOTDeviceSmartMeterJson @JSON ", JS);
            return newIOTDEvice.ToString();
        }
        public IOTDeviceSmartMeter AddJsonIOTDeviceSmartMeter(IOTDeviceSmartMeter newIOTDEvice)
        {

            //SqlParameter EnergyAmountkWh = new SqlParameter("@EnergyAmountkWh", newIOTDEvice.EnergyAmountkWh);
            //SqlParameter VoltageReading = new SqlParameter("@VoltageReading", newIOTDEvice.VoltageReading);
            //SqlParameter PowerReading = new SqlParameter("@PowerReading", newIOTDEvice.PowerReading);
            //SqlParameter ConnectionStatus = new SqlParameter("@ConnectionStatus", newIOTDEvice.ConnectionStatus);
            //SqlParameter DeviceId         = new SqlParameter("@DeviceId", newIOTDEvice.DeviceId);
            //SqlParameter EventProcessedUtcTime = new SqlParameter("@EventProcessedUtcTime", newIOTDEvice.EventProcessedUtcTime);


            //_context.Database.ExecuteSqlRaw("dbo.Insert_Select_AllIOTDeviceSmartMeter @EnergyAmountkWh, @VoltageReading,@PowerReading,@ConnectionStatus,@DeviceId,@EventProcessedUtcTime",
            // EnergyAmountkWh, VoltageReading, PowerReading,ConnectionStatus ,
            //DeviceId,EventProcessedUtcTime);
            //return newIOTDEvice;   Insert_Select_AllIOTDeviceSmartMeterJson
            _context.Database.ExecuteSqlRaw("dbo.Insert_Select_AllIOTDeviceSmartMeter  { 0},{ 1},{ 2},{ 3},{ 4},{ 5}",

                  newIOTDEvice.EnergyAmountkWh, newIOTDEvice.VoltageReading, newIOTDEvice.PowerReading, newIOTDEvice.ConnectionStatus,
                 newIOTDEvice.DeviceId, newIOTDEvice.EventProcessedUtcTime);
            return newIOTDEvice;
        }
        public IEnumerable<IOTUsageSmartMeter> GetAllUsageSmartMeters()
        {
            SqlParameter parameter1 = new SqlParameter("@ConnectionStatus", "2");
            var res = _context.iotusageSmartM
                .FromSqlRaw<IOTUsageSmartMeter>("dbo.Select_IOTUsageSmartMeter @ConnectionStatus ",parameter1)
                .ToList();
            return res;
        }

        public IEnumerable<IOTUsagemontSmartMeter> GetAllMonthlySmartMeters()
        {
            SqlParameter parameter1 = new SqlParameter("@ConnectionStatus", "2");
            var res = _context.iotusagemontSmartM
                .FromSqlRaw<IOTUsagemontSmartMeter>("dbo.Select_IOTMonthlySmartMeter @ConnectionStatus ", parameter1)
                .ToList();
            return res;
        }

        public BillingsRealData GetBilling_Id(int id)
        {
            SqlParameter parameter = new SqlParameter("@BillingsId", id);
            return _context.billingsDatas
                .FromSqlRaw<BillingsRealData>("dbo.GetBillingswithId @BillingsId", parameter)
                .ToList().FirstOrDefault();
                  
        }
        //public IEnumerable<NotificatnModel> GetAllNotification(int subscriiberid)
        //{
        //    SqlParameter parameter = new SqlParameter("@subscriber_id", subscriiberid);
        //    var res = _context.Notificatns
        //        .FromSqlRaw<NotificatnModel>("dbo.GetAllNotification @subscriber_id", parameter)
        //        .ToList();
        //    return res;
        //}
        public NotificatnModel AddNotificatn(NotificatnModel newNotify)
        {

            SqlParameter Subscriber = new SqlParameter("@Subscriber_ID", newNotify.SUBSCRIBER_ID);
            SqlParameter UsageSet = new SqlParameter("@Usage_ByUser", newNotify.UNITS_SETBY_USER);

            _context.Database.ExecuteSqlRaw("dbo.Insert_UpdateNotificatn_ByUser @Subscriber_ID, @Usage_ByUser",
             Subscriber, UsageSet);
            return newNotify;

        }



        public IEnumerable<TarrifInfoModel> GetAllTarrifs(string id)
        {
            SqlParameter parameter = new SqlParameter("@Stakeholder_ID", id);
            var res = _context.tarrifInfos           
                .FromSqlRaw<TarrifInfoModel>("dbo.GetAllTarrifs_ByStakeholder @Stakeholder_ID", parameter)
                .ToList();
            return res;
        }
        public IEnumerable<BillingsRealData> GetAllTotal_EnergyConsumedMonthly(string id)
        {
            SqlParameter parameter = new SqlParameter("@Stakeholder_ID", id);
            var res = _context.billingsDatas
                .FromSqlRaw<BillingsRealData>("dbo.GetAllTotal_EnergyConsumedMonthly @Stakeholder_ID", parameter)
                .ToList();
            return res;
        }
        public IEnumerable<BillingsRealData> GetAllBillings_First3Month(int id)
        {
            SqlParameter parameter = new SqlParameter("@Subscriber_ID", id);
            var res = _context.billingsDatas
                .FromSqlRaw<BillingsRealData>("dbo.GetAllBillings_FirstThreeMonth @Subscriber_ID", parameter)
                .ToList();
            return res;
        }

        public IEnumerable<BillingsRealData> GetAllBillings_BySubscriberIdMonth(int id,string billingMonth)
        {
            SqlParameter parameter = new SqlParameter("@Subscriber_ID", id);
            SqlParameter parameter1 = new SqlParameter("@billingMonth", billingMonth);
            var res = _context.billingsDatas
                .FromSqlRaw<BillingsRealData>("dbo.GetAllBillings_BySubscriberIdMonth @Subscriber_ID,@billingMonth", parameter,parameter1)
                .ToList();
            return res;
        }

        public BillingsRealData GetBillingBySubscriberIdCurrentMonth(int id)
        {
            SqlParameter parameter = new SqlParameter("@Subscriber_ID", id);
            return _context.billingsDatas
                .FromSqlRaw<BillingsRealData>("dbo.GetAllBillings_BySubscriberId_CurrentMonth @Subscriber_ID", parameter)
                .ToList().FirstOrDefault();
                 
        }
        public IEnumerable<BillingsRealData> GetAllBillingsStack(DateTime startDate, DateTime endDate, string STAKEHOLDER_ID)
        {
            SqlParameter parameter1 = new SqlParameter("@startDate", startDate);
            SqlParameter parameter2 = new SqlParameter("@endDate", endDate);
            SqlParameter parameter3 = new SqlParameter("@STAKEHOLDER_ID", STAKEHOLDER_ID);
            var res = _context.billingsDatas
                .FromSqlRaw<BillingsRealData>("dbo.Select_AllBillingsStack @startDate, @endDate,@STAKEHOLDER_ID", parameter1, parameter2, parameter3)
                .ToList();
            return res;
            //return _context.subscriberInfos.ToList();
        }
        public IEnumerable<BillingsRealData> GetAllBillings(DateTime startDate, DateTime endDate,int Subscriber_ID)
        {
            SqlParameter parameter1 = new SqlParameter("@startDate", startDate);
            SqlParameter parameter2 = new SqlParameter("@endDate", endDate);
            SqlParameter parameter3 = new SqlParameter("@Subscriber_ID", Subscriber_ID);
            var res = _context.billingsDatas
                .FromSqlRaw<BillingsRealData>("dbo.Select_AllBillings @startDate, @endDate,@Subscriber_ID", parameter1, parameter2,parameter3)
                .ToList();
            return res;
            //return _context.subscriberInfos.ToList();
        }
        public IEnumerable<BillingsRealData> SearchBilling(string searchTerm, DateTime StartDate, DateTime EndDate)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return _context.billingsDatas;
            }

            return _context.billingsDatas.Where(e => e.DEviceId.ToString().Contains(searchTerm) ||
                                            (e.SUBSCRIBER_ID.Equals(searchTerm)));

            //return null;
        }

        public BillingsRealData UpdateSelectedBillings(BillingsRealData UpdateDevice)
        {
            SqlParameter parameter1 = new SqlParameter("@DeviceId", UpdateDevice.DEviceId);
            SqlParameter parameter2 = new SqlParameter("@startDate", UpdateDevice.startDate);
            SqlParameter parameter3 = new SqlParameter("@endDate", UpdateDevice.endDate);
            //SqlParameter parameter4 = new SqlParameter("@Billings_Id", UpdateDevice.Billings_Id);

            _context.Database.ExecuteSqlRaw("dbo.Insert_SelectEnergyConsumed @DeviceId, @startDate, @endDate,@Billings_Id", parameter1, parameter2, parameter3);

            //var deviceInfo = _context.deviceInfos.Attach(UpdateDevice);
            //UpdateDevice.Transaction_Date = DateTime.UtcNow;
            //deviceInfo.State = EntityState.Modified;
            //_context.SaveChanges();
            return UpdateDevice;
        }

        public IEnumerable<BillingDataA> UpdateSelectedBillings(string DeviceId, DateTime startDate, DateTime endDate,int Billings_Id)
        {
            SqlParameter parameter1 = new SqlParameter("@iotDeviceId", DeviceId);
            SqlParameter parameter2 = new SqlParameter("@StartDate", startDate);
            SqlParameter parameter3 = new SqlParameter("@EndDate", endDate);
            SqlParameter parameter4 = new SqlParameter("@Billings_Id", Billings_Id);
            var res = _context.billingsDataAs
                .FromSqlRaw<BillingDataA>("dbo.Insert_SelectEnergyConsumed @iotDeviceId, @StartDate, @EndDate,@Billings_Id", parameter1, parameter2, parameter3, parameter4)
                .ToList();
            return res;
            //return _context.subscriberInfos.ToList();
        }


        //public IEnumerable<BillingsInfoModel> GetAllBillings(DateTime startDate, DateTime endDate)
        //{
        //    SqlParameter parameter1 = new SqlParameter("@startDate", startDate);
        //    SqlParameter parameter2 = new SqlParameter("@endDate", endDate);
        //    return _context.billingsInfos
        //        .FromSqlRaw<BillingsInfoModel>("dbo.Select_AllBillings @startDate, @endDate", parameter1, parameter2)
        //        .ToList();
        //    //return _context.subscriberInfos.ToList();
        //}

        public BillingsInfoModel GetBilling(int id)
        {
            SqlParameter parameter = new SqlParameter("@BillingsId", id);
            return _context.billingsInfos
                .FromSqlRaw<BillingsInfoModel>("dbo.GetBillings_ById @BillingsId", parameter)
                .ToList().FirstOrDefault();
            //return _context.subscriberInfos.FromSqlRaw<SubscriberInfoModel>("GetDevices_ById {0}",id)
            //    .ToList().FirstOrDefault();           
        }

        public IEnumerable<BillingsInfoModel> Search(string searchTerm, DateTime StartDate, DateTime EndDate)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return _context.billingsInfos;
            }

            return _context.billingsInfos.Where(e => e.DEviceId.ToString().Contains(searchTerm) ||
                                            (e.SUBSCRIBER_ID.Equals(searchTerm)));
        }

        public BillingsInfoModel Update(BillingsInfoModel UpdateDevice)
        {
            SqlParameter Billings_Id = new SqlParameter("@Billings_Id", UpdateDevice.Billings_Id);
            SqlParameter Device_Id = new SqlParameter("@DEviceId", UpdateDevice.DEviceId);
            SqlParameter Subscriber_ID = new SqlParameter("@Subscriber_ID", UpdateDevice.SUBSCRIBER_ID);
            SqlParameter STAKEHOLDER_ID = new SqlParameter("@STAKEHOLDER_ID", UpdateDevice.STAKEHOLDER_ID);
            SqlParameter Usage_Duration = new SqlParameter("@USAGE_DURATION", UpdateDevice.USAGE_DURATION);
            SqlParameter Monthly_Duration = new SqlParameter("@MONTHLY_DURATION_PER_HR", UpdateDevice.MONTHLY_DURATION_PER_HR);
            SqlParameter Amount_Consumption = new SqlParameter("@AMOUNT_CONSUMPTION_PER_HR", UpdateDevice.AMOUNT_CONSUMPTION_PER_HR);

            _context.Database.ExecuteSqlRaw("dbo.Update_Billings @Billings_Id,@DEviceId,@Subscriber_ID,@STAKEHOLDER_ID, @USAGE_DURATION,@MONTHLY_DURATION_PER_HR,@AMOUNT_CONSUMPTION_PER_HR",
             Billings_Id,Device_Id,Subscriber_ID, STAKEHOLDER_ID, Usage_Duration, Monthly_Duration,
            Amount_Consumption);

            //var deviceInfo = _context.deviceInfos.Attach(UpdateDevice);
            //UpdateDevice.Transaction_Date = DateTime.UtcNow;
            //deviceInfo.State = EntityState.Modified;
            //_context.SaveChanges();
            return UpdateDevice;
        }

        //TARRIFS
        public TarrifInfoModel AddTarrif(TarrifInfoModel newTarrif)
        {

            SqlParameter STAKEHOLDER_ID = new SqlParameter("@STAKEHOLDER_ID", newTarrif.STAKEHOLDER_ID);
            SqlParameter TarrifAmount = new SqlParameter("@TarrifAmount", newTarrif.TarrifAmount);
            SqlParameter TypeOfHouse = new SqlParameter("@TypeOfHouse", newTarrif.TypeOfHouse);
            //SqlParameter Amount_Consumption = new SqlParameter("@AMOUNT_CONSUMPTION_PER_HR", newDevice.AMOUNT_CONSUMPTION_PER_HR);

            _context.Database.ExecuteSqlRaw("dbo.Insert_Select_Tarrifs @STAKEHOLDER_ID, @TarrifAmount,@TypeOfHouse",
             STAKEHOLDER_ID, TarrifAmount, TypeOfHouse);
            return newTarrif;

        }

        public TarrifInfoModel UpdateTarrif(TarrifInfoModel UpdateTarrif)
        {
            
            SqlParameter Tarrif_Id = new SqlParameter("@Tarrif_Id", UpdateTarrif.Tarrif_Id);
            SqlParameter STAKEHOLDER_ID = new SqlParameter("@STAKEHOLDER_ID", UpdateTarrif.STAKEHOLDER_ID);
            SqlParameter TarrifAmount = new SqlParameter("@TarrifAmount", UpdateTarrif.TarrifAmount);
            SqlParameter TypeOfHouse = new SqlParameter("@TypeOfHouse", UpdateTarrif.TypeOfHouse);
            //SqlParameter Amount_Consumption = new SqlParameter("@AMOUNT_CONSUMPTION_PER_HR", UpdateDevice.AMOUNT_CONSUMPTION_PER_HR);

            _context.Database.ExecuteSqlRaw("dbo.Update_TarrifDetails @Tarrif_Id,@STAKEHOLDER_ID, @TarrifAmount,@TypeOfHouse",
             Tarrif_Id,STAKEHOLDER_ID, TarrifAmount, TypeOfHouse);
           
            return UpdateTarrif;
        }

        public TarrifInfoModel GetTarrifs(int id)
        {
            SqlParameter parameter = new SqlParameter("@Id", id);
            return _context.tarrifInfos
                .FromSqlRaw<TarrifInfoModel>("dbo.GetTarrifs_ById @Id", parameter)
                .ToList().FirstOrDefault();                      
        }

        public paymentInfo GetPaymentInfo(int id)
        {
            SqlParameter parameter = new SqlParameter("@Subscriber_ID", id);
            return _context.paymentInfos
                .FromSqlRaw<paymentInfo>("dbo.GetPayments_BySubscriberId @Subscriber_ID", parameter)
                .ToList().FirstOrDefault();
        }

        public void UpdatePaymentDetail(paymentUpdateInfo UpdatePayment)
        {
            //_context.Database.ExecuteSqlRaw("dbo.Update_Payment_Billings { 0},{ 1},{ 2},{ 3},{ 4}",
            //    UpdatePayment.Billings_Id, UpdatePayment.SUBSCRIBER_ID,
            //    UpdatePayment.STAKEHOLDER_ID, UpdatePayment.AMOUNT_PAID,
            //    UpdatePayment.STATUS_OF_PAYMENT );
            try
            {   
                SqlParameter AMOUNT_PAID = new SqlParameter("@AMOUNT_PAID", UpdatePayment.AMOUNT_PAID);
                SqlParameter Billings_Id = new SqlParameter("@BillingsID", UpdatePayment.Billings_Id);
                SqlParameter STAKEHOLDER_ID = new SqlParameter("@STAKEHOLDER_ID", UpdatePayment.STAKEHOLDER_ID);
                SqlParameter STATUS_OF_PAYMENT = new SqlParameter("@STATUS_OF_PAYMENT", UpdatePayment.STATUS_OF_PAYMENT);
                SqlParameter SUBSCRIBER_ID = new SqlParameter("@Subscriber_ID", UpdatePayment.SUBSCRIBER_ID);
                
                _context.Database.ExecuteSqlRaw("dbo.Update_Payment_Billings @BillingsID,@Subscriber_ID,@STAKEHOLDER_ID,@AMOUNT_PAID,@STATUS_OF_PAYMENT ",
                 Billings_Id, SUBSCRIBER_ID, STAKEHOLDER_ID, AMOUNT_PAID, STATUS_OF_PAYMENT);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
          
            }

        }

    }
}
