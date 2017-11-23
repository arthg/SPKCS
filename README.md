# Requirements
See the Gherkin feature file here [WeatherAlertSystem](https://github.com/arthg/SPKCS/blob/master/WeatherAlertSystem.feature "WeatherAlertSystem")

# Instructions for running from CLI
1. cd to project directory
2. dotnet run 

# Instructions for running from Visual Studio
1. Load the solution
2. Build the solution
3. Ctrl+F5

# Implementation notes / assumptions 
* The case study was implemented by reverse engineering the response JSON, not so much from API documentation.  May need to re-visit the hard-coded alertable event string literals if different than what appears in the actual weather feed.  (The feed is not currently reporting those forecast events) 
* The "weather event feed" reports high and low temperatures in degrees F.  The high and low temperature limits for weather alerts are specified in degrees F.  Therefore no temperature conversions required.
* Formatting details were not specified the on case study description, assumed space delimited.

#  Open Source Tools
1. RestSharp
2. AutoMapper
3. NUnit
4. FluentAssertions
5. Exceptionless.RandomData
6. EnumerableExtensions is a modified version of a helper that I did not personally author

# Opportunities for improvement
1. Unhandled exception logging
2. Url for weather data endpoint from configuration
3. Use discovery to initialize mappers
4. Consider using AutoFixture for random initialization in mapper tests
5. Better error handling after RestSharp.Execute call?  Probably no need.
6. Consider using dynamic for REST response parsing.  Did that, did not like the outcome, rolled it back.  Still, there may be better approach than the current one.
7. Consider re-organizing project structure, break up WeatherFeedClient assembly
