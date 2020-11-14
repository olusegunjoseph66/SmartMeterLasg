using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartMeterLibServices;
using SmartMeterLibServices.Model;

namespace SmartMeterWebApp.Pages.Adminstrators
{
    public class IndexModel : PageModel
    {
        private readonly SmartMeterLibServices.AppDbContext _context;

        public IndexModel(SmartMeterLibServices.AppDbContext context)
        {
            _context = context;
        }

        public IList<DeviceInfoModel> DeviceInfoModel { get;set; }

        public async Task OnGetAsync()
        {
            DeviceInfoModel = await _context.deviceInfos.ToListAsync();
        }
    }
}
