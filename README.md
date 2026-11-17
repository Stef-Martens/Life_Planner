# Life Planner

A full-stack planning app that breaks yearly goals down into monthly, weekly and daily tasks.

You set goals for this year (and next), choose which ones to focus on each month, plan your week, and track your daily tasks from a single dashboard.

![.NET 8](https://img.shields.io/badge/.NET-8-512BD4?logo=dotnet&logoColor=white)
![React](https://img.shields.io/badge/React-18-61DAFB?logo=react&logoColor=black)
![TypeScript](https://img.shields.io/badge/TypeScript-5-3178C6?logo=typescript&logoColor=white)
![Auth0](https://img.shields.io/badge/Auth-Auth0-EB5424?logo=auth0&logoColor=white)

## Features

- **Sign-in with Auth0** (OpenID Connect). The API validates JWT bearer tokens against Auth0's JWKS.
- **Onboarding flow**: if you haven't set goals for the current or next year yet, the app asks you for them first.
- **Goal hierarchy**: yearly goals → monthly goals → daily agenda and tasks, with categories and subtasks.
- **Drag-and-drop planning** on the dashboard (react-dnd).

## Tech stack

| Layer    | Technology |
|----------|------------|
| Frontend | React 18, TypeScript, Vite, React Router, Tailwind CSS + daisyUI, Axios |
| Backend  | ASP.NET Core 8 Web API, Entity Framework Core 8, Swagger/OpenAPI |
| Database | SQL Server (Windows) or MySQL via Pomelo (macOS/Linux, Docker) |
| Auth     | Auth0 (JWT bearer) |
| Tests    | xUnit, Moq, `WebApplicationFactory` integration tests, EF Core InMemory |

## Architecture

```
lifeplanner.client/      React + Vite single-page app
LifePlanner.Server/
├── Controllers/         REST endpoints (Goals, MonthlyGoals, DailyAgendas, DailyTasks, Subtasks, Categories, Users)
├── Services/            Business logic (generic service + specialised Goal/User services)
├── Repositories/        Data access (generic repository pattern over EF Core)
├── Models/              Entity models
├── Data/                DbContext
└── Migrations/          EF Core migrations
Tests/                   Unit and integration tests
```

The backend uses a **Controller → Service → Repository** layering, with generic base classes so that new entities need very little boilerplate. Dependencies are registered through ASP.NET Core's built-in DI container.

## Running locally

**Prerequisites:** .NET 8 SDK, Node 20+, and SQL Server LocalDB (Windows) or MySQL (macOS/Linux, e.g. in Docker).

1. **Database connection.** On Windows the app uses LocalDB out of the box. On macOS/Linux, set the MySQL connection string with user secrets so that no password ends up in git:

   ```bash
   cd LifePlanner.Server
   dotnet user-secrets set "ConnectionStrings:LifePlannerServerContextDocker" \
     "Server=localhost;Database=LifePlanner;User=root;Password=<your-password>"
   ```

2. **Apply migrations and start the API:**

   ```bash
   dotnet ef database update --project LifePlanner.Server
   dotnet run --project LifePlanner.Server
   ```

   Swagger UI is available at `/swagger` in development.

3. **Start the frontend:**

   ```bash
   cd lifeplanner.client
   npm install
   npm run dev
   ```

4. **Run the tests:**

   ```bash
   dotnet test
   ```

## Status

This is a personal project and still a work in progress. The backend API and data model cover the full goal → month → day hierarchy. The frontend currently has the onboarding flow and the dashboard; the monthly and weekly planning screens are next.
