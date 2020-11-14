using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartMeterLibServices;
using SmartMeterLibServices.Model;

namespace SmartMeterWebApp.Pages.Adminstrators
{
    public class EditModel : PageModel
    {
        private readonly SmartMeterLibServices.AppDbContext _context;

        public EditModel(SmartMeterLibServices.AppDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DeviceInfoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceInfoModelExists(DeviceInfoModel.Device_ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DeviceInfoModelExists(int id)
        {
            return _context.deviceInfos.Any(e => e.Device_ID == id);
        }
    }
}
