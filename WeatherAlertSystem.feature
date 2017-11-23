Feature: As a consumer of WeatherWatch
I want to view all the current weather event alerts for specific forecast events
so that I know how to prepare for the expected weather conditions

# Glossary:
# "weather event feed": a weather data web service that returns current weather conditions
# "weather alert system": a command line application provided by WeatherWatch 

Background:
  Given a "weather event feed" web service
    And a "weather alert system" command line application

Scenario:  Significant weather event displayed
  Given the "weather event feed" forecast calls for <forecast event>
   When the "weather alert system" is executed
   Then the "weather alert" is displayed on the console on a single line
    And the "weather alert" includes the day of the week
    And the "weather alert" includes the date 
    And the "weather alert" includes the <weather alert type>
    And the "weather alert" items are seperated by a single space

Examples:
|forecast event                   |weather alert type        |
|rain                             |rain alert                |
|thunderstorms                    |thunderstorm alert        |
|snow                             |snow alert                |
|ice                              |ice alert                 |
|high temperature above 85 degress|high heat alert           |
|low temperature below 32 degrees |freezing temperature alert|

Scenario:  Multiple significant weather events generated for a forecast event
  Given the "weather event feed" forecast calls for snow
    And the "weather event feed" forecast calls for low temperature below 32 degrees
   When the "weather alert system" is executed
   Then a "weather alert" is displayed on the console for snow
    And a "weather alert" is displayed on the console for freezing temperature
    And the "weather alert" content is identical to the single alert scenario

Scenario:  Display multiple significant weather events
  Given the "weather event feed" forecast calls for multiple forecast events
   When the "weather alert system" is executed
   Then a one line "weather alert" is displayed on the console for each forecast event
    And the "weather alert" content is identical to the single alert scenario

Scenario:  No significant weather events in the weather event feed
  Given the "weather event feed" forecast does not include any forecast events of interest
   When the "weather alert system" is executed
   Then the message "No weather alerts at this time." is displayed on the console
#TODO: confirm business requirement for wording

Scenario:  An unexpected error occurs
   When the "weather alert system" is executed
    And an expected error occurs for example the weather feed service is not responsive
   Then an appropriate error message is displayed on the console
#TODO: need business requirement for wording


