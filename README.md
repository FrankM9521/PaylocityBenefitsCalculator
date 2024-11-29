Paylocity Benefits Calculator 11/29/2024 Frank Malinowski

"Clean" Architecture Version

This is my current implementation of the architecture I started using in 2013. 
I am very familiar with Uncle Bob and clean code, the term "clean" architecture 
has confused me as I have come across it in articles and white papers, but felt 
that was more like a blind man describing an elephant. I did not even realize
"Clean" architecture was written by Bob Martin, I just figured it was 
piggybacking off the name. I felt like a fish who had to stop and think to 
be able to describe water.

Clean architecture is simple. By "on the side", I stumbled through the following -

Implementation does not reference implementation. Instead, the interface projects 
for the implementation are what is referenced.  The "on the side" is the DI container. 
The DI container determines what implementation to inject into the interface, 
either explicitly or assembly scanning. It is the DI container (or containers if there is
a need to be more granular, e.g having a container to wire up an API layer to domain 
services, and another to wire up domain services to repositories)

Additionally, there are few cross cutting concerms. The Employee domain only references 
Employee and Shared  models. The PayCheck domain only references PayCheck models 
and shared models. Cross cutting concerns were only a factor in the Valiidation Service,
which in the real world would be replaced by something like Fluent Validation

The general layout here is 2 API projects (Employee and Paycheck), DI containers for each, 
domain services for each, repositories for each, Validation Service, shared projects and config. 

The solution builds, but I had to add a few stubs and inject a few more dependencies that 
I idid not wire up.

Paylocity.Employees and Paylocity.PayChecks both have these projects
	Api				- Web API project
	Api.DependencyInjection		- Wire up for our solution
	DataContext			- Mock context
	DataContext.Interfaces		- Mock context Interfaces
	Dtos				- POCO data transfer objects
	Entities			- POCO entity models
	Models				- POCO domain models
	Repositories			- Data access
	Repositories.Interfaces		- Data access interfaces
	DomainServices			- Domain services
	DomainServices.Interfaces	- Domain service interfaces 

Additionally (For all the wrong reasons, but I needed to do it right)
Paylocity.Validation
Paylocity.Validation.Interfaces
Paylocity.Validation.Models

And the following shared projects
Paylocity.Shared/	
	DomainServices.Interfaces
	Dto
	Entitities
	Mappers.DomainDto
	Mappers.EntityDomain
	Models
	Repositories.Interfaces







