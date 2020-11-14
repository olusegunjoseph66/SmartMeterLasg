using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartMeterLibServices.Model;

namespace SmartMeterWebApp.Pages.Subscribers
{
    public class CustomStripePayModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string stripePublishableKey { get; set; } = "pk_test_51HQIUXIaMonsw6WHxYrDbLUWfAFG2QYUncbu7S2Bbf5SVewZz0P0mQD2bnXUj39R9xIJKNGAFXpxLBCzU4S58nOf00cv3nENRA";

        private readonly IHttpClientFactory _httpClientFactory;

        public CustomStripePayModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty(SupportsGet = true)]
        public string stripeEmail { get; set; } 

            public IActionResult OnGet()
        {
            //string stripePublishableKey = ConfigurationManager.AppSettings["Publishablekey"];
            var model = new CustomViewModel() { StripePublishableKey = stripePublishableKey, StripeEmail = HttpContext.User.Identity.Name };
            return Page();
        }

       
        
        public IActionResult OnPost(CustomViewModel customViewModel)
        {
            System.Diagnostics.Debug.WriteLine(customViewModel);
            return RedirectToPage("/Subscribers/Confirmation", "OnGet");

        }
                
    }
}
