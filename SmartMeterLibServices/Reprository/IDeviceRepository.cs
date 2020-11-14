using SmartMeterLibServices.Model;
using System;
using System.Collections.Generic;

namespace SmartMeterLibServices.Reprository
{
    public interface IDeviceRepository
    {
        IOTDEviceDetailModel GetIOTDEviceDetailsBySubscriberId(int id);
        IEnumerable<DeviceInfoModel> GetAllDevicesStack(DateTime startDate, DateTime endDate, string STAKEHOLDER_ID);
        DeviceInfoModel Add(DeviceInfoModel newDevice);
        IEnumerable<DeviceInfoModel> CountDevices(DeviceInfoModel device);
        IEnumerable<DeviceCount> CountByDevices();
        DeviceInfoModel Delete(int id);
        IEnumerable<DeviceInfoModel> GetAllDevices(DateTime startDate, DateTime endDate, int Subscriber_ID);
        DeviceInfoModel GetDevice(int id);
        IEnumerable<DeviceInfoModel> Search(string searchTerm, DateTime StartDate, DateTime EndDate);
        DeviceInfoModel Update(DeviceInfoModel UpdateDevice);
        IEnumerable<IOTDEviceDetailModel> GetAllIOTDEviceDetailsStack(string STAKEHOLDER_ID);
        IEnumerable<IOTDEviceDetailModel> GetAllIOTDEviceDetails(int Subscriber_ID);
        IOTDEviceDetailModel AddIOTDEviceDetails(IOTDEviceDetailModel newIOTDEvice);
        IOTDEviceDetailModel GetIOTDEviceDetailsById(int id);
        IOTDEviceDetailModel UpdateIOTDEviceDetails(IOTDEviceDetailModel UpdateDevice);
      }
}