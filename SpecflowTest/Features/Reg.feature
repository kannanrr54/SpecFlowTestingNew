Feature: Registration of users

A short summary of the feature
Background: 
	User in login page
/*Rule: below proviided scenrios are in sigle rule
	Background: only applies for the scenrios below
	/*@xml-RegData.xml @smoke
	/*Scenario: User enters in valid data
		/*When User enters registration data
		/*Then click on submit button
		/*Then USer registration successfull

	/*Scenario: User enters in multiple values
		/*When user enters "kannan" and "rr" as fname and lname
		/*Then click on submit button
		/*Then Error comes
/*Rule: below proviided scenrios are in sigle rule

	@xml-RegData.xml @smoke
	Scenario: User enters in valid data
		When User enters registration data
		Then click on submit button
		Then USer registration successfull

	Scenario: User enters in multiple values
		When user enters "kannan" and "rr" as fname and lname
		Then click on submit button
		Then Error comes

Scenario: User enters in list of values
	When user enters "<fname>" and "<lname>" as fname and lname
	Then click on submit button
	Then Error comes
	Then enters in country
	| country | city |
	| India   | a    |
	| Japan   | b    |

	Examples: 
	| fname  | lname |
	| kannan | rr    |
	| chinnu | rr    |
