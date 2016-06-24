Feature: BookingFeature
	The Booking Calculator should return the correct duration based on the supplied pattern

Scenario: Holiday booking in days
	Given I work Monday to Friday pattern specified in days
	And I have booked off 2016-01-01 to 2016-01-12 in days
	When I calculate the duration
	Then the result should be 10 days

Scenario: Holiday booking in hours
	Given I work Monday to Friday pattern specified in minutes
	And I have booked off 2016-01-01 to 2016-01-12 in minutes
	When I calculate the duration
	Then the result should be 4320 minutes

Scenario: Possible Ferry crossings to France
	Given a pattern of one ferry crossing every 3 days
	And I have want to book a ferry between 2016-01-01 to 2016-01-12 
	When I calculate the duration
	Then the result should be 4 crossings

Scenario: ASDA shopping delivery slots
	Given a pattern of available delivery slots
	And I want to get my shopping delivered on 2016-01-01
	When I calculate the duration
	Then the result should be 7 possible slots
