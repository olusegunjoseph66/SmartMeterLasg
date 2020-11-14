using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeterLibServices.ExternalServices
{
    public static class SMS_Services
    {
        public static async Task<string> SendSMS(string number, string msg, IConfiguration configuration)
        {

            //refer to parameters to complete correct url string
            await Task.Run(() =>
            {
                String result;
                //string apiKey = configuration["SMS:APIKEY"];
                string apiKey = "Dd8YV+w1dAc-t8bT3SohCRV8BV30RrUhUV10h61n4p";


                string numbers = number; // in a comma seperated list
                string message = msg;
                //string sender = configuration["SMS:Sender"];
                string sender = "Smart Meter";


                String url = "https://api.txtlocal.com/send/?apikey=" + apiKey + "&numbers=" + numbers + "&message=" + message + "&sender=" + sender;
                StreamWriter myWriter = null;
                HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

                objRequest.Method = "POST";
                objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
                objRequest.ContentType = "application/x-www-form-urlencoded";
                try
                {
                    myWriter = new StreamWriter(objRequest.GetRequestStream());
                    myWriter.Write(url);
                }
                catch (Exception e)
                {
                    return e.Message;
                }
                finally
                {
                    myWriter.Close();
                }

                HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                    // Close and clean up the StreamReader
                    sr.Close();
                }
                return result;

            });

            return string.Empty;


        }
    }
}

