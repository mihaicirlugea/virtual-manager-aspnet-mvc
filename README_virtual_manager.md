# Virtual Manager – ASP.NET Core MVC

A web application that works as a personal "virtual manager": it groups your **tasks** and your **money** under
user-defined *ecosystems* (areas of life or projects) and shows a dashboard with the overall picture.

> **Status: working prototype / work in progress (not production-ready).**
> The core modules (authentication, ecosystems, activities, finances, dashboard) are implemented.
> Several planned modules are only half-built and there are known security and design issues, all listed
> honestly below. The UI is in Romanian.

## Features

- **Authentication & roles** – register / login / logout with ASP.NET Core Identity; an `Admin` role with a page that
  lists all registered users.
- **Ecosystems** – full CRUD. An ecosystem is a container (e.g. "Faculty", "Side project") with a macro objective;
  its details page shows the activities and financial flows linked to it.
- **Activities (tasks)** – full CRUD with status (Backlog / In Progress / Done), effort in hours and the owning ecosystem.
- **Finances** – full CRUD for income and expense transactions (amounts in RON) linked to an ecosystem.
- **Dashboard** – active tasks, total balance, this month's expenses, a pie chart of expenses per ecosystem and a bar
  chart of effort hours over the last 7 days (Chart.js).

## Tech stack

C# · .NET 8 · ASP.NET Core MVC (Razor views) · ASP.NET Core Identity · Entity Framework Core 8 · SQLite ·
Bootstrap · Chart.js

**Architecture:** `Controllers → Services → Repositories → EF Core (ApplicationDbContext)`, wired through
dependency injection in `Program.cs`.

## How to run

Requirements: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

```bash
dotnet tool install --global dotnet-ef      # once
cd PAW_PROJECT
dotnet ef database update                   # creates the SQLite database (Ecosisteme.db)
dotnet run
```

The database is **not** included in the repository and is **not** created automatically at startup, so
`dotnet ef database update` is required on the first run (in Visual Studio: *Package Manager Console →
`Update-Database`*). On startup the app seeds the `Admin` and `User` roles and a default admin account
(see `Data/IdentitySeed.cs`).

## Known issues and limitations

### Security / privacy
1. **Data is not separated per user.** `Ecosistem`, `Activitate` and `FluxCapital` have no owner, so every logged-in
   user sees and can edit everyone's data. This is the biggest gap for an app that handles personal finances.
2. **Delete via GET.** In *Activities* and *Finances*, the "Delete" button is a plain link that deletes immediately
   (no confirmation, no POST, no anti-forgery token), so it can be triggered accidentally or by a malicious link.
   Ecosystems already use the correct confirm-then-POST flow.
3. **Default admin account with credentials hard-coded** in `Data/IdentitySeed.cs`. Fine for a demo, must be
   changed (e.g. read from configuration / user secrets) before any real deployment.
4. **Weak registration flow.** Accounts are created with `EmailConfirmed = true` (no email verification), the
   password policy is relaxed (min. 6 characters, no complexity), and new users are never assigned the `User` role
   (the role is created but not used).
5. **Email sent to a third party.** The navbar avatar is loaded from `ui-avatars.com` using the user's email as a
   URL parameter. The `PozaProfilBase64` profile-picture field exists but is not used yet.

### Unfinished parts
6. `Eticheta` (tags), `ObiectivIntermediar` (intermediate goals) and `Risipa` (waste / losses) exist as models and
   database tables but have **no controllers or views yet**.
7. `Views/Home/Tasks`, `AddTask`, `AddTransaction`, `Finance` and `Settings` are **static mock-ups** (forms do not
   submit, hard-coded sample values) and are not reachable from any controller action. They are leftovers
   from the first design iteration and should be removed or finished.
8. No automated tests.

### Code quality / design
9. The repository classes live in `Views/Repositories/` instead of a top-level `Repositories/` folder, and one file is
   named `IFluxCapitalRepository.cs.cs`. `IFluxCapitalService` is declared without a namespace.
10. The connection string in `appsettings*.json` (SQL Server LocalDB) is **unused**: `Program.cs` hard-codes a SQLite
    connection and the `.csproj` references both providers.
11. Dashboard calculations load whole tables in memory and aggregate them in C# instead of in the database. "Expenses
    this month" compares only the month number, not the year. Weekday names depend on the server culture.
12. Statuses (`0/1/2`) and transaction direction (`1/-1`) are magic numbers; they should be enums.
13. `ModelState.Remove("FluxCapitals")` in `EcosistemsController` refers to a property that does not exist
    (it is called `Fluxuri`); harmless today but misleading.

## Roadmap

- [ ] Add an owner (`UserId`) to ecosystems, activities and transactions and filter every query by the current user.
- [ ] Change Delete actions to POST with confirmation and anti-forgery protection.
- [ ] Move the seeded admin credentials to configuration / user secrets; add email confirmation.
- [ ] Apply database migrations automatically in Development; use a single configurable connection string.
- [ ] Build the tags, intermediate-goals and waste modules, or remove them.
- [ ] Remove the static mock-up views; add unit tests for the services.
- [ ] Replace magic numbers with enums; move aggregation to the database; self-host Chart.js and avatars.
- [ ] Move `Views/Repositories/` to `Repositories/`.

## Project structure

```
PAW_PROJECT.sln
└── PAW_PROJECT/
    ├── Controllers/    # MVC controllers (Account, Admin, Ecosistems, Activitati, FluxCapitals, Home)
    ├── Services/       # business layer (interfaces + implementations)
    ├── Views/          # Razor views (+ Repositories/, see issue 9)
    ├── Models/         # entities and view models
    ├── Data/           # ApplicationDbContext, Identity seed
    ├── Migrations/     # EF Core migrations (SQLite)
    └── Program.cs      # DI, Identity, authorization, pipeline
```
