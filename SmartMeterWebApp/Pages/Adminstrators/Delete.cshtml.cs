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
    public class DeleteModel : PageModel
    {
        private readonly SmartMeterLibServices.AppDbContext _context;

        public DeleteModel(SmartMeterLibServices.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DeviceInfoModel DeviceInfoModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DeviceInfoModel = await _context.deviceInfos.FirstOrDefaultAsync(m => m.Device_ID == id);

            if (DeviceInfoModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DeviceInfoModel = await _context.deviceInfos.FindAsync(id);

            if (DeviceInfoModel != null)
            {
                _context.deviceInfos.Remove(DeviceInfoModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
