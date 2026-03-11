# Tasks Management System API – Backend (.NET)

A **RESTful Backend API built using ASP.NET Core** to manage tasks and organize them into sections.
The system allows users to create tasks, organize them into sections, and collaborate by sharing tasks or sections with other users.
This project demonstrates **Clean Architecture, Authentication & Authorization, Validation, and scalable API design**.

---

## Technologies Used

* **Backend:** ASP.NET Core Web API, Entity Framework Core, SQL Server, LINQ
* **Security:** JWT Authentication, ASP.NET Core Identity
* **Development Tools:** AutoMapper, FluentValidation, xUnit
* **API Testing:** Swagger, Postman

---

## Key Features

* User Registration and Login with JWT
* Create, Update, and Delete Tasks
* Organize Tasks into Sections
* Share Tasks and Sections with other users
* Input validation using FluentValidation
* Consistent API responses with ApiResponseMiddleware
* Structured using Clean Architecture & SOLID principles
* Unit Testing using xUnit

---

## Architecture

The project follows **Clean Architecture** to ensure separation of concerns, scalability, and maintainability.

**Layers:**

1️⃣ **Domain Layer**

* Core entities: `ApplicationUser`, `Section`, `TaskItem`, `TaskShare`, `SectionShare`
* Defines business models and relationships
* Independent from frameworks

2️⃣ **Application Layer**

* DTOs, Interfaces, Validators, Services
* Handles business logic workflows and validation
* Defines interfaces for repositories and services

3️⃣ **Infrastructure Layer**

* Database context, Repository implementations, Identity integration, JWT services
* Handles persistence, authentication, and external service interactions

4️⃣ **API Layer**

* Controllers, Middleware, Program.cs
* Exposes RESTful endpoints and handles HTTP requests
* Returns consistent API responses

---

## Project Structure

```
Tasks-Management-System
│
├── Tasks_Management_System.API
│   ├── Controllers
│   ├── Middleware
│   └── Program.cs
│
├── Tasks_Management_System.Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Validators
│   └── Common
│
├── Tasks_Management_System.Domain
│   └── Entities
│
├── Tasks_Management_System.Infrastructure
│   ├── Data
│   ├── Repositories
│   └── Services
│
└── Tasks_Management_System.Tests
    └── UnitTests
```

---

## Database Design

**ApplicationUser**

* Id, Name, Email, PasswordHash
* Relationships: owns Sections & Tasks, can receive shared Tasks & Sections

**Section**

* Id, Name, OwnerId
* Contains multiple Tasks
* Can be shared with other users

**TaskItem**

* Id, Title (unique), Description, SectionId, OwnerId
* Belongs to Section and Owner
* Can be shared with multiple users

**Sharing System**

* `TaskShare`: TaskId, UserId
* `SectionShare`: SectionId, UserId

---

## API Endpoints

### Authentication

| Method | Endpoint           | Description       |
| ------ | ------------------ | ----------------- |
| POST   | /api/auth/register | Register new user |
| POST   | /api/auth/login    | Login user        |
| POST   | /api/auth/logout   | Logout user       |

### Sections

| Method | Endpoint             | Description         |
| ------ | -------------------- | ------------------- |
| POST   | /api/sections        | Create new section  |
| PUT    | /api/sections/{id}   | Update section      |
| DELETE | /api/sections/{id}   | Delete section      |
| GET    | /api/sections/my     | Get user's sections |
| GET    | /api/sections/shared | Get shared sections |

### Tasks

| Method | Endpoint                       | Description              |
| ------ | ------------------------------ | ------------------------ |
| POST   | /api/tasks                     | Create new task          |
| PUT    | /api/tasks/{id}                | Update task              |
| DELETE | /api/tasks/{id}                | Delete task              |
| GET    | /api/tasks/my                  | Get user's tasks         |
| GET    | /api/tasks/section/{sectionId} | Get tasks inside section |
| GET    | /api/tasks/shared              | Get shared tasks         |

### Sharing

| Method | Endpoint            | Description                     |
| ------ | ------------------- | ------------------------------- |
| POST   | /api/tasks/share    | Share task with another user    |
| POST   | /api/sections/share | Share section with another user |

---

## How to Run the Project

1️⃣ Clone the Repository:

```bash
git clone https://github.com/samaa-abdelmged/Tasks-Management-System/tree/master
```

2️⃣ Navigate to Project Folder:

```bash
cd Tasks-Management-System
```

3️⃣ Restore Dependencies:

```bash
dotnet restore
```

4️⃣ Apply Database Migration:

```bash
dotnet ef database update
```

5️⃣ Run the Project:

```bash
dotnet run
```

6️⃣ Open Swagger for API Testing:

```text
https://localhost:xxxx/swagger
```

---

## Future Improvements

* Role-Based Authorization: Admin/User roles for managing users and resources
* Real-Time Notifications: Using SignalR for task/section sharing updates
* Task Status Management: Pending, In Progress, Completed
* File Attachments: Upload documents, images, or notes to tasks
* Pagination & Filtering: Improve API performance and search functionality
* Logging & Monitoring: Integrate Serilog for error tracking and system monitoring

---

## 👩‍💻 Developer Info

| Field        | Details                                                                    |
| ------------ | -------------------------------------------------------------------------- |
| **Name**     | Samaa Abdelmged Roshdy                                                     |
| **Role**     | Full Stack .NET Developer                                                  |
| **Location** | Cairo, Egypt                                                               |
| **Phone**    | +20 101 450 4030                                                           |
| **Email**    | [samaaabdelmged@gmail.com](mailto:samaaabdelmged@gmail.com)                |
| **LinkedIn** | [linkedin.com/in/samaa-abdelmged](https://linkedin.com/in/samaa-abdelmged) |


