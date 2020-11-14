using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartMeterLibServices.Reprository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartMeterWebApp
{
    public class SetMeterUsageNotificationBackgroundService : BackgroundService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IServiceProvider _serviceProvider;

        private readonly IEmailSender _emailsender;
    

        public SetMeterUsageNotificationBackgroundService(IServiceProvider serviceProvider,
           IEmailSender emailSender)
        {
            _serviceProvider = serviceProvider;
           
            _emailsender = emailSender;
           
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var scope = _serviceProvider.CreateScope();
                var subscribergprocessor = scope.ServiceProvider.GetRequiredService<ISubscriberRepository>();

                //get all the subscriber from the db 
                var subscribers = subscribergprocessor.GetAllSubscribers();

                //loop thru all the subscriber and send it to notification
                foreach (var subscriber in subscribers)
                {

                   var billingprocessor =  scope.ServiceProvider.GetRequiredService<IBillingRepository>();


                    //var notify = billingprocessor.GetAllNotification(subscriber.Subscriber_ID).FirstOrDefault();
                    //if (notify != null)                       
                    //    await _emailsender.SendEmailAsync(subscriber.Email, "Meter Usage Notification", $"total amount consumed is : {notify.AMOUNT_CONSUMPTION_PER_HR}");
                }
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
