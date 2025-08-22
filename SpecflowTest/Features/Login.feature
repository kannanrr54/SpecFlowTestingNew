Feature: Login
Perform login success
@xml-RegData.xml
Scenario: Login with valid credentials
	Given User navigated to the login page
	When User enters in correct username and password
	Then User navigates to home page
@xml-testdata.xml
Scenario: Login with invalid credentials
	Given User navigated to the login page
	When User enters in incorrect username and password
	Then User navigates to home page


