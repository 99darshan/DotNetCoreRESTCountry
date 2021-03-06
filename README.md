

[![NuGet](https://img.shields.io/nuget/dt/RestAPI.Countries.svg)  ![NuGet](https://img.shields.io/nuget/v/RestAPI.Countries.svg)](https://www.nuget.org/packages/RestAPI.Countries/) 

# Introduction
#### DotNetCoreRESTCountry is a nuget package which acts as a Wrapper Library For REST Country API v2.0.5 provided by https://restcountries.eu/ . REST countries API, includes information like common Name, official Name, TLD,two and three letter country code, ISO numeric code, CIOC,currencies, callingCodes, capital, alternative spellings, region, subRegion, borders, etc.

# Usage

#### Download Nuget package either using Nuget Packekage Manager or .NET CLI
#### Using Nuget Package Manager `Install-Package RestAPI.Countries -Version 1.0.1	` 
#### Using .NET CLI `dotnet add package RestAPI.Countries --version 1.0.1	`

#### Use Static methods defined in RestCountry Class to call the specific API Endpoints.
For Example ```RestCountry.Currency("cop")``` will make the GET Request to the  ```https://restcountries.eu/rest/v2/currency/cop``` endpoint of REST Countries API. 

#### Static methods defined in RestCountry class returns a list of Country objects. Access the properties defined in Country class to get various information about the countries. 

#### Methods defined in RestCountry Class takes in a list of strings as optional second parameter which filters the output of your request to include only the specified fields.

#### Make asynchronous requests to the API using the Async version of methods defined in RestCountry Class.
```RestCountry.CurrencyAsync("cop")```

```C#
using DotNetRestCountries; // Reference Nuget Package
using System;
using System.Collections.Generic;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Call static methods listed in RestCountry class. 
            // These methods will return a List of Country Objects. 
	        // Use properties in Country Class to access all the informations like name, capital, calling codes, etc. for a given country
            // https://restcountries.eu/rest/v2/all 
            List<Country> allCountries = RestCountry.All();
            foreach(var con in allCountries)
            {
                Console.WriteLine(con.Name + " -- " + con.NativeName + " -- " + con.TopLevelDomain );

            }

            //[FILTER RESPONSE] 
            // Pass an optional List of filters to obtain results with only the specified properties for countries

            // This will return a List of Countries having string "island" in their name. 
	        // Since we passed in the List of filters, the returned Country objects will only have values for Name and Capital Properties.

            // https://restcountries.eu/rest/v2/name/island?fields=name;capital
            List<Country> filteredResult = RestCountry.Name("island", new List<string> { Filters.Name, Filters.Capital });
            Console.WriteLine(filteredResult[0].Name);

            // Make asynchronous requests to the API using the Async version of methods defined in RestCountry Class.

            // Makes async request to the RESTCountries endpoint  https://restcountries.eu/rest/v2/capital/kathmandu
            List<Country> asyncResult = RestCountry.CapitalAsync("Kathmandu");
            Console.WriteLine(asyncResult[0].Name);
        }
    }
}

```

### Credits
***
Thanks to Fayder Florez for developing [REST Countries API](https://github.com/fayder/restcountries)


### Related Projects
***
+ [Countries101](https://github.com/99darshan/countries101)
+ [gocountries](https://github.com/alediaferia/gocountries)
+ [restcountry](https://github.com/davidesantangelo/restcountry)

### License
***
This DotNetCoreRESTCountry package is released under MIT License.
