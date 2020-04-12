using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRestCountries;
using Xunit;

namespace Tests
{
    public class RestCountryTests
    {
        private void _NepalResultFiltered (Country con)
        {
            Assert.NotEqual ("Kathmandu", con.Capital);
            Assert.Null (con.Currencies);
            Assert.Null (con.RegionalBlocs);
        }
        private void _NepalResultFull (Country con)
        {
            Assert.Equal (28.0, con.Latlng[0]);
            Assert.Equal (28431500, con.Population);
            Assert.IsType<Translations> (con.Translations);
            Assert.IsType<Language[]> (con.Languages);
            Assert.Equal ("NPR", con.Currencies[0].Code);
            Assert.Empty (con.RegionalBlocs[0].OtherNames);
        }

        [Fact]
        public void AllTest ()
        {
            List<string> filters = new List<string> () { Filters.Name };
            List<Country> allCountriesFiltered = RestCountry.All (filters);
            List<Country> allCountries = RestCountry.All ();
            List<string> filterByAlphaCode = new List<string> () { Filters.Alpha2Code };
            List<Country> alllCountriesAlphaCodeFilter = RestCountry.All (filterByAlphaCode);
            foreach (Country con in allCountries)
            {
                if (con.Name == "Nepal")
                {
                    _NepalResultFull (con);
                }
            }

            foreach (Country con in allCountriesFiltered)
            {
                if (con.Name == "Nepal")
                {
                    _NepalResultFiltered (con);
                }
            }

            foreach (Country con in alllCountriesAlphaCodeFilter)
            {
                if (con.Name == "Nepal")
                {
                    Assert.Equal ("NP", con.Alpha2Code);
                }
            }
        }

        [Fact]
        public async void AllTestAsync ()
        {
            List<string> filters = new List<string> () { Filters.Name };
            List<Country> allCountries = await RestCountry.AllAsync (filters);
            List<Country> countries = await RestCountry.AllAsync ();

            foreach (Country con in allCountries)
            {
                if (con.Name == "Nepal")
                {
                    _NepalResultFiltered (con);

                }
            }

            foreach (Country con in countries)
            {
                if (con.Name == "Nepal")
                {
                    _NepalResultFull (con);
                }
            }
        }

        // name
        [Fact]
        public void NameTest ()
        {
            List<Country> con = RestCountry.Name ("ne");

            foreach (Country c in con)
            {
                if (c.Name == "Nepal") _NepalResultFull (c);
            }

        }
    }
}