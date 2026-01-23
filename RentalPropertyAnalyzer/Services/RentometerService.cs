using Microsoft.Extensions.Configuration;
using RestSharp;

namespace RentalPropertyAnalyzer.Services
{
    public class RentometerService
    {
        public RentometerService() 
        {
            
        }

        public void RentometerAPICall (string address, int bedrooms)
        {
            string URL = $"https://www.rentometer.com/api/v1/summary?api_key=Zhn6xWTMs8CSegXXEkv7xQ&address={address}&bedrooms={bedrooms}";
            var client = new RestClient(URL);

            var request = new RestRequest();
            request.AddHeader("Cookie", "ahoy_visit=cd3a1423-c7d0-4902-a4b5-44a957bed8e3; ahoy_visitor=88443c25-044c-4781-8c24-1f9ce6387ef0");
            RestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                try
                {
                    string jsonReturned = response.Content ?? string.Empty;


                }
                catch
                {

                }

            }
            else
            {
               
            }
        }

    }
}
