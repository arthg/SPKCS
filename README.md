# Requirements
See the Gherkin feature file here [https://github.com/arthg/SPKCS/blob/master/WeatherAlertSystem.feature](https://github.com/arthg/SPKCS/blob/master/WeatherAlertSystem.feature "WeatherAlertSystem")

#  Open Source Tools
1. RestSharp
2. AutoMapper
3. NUnit
4. FluentAssertions
5. Exceptionless.RandomData
6. EnumerableExtensions is a modified versions of a helper that I did not personally author

# Tests
As of this time there is some kind of environment problem on my personal laptop that is preventing the tests from running.  Even so I did the best I could to drive TDD however I need to troubleshoot that to get the tests to execute :( 

# Implementation notes
* The case study was implemented by reverse engineering the response JSON, not so much from API documentation.  May need to re-visit hard-coded alertable event strings if different than what appears in the actual weather feed.  (The feed is not currently reporting the forecast events) 
# Implementation assumptions 
* The "weather event feed" reports high and low temperatures in degrees F
* The high and low temperature limits for weather alerts are specified in degrees F
* Formatting details were not specified the on requirements, assume space delimited

# Opportunities for improvement
1. Unhandled exception logging
2. Url for weather data endpoint from configuration
3. Use discovery to initialize mappers
4. Consider using AutoFixture for random initialization in mapper tests
5. Assert(s) in integration test(s)
6. Better error handling after RestSharp.Execute call?
7. Consider using dynamic for REST response parsing.  Did that, did not like the outcome, rolled it back.  Still, there may be better approach than the current one.
8. Configure High/Low temperature limits

# Instructions for running from CLI
1. cd to project directory
2. dotnet run 

# Instructions for running from Visual Studio
1. Load the solution
2. Build the solution
3. Ctrl+F5