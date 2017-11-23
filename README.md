#Open Source Tools:
1. RestSharp
2. AutoMapper
3. NUnit
4. FluentAssertions
5. Exceptionless.RandomData

#Tests
As of this time there is some kind of environment problem that is preventing the tests from running.  Even so I did the best I could to drive TDD however I need to troubleshoot that to get the tests to execute :( 

#Opportunities for improvement:
1. Unhandled exception logging
2. Url for weather data endpoint from configuration
3. Use discovery to initialize mappers
4. Consider using AutoFixture for random initialization in mapper tests
5. Assert(s) in integration test(s)
6. Better error handling after RestSharp.Execute call?
7. Consider using dynamic for REST response parsing.  Did that, did not like the outcome, rolled it back.  Still, there may be better approach than the current one.

#Instructions for running from CLI
1. cd to project directory
2. dotnet run 

#Instructions for running from Visual Studio
1. Load the solution
2. Build the solution
3. Ctrl+F5