# Principles:
- Clean architecture
- DDD
- Modular monolith (bounded context = module)
- CQS - use separate backend models for query (reads) vs command (data changes) but use the same db

# Project/folder structure:
- Folder for each module named [ProductName].[ModuleName]
  - [ProductName].Domain.[ModuleName] project for each module (bounded context)
    - here we create folders for each feature
    - inside the feature folder we put all aggeregate roots, entities, events, value objects, domain services... etc, related to the feature
    - avoid adding shared folders, but it is allowed (if we need to add to shared, it should make us think if our aggregates are well formed, and also do not overuse the DRY principle)
    - repository folder for repository interfaces related to the feature
  - [ProductName].Domain.Base project for code common across all bounded contextes
    - here goes only tehnical stuff, not domain logic 
    - IAggregateRoot
    - IEntity
    - IValueObject
    - ...
  - [ProductName].Persistance.[ModuleName] project - todo
    - at first start with only one Persistance project, there is to much con's to split them (views for data from other contextes, complicated query side across contexte vs a simple query sid with all available db objects)
    - db scheme for each module, shema name = ModuleName
    - has class for each table form it's db scheme
    - has ef configuration mapping for table to it's corresponding class
    - has folder repositories with implementation of it's modules repositories
    - maps class of each table to domain object property inside repository (use mapping klasses only when needed not by default)
    - has implementation of IUnitOfWork for it's module (inside it is the logic of raising domain events)
    - Migrations folder for migration scripts
    - below only if module separation is needed:
        - has views on tables form other modules (think dbcontext has issues if two models import same tables), so it will disable updates on different schema tables, and will be a clear indicator where integration is happening
    
  - [ProductName].Application.Command.[ModuleName] project
    - folder for each feature
    - under feature folder we put applicationServices, commands, command results of the feature
    - interface IUnitOfWork
    - security - classes with queries that check access - todo
  - [ProductName].Application.Query.[ModuleName] project - todo
    - folder for each feature
    - under feature folder we add [QueryDescription]Query.cs class for each query, [QueryDescription]QueryInput.cs (query input class), [ViewDescrittion]View.cs (query results class)
    - does not use the Domain projects, but uses the Persistance objects to write queries against
      - if using modules on persistance, uses objects of the module (it's dbContext) 
    - security - classes with queries that check access - try to write them as queriables that can be part of the query for better performance
  - [ProductName].API.[ModuleName] - todo
    - folder for each feature
    - under feature name we add controllers with either Query or Command sufix that implement api endpoints and call application.Command or application.Query classes
    - reuse queryInputs, Views, commands and commandresults from applicationService, don't map to new objects - if needed (eg. these objects can't serialize, than do it on a case by case basis)
  - [ProductName].Integration.Internal.[ModuleName]
    - the idea here is to have a clear visibility on who depends on this module, so when we make a change to it, we can have more visibility on who is affected
    - create a separate folder for each module that depends on this module
    - under module folder we add:
        - IntegrationServices - same as applicationServices    
            - interfaces and implementations of IntegrationServices both go here (need interfaces because we want to mock them in unit tests)
        - DomainEventHandlers - classes that handle domain events from this module and call IntegrationServices
            - We use events for the same reason we use events in the domain, to decouple changes to different aggregates, only here its aggregates across bounded contexts
  - [ProductName].Ingegration.External.[ModuleName] - todo
    - same as internal but for external systems
  - [ProductName].AsyncJobs.[ModuleName] (optional) - todo
    - use to start hangfire and execute async jobs

# Guideliness:
## Aggregates
- Try to name your aggregates and methods to reflect behaviour, not structure or crud. Eg. update[tableName] to Approve, Deny.
- Try to use small aggregates, better to use two if possible, than one large. Model them by the needs of the request, not by structure of what the object has in the real world. Think how many properties of the aggergate does your request (command) really need, if it's just a few, and the aggergate is already heavy, consider creating a new one. Usualy if one command changes one property, the other just reads it, so we can split them, since it won't violate invariants realted to that property. Of course we prefer to have all actions on the same object, when it's performant and easy to manage :).
- avoid passing domain services into aggergates, since it adds an outside dependency to them... rather force the change on the aggregate by making the method on aggregate internal, and forcing the usege of the domain service which changes the aggregate
- create separate repositories for each aggregate, and use the aggeregates repository for domain service queries related to that aggregate
## Domain objects
- Use access modifiers to expres the intent of what you meant when designing domain objects. Aggergates, and their's applicationService used methods are public, methods used by other objects are internal, and everything else is private (or protected). This is what we would like, but there are compromisee:
  - the persistance layer needs to be able read all properties of the aggregate to save it, so their getters are gonna need to be public (would be nice to be able to avoid this)
  - all of our aggregates and entities need public constructors so we can create them in our repositories, these are not to be used anywhere else (would be nice to be able to avoid this)
