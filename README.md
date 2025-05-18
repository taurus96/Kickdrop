# Kickdrop

Kickdrop is a sample ASP.NET Core Web API application for managing a shoe product catalog. It demonstrates modern .NET backend development practices, including Entity Framework Core, JWT authentication, and integration/unit testing.

---

## Table of Contents

- [Features](#features)
- [Architecture Overview](#architecture-overview)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Setup & Bootstrapping](#setup--bootstrapping)
- [Project Structure](#project-structure)
- [Testing](#testing)
- [API Endpoints](#api-endpoints)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [License](#license)

---

## Features

- RESTful API for managing shoes (CRUD)
- JWT-based authentication
- Entity Framework Core with SQL Server (LocalDB by default)
- Health check endpoint
- Swagger/OpenAPI documentation
- Unit and integration tests
- Docker support for containerized deployment
---

## Architecture Overview

- **ASP.NET Core Web API**: Main backend framework.
- **Entity Framework Core**: Data access layer, using code-first migrations.
- **Authentication**: JWT Bearer tokens, with demo login endpoint.
- **Dependency Injection**: All services and repositories are registered via DI.
- **Testing**: xUnit for unit and integration tests, using in-memory or LocalDB as appropriate.

**Core Layers:**
- `Controllers/` — API endpoints (e.g., `ProductController`, `AuthController`, `HealthController`)
- `Services/` — Business logic (e.g., `ProductService`)
- `Data/` — EF Core `DbContext` and migrations
- `Models/` — Domain models (e.g., `Shoe`)
- `Tests/` — Unit and integration tests

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
- (Optional) [Visual Studio 2022+](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Setup & Bootstrapping

1. **Clone the repository:**
`````sh
   git clone https://github.com/your-org/kickdrop.git
   cd kickdrop
`````
2. **Restore dependencies:**
`````sh
  dotnet restore
`````

3. **Apply database migrations:**
`````sh
  cd Kickdrop.Api
  dotnet ef database update
`````

4. **Run the API:**
`````sh
  dotnet run --project Kickdrop.Api
`````

The API will be available at https://localhost:18620 (see launchSettings.json).

5. **Explore the API:**
Swagger UI: https://localhost:18620/swagger

---

## Docker Support

A `Dockerfile` is provided in the `Kickdrop.Api` directory for containerized builds and deployments.

**To build and run the API with Docker:**

```sh
cd Kickdrop.Api
docker build -t kickdrop-api .
docker run -p 18620:18620 --name kickdrop-api kickdrop-api
```

- The API will be available at [http://localhost:18620](http://localhost:18620).
- If you use SQL Server, ensure your connection string points to a reachable SQL Server instance (not LocalDB).

---

## Project Structure

```
Kickdrop/
├── Kickdrop.Api/         # Main Web API project
│   ├── Controllers/
│   ├── Data/
│   ├── Models/
│   ├── Services/
│   ├── appsettings.json
│   └── Program.cs
├── Kickdrop.Tests/       # Unit and integration tests
│   ├── Integration/
│   ├── Unit/
│   └── CustomWebApplicationFactory.cs
└── README.md
```

---

## Testing
Unit Tests: Located in Unit
Integration Tests: Located in Integration

To run all tests:
```sh
dotnet run --project Kickdrop.Api
```

Integration tests use an in-memory database by default, but can be configured to use LocalDB as in appsettings.json.

---

## API Endpoints
- `POST /api/auth/login` — Obtain JWT token (demo: admin/password)
- `GET /api/product` — List all shoes
- `GET /api/product/{id}` — Get shoe by ID
- `POST /api/product` — Create a new shoe (requires JWT)
- `PUT /api/product/{id}` — Update a shoe (requires JWT)
- `DELETE /api/product/{id}` — Delete a shoe (requires JWT)
- `GET /api/product/color/{color}` — List shoes by color
- `GET /api/health` — Health check

---

## Configuration
Contributions are welcome! Please fork the repository and submit a pull request.

---

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

---

## License

This project is licensed under the MIT License.