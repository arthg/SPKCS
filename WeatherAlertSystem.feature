Feature: As a consumer of WeatherWatch
I want to view current weather event alerts for specific forecast events
so that I know how to prepare for expected weather conditions

# Glossary:
# "weather event feed": a weather data web service that returns current weather conditions
# "weather alert system": a command line application provided by WeatherWatch 

Background:
  Given a "weather event feed" web service
    And a "weather alert system" command line application

Scenario:  Significant weather event displayed
  Given the "weather event feed" forecast calls for <forecast event>
   When the "weather alert system" is executed
   Then the "weather alert" is displayed on the console
    And the "weather alert" includes the day of the week
    And the "weather alert" includes the date 
    And the "weather alert" includes the <weather alert type>


Examples:
|forecast event                   |weather alert type        |
|rain                             |rain alert                |
|thunderstorms                    |thunderstorm alert        |
|snow                             |snow alert                |
|ice                              |ice alert                 |
|high temperature above 85 degress|high heat alert           |
|low temperature below 32 degrees |freezing temperature alert|

Scenario:  Multiple significant weather events displayed
  Given the "weather event feed" forecast calls for multiple forecast events
   When the "weather alert system" is executed
   Then a one line "weather alert" is displayed on the console for each forecast event
    And the "weather alert" content is identical to the single alert scenario


# futures:
# configure URL for weather event feed
# configure high / low temps
