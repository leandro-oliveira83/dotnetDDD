
# Clean Architecture

Clean Architecture was created by Robert C. Martin and promoted in his book Clean Architecture: A Craftsman’s Guide to Software Structure. Like other software design philosophies, Clean Architecture tries to provide a methodology to be used in coding, in order to facilitate code development, allow for better maintenance, updating and less dependencies.

## Versions

The master branch is now using .NET Core Version 5.0.

# Goals

The goal of this repository is to provide a basic solution structure that can be used to build Domain-Driven Design (DDD)-based or simply well-factored, SOLID applications using .NET Core. Learn more about these topics here:

## The Core Project

The Core project is the center of the Clean Architecture design, and all other project dependencies should point toward it. As such, it has very few external dependencies. The one exception in this case is the `System.Reflection.TypeExtensions` package, which is used by `ValueObject` to help implement its `IEquatable<>` interface. The Core project should include things like:

- Entities
- Aggregates
- Domain Events
- DTOs
- Interfaces
- Event Handlers
- Domain Services