- try to use value objects for properties that change togeather (eg. price value object which has amount and currency, instead of having amount and currency directly on the entity)
- for aggregate/entity default values, try to set them in the constructor not use a default property value, we want to be explicit as we can
## Design
- favor more api's with simpler logic than fewer with complex logic. Eg. instead of sending all 10 properties to update, with one property having side effects (changing somehting else based on it's value), try to brake it into two, with the one property having side effects clearly separated on the ui (since it will most likely be more intuitive to the user that somehting other than just field update is going on) and having a clear behaviour drive name with it :)
- It is fine to have same name classes, even behaviour in different modules. Thing is that once we decided a boundary of a bounded countext, we did say that changes to it will happen only for reasons inside it, so we don't want to reuse stuff from other modules, even if it behaves the same, because it will probably have a different reason to change. If we do see a lot of same behavoiur going on, maybe we should revisit our bounded context boundaries.
## Validation
- use ResultPattern rather than exceptions for enforcing invariants and validations
- validate command in app layer with stuff it can validate (types, lenght, not null) to improve performance (not having to load a heavy aggeregate just to check for null)
- validate aggregate inside aggregate for stuff the aggregate knows (it's own properties)
- validate in domain service stuff that is outside of the aggregate
- security checks and transaction commits are done by Aplication layer
## Events
- create domain events (IDomainEvent) for cross aggregate communication (in same bounded context or in different bounded context) to have aggregates decoupled (not have aggregate call aggregate)
- event handlers
    - use inside bounded countext to make many aggregates (from same bounded context) change in same transaction
    - use inside internal to make many aggregates (from different bounded context) change in same transaction
    - use inside external to change aggregate and make call to external system in same transaction
    - use inside external to add async jobs for external systems (email, payment service...)
- consider using saga pattern instead of event handlers if the process is complex. Saga raises it's own events, so it can raise an event after an agregate change, that would otherweise not be thrown by the aggregate, so that that event can trigger another saga action - TODO


# Questions:
- IUnitOfWork goes into app layer, because it's the only one that is gonna use it, it's controlling transactions... we don't need it in domain but where is it implemented, in persistance or in app layer?
- transaction accross modules with different UnitsOfWork on each module wrapped in transaction?
- feature name is the application service (without the service part) name also maybe equals to aggreget root? Strange to have the feature name in domain without it having a coresponding object name, but maybe best to have it only like a namspace to group many objects that implement that high level feature
- in application layer we use application services... we could use the command handlers instead, and mediator so the caller does not need to know which app service to call, it can just send the command... but seems to add complexity for no reason, or does it?
- do we need an Internal project, and should it have implementation of others modules interfaces by using it's domain, or should it have implementations of it's own interfaces by using other modules domains?
- do we want to use identity classes?
- class for each table and mapping in repository vs using domain objects directly in configuration mapping? Went with first for now.
- when changing two aggregates use events? or explicitly in application service? form same or different context in same transaction?
- use domain events for cross bounded context communication? how? how is it different from using interfaces? hangfire fro eventual consistency scenarios? event handlers outside of transaction through async jobs only?
- example code of what goes to app service vs what goes to domain service
- aggregate story - each method (or a small number of them) in it's own interface, than with implementation on class, instead of adding a method directly on a class, so we can end up with one class with many interface implementations. Than we only use the interface where we need, and could potentionally have a repository for each interface to just fill the part of the aggregate needed for that interface. Needs adidtional research, but sounds like we would still have a partially loadaded aggregate and enforcing invariants would be questionable right?
- guids or int for id's? App code can generate guids and they are unique, so switching id's between entities failes, while int's take less space and are more readable. Can Guid.NewGuid() be used in aggregate?
- undescore for private class variables?
- how much overhead would we have to introduce a project that acts as a service bus, and has definitions of all integration events (communication between modules), and have modules subscribe to it?

# Logistics discussions:
  - Transport status change emits event that calls it's shipments shipmentRoute state change, and this also throws event to update shipment status. 
    - Why? Since Transport, ShipmentRoute and Shipment are all aggregates (and have their own use cases without needing others), we try to avoid adding each of them as a property of another (eg. adding a List of shipmentRoutes under Transport) since doing so would compromise the invariants of the parent (in our case Transport, since routes can change without transport). Also loading all shipmentRoutes on transport, for each transport change is not performant.
    - DomainService? Tried to add it to domain service, but ended up with application needing to load transport and separately load transports ShipmentRoutes, which looks like it needs to know to much (and can load some other transports routes, or load this transports routes partially, we can't control it). Tried to move the loading to the domainService, but than it also needs to call repo.update and that should be the responsibility of a app service... tried to return all loaded object from the domain service but than the app service needs to know what to update, and if it misses to update something we have an inconsistency
    - Shipment creation creates ShipmentRoutes so did this with a domain service, and in this case the app service needs to call a repository update for each created aggregate. Left it but would rather have it move to a domain event right? 
 
# Tehnical
- to create migrations in the persistance project, with the configuration in the CA project run the following command:
    - dotnet ef migrations add InitialMigraton --project Logistics.Persistance.Shipping/Logistics.Persistance.Shipping.csproj --startup-project Logistics.Test.CA/Logistics.Test.CA.csproj
    - this is available by the "CreateHostBuilder" method in the Program.cs file of the CA project