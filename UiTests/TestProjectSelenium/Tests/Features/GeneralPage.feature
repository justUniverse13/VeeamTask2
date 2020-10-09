Feature: GeneralPage

@Smoke
@FullRegression
Scenario: Count amount of Veeam vacancies
	When I open Veeam page
	Then I select 'Romania' item from selector by text 'United States'
	And I check 'ch-7' item by id from selector by text 'All languages'
	Then there are '21' results
	
	