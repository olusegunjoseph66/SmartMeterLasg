using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartMeterLibServices.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMeterLibServices
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<UserInfoModel> userInfos { get; set; }
        public DbSet<SubscriberInfoModel> subscriberInfos { get; set; }
        public DbSet<StackholderInfoModel> stackholderInfoModels { get; set; }
        public DbSet<DeviceInfoModel> deviceInfos { get; set; }
        public DbSet<BillingsInfoModel> billingsInfos { get; set; }
        public DbSet<IOTDeviceSmartMeter> IOTDeviceSmartMeter { get; set; }
        public DbSet<IOTDEviceDetailModel> IOTDEviceDetail { get; set; }
        public DbSet<TarrifInfoModel> tarrifInfos { get; set; }
#pragma warning disable CS0618 // Type or member is obsolete
        public DbQuery<BillingsRealData> billingsDatas { get; set; }
        public DbQuery<IOTUsageSmartMeter> iotusageSmartM { get; set; }
        public DbQuery<IOTUsagemontSmartMeter> iotusagemontSmartM { get; set; }
        public DbQuery<MobileBillingModel> mobileBillings { get; set; }
        public DbQuery<BillingDataA> billingsDataAs { get; set; }
        public DbQuery<NotificatnModel> Notificatns { get; set; }
        public DbQuery<BillingDataS> billingsDataSure { get; set; }
        public DbQuery<MobileBillingDash> Dashashbord { get; set; }
        public DbQuery<paymentInfo> paymentInfos { get; set; }
#pragma warning restore CS0618 // Type or member is obsolete

    }
}
