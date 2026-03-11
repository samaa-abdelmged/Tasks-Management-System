Tasks Management System – Full Stack Application

A full-stack task management system built using ASP.NET Core (.NET) for the backend and Angular for the frontend.
The system helps users manage tasks efficiently, organize them into sections, and collaborate by sharing tasks with others.

This project demonstrates real-world development practices including Clean Architecture, secure authentication, scalable API design, and modern frontend integration.

Project Structure:
The application is divided into two main parts:

Backend:
Built using ASP.NET Core Web API and responsible for:
Business logic
Authentication & Authorization
Data management
API endpoints

Frontend:
Built using Angular, providing:
User-friendly interface
Task management dashboard
Section organization
Task collaboration features

Key Features:
User authentication and authorization using JWT
Create, update, and delete tasks
Organize tasks into sections
Task sharing to support collaboration
Consistent API responses using ApiResponseMiddleware
Input validation using Fluent Validation
Unit testing using xUnit
API documentation using Swagger

Technologies Used:

Backend:
ASP.NET Core Web API
Entity Framework Core
SQL Server
LINQ
JWT Authentication
AutoMapper
FluentValidation
xUnit

Frontend:
Angular
TypeScript
HTML
CSS
Architecture & Design Patterns
The backend follows Clean Architecture principles to ensure maintainability and scalability.

Design patterns used:
Repository Pattern
DTO Pattern
Dependency Injection
Middleware-based error handling
SOLID Principles
API Testing & Documentation
The API was tested and documented using:
Swagger
Postman

Project Goal:
The goal of this project is to demonstrate the development of a secure and scalable full-stack application, applying modern backend architecture with a responsive frontend interface.
