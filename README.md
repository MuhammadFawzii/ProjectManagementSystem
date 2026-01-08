# ?? Project Management System API

A comprehensive, enterprise-grade **RESTful API** for project and task management built with **.NET 9**, following **Clean Architecture** principles, **CQRS pattern**, and **Domain-Driven Design (DDD)**.

## ?? Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Architecture](#architecture)
- [Technology Stack](#technology-stack)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Project Structure](#project-structure)
- [Authentication & Authorization](#authentication--authorization)
- [Database Schema](#database-schema)
- [Contributing](#contributing)
- [License](#license)

## ?? Overview

This Project Management System provides a robust backend API for managing projects, tasks, and team collaboration. It's designed with scalability, maintainability, and security in mind, making it suitable for both small teams and enterprise-level applications.

## ? Features

### ?? Authentication & Authorization
- **JWT-based authentication** with access and refresh tokens
- **Permission-based authorization** with granular access control
- **Role-based access control (RBAC)**
- Secure token refresh mechanism
- Mock authentication (ready for integration with identity providers)

### ?? Project Management
- **CRUD operations** for projects
- Budget management with multi-currency support
- Project search and filtering
- Pagination and sorting capabilities
- Project ownership and access control
- Soft delete functionality

### ? Task Management
- **CRUD operations** for tasks
- Task assignment to users
- Task status tracking (NotStarted, InProgress, Completed, Blocked, Cancelled)
- Task search and filtering within projects
- Task dependencies and relationships
- Bulk operations support

### ?? API Features
- **API versioning** (v1, v2) with backward compatibility
- **OpenAPI/Swagger** documentation
- **Scalar API Explorer** for enhanced API testing
- Comprehensive error handling with ProblemDetails
- Request/response logging
- Performance monitoring middleware

### ??? Technical Features
- **Clean Architecture** with separation of concerns
- **CQRS pattern** using MediatR
- **Repository pattern** with Unit of Work
- **AutoMapper** for object mapping
- **FluentValidation** for request validation
- **Serilog** for structured logging
- **Entity Framework Core** with SQL Server
- Generic repository with expression-based querying
- Database seeding with sample data

## ??? Architecture

The solution follows **Clean Architecture** principles with clear separation of concerns:

```
ProjectManagementSystem/
??? ?? ProjectManagementSystem.API          # Presentation Layer
?   ??? Controllers/                        # API Controllers
?   ??? Middlewares/                        # Custom middleware
?   ??? Extensions/                         # Service extensions
?   ??? OpenApi/                           # OpenAPI configuration
?
??? ?? ProjectManagementSystem.Application  # Application Layer
?   ??? Projects/                          # Project features
?   ?   ??? Commands/                      # Write operations (CQRS)
?   ?   ??? Queries/                       # Read operations (CQRS)
?   ?   ??? Dtos/                         # Data Transfer Objects
?   ??? ProjectTasks/                      # Task features
?   ??? Authentication/                    # Auth features
?   ??? Common/                           # Shared application logic
?   ??? Extensions/                       # Service registrations
?
??? ?? ProjectManagementSystem.Domain       # Domain Layer
?   ??? Entities/                         # Domain entities
?   ??? Constants/                        # Domain constants
?   ??? Exceptions/                       # Custom exceptions
?   ??? IRepositories/                    # Repository interfaces
?
??? ?? ProjectManagementSystem.Infrastructure # Infrastructure Layer
    ??? Persistence/                      # EF Core DbContext
    ??? Repositories/                     # Repository implementations
    ??? Seeders/                         # Database seeding
    ??? Permissions/                     # Authorization policies
    ??? Extensions/                      # Service registrations
```

## ??? Technology Stack

### Core Framework
- **.NET 9.0** - Latest .NET framework
- **C# 13.0** - Latest C# language features

### Libraries & Packages
- **ASP.NET Core 9.0** - Web API framework
- **Entity Framework Core 9.0** - ORM
- **MediatR** - CQRS implementation
- **AutoMapper** - Object-to-object mapping
- **FluentValidation** - Request validation
- **Serilog** - Structured logging
- **Asp.Versioning** - API versioning
- **Swashbuckle** - OpenAPI/Swagger
- **Scalar** - Modern API documentation UI

### Database
- **SQL Server** - Primary database
- **Entity Framework Core** - Database access

### Security
- **JWT Bearer Authentication** - Token-based auth
- **Microsoft.AspNetCore.Authentication.JwtBearer** - JWT middleware

## ?? Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- SQL Server 2019 or later
- Visual Studio 2022 or VS Code
- Git

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/yourusername/project-management-system.git
cd project-management-system
```

2. **Update connection string**
   
   Edit `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "ProjectManagementDB": "Server=YOUR_SERVER;Database=ProjectManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

3. **Install EF Core tools** (if not already installed)
```bash
dotnet tool install --global dotnet-ef
```

4. **Apply database migrations**
```bash
dotnet ef database update --project ProjectManagementSystem.Infrastructure --startup-project ProjectManagementSystem.API
```

5. **Run the application**
```bash
cd ProjectManagementSystem.API
dotnet run
```

6. **Access the API**
- Swagger UI: `https://localhost:7XXX/swagger`
- Scalar API Docs: `https://localhost:7XXX/scalar/v1`
- OpenAPI Spec: `https://localhost:7XXX/openapi/v1.json`

### Sample Data
The application automatically seeds the database with 15 sample projects and multiple tasks on first run, including:
- E-Commerce Platform
- Mobile Fitness App
- Data Migration Project
- IoT Platform
- NFT Marketplace
- Video Streaming Platform
- And more...

## ?? API Documentation

### Base URL
```
Development: https://localhost:7XXX/api/v{version}
Production: https://yourdomain.com/api/v{version}
```

### Authentication Endpoints

#### Login
```http
POST /api/v1/authentication/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "password123"
}
```

**Response:**
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "7a6f23b4e1d04c9a8f5b6d7c8a9e01f1",
  "expiresIn": 3600
}
```

#### Refresh Token
```http
POST /api/v1/authentication/refresh
Content-Type: application/json

{
  "refreshToken": "7a6f23b4e1d04c9a8f5b6d7c8a9e01f1"
}
```

### Project Endpoints

#### Get All Projects (with pagination, search, sorting)
```http
GET /api/v1/projects?pageNumber=1&pageSize=10&searchPhrase=e-commerce&sortBy=Name&sortDirection=Ascending
Authorization: Bearer {token}
```

**Query Parameters:**
- `pageNumber` - Page number (default: 1)
- `pageSize` - Items per page (default: 10)
- `searchPhrase` - Search in name and description
- `sortBy` - Sort by field (Name, CreatedAt, Budget, ExpectedStartDate, OwnerId)
- `sortDirection` - Ascending or Descending

**Response:**
```json
{
  "items": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "E-Commerce Platform",
      "description": "Online shopping platform",
      "ownerId": "11111111-1111-1111-1111-111111111111",
      "createdAt": "2024-08-01T00:00:00Z",
      "expectedStartDate": "2024-08-01T00:00:00Z",
      "actualEndDate": null,
      "budget": 150000,
      "tasks": []
    }
  ],
  "totalCount": 15,
  "pageSize": 10,
  "pageNumber": 1,
  "totalPages": 2
}
```

#### Get Project by ID (v1)
```http
GET /api/v1/projects/{projectId}
Authorization: Bearer {token}
```

#### Get Project by ID (v2 - includes Currency)
```http
GET /api/v2/projects/{projectId}
Authorization: Bearer {token}
```

**Response:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "E-Commerce Platform",
  "description": "Online shopping platform",
  "ownerId": "11111111-1111-1111-1111-111111111111",
  "createdAt": "2024-08-01T00:00:00Z",
  "expectedStartDate": "2024-08-01T00:00:00Z",
  "currency": "USD",
  "actualEndDate": null,
  "budget": 150000
}
```

#### Create Project
```http
POST /api/v1/projects
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "New Project",
  "description": "Project description",
  "expectedStartDate": "2025-02-01T00:00:00Z",
  "budget": 50000
}
```

**Response:** `201 Created` with project ID

#### Update Project
```http
PUT /api/v1/projects/{projectId}
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Updated Project Name",
  "description": "Updated description",
  "expectedStartDate": "2025-02-01T00:00:00Z",
  "budget": 60000
}
```

**Response:** `204 No Content`

#### Delete Project
```http
DELETE /api/v1/projects/{projectId}
Authorization: Bearer {token}
```

**Response:** `204 No Content`

#### Update Project Budget
```http
PUT /api/v1/projects/{projectId}/budget
Authorization: Bearer {token}
Content-Type: application/json

{
  "budget": 75000
}
```

**Response:** `204 No Content`

### Task Endpoints

#### Get All Tasks in Project
```http
GET /api/v1/projects/{projectId}/tasks?pageNumber=1&pageSize=10
Authorization: Bearer {token}
```

#### Get Task by ID
```http
GET /api/v1/projects/{projectId}/tasks/{taskId}
Authorization: Bearer {token}
```

**Response:**
```json
{
  "id": "4fa85f64-5717-4562-b3fc-2c963f66afa6",
  "projectId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "Implement feature X",
  "description": "Detailed task description",
  "assignedUserId": "44444444-4444-4444-4444-444444444444",
  "status": "InProgress",
  "createdAt": "2025-01-08T00:00:00Z"
}
```

#### Create Task
```http
POST /api/v1/projects/{projectId}/tasks
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Implement feature X",
  "description": "Detailed task description",
  "assignedUserId": "44444444-4444-4444-4444-444444444444"
}
```

**Response:** `201 Created` with task details

#### Update Task
```http
PUT /api/v1/projects/{projectId}/tasks/{taskId}
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Updated task title",
  "description": "Updated description",
  "assignedUserId": "44444444-4444-4444-4444-444444444444"
}
```

**Response:** `204 No Content`

#### Update Task Status
```http
PATCH /api/v1/projects/{projectId}/tasks/{taskId}/status
Authorization: Bearer {token}
Content-Type: application/json

{
  "status": "InProgress"
}
```

**Status values:** `NotStarted`, `InProgress`, `Completed`, `Blocked`, `Cancelled`

**Response:** `204 No Content`

#### Assign User to Task
```http
PATCH /api/v1/projects/{projectId}/tasks/{taskId}/assign
Authorization: Bearer {token}
Content-Type: application/json

{
  "assignedUserId": "44444444-4444-4444-4444-444444444444"
}
```

**Response:** `204 No Content`

#### Delete Task
```http
DELETE /api/v1/projects/{projectId}/tasks/{taskId}
Authorization: Bearer {token}
```

**Response:** `204 No Content`

### Error Response Format (ProblemDetails)

All errors follow RFC 9110 Problem Details format:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
  "title": "Not Found",
  "status": 404,
  "detail": "Project with ID '3fa85f64-5717-4562-b3fc-2c963f66afa6' was not found",
  "instance": "/api/v1/projects/3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

## ?? Authentication & Authorization

### Permission System

The API uses a fine-grained permission system:

#### Project Permissions
- `project:create` - Create new projects
- `project:read` - View projects
- `project:update` - Update project details
- `project:delete` - Delete projects
- `project:manage_budget` - Manage project budgets

#### Task Permissions
- `task:create` - Create tasks
- `task:read` - View tasks
- `task:update` - Update task details
- `task:delete` - Delete tasks
- `task:assign_user` - Assign users to tasks
- `task:update_status` - Change task status

### Usage in Controllers
```csharp
[Authorize(Permission.Project.Create)]
[HttpPost]
public async Task<ActionResult<Guid>> CreateProject([FromBody] CreateProjectCommand command)
{
    var projectId = await mediator.Send(command);
    return CreatedAtAction(nameof(GetProject), new { projectId }, projectId);
}
```

### JWT Token Structure

Access tokens include:
- User ID
- Email
- First Name & Last Name
- Roles (claims)
- Permissions (claims)
- Expiration time

## ?? Database Schema

### Projects Table
| Column | Type | Description |
|--------|------|-------------|
| Id | uniqueidentifier | Primary key |
| Name | nvarchar(200) | Project name |
| Description | nvarchar(max) | Project description (nullable) |
| OwnerId | uniqueidentifier | Project owner ID |
| CreatedAt | datetime2 | Creation timestamp |
| ExpectedStartDate | datetime2 | Expected start date |
| ActualEndDate | datetime2 | Actual end date (nullable) |
| Budget | decimal(18,2) | Project budget |
| Currency | nvarchar(10) | Currency code (nullable) |

### ProjectTasks Table
| Column | Type | Description |
|--------|------|-------------|
| Id | uniqueidentifier | Primary key |
| ProjectId | uniqueidentifier | Foreign key to Projects |
| Title | nvarchar(200) | Task title |
| Description | nvarchar(max) | Task description (nullable) |
| AssignedUserId | uniqueidentifier | Assigned user ID (nullable) |
| Status | int | Task status enum |
| CreatedAt | datetime2 | Creation timestamp |

### Task Status Enum Values
- `0` - NotStarted
- `1` - InProgress
- `2` - Completed
- `3` - Blocked
- `4` - Cancelled

## ??? Design Patterns & Principles

### SOLID Principles
- **Single Responsibility** - Each class has one reason to change
- **Open/Closed** - Open for extension, closed for modification
- **Liskov Substitution** - Derived classes are substitutable
- **Interface Segregation** - Clients use specific interfaces
- **Dependency Inversion** - Depend on abstractions

### Patterns Implemented
- **Clean Architecture** - Separation of concerns with 4 layers
- **CQRS** - Command Query Responsibility Segregation via MediatR
- **Repository Pattern** - Data access abstraction
- **Unit of Work** - Transaction management
- **Mediator Pattern** - Request/response handling
- **Dependency Injection** - Loose coupling throughout
- **Options Pattern** - Configuration management
- **Result Pattern** - Explicit error handling

### Middleware Pipeline
1. **Error Handling Middleware** - Global exception handling
2. **Request Time Logging Middleware** - Performance monitoring
3. **Serilog Request Logging** - Structured logging
4. **Authentication Middleware** - JWT validation
5. **Authorization Middleware** - Permission checks

## ?? Testing

### Running Tests
```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test /p:CollectCoverage=true

# Run specific project tests
dotnet test ProjectManagementSystem.Tests
```

## ?? Deployment

### Prerequisites
- SQL Server database
- Environment variables configured
- SSL certificate (for production)

### Environment Variables
```bash
ConnectionStrings__ProjectManagementDB=<connection-string>
Jwt__Secret=<jwt-secret-key>
Jwt__Issuer=<issuer>
Jwt__Audience=<audience>
```

### Docker Support (Coming Soon)
```bash
docker build -t project-management-api .
docker run -p 8080:8080 project-management-api
```

### Azure Deployment
The API is ready for deployment to:
- Azure App Service
- Azure Container Apps
- Azure Kubernetes Service (AKS)

## ?? Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Code Style Guidelines
- Follow C# coding conventions
- Use meaningful variable names
- Add XML documentation comments for public APIs
- Write unit tests for new features
- Keep methods small and focused
- Use LINQ for data queries

### Commit Message Format
```
<type>(<scope>): <subject>

<body>

<footer>
```

**Types:** feat, fix, docs, style, refactor, test, chore

## ?? License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## ?? Authors

- **Your Name** - *Initial work* - [YourGitHub](https://github.com/yourusername)

## ?? Acknowledgments

- Clean Architecture by Robert C. Martin
- CQRS pattern and MediatR library
- .NET community and contributors
- Entity Framework Core team

## ?? Support

- ?? Email: support@yourproject.com
- ?? Issues: [GitHub Issues](https://github.com/yourusername/project-management-system/issues)
- ?? Discussions: [GitHub Discussions](https://github.com/yourusername/project-management-system/discussions)

## ??? Roadmap

### Version 2.0 (Planned)
- [ ] Real-time notifications with SignalR
- [ ] File attachments for projects and tasks
- [ ] Task comments and activity history
- [ ] Email notifications
- [ ] Audit logging
- [ ] Advanced reporting and analytics
- [ ] Integration with external tools (Slack, Microsoft Teams)
- [ ] GraphQL API support
- [ ] Webhooks for event subscriptions

### Version 3.0 (Future)
- [ ] Mobile SDK (iOS & Android)
- [ ] AI-powered task recommendations
- [ ] Advanced project templates
- [ ] Time tracking integration
- [ ] Resource management
- [ ] Gantt chart support
- [ ] Multi-tenancy support
- [ ] Advanced analytics dashboard

## ?? Project Statistics

- **Lines of Code:** ~5,000+
- **API Endpoints:** 20+
- **Database Tables:** 2 (Projects, ProjectTasks)
- **Supported Operations:** CRUD + Complex queries
- **Design Patterns:** 7+
- **Middleware Components:** 3

## ?? Configuration

### appsettings.json Structure
```json
{
  "ConnectionStrings": {
    "ProjectManagementDB": "Server=.;Database=ProjectManagementDB;Trusted_Connection=True;"
  },
  "Jwt": {
    "Secret": "your-secret-key-here",
    "Issuer": "ProjectManagementSystem",
    "Audience": "ProjectManagementSystem.API",
    "ExpirationInMinutes": 60
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    }
  }
}
```

---

**? If you find this project useful, please consider giving it a star!**

**?? Found a bug? [Open an issue](https://github.com/yourusername/project-management-system/issues)**

**?? Have a feature request? [Start a discussion](https://github.com/yourusername/project-management-system/discussions)**

**?? Want to contribute? Check our [Contributing Guidelines](#contributing)**

---

Made with ?? using .NET 9 and Clean Architecture
