using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;

namespace DotNetRestCountries
{
    class RestCountry
    {
        private static HttpClient _client = new HttpClient();
        private const string _BASEURL = "https://restcountries.eu/rest/v2/";

        // comments documentation, auto generate
        public static async void All()
        {
            string requestUrl = _BASEURL + "all";

            try
            {
                HttpResponseMessage res = await _client.GetAsync(requestUrl);
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    

                }

            }
            catch (Exception e)
            {

            }

        }
    }
}
