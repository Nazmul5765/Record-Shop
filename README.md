# Record Shop API

A RESTful ASP.NET Core Web API for managing a record shop database.

This project allows users to create, read, update, delete and filter albums stored in a SQL Server database.

The application was built using layered architecture with Controllers, Services and Repositories, alongside Entity Framework Core and SQL Server.

---

# Features

## Core Features

- Get all albums
- Get album by ID
- Add new albums
- Update album details
- Delete albums

## Filtering Features

- Get albums by album title
- Get albums by artist
- Get albums by genre
- Get albums by release year

## Health Checks

- API health check
- Database connection health check

## Error Handling

The API includes validation and error handling for invalid requests, missing data and missing resources.

---

# Technologies Used

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger/OpenAPI
- NUnit
- Moq
- Shouldly

---

# Architecture

The project follows a layered architecture:

```text
Controllers → Services → Repositories → Database
```

- Controllers handle HTTP requests and responses
- Services contain business logic
- Repositories handle database access
- Entity Framework Core handles communication with SQL Server

---

# API Endpoints

## Albums

| Method | Endpoint | Description |
|---|---|---|
| GET | `/Albums` | Get all albums |
| GET | `/Albums/{id}` | Get album by ID |
| POST | `/Albums` | Add a new album |
| PUT | `/Albums/{id}` | Update an album |
| DELETE | `/Albums/{id}` | Delete an album |

## Filtering Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/Albums/title/{title}` | Get album by title |
| GET | `/Albums/artist/{artistName}` | Get albums by artist |
| GET | `/Albums/genre/{genre}` | Get albums by genre |
| GET | `/Albums/releaseYear/{releaseYear}` | Get albums by release year |

## Health Check Endpoint

| Method | Endpoint | Description |
|---|---|---|
| GET | `/health` | Check API and database health |

---

# Running the Project

## Clone the Repository

```bash
git clone <your-github-repository-url>
```

## Navigate into the Project

```bash
cd RecordShop
```

## Apply Database Migrations

```bash
update-database
```

## Run the Application

```bash
dotnet run
```

---

# Swagger

Swagger UI is enabled for testing API endpoints.

Example:

```text
https://localhost:xxxx/swagger
```

---

# Testing

The project includes unit tests for:

- Repository layer
- Service layer
- Controller layer

Testing tools used:

- NUnit
- Moq
- Shouldly

Run tests with:

```bash
dotnet test
```

---

# Future Improvements

Possible future improvements include:

- JWT Authentication
- Docker support
- Pagination
- Sorting
- Frontend integration
- Advanced health check reporting

---

# Author

Nazmul Hussain