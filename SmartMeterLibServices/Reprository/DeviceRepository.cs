using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SmartMeterLibServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartMeterLibServices.Reprository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly AppDbContext _context;

        public DeviceRepository(AppDbContext context)
        {
            _context = context;
        }
        public DeviceInfoModel Add(DeviceInfoModel newDevice)
        {

            //SqlParameter Subscriber_ID  =        new SqlParameter("@Subscriber_ID", newDevice.Subscriber_ID);
            //SqlParameter STAKEHOLDER_ID =        new SqlParameter("@STAKEHOLDER_ID", newDevice.STAKEHOLDER_ID);
            //SqlParameter Imei_Number    =        new SqlParameter("@Imei_Number", newDevice.Imei_Number);
            //SqlParameter Device_Name    =        new SqlParameter("@Device_Name", newDevice.Device_Name);
            //SqlParameter Address        =        new SqlParameter("@Address", newDevice.Address);
            //SqlParameter Bus_Stop       =        new SqlParameter("@Bus_Stop", newDevice.Bus_Stop);
            //SqlParameter State          =         new SqlParameter("@State", newDevice.State);
            //SqlParameter Country        =        new SqlParameter("@Country", newDevice.Country);
            //SqlParameter Lga            =        new SqlParameter("@Lga", newDevice.Lga);
            //SqlParameter Verify_Address =        new SqlParameter("@Verify_Address", newDevice.Verify_Address);
            //SqlParameter Delivery_Flag  =        new SqlParameter("@Delivery_Flag", newDevice.Delivery_Flag);
            //SqlParameter Flag_Operation =        new SqlParameter("@Flag_Operation", newDevice.Flag_Operation);
      
            //_context.Database.ExecuteSqlRaw("dbo.Insert_Select_AllDevices @Subscriber_ID,@STAKEHOLDER_ID, @Imei_Number,@Device_Name,@Address,@Bus_Stop,@State,@Country,@Lga,@Verify_Address,@Delivery_Flag,@Flag_Operation",
            _context.Database.ExecuteSqlRaw("dbo.Insert_Select_AllDevices {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}",

           newDevice.Subscriber_ID, newDevice.STAKEHOLDER_ID, newDevice.Imei_Number, newDevice.Device_Name,newDevice.IOTDEviceId,
            newDevice.TypeOfHouseStatus,newDevice.Address, newDevice.Bus_Stop, newDevice.State, newDevice.Country, newDevice.Lga, newDevice.Verify_Address,
            newDevice.Delivery_Flag, newDevice.Flag_Operation);
            return newDevice;

        }

        public IEnumerable<DeviceCount> CountByDevices()
        {
            return _context.deviceInfos.GroupBy(e => e.Device_Name)
                            .Select(g => new DeviceCount()
                            {
                                //Device_Name = g.Key.Value,
                                Device_Name = g.Key,
                                Count = g.Count()
                            }).ToList();
        }

        public IEnumerable<DeviceInfoModel> CountDevices(DeviceInfoModel device)
        {
            throw new NotImplementedException();
        }

        public DeviceInfoModel Delete(int id)
        {

            SqlParameter Device_ID = new SqlParameter("@Device_ID", id);
            SqlParameter Device_IDs = new SqlParameter("@Id", id);

            DeviceInfoModel deviceToDelete = _context.deviceInfos
                .FromSqlRaw<DeviceInfoModel>("dbo.GetDevices_ById @Id", Device_IDs)
                .ToList().FirstOrDefault();


            if (deviceToDelete != null)
            {
                 _context.Database.ExecuteSqlRaw("dbo.Delete_Devices @Device_ID", Device_ID);
            }
            return deviceToDelete;
        }
        public IEnumerable<DeviceInfoModel> GetAllDevicesStack(DateTime startDate, DateTime endDate, string STAKEHOLDER_ID)
        {
            SqlParameter parameter1 = new SqlParameter("@startDate", startDate);
            SqlParameter parameter2 = new SqlParameter("@endDate", endDate);
            SqlParameter parameter3 = new SqlParameter("@STAKEHOLDER_ID", STAKEHOLDER_ID);
            return _context.deviceInfos
                .FromSqlRaw<DeviceInfoModel>("dbo.Select_AllDevicesStack @startDate, @endDate,@STAKEHOLDER_ID", parameter1, parameter2, parameter3)
                .ToList();
            //return _context.subscriberInfos.ToList();
        }
        public IEnumerable<DeviceInfoModel> GetAllDevices(DateTime startDate, DateTime endDate,int Subscriber_ID)
        {
            SqlParameter parameter1 = new SqlParameter("@startDate", startDate);
            SqlParameter parameter2 = new SqlParameter("@endDate", endDate);
            SqlParameter parameter3 = new SqlParameter("@Subscriber_ID", Subscriber_ID);
            return _context.deviceInfos
                .FromSqlRaw<DeviceInfoModel>("dbo.Select_AllDevices @startDate, @endDate,@Subscriber_ID", parameter1, parameter2,parameter3)
                .ToList();
            //return _context.subscriberInfos.ToList();
        }

        public DeviceInfoModel GetDevice(int id)
        {
            SqlParameter parameter = new SqlParameter("@Id", id);
            return _context.deviceInfos
                .FromSqlRaw<DeviceInfoModel>("dbo.GetDevices_ById @Id", parameter)
                .ToList().FirstOrDefault();
            //return _context.subscriberInfos.FromSqlRaw<SubscriberInfoModel>("GetDevices_ById {0}",id)
            //    .ToList().FirstOrDefault();           
        }

        public IEnumerable<DeviceInfoModel> Search(string searchTerm,DateTime StartDate, DateTime EndDate)
        {
            //if (string.IsNullOrEmpty(searchTerm))
            //{
            //    return _context.deviceInfos;
            //}

            return _context.deviceInfos.Where(e => e.Device_ID.ToString().Contains(searchTerm) ||
                                            (e.Device_Name.Contains(searchTerm)) ||
                                            (e.Transaction_Date.Equals(StartDate) &&
                                            e.Transaction_Date.Equals(EndDate)));
        }

        public DeviceInfoModel Update(DeviceInfoModel UpdateDevice)
        {
            SqlParameter Device_ID = new SqlParameter("@Device_ID", UpdateDevice.Device_ID);
            SqlParameter Subscriber_ID = new SqlParameter("@Subscriber_ID", UpdateDevice.Subscriber_ID);
            SqlParameter STAKEHOLDER_ID = new SqlParameter("@STAKEHOLDER_ID", UpdateDevice.STAKEHOLDER_ID);
            SqlParameter Imei_Number = new SqlParameter("@Imei_Number", UpdateDevice.Imei_Number);
            SqlParameter Device_Name = new SqlParameter("@Device_Name", UpdateDevice.Device_Name);
            SqlParameter IOTDEviceId = new SqlParameter("@IOTDEviceId", UpdateDevice.IOTDEviceId);
            SqlParameter TypeOfHouseStatus = new SqlParameter("@TypeOfHouseStatus", UpdateDevice.TypeOfHouseStatus);
            SqlParameter Address = new SqlParameter("@Address", UpdateDevice.Address);
            SqlParameter Bus_Stop = new SqlParameter("@Bus_Stop", UpdateDevice.Bus_Stop);
            SqlParameter State = new SqlParameter("@State", UpdateDevice.State);
            SqlParameter Lga = new SqlParameter("@Lga", UpdateDevice.Lga);
            SqlParameter Verify_Address = new SqlParameter("@Verify_Address", UpdateDevice.Verify_Address);
            SqlParameter Delivery_Flag = new SqlParameter("@Delivery_Flag", UpdateDevice.Delivery_Flag);
            SqlParameter Flag_Operation = new SqlParameter("@Flag_Operation", UpdateDevice.Flag_Operation);
            SqlParameter Country = new SqlParameter("@Country", UpdateDevice.Country);

            _context.Database.ExecuteSqlRaw("dbo.Update_DevicesDetails @Device_ID,@Subscriber_ID,@STAKEHOLDER_ID, @Imei_Number,@Device_Name,@IOTDEviceId,@TypeOfHouseStatus,@Address,@Bus_Stop,@State,@Lga,@Verify_Address,@Delivery_Flag,@Flag_Operation,@Country",
            Device_ID,Subscriber_ID, STAKEHOLDER_ID, Imei_Number, Device_Name, IOTDEviceId,
            TypeOfHouseStatus, Address, Bus_Stop, State, Lga, Verify_Address,
            Delivery_Flag, Flag_Operation, Country);

            //var deviceInfo = _context.deviceInfos.Attach(UpdateDevice);
            //UpdateDevice.Transaction_Date = DateTime.UtcNow;
            //deviceInfo.State = EntityState.Modified;
            //_context.SaveChanges();
            return UpdateDevice;
        }
        public IEnumerable<IOTDEviceDetailModel> GetAllIOTDEviceDetailsStack( string STAKEHOLDER_ID)
        {
           
            SqlParameter parameter3 = new SqlParameter("@STAKEHOLDER_ID", STAKEHOLDER_ID);
            return _context.IOTDEviceDetail
                .FromSqlRaw<IOTDEviceDetailModel>("dbo.Select_AllIOTDEviceDetailsStack @STAKEHOLDER_ID",  parameter3)
                .ToList();
            //return _context.subscriberInfos.ToList();
        }
        public IEnumerable<IOTDEviceDetailModel> GetAllIOTDEviceDetails( int Subscriber_ID)
        {
            
            SqlParameter parameter3 = new SqlParameter("@Subscriber_ID", Subscriber_ID);
            return _context.IOTDEviceDetail
                .FromSqlRaw<IOTDEviceDetailModel>("dbo.Select_AllIOTDEviceDetails @Subscriber_ID", parameter3)
                .ToList();
            //return _context.subscriberInfos.ToList();
        }
        public IOTDEviceDetailModel AddIOTDEviceDetails(IOTDEviceDetailModel newIOTDEvice)
        {

            SqlParameter Subscriber_ID = new SqlParameter("@Subscriber_ID", newIOTDEvice.Subscriber_ID);
            SqlParameter STAKEHOLDER_ID = new SqlParameter("@STAKEHOLDER_ID", newIOTDEvice.STAKEHOLDER_ID);
             SqlParameter ScopeID = new SqlParameter("@ScopeID", newIOTDEvice.ScopeID);
            SqlParameter PrimaryKey = new SqlParameter("@PrimaryKey", newIOTDEvice.PrimaryKey);
            SqlParameter IOTDEviceId = new SqlParameter("@IOTDEviceId", newIOTDEvice.IOTDEviceId);
           

            _context.Database.ExecuteSqlRaw("dbo.Insert_Select_AllIOTDEviceDetails @Subscriber_ID,@STAKEHOLDER_ID, @ScopeID,@PrimaryKey,@IOTDEviceId",
             Subscriber_ID, STAKEHOLDER_ID, ScopeID, PrimaryKey,
            IOTDEviceId);
            return newIOTDEvice;

        }
        public IOTDEviceDetailModel GetIOTDEviceDetailsBySubscriberId(int id)
        {
            SqlParameter parameter = new SqlParameter("@Subscriber_ID", id);
            return _context.IOTDEviceDetail
                .FromSqlRaw<IOTDEviceDetailModel>("dbo.GetIOTDEviceDetails_BySubscriberId @Subscriber_ID", parameter)
                .ToList().FirstOrDefault();
        }
        public IOTDEviceDetailModel GetIOTDEviceDetailsById(int id)
        {
            SqlParameter parameter = new SqlParameter("@IOTDEviceDetail_Id", id);
            return _context.IOTDEviceDetail
                .FromSqlRaw<IOTDEviceDetailModel>("dbo.GetIOTDEviceDetails_ById @IOTDEviceDetail_Id", parameter)
                .ToList().FirstOrDefault();
        }
        public IOTDEviceDetailModel UpdateIOTDEviceDetails(IOTDEviceDetailModel UpdateDevice)
        {
            SqlParameter Device_ID = new SqlParameter("@IOTDEviceDetail_Id", UpdateDevice.IOTDEviceDetail_Id);
            SqlParameter Subscriber_ID = new SqlParameter("@Subscriber_ID", UpdateDevice.Subscriber_ID);
            SqlParameter STAKEHOLDER_ID = new SqlParameter("@STAKEHOLDER_ID", UpdateDevice.STAKEHOLDER_ID);
            SqlParameter ScopeID = new SqlParameter("@ScopeID", UpdateDevice.ScopeID);
            SqlParameter PrimaryKey = new SqlParameter("@PrimaryKey", UpdateDevice.PrimaryKey);
            SqlParameter IOTDEviceId = new SqlParameter("@IOTDEviceId", UpdateDevice.IOTDEviceId);
           

            _context.Database.ExecuteSqlRaw("dbo.Update_IOTDevicesDetails @IOTDEviceDetail_Id,@Subscriber_ID,@STAKEHOLDER_ID, @ScopeID,@PrimaryKey,@IOTDEviceId",
            Device_ID, Subscriber_ID, STAKEHOLDER_ID, ScopeID, PrimaryKey,
            IOTDEviceId);

            
            return UpdateDevice;
        }
    }

}
