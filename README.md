# 🚀 Project Management System API

A comprehensive, **enterprise-grade RESTful API** for project and task management built with **.NET 9**, following **Clean Architecture**, **CQRS**, and **Domain-Driven Design (DDD)** principles.

---

## 📚 Table of Contents

* [Overview](#overview)
* [Key Features](#key-features)
* [Architecture](#architecture)
* [Technology Stack](#technology-stack)
* [Design Patterns & Principles](#design-patterns--principles)
* [Getting Started](#getting-started)
* [API Documentation](#api-documentation)
* [Authentication & Authorization](#authentication--authorization)
* [Database Schema](#database-schema)
* [Configuration](#configuration)
* [Contributing](#contributing)
* [License](#license)

---

## 🧭 Overview

The **Project Management System API** provides a robust backend for managing projects, tasks, and team collaboration.
It is designed with **scalability**, **maintainability**, and **security** in mind, making it suitable for both small teams and enterprise-level applications.

---

## ✨ Key Features

### 🔐 Authentication & Authorization

* JWT-based authentication (Access & Refresh Tokens)
* Role-Based Access Control (RBAC)
* Permission-based authorization
* Secure refresh token mechanism
* Mock authentication (ready for external identity providers)

---

### 📁 Project Management

* Full CRUD operations
* Budget management with multi-currency support
* Search, filtering, pagination, and sorting
* Ownership and access control
* Soft delete support

---

### 📝 Task Management

* Full CRUD operations
* Task assignment to users
* Status tracking:

  * `NotStarted`, `InProgress`, `Completed`, `Blocked`, `Cancelled`
* Task filtering per project
* Task dependencies & bulk operations

---

### 🌐 API Capabilities

* API Versioning (v1, v2)
* OpenAPI / Swagger documentation
* Scalar API Explorer
* RFC 9110 ProblemDetails error handling
* Request/response logging
* Performance monitoring middleware

---

### ⚙️ Technical Highlights

* Clean Architecture (4 layers)
* CQRS with MediatR
* Repository + Unit of Work
* AutoMapper
* FluentValidation
* Serilog structured logging
* EF Core + SQL Server
* Generic repositories with expression-based querying
* Database seeding with sample data

---

## 🏗 Architecture

The solution strictly follows **Clean Architecture**, ensuring separation of concerns:

```
ProjectManagementSystem/
├── ProjectManagementSystem.API
│   ├── Controllers
│   ├── Middlewares
│   ├── Extensions
│   └── OpenApi
│
├── ProjectManagementSystem.Application
│   ├── Projects
│   │   ├── Commands
│   │   ├── Queries
│   │   └── Dtos
│   ├── ProjectTasks
│   ├── Authentication
│   ├── Common
│   └── Extensions
│
├── ProjectManagementSystem.Domain
│   ├── Entities
│   ├── Constants
│   ├── Exceptions
│   └── IRepositories
│
└── ProjectManagementSystem.Infrastructure
    ├── Persistence
    ├── Repositories
    ├── Seeders
    ├── Permissions
    └── Extensions
```

---

## 🧰 Technology Stack

### Core

* **.NET 9**
* **C# 13**

### Frameworks & Libraries

* ASP.NET Core
* Entity Framework Core
* MediatR
* AutoMapper
* FluentValidation
* Serilog
* Asp.Versioning
* Swashbuckle (Swagger)
* Scalar API UI

### Database

* SQL Server
* EF Core ORM

### Security

* JWT Bearer Authentication

---

## 🧠 Design Patterns & Principles

### SOLID Principles

* Single Responsibility
* Open/Closed
* Liskov Substitution
* Interface Segregation
* Dependency Inversion

### Patterns Used

* Clean Architecture
* CQRS
* Repository Pattern
* Unit of Work
* Mediator
* Dependency Injection
* Options Pattern
* Result Pattern

### Middleware Pipeline

1. Global Exception Handling
2. Request Time Logging
3. Serilog Request Logging
4. Authentication
5. Authorization

---

## 🚀 Getting Started

### Prerequisites

* .NET 9 SDK
* SQL Server 2019+
* Visual Studio 2022 or VS Code
* Git

---

### Installation

#### 1️⃣ Clone Repository

```bash
git clone https://github.com/yourusername/project-management-system.git
cd project-management-system
```

#### 2️⃣ Configure Database

Update `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "ProjectManagementDB": "Server=YOUR_SERVER;Database=ProjectManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

#### 3️⃣ Install EF Tools

```bash
dotnet tool install --global dotnet-ef
```

#### 4️⃣ Apply Migrations

```bash
dotnet ef database update \
 --project ProjectManagementSystem.Infrastructure \
 --startup-project ProjectManagementSystem.API
```

#### 5️⃣ Run the API

```bash
cd ProjectManagementSystem.API
dotnet run
```

---

### Access Endpoints

* Swagger UI: `https://localhost:7XXX/swagger`
* Scalar Docs: `https://localhost:7XXX/scalar/v1`
* OpenAPI Spec: `https://localhost:7XXX/openapi/v1.json`

---

### Sample Data

On first run, the database is seeded with **15+ projects**, including:

* E-Commerce Platform
* Mobile Fitness App
* IoT Platform
* NFT Marketplace
* Video Streaming Platform

---

## 📖 API Documentation

### Base URL

```
https://localhost:7XXX/api/v{version}
```

---

### Authentication

#### Login

```http
POST /api/v1/authentication/login
```

#### Refresh Token

```http
POST /api/v1/authentication/refresh
```

---

### Projects

* `GET /projects`
* `GET /projects/{id}` (v1, v2)
* `POST /projects`
* `PUT /projects/{id}`
* `PUT /projects/{id}/budget`
* `DELETE /projects/{id}`

---

### Tasks

* `GET /projects/{projectId}/tasks`
* `GET /projects/{projectId}/tasks/{taskId}`
* `POST /projects/{projectId}/tasks`
* `PUT /projects/{projectId}/tasks/{taskId}`
* `PATCH /tasks/{taskId}/status`
* `PATCH /tasks/{taskId}/assign`
* `DELETE /projects/{projectId}/tasks/{taskId}`

---

## 🔐 Authentication & Authorization

### Permission System

#### Project Permissions

* `project:create`
* `project:read`
* `project:update`
* `project:delete`
* `project:manage_budget`

#### Task Permissions

* `task:create`
* `task:read`
* `task:update`
* `task:delete`
* `task:assign_user`
* `task:update_status`

---

### Controller Usage

```csharp
[Authorize(Permission.Project.Create)]
[HttpPost]
public async Task<ActionResult<Guid>> CreateProject(CreateProjectCommand command)
{
    var id = await mediator.Send(command);
    return CreatedAtAction(nameof(GetProject), new { id }, id);
}
```

---

## 🗄 Database Schema

### Projects

| Column            | Type             |
| ----------------- | ---------------- |
| Id                | uniqueidentifier |
| Name              | nvarchar(200)    |
| Description       | nvarchar(max)    |
| OwnerId           | uniqueidentifier |
| CreatedAt         | datetime2        |
| ExpectedStartDate | datetime2        |
| ActualEndDate     | datetime2        |
| Budget            | decimal(18,2)    |
| Currency          | nvarchar(10)     |

---

### ProjectTasks

| Column         | Type             |
| -------------- | ---------------- |
| Id             | uniqueidentifier |
| ProjectId      | uniqueidentifier |
| Title          | nvarchar(200)    |
| Description    | nvarchar(max)    |
| AssignedUserId | uniqueidentifier |
| Status         | int              |
| CreatedAt      | datetime2        |

---

## ⚙️ Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "ProjectManagementDB": "Server=.;Database=ProjectManagementDB;Trusted_Connection=True;"
  },
  "Jwt": {
    "Secret": "your-secret-key",
    "Issuer": "ProjectManagementSystem",
    "Audience": "ProjectManagementSystem.API",
    "ExpirationInMinutes": 60
  }
}
```

---

## 🤝 Contributing

Contributions are welcome. Please follow Clean Architecture and CQRS conventions.

---

## 📄 License

This project is licensed under the **MIT License**.

