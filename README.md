# PaylocityBenefitsCalculator
Paylocity Benefits Calculator 11/24/2024 Frank Malinowski

This is a .NET Core 6 Application
The Application runs, all end points are viewable in swagger. Should run without issue after nuget package restore

********************************************************************************************************
* Testing																																						*
*********************************************************************************************************
NOTE - The INTEGRATION TESTS ARE FALKEY. They will SOMETIMES fail. I don't have time to figure it out, but tests are running in parallel
			even though they are in the same collection. TO GUARANTEE AN INTEGRATION TEST WILL PASS, run it individually. If you run them as a group, 
			sometimes they fail. I think  it's ny mock static DB. The unit test are fine

I was having an issue with HttpClient, so in the integration tests I tested at the controller level. 

I completed the Integration Tests and added one more. However my primary focus was on unit testing the calculations and validations 
and the PayCheck Command Interactor

Here are the requirements and the tests that validate them

	1. Able To View Employee And Dependents												-  DependentIntegrationTests.WhenAskedForAllDependents_ShouldReturnAllDependents
	2. An Employee May Only have One Spouse Or Domestic Partner			- DependentIntegrationTests.WhenSpouseOrDomesticPartnerExists_ShouldNotCreateAnother
	3. An Employee May Have An Unlimited Number of Children					- CalculatePayrollCommandHandler_Tests.ItCalculatesCorrectly  (Theory goes up to 10 children)
	4. Calculates Pay Check With the Following Rules -
			A.  26 Paychecks Per Year With Deductions Spread Out Evenly Over the Year	-  CalculatePayCheckCommanHandler_IntegrationTests.When26ChecksExist_ItDoesNotAddAnother
																																			- CalculatePayCheckCommanHandler_IntegrationTests.ItCalculatesYearCorrectly
			B. Employees Have a Base Cost of $1,000 Per Month											- CalculatePayrollCommandHandler_Tests.ItCalculatesCorrectly
			C. Each Dependent Represents an additional $600 Per Month							- CalculatePayrollCommandHandler_Tests.ItCalculatesCorrectly
			D. Employees that make more than $80,000 Per Year Will Incur an						- CalculatePayrollCommandHandler_Tests.ItCalculatesCorrectly
				additional 2% of their yearly salary in benefits cost 
			E. Dependents that are over 50 years old will incur an additional $200				- CalculatePayrollCommandHandler_Tests.ItCalculatesCorrectly
				per month

********************************************************************************************************
* Application Structure																																*
*********************************************************************************************************
I wanted to avoid additional projects, so I organized the project folders into  API, Business Logic and Data folders

** API						-	The API layer consists of the existing controllers, and an additional controller for PayChecks. 
									This controller uses CQRS with Mediator to Calculate the Pay Check and a RESTful API to Get Paychecks
								- Changed controllers to return IActionResult for both flexibility and readability
								- Added a base class to wrap API responses
								- I used records in some areas, but did not have time to change the DTOs

** Business Logic		- To Handle Deductions, I created individual classes for each deduction type that implement an ICalculate interface. These are located in Calculators/Deductions
										1. CalculateHighEarnerDeductionCalculator
										2. DependentDeductionCalculator
										3. SeniorBenefitsDeductionCalculator
										4. StandardBenfitDeductionCalculator

									These calulator are injected with an instance of an ICalculationsLibrary. There are 2 Calculation Libraries. These are located in Calculators/CalculationLibraries
										1. StandardCalculationLibrary						- Used for the first  25 paychecks
										2. LastPayCheckOfYearCalculationLibrary		- Used on the last paycheck to resolve any drift due to rounding
									
									Calculators are instanced by CalculationLibraryFactory

									Calculators are loaded into a DeductionCalculatorCollection which implements ICalculatorCollection. More Deduction Calculators can be created for additional deduction types,
									or a new type of calculator that implements its' own ICalculatorCollection. Such as one for calculating pay for overtime or bonus

									- To Handle Validation, I created Validators that implement an IValidate interface and instances of IValidationCollection
										1. ValidateDependentOnlyHasOneSpouseOrDomesticPartner - Runs when a Dependent is created
										2. ValidateEmployeeHasLessThan26Checks - Runs when a Pay Check is being created

										THERE WAS A LANDMINE in the relationship between Employee and Dependents. Each Dependent contained an instance of the Employee. Which in turn
											had instances of the dependents. I removed the Employee property from the model and added employee ID

									- Creating a Pay Check is done by a command handler.  The creation is done in Services/CalculatePayCheckService. This centralozes creation. The process is simple.
										First gross pay is calculated and then each deduction calculator is ran. After that, a new entry is created in the DB. The result is returned as an immutable record

** Data							I created a static singleton to be the data store and seed it with the same data that was supplied in the controllers. Then just repositories and lots of interfaces

Yes, I over engineered and did spend a weekend on it, but I was having fun.
					
