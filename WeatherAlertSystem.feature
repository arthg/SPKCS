Feature: As a consumer of WeatherWatch
I want to view current weather event alerts for specific forecast events
so that I know how to prepare for expected weather conditions

# Glossary:
# "weather event feed": a weather data web service that returns current weather conditions
# "weather alert system": a command line application provided by WeatherWatch 

# Assumptions:
# The "weather event feed" reports high and low temperatures in degrees F
# The high and low temperature limits for waether alerts are specified in degrees F
# When a given "weather event feed" forecast has more than 1 alertable event
# (for sample forcecast of snow and low temperature below 32 degrees)
# that there will be more than one alert emitted.  The requirements state
# if XXX, generate so that implies to generate an alert for each 

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

#TODO: confirm business requirement
Scenario:  No significant weather events in the weather event feed
  Given the "weather event feed" forecast does not include any forecast events of interest
   When the "weather alert system" is executed
   Then the message "No weather alerts at this time." is displayed on the console

# futures:
# configure URL for weather event feed
# configure high / low temps
