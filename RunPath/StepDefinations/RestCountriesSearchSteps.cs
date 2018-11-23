using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace RestCountries.StepDefinations
{
   
    [Binding]
    public class RestCountriesSearchSteps
    {
        TriggerRestClient objTriggerRestClient = new TriggerRestClient();


        [Given(@"I have the Rest countries service is up")]
        public void GivenIHaveTheRestCountriesServiceIsUp()
        {
            // Need to write code to make service up if need
            bool serviceStatus = true;
            Assert.IsTrue(serviceStatus, "Service is up and running");
            
        }
        
        [When(@"I request for all countries")]
        public void WhenIRequestForAllCountries()
        {
            var uri = String.Format("{0}/rest/v2/all", ConfigurationManager.AppSettings["baseURI"].ToLower());
            var objResponse =  objTriggerRestClient.RestRequestExecute<List<Country>>(RestSharp.Method.GET, uri);
            ScenarioContext.Current.Add("AllCountries", objResponse.Data);
            Assert.IsTrue((int)objResponse.StatusCode == 200, "Request To All  Countries was UnSuccessful with status : " + (int)objResponse.StatusCode);
           
        }
    

        [When(@"I request for a country (.*)")]
        public void WhenIRequestForACountry(string strName)
        {
            var uri = String.Format("{0}/rest/v2/name/{1}", ConfigurationManager.AppSettings["baseURI"].ToLower(), strName);
            var objResponse = objTriggerRestClient.RestRequestExecute<List<Country>>(RestSharp.Method.GET, uri);
            ScenarioContext.Current.Add("CountriesByName", objResponse.Data);
            Assert.IsTrue((int)objResponse.StatusCode == 200, "Request To Countries by Name was UnSuccessful with status : " + (int)objResponse.StatusCode);
           
        }

        [When(@"I request for a incorrect country (.*)")]
        public void WhenIRequestForIncorrectCountry(string strName)
        {
            var uri = String.Format("{0}/rest/v2/name/{1}", ConfigurationManager.AppSettings["baseURI"].ToLower(), strName);
            var objResponse = objTriggerRestClient.RestRequestExecute<List<Country>>(RestSharp.Method.GET, uri);
             ScenarioContext.Current.Add("IncorrectCountriesByName", objResponse.StatusCode);
            

        }

        [Then(@"All the countries and their details  returned")]
        public void ThenAllTheCountriesAndTheirDetailsReturned()
        {
            var obj = (List<Country>)ScenarioContext.Current["AllCountries"];
            Assert.IsTrue(obj.Count == 250, "All countries not returned");
        }

        
        [Then(@"All countries with name (.*) details in response")]
        public void ThenIDetailsInResponse(string CountryName)
        {
            var obj = (List<Country>)ScenarioContext.Current["CountriesByName"];
            List<Country> countriesMatching = obj.Where(country => country.name.Contains(CountryName)).ToList();
            List<Country> countriesNotMatching = obj.Where(country => !country.name.Contains(CountryName)).ToList();
            Assert.IsTrue(countriesNotMatching.Count == 0 && countriesMatching.Count > 0, "All countries not returned");
        }

        [Then(@"No country with name (.*) details in response")]
        public void ThenNoCountryWithNameLondonDetailsInResponse(string IncorrectCountryName)
        {
            var obj = (int)ScenarioContext.Current["IncorrectCountriesByName"];
            Assert.IsTrue((int)obj == 404, "Request To Countries by Name was Successful for incorrect Name with Status Code :   " + obj);            
        }


        [Then(@"No other country details returned in")]
        public void ThenNoOtherCountryDetailsReturnedIn()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I request for a capital city (.*)")]
        public void WhenIRequestForACapitalCity(string strCityName)
        {
            var uri = String.Format("{0}/rest/v2/capital/{1}", ConfigurationManager.AppSettings["baseURI"].ToLower(), strCityName);
            var objResponse = objTriggerRestClient.RestRequestExecute<List<Country>>(RestSharp.Method.GET, uri);
            ScenarioContext.Current.Add("CountriesByCityName", objResponse.Data);
            Assert.IsTrue((int)objResponse.StatusCode == 200, "Request To Countries by Name was UnSuccessful with status : " + (int)objResponse.StatusCode);

        }

        [Then(@"All countries with capital city (.*) details in response")]
        public void ThenAllCountriesWithCapitalCityLondonDetailsInResponse(string strCityName)
        {
            var obj = (List<Country>)ScenarioContext.Current["CountriesByCityName"];
            List<Country> countriesMatching = obj.Where(country => country.capital.Contains(strCityName)).ToList();
            List<Country> countriesNotMatching = obj.Where(country => !country.capital.Contains(strCityName)).ToList();
            Assert.IsTrue(countriesNotMatching.Count == 0 && countriesMatching.Count > 0, "All countries not returned");
        }


    }
}
