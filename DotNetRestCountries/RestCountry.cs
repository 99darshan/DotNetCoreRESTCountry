using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotNetRestCountries
{
    public class RestCountry
    {
        private static HttpClient _client = new HttpClient();
        private const string _BASEURL = "https://restcountries.eu/rest/v2/";

        /// <summary>
        /// Private method which send HttpGet requests to the RestCountries API
        /// and Deserializes the JSON response to a List of Country objects.
        /// </summary>
        /// <param name="requestUrl">Restcountries api Endpoint</param>
        /// <returns></returns>
        private static List<Country> _GetCountriesListResponse(string requestUrl)
        {
            requestUrl = _BASEURL + requestUrl;
            HttpResponseMessage res = null;
            try
            {
                res = _client.GetAsync(requestUrl).Result;
                if(res.StatusCode == HttpStatusCode.OK)
                {
                    string resBody = res.Content.ReadAsStringAsync().Result;

                    List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(resBody);
                    return countries;             
                }
                else
                {
                    throw new HttpRequestException("Invalid Response code, API didn't respond with status code 200.");
                }

            }
            catch(Exception e)
            {
                throw;
            }
            finally
            {
                if (res != null) res.Dispose();
            }
        }

        //TODO: Filter results

        /// <summary>
        /// Get results for all countries 
        /// </summary>
        public static void All()
        {
            _GetCountriesListResponse("all");
        }
        
        /// <summary>
        /// Search by country name. It can be native or partial name.
        /// </summary>
        /// <param name="name">full or partial name of a country</param>
        public static void Name(string name)
        {
            string endPoint = "name/" + name;
            _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Search by country full name
        ///restcountries.eu/rest/v2/name/{name}?fullText=true
        /// </summary>
        /// <param name="fullName">full name of country</param>
        public static void FullName(string fullName)
        {
            string endPoint = "name/" + fullName + "?fullText=true";
            _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Search by ISO 3166-1 2-letter or 3-letter country code
        /// </summary>
        /// <param name="code">ISO 3166-1 2-letter or 3-letter country code</param>
        public static void Code(string code)
        {
            string endPoint = "alpha/" + code;
            _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Search by list of ISO 3166-1 2-letter or 3-letter country codes //restcountries.eu/rest/v2/alpha?codes={code};{code};{code}
        /// </summary>
        /// <param name="codes">List of 2-letter or 3-letter country codes</string></param>
        public static void ListOfCodes(List<string> codes)
        {
            string endPoint = "alpha?codes=";
            for(var i = 0; i < codes.Count; i++)
            {
                if (i == codes.Count - 1) endPoint += codes[i];
                endPoint += codes[i] + ";";
            }
            _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Search by ISO 4217 currency code
        /// </summary>
        /// <param name="currencyCode">Valid ISO 4217 currency code</param>
        public static void Currency(string currencyCode)
        {
            string endPoint = "currency/" + currencyCode;
            _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Search by ISO 639-1 language code.
        /// </summary>
        /// <param name="langCode">Valid ISO 639-1 language code</param>
        public static void Language(string langCode)
        {
            string endPoint = "lang/" + langCode;
            _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Search by capital city
        /// </summary>
        /// <param name="capital">capita city name</param>
        public static void Capital(string capital)
        {
            string endPoint = "capital/" + capital;
            _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Search by calling code
        /// </summary>
        /// <param name="callingCode"></param>
        public static void CallingCode(int callingCode)
        {
            string endPoint = "callingcode/" + callingCode;
            _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Search by region: Africa, Americas, Asia, Europe, Oceania
        /// </summary>
        /// <param name="region">region name</param>
        public static void Region(string region)
        {
            string endPoint = "region/" + region;
            _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Search by regional bloc: EU, EFTA, CARICOM, PA, AU, USAN, EEU, AL, ASEAN, CAIS, CEFTA, NAFTA, SAARC
        /// </summary>
        /// <param name="bloc"></param>
        public static void RegionalBloc(string bloc)
        {
            string endPoint = "regionalbloc/" + bloc;
            _GetCountriesListResponse(endPoint);
        }

    }
}
