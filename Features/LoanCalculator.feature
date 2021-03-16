Feature: LoanCalulator
	Slider loan amount calculator

@mytag
Scenario: Select date from Weekday as repayment date
	Given The amount of 300 has been selected as loan amount in slider
	When the date selected in repayment date selector is a Weekend
	Then First repayment option is shown to the user is friday by default
	And Loan Amount in summary is equal to 300