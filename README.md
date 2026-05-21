# Clean Architecture Template (.NET 10)

This is a project template built using **Clean Architecture** principles, ensuring the system is maintainable, scalable, and easy to test.

## 🚀 Tech Stack
- **Backend:** .NET 10.0 (ASP.NET Core API)
- **Database:** PostgreSQL with Entity Framework Core
- **Architecture:** Clean Architecture
- **Supporting Libraries:**
  - **MediatR:** Implementation of the CQRS pattern.
  - **FluentValidation:** Data validation.
  - **Mapster:** Object-to-object mapping (DTOs to Entities).
  - **Npgsql:** PostgreSQL provider for EF Core.
  - **Swashbuckle/OpenAPI:** Swagger UI for API testing.

## 🛠 Installation as a Template
You can install this project as a `dotnet new` template to easily create new projects with your own namespace.

### 1. Install the template
Open your terminal in the root directory of this project and run:
```bash
dotnet new install .
```

### 2. Create a new project using the template
Now you can create a new project anywhere on your machine:
```bash
dotnet new cleanarch -n YourProjectName
```
This will automatically replace `CleanArch` with `YourProjectName` in all files and folders.

## 📁 Project Structure
- **CleanArch.Domain:** Contains core entities, repository interfaces, value objects, and business logic. (The central layer, independent of other layers).
- **CleanArch.Application:** Contains request handlers, Commands, Queries, DTOs, and application services.
- **CleanArch.Infrastructure:** Contains database configuration (DbContext), repository implementations, migrations, and third-party service integrations.
- **CleanArch.API.Host:** The presentation layer (Web API), containing Controllers and Middleware configurations (Exception Handling, Swagger).
- **CleanArch.Share:** Contains shared code used across the entire solution (Constants, Common Models).
- **CleanArch.Test:** Contains unit and integration tests for all layers using xUnit.

## 🛠 Setup Instructions

### 1. Database Configuration
The project uses PostgreSQL. You need to configure the connection string in `appsettings.Development.json` (this file is included in `.gitignore` for local security).

If you don't have this file, create it with the following template:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=CleanArchDb;Username=postgres;Password=your_password"
  }
}
```

### 2. Run Migrations
Open your terminal in the root directory and run the following command to initialize the database:
```bash
dotnet ef database update --project CleanArch.Infrastructure --startup-project CleanArch.API.Host
```

### 3. Run the Project
```bash
dotnet run --project CleanArch.API.Host
```
Once running, navigate to `https://localhost:{port}/swagger` to test the API.

### 4. Run Tests
To run unit and integration tests across the solution, execute:
```bash
dotnet test
```

## 🐳 Docker Support
The project supports Docker, allowing you to run both the application and the PostgreSQL database with a single command.

### 1. Run with Docker Compose
```bash
docker-compose up --build
```
The application will be available at: `http://localhost:5000`

### 2. Docker Database Connection Info
- **Host:** `db`
- **Port:** `5432`
- **User/Password:** `postgres/postgres`
- **Database:** `CleanArchDb`

## 🛡 Global Exception Handling
The system integrates a `GlobalExceptionHandler` using the .NET `IExceptionHandler` interface. All exceptions will be returned in the standardized **Problem Details** (JSON) format.
