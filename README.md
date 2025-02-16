# Backend Application with Clean Code Principles and CQRS

This is a backend application built using Clean Code principles and the CQRS (Command Query Responsibility Segregation) pattern. The goal of this project is to implement a well-structured and maintainable backend service that follows best practices.

## Features:
- **Clean Code**: The project follows clean code principles to ensure readability, maintainability, and scalability.
- **CQRS**: Command Query Responsibility Segregation (CQRS) is used to separate command (write) operations from query (read) operations, improving performance and scalability.
- **Security Enhancements**: Future security features will include:
  - **CORS** (Cross-Origin Resource Sharing) for controlling how resources can be requested from different origins.
  - **Anti-forgery tokens** to protect against Cross-Site Request Forgery (CSRF) attacks.
  - **JWT Authentication** (JSON Web Tokens) to ensure secure user authentication and authorization.
  - **Data Tampering Protection**: Implementing security mechanisms to detect and prevent **parameter tampering**.
- **Unit Testing**: 
  - **xUnit** will cover the application's core functionalities and edge cases.
- **Integration Testing**: 
  - The application will use **In-Memory Database** or a **real test database** to simulate the production environment and ensure that data flows properly through different layers.

## Planned Features:
- **CQRS Integration**: Separate models for commands and queries to keep the logic simple and decoupled.
- **Security Patches**:
  - **JWT Authentication**: For secure user login and session management.
  - **CORS Setup**: To prevent unauthorized access from foreign domains.
  - **Anti-forgery**: Protection against CSRF attacks.
  - **Parameter Tampering Protection**: Implementing mechanisms prevent manipulation of data and ensure data integrity during transmission.
- **Unit Testing**: 
  - Writing **xUnit** tests to validate backend logic, ensuring that all components perform as expected.
- **Integration Testing**: 
  - Use of **In-Memory Database** for integration testing.

## Technology Stack:
- **Backend**:
  - **.NET 8**
  - **ASP.NET Core Web API** for creating RESTFUL services
  - **MediatR** for implementing CQRS (Command Query Responsibility Segregation)
  - **AutoMapper** for object mapping
  - **Entity Framework Core** for ORM and database interactions
  - **JWT Authentication** for secure user access
  - **xUnit** for writing unit tests
  - **FluentAssertions** for more readable assertions in unit tests
  - **FluentValidation** for model validation before MediatR command/query handling
  - **FakeItEasy** for creating fake objects (mocking library)
