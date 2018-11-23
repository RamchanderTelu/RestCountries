Feature: RestCountriesSearch
	In order to validation rest countires api, need to test by searching countries

Background: 
	Given I have the Rest countries service is up	 

@SearchAll
Scenario: Able to see all the countries 
	When I request for all countries
	Then  All the countries and their details  returned


@SearchByCountry
Scenario Outline: Able to search for a country by Name	
	When I request for a country <CountryName>
	Then All countries with name <CountryName> details in response
	Examples:
| CountryName      |
| India |
| Ukraine|

@SearchByIncorrectCountry
Scenario Outline: Able to search for a country by incorrect Name	
	When I request for a incorrect country <CountryName>
	Then No country with name <CountryName> details in response
	Examples:
| CountryName      |
| London |
| Paris|

@SearchByCity
Scenario Outline: Able to search for a Captial City by Name	
	When I request for a capital city <CapitalCity>
	Then All countries with capital city <CapitalCity> details in response
	Examples:
| CapitalCity      |
| London |
| Paris|
 
 
	 