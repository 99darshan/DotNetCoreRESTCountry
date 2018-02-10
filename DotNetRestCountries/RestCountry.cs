using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotNetRestCountries
{
    public static class RestCountry
    {

        private const string _BASEURL = "https://restcountries.eu/rest/v2/";

        /// <summary>
        /// Private method which send HttpGet requests to the RestCountries API
        /// and Deserializes the JSON response to a List of Country objects.
        /// </summary>
        /// <param name="requestUrl">Restcountries api Endpoint</param>
        /// <returns>List of Country objects</returns>
        private static List<Country> _GetCountriesListResponse(string requestUrl)
        {
            HttpClient client = new HttpClient();
            requestUrl = _BASEURL + requestUrl;
            HttpResponseMessage res = null;
            try
            {
                res = client.GetAsync(requestUrl).Result;
                if(res.StatusCode == HttpStatusCode.OK)
                {
                    string resBody = res.Content.ReadAsStringAsync().Result;
                    //List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(resBody);
                    List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(resBody, new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
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

        /// <summary>
        /// Private ASYNC method which send HttpGet requests to the RestCountries API
        /// and Deserializes the JSON response to a List of Country objects.
        /// </summary>
        /// <param name="requestUrl">Restcountries api Endpoint</param>
        /// <returns>List of Country objects</returns>
        private async static Task<List<Country>> _GetCountriesListResponseAsync(string requestUrl)
        {
            HttpClient client = new HttpClient();
            requestUrl = _BASEURL + requestUrl;
            HttpResponseMessage res = null;
            try
            {
                res = await client.GetAsync(requestUrl);
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    string resBody = await res.Content.ReadAsStringAsync();
                    //List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(resBody);
                    List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(resBody, new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    return countries;
                }
                else
                {
                    throw new HttpRequestException("Invalid Response code, API didn't respond with status code 200.");
                }

            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (res != null) res.Dispose();
            }
        }

        private static string _GetEndpointWithFilters(string endPoint, List<string> filters)
        {
            endPoint += "fields=";
            for(int i = 0; i < filters.Count; i++)
            {
                endPoint += filters[i] + ";";
            }
            return endPoint;
        }

        /// <summary>
        /// Get results for all countries
        /// </summary>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns>List of Country objects</returns>
        public static List<Country> All(List<string> filters = null)
        {
            string endPoint = "all";
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Async method to Get results for all countries
        /// </summary>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns>Task<T>, T = List of Country objects</returns>
        public async static Task<List<Country>> AllAsync(List<string> filters = null)
        {
            string endPoint = "all";
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return await  _GetCountriesListResponseAsync(endPoint);
        }

        /// <summary>
        /// Search by Country name. It can be native or partial name.
        /// </summary>
        /// <param name="name">full or partial name</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns>List of Country objects</returns>
        public static List<Country> Name(string name, List<string> filters = null)
        {
            string endPoint = "name/" + name;
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Async method to Search by Country name. It can be native or partial name.
        /// </summary>
        /// <param name="name">full or partial name</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns></returns>
        public async static Task<List<Country>> NameAsync(string name, List<string> filters = null)
        {
            string endPoint = "name/" + name;
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return await _GetCountriesListResponseAsync(endPoint);
        }

        /// <summary>
        /// Search by country full name
        /// </summary>
        /// <param name="fullName">full name</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns>List of Country objects</returns>
        public static List<Country> FullName(string fullName, List<string> filters = null)
        {
            string endPoint = "name/" + fullName + "?fullText=true";
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint +"&", filters);
            return _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Async method to Search by country full name
        /// </summary>
        /// <param name="fullName">full name</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns></returns>
        public async static Task<List<Country>> FullNameAsync(string fullName, List<string> filters = null)
        {
            string endPoint = "name/" + fullName + "?fullText=true";
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "&", filters);
            return await _GetCountriesListResponseAsync(endPoint);
        }

        /// <summary>
        /// Search by ISO 3166-1 2-letter or 3-letter country code
        /// </summary>
        /// <param name="code">ISO 3166-1 2-letter or 3-letter country code</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns>List of Country objects</returns>
        public static List<Country> Code(string code, List<string> filters = null)
        {
            string endPoint = "alpha/" + code;
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Async method to Search by ISO 3166-1 2-letter or 3-letter country code
        /// </summary>
        /// <param name="code">ISO 3166-1 2-letter or 3-letter country code</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns></returns>
        public async static Task<List<Country>> CodeAsync(string code, List<string> filters = null)
        {
            string endPoint = "alpha/" + code;
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return await _GetCountriesListResponseAsync(endPoint);
        }

        /// <summary>
        /// Search by list of ISO 3166-1 2-letter or 3-letter country codes //restcountries.eu/rest/v2/alpha?codes={code};{code};{code}
        /// </summary>
        /// <param name="codes"> List of ISO 3166-1 2-letter or 3-letter country codes</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns></returns>
        public static List<Country> ListOfCodes(List<string> codes, List<string> filters = null)
        {
            string endPoint = "alpha?codes=";
            for (var i = 0; i < codes.Count; i++) endPoint += codes[i] + ";";
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "&", filters);
            return _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Async method to Search by list of ISO 3166-1 2-letter or 3-letter country codes //restcountries.eu/rest/v2/alpha?codes={code};{code};{code}
        /// </summary>
        /// <param name="codes"> List of ISO 3166-1 2-letter or 3-letter country codes</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns></returns>
        public async static Task<List<Country>> ListOfCodesAsync(List<string> codes, List<string> filters = null)
        {
            string endPoint = "alpha?codes=";
            for (var i = 0; i < codes.Count; i++) endPoint += codes[i] + ";";
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "&", filters);
            return await _GetCountriesListResponseAsync(endPoint);
        }


        /// <summary>
        /// Search by ISO 4217 currency code
        /// </summary>
        /// <param name="currencyCode">Valid ISO 4217 currency code</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns>List of Country objects</returns>
        public static List<Country> Currency(string currencyCode, List<string> filters = null)
        {
            string endPoint = "currency/" + currencyCode;
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Async method to Search by ISO 4217 currency code
        /// </summary>
        /// <param name="currencyCode">Valid ISO 4217 currency code</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns></returns>
        public async static Task<List<Country>> CurrencyAsync(string currencyCode, List<string> filters = null)
        {
            string endPoint = "currency/" + currencyCode;
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return await _GetCountriesListResponseAsync(endPoint);
        }


        /// <summary>
        /// Search by ISO 639-1 language code.
        /// </summary>
        /// <param name="langCode">valid ISO 639-1 lang code</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns>List of Country objects</returns>
        public static List<Country> Language(string langCode, List<string> filters = null)
        {
            string endPoint = "lang/" + langCode;
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Async method to Search by ISO 639-1 language code.
        /// </summary>
        /// <param name="langCode">valid ISO 639-1 lang code</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns></returns>
        public async static Task<List<Country>> LanguageAsync(string langCode, List<string> filters = null)
        {
            string endPoint = "lang/" + langCode;
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return await _GetCountriesListResponseAsync(endPoint);
        }

        /// <summary>
        /// Sarch by capital city
        /// </summary>
        /// <param name="capital">capital city name</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns>List of Country objects</returns>
        public static List<Country> Capital(string capital, List<string> filters = null)
        {
            string endPoint = "capital/" + capital;
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters); 
            return _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Async method to Sarch by capital city
        /// </summary>
        /// <param name="capital">capital city name</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns> </returns>
        public async static Task<List<Country>> CapitalAsync(string capital, List<string> filters = null)
        {
            string endPoint = "capital/" + capital;
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return await _GetCountriesListResponseAsync(endPoint);
        }

        /// <summary>
        /// Search by Calling code
        /// </summary>
        /// <param name="callingCode"></param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns>List of Country objects</returns>
        public static List<Country> CallingCode(int callingCode, List<string> filters = null)
        {
            string endPoint = "callingcode/" + callingCode;
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Async method to Search by Calling code
        /// </summary>
        /// <param name="callingCode"></param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns></returns>
        public async static Task<List<Country>> CallingCodeAsync(int callingCode, List<string> filters = null)
        {
            string endPoint = "callingcode/" + callingCode;
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return await _GetCountriesListResponseAsync(endPoint);
        }

        /// <summary>
        /// Search by regionL Africa, Americas, Asia, Europe,  Oceania
        /// </summary>
        /// <param name="region">region name</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns>List of Country objects</returns>
        public static List<Country> Region(string region, List<string> filters = null)
        {
            string endPoint = "region/" + region;
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Async method to Search by regionL Africa, Americas, Asia, Europe,  Oceania
        /// </summary>
        /// <param name="region">region name</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns></returns>
        public async static Task<List<Country>> RegionAsync(string region, List<string> filters = null)
        {
            string endPoint = "region/" + region;
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return await _GetCountriesListResponseAsync(endPoint);
        }

        /// <summary>
        /// Search by regional bloc: EU, EFTA, CARICOM, PA, AU, USAN, EEU, AL, ASEAN, CAIS, CEFTA, NAFTA, SAARC
        /// </summary>
        /// <param name="bloc">EU, EFTA, CARICOM, PA, AU, USAN, EEU, AL, ASEAN, CAIS, CEFTA, NAFTA, SAARC</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns>List of Country objects</returns>
        public static List<Country> RegionalBloc(string bloc, List<string> filters = null)
        {
            string endPoint = "regionalbloc/" + bloc;
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return _GetCountriesListResponse(endPoint);
        }

        /// <summary>
        /// Async method to Search by regional bloc: EU, EFTA, CARICOM, PA, AU, USAN, EEU, AL, ASEAN, CAIS, CEFTA, NAFTA, SAARC
        /// </summary>
        /// <param name="bloc">EU, EFTA, CARICOM, PA, AU, USAN, EEU, AL, ASEAN, CAIS, CEFTA, NAFTA, SAARC</param>
        /// <param name="filters">List to specify fields to be included in the result</param>
        /// <returns>List of Country objects</returns>
        public async static Task<List<Country>> RegionalBlocAsync(string bloc, List<string> filters = null)
        {
            string endPoint = "regionalbloc/" + bloc;
            endPoint = filters == null ? endPoint : _GetEndpointWithFilters(endPoint + "?", filters);
            return await _GetCountriesListResponseAsync(endPoint);
        }

    }
}
