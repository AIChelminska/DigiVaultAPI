# DigiVault API

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-9.0-512BD4?logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core-9.0.1-512BD4?logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15+-4169E1?logo=postgresql&logoColor=white)
![MediatR](https://img.shields.io/badge/MediatR-12.4.1-512BD4)
![Swagger](https://img.shields.io/badge/Swagger-7.2.0-85EA2D?logo=swagger&logoColor=black)
![License](https://img.shields.io/badge/license-MIT-green)

A RESTful back-end for **DigiVault** — a digital course marketplace where users can browse, purchase, and review courses, sellers can manage their own catalogue, and administrators can oversee users, orders, and platform operations.

Built with **ASP.NET Core 9**, following a vertical-slice architecture using the CQRS pattern (MediatR).

---

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Environment Variables](#environment-variables)
  - [Running Locally](#running-locally)
  - [Running with Docker](#running-with-docker)
- [Database](#database)
- [Authentication](#authentication)
- [Seeded Data](#seeded-data)

---

## Features

- User registration and JWT-based authentication
- Course catalogue with search, category filter, price range, and multiple sort options
- Seller panel — create, update, and control visibility of courses
- Shopping cart and wishlist management
- Checkout flow with automatic commission calculation and balance crediting
- Course library (purchased courses)
- Rating and review system tied to course ownership
- Course reporting and notification system
- Admin panel — user management (activate/deactivate), course oversight, order reporting *(in progress)*

---

## Tech Stack

| Technology | Version |
|---|---|
| [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/) | 9.0 |
| [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) | 9.0.1 |
| [Npgsql EF Core Provider](https://www.npgsql.org/efcore/) | 9.0.4 |
| [PostgreSQL](https://www.postgresql.org/) | 15+ |
| [MediatR](https://github.com/jbogard/MediatR) | 12.4.1 |
| [FluentValidation](https://docs.fluentvalidation.net/) | 11.3.0 |
| [Mapster](https://github.com/MapsterMapper/Mapster) | 7.4.0 |
| [BCrypt.Net-Next](https://github.com/BcryptNet/bcrypt.net) | 4.0.3 |
| [Microsoft.AspNetCore.Authentication.JwtBearer](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-authn) | 9.0.1 |
| [Swashbuckle (Swagger)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) | 7.2.0 |

---

## Project Structure

The project follows a **vertical-slice / feature-folder** layout where all code related to a domain concept lives together.

```
DigiVaultAPI/
├── Behaviors/
│   └── ValidationBehavior.cs       # MediatR pipeline — runs FluentValidation before handlers
├── Controllers/                    # Thin controllers — delegate to MediatR
│   ├── AuthController.cs
│   ├── CoursesController.cs
│   ├── CategoriesController.cs
│   ├── CartController.cs
│   ├── WishlistController.cs
│   ├── OrdersController.cs
│   ├── ProfileController.cs
│   ├── SellerController.cs
│   ├── ReviewsController.cs
│   ├── ReportsController.cs
│   ├── NotificationsController.cs
│   └── AdminController.cs
├── Data/
│   ├── DigiVaultDbContext.cs        # EF Core context with fluent configuration
│   └── DigiVaultSeeder.cs          # Optional dev seed data
├── Exceptions/                     # Domain exceptions (404, 401, 403, 409)
├── Features/                       # Vertical slices
│   ├── Admin/
│   ├── Auth/
│   ├── Cart/
│   ├── Categories/
│   ├── Courses/
│   ├── Notifications/
│   ├── Orders/
│   ├── Profile/
│   ├── Reports/
│   ├── Review/
│   └── Wishlist/
│       ├── Handlers/               # MediatR request handlers
│       ├── Messages/               # Request/Response DTOs
│       ├── Providers/              # Read-side (queries)
│       ├── Services/               # Write-side (commands + business rules)
│       ├── Mapping/                # Mapster configuration
│       └── Validators/             # FluentValidation validators
├── Middleware/
│   └── ExceptionHandlerMiddleware.cs
├── Migrations/
├── Models/                         # EF Core entities
│   ├── User.cs
│   ├── Course.cs
│   ├── Category.cs
│   ├── Order.cs / OrderItem.cs
│   ├── UserCourse.cs
│   ├── CartItem.cs
│   ├── WishlistItem.cs
│   ├── Review.cs
│   ├── Notification.cs
│   ├── CourseReport.cs
│   └── PlatformSettings.cs
├── appsettings.json
├── appsettings.Development.json.example
├── Dockerfile
└── Program.cs
```

---

## API Endpoints

> Endpoints marked with **🔒** require a valid JWT Bearer token. Endpoints marked with **🛡️** additionally require the `Worker` role (admin).

### Auth — `/api/auth`

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| `POST` | `/api/auth/login` | — | Authenticate and receive a JWT token |
| `POST` | `/api/auth/register` | — | Create a new user account |

### Courses — `/api/courses`

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| `GET` | `/api/courses` | — | Paginated course catalogue (search, category, price range, sort) |
| `GET` | `/api/courses/popular` | — | Top courses by sales count |
| `GET` | `/api/courses/newest` | — | Most recently added courses |
| `GET` | `/api/courses/top-rated` | — | Highest-rated courses |
| `GET` | `/api/courses/{id}` | — | Course detail |
| `GET` | `/api/courses/purchased` | 🔒 | Courses owned by the authenticated user |

**Query parameters for `GET /api/courses`:**  
`search`, `categoryId`, `minPrice`, `maxPrice`, `sortBy` (`popular` \| `newest` \| `top-rated` \| `price-asc` \| `price-desc`), `page`, `pageSize`

### Categories — `/api/categories`

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| `GET` | `/api/categories` | — | List all categories |

### Cart — `/api/cart`

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| `GET` | `/api/cart` | 🔒 | Get current user's cart |
| `POST` | `/api/cart/{idCourse}` | 🔒 | Add course to cart |
| `DELETE` | `/api/cart/{idCourse}` | 🔒 | Remove course from cart |

### Wishlist — `/api/wishlist`

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| `GET` | `/api/wishlist` | 🔒 | Get current user's wishlist |
| `POST` | `/api/wishlist/{idCourse}` | 🔒 | Add course to wishlist |
| `DELETE` | `/api/wishlist/{idCourse}` | 🔒 | Remove course from wishlist |

### Orders — `/api/orders`

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| `GET` | `/api/orders` | 🔒 | List orders for the authenticated user |
| `GET` | `/api/orders/{idOrder}` | 🔒 | Order detail |
| `POST` | `/api/orders` | 🔒 | Checkout — creates an order from the cart |

### Profile — `/api/profile`

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| `GET` | `/api/profile` | 🔒 | Get profile data |
| `PATCH` | `/api/profile/name` | 🔒 | Update display name |
| `PATCH` | `/api/profile/email` | 🔒 | Update email (requires current password) |
| `PATCH` | `/api/profile/password` | 🔒 | Change password (requires current password) |

### Seller — `/api/seller`

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| `GET` | `/api/seller/courses` | 🔒 | List courses created by the authenticated seller |
| `POST` | `/api/seller/courses` | 🔒 | Create a new course |
| `PUT` | `/api/seller/courses/{id}` | 🔒 | Update a course |
| `PATCH` | `/api/seller/courses/{id}/visibility` | 🔒 | Toggle course visibility |

### Reviews — `/api/courses/{idCourse}/reviews`

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| `GET` | `/api/courses/{idCourse}/reviews` | — | List reviews for a course |
| `POST` | `/api/courses/{idCourse}/reviews` | 🔒 | Add or update a review (must own the course) |
| `DELETE` | `/api/courses/{idCourse}/reviews` | 🔒 | Delete own review |

### Reports — `/api/reports`

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| `POST` | `/api/reports` | 🔒 | Submit a course report |

### Notifications — `/api/notifications`

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| `GET` | `/api/notifications` | 🔒 | List notifications for the authenticated user |
| `PATCH` | `/api/notifications/{idNotification}` | 🔒 | Mark a notification as read |

### Admin — `/api/admin`

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| `GET` | `/api/admin/users` | 🛡️ | List all users |
| `POST` | `/api/admin/users/set-as-active` | 🛡️ | Activate a user account |
| `POST` | `/api/admin/users/set-as-not-active` | 🛡️ | Deactivate a user account |
| `GET` | `/api/admin/courses` | 🛡️ | Full course catalogue including hidden courses |
| `GET` | `/api/admin/orders` | 🛡️ | Paginated order list with optional filters |

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9)
- [PostgreSQL 15+](https://www.postgresql.org/download/)

### Environment Variables

Copy the example configuration file and fill in your values:

```bash
cp DigiVaultAPI/appsettings.Development.json.example DigiVaultAPI/appsettings.Development.json
```

`appsettings.Development.json`:

```json
{
  "Jwt": {
    "Key": "YOUR_SECRET_KEY_MIN_32_CHARACTERS_LONG",
    "Issuer": "DigiVaultAPI",
    "Audience": "DigiVaultMobile",
    "ExpiryMinutes": 60
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=DigiVaultDb;Username=YOUR_USER;Password=YOUR_PASSWORD"
  }
}
```

| Key | Description |
|-----|-------------|
| `Jwt:Key` | HMAC-SHA256 secret — **minimum 32 characters** |
| `Jwt:Issuer` | Token issuer claim |
| `Jwt:Audience` | Token audience claim |
| `Jwt:ExpiryMinutes` | Token lifetime in minutes |
| `ConnectionStrings:DefaultConnection` | PostgreSQL connection string |

### Running Locally

```bash
# 1. Clone the repository
git clone https://github.com/AIChelminska/DigiVaultAPI.git
cd DigiVaultAPI

# 2. Configure environment (see above)
cp DigiVaultAPI/appsettings.Development.json.example DigiVaultAPI/appsettings.Development.json
# Edit DigiVaultAPI/appsettings.Development.json with your values

# 3. Restore dependencies
dotnet restore

# 4. Run the application
dotnet run --project DigiVaultAPI
```

The API will be available at:
- HTTP: `http://localhost:5052`
- HTTPS: `https://localhost:7084`
- Swagger UI: `http://localhost:5052/swagger` *(Development only)*

### Running with Docker

The repository ships with a `docker-compose.yml` that starts **both** the API and a PostgreSQL database together.

```bash
# 1. Create your local env file from the template
cp .env.example .env
# Edit .env — fill in POSTGRES_PASSWORD and JWT_KEY at minimum

# 2. Start both services (API + PostgreSQL)
docker compose up --build

# 3. Stop and remove containers
docker compose down
```

The API will be available at `http://localhost:8080` (configurable via `API_HOST_PORT` in `.env`).  
The database is exposed on `localhost:5433` by default (configurable via `POSTGRES_HOST_PORT`).

> Migrations and seeding run automatically on API startup — no extra steps needed.

---

## Database

The application uses **PostgreSQL** with Entity Framework Core Code-First migrations.

**Migrations run automatically on startup** — no manual `dotnet ef` command is required. On first run the full schema is created from `20260309222115_InitialCreate`.

Tables created by the migration:

| Table | Description |
|-------|-------------|
| `Users` | User accounts with roles, balance, and active flag |
| `Courses` | Course listings with pricing, rating aggregates, and visibility |
| `Categories` | Course categories |
| `Orders` / `OrderItems` | Purchase records with price and commission snapshots |
| `UserCourses` | Many-to-many: users ↔ owned courses |
| `CartItems` | Per-user shopping cart |
| `WishlistItems` | Per-user wishlist |
| `Reviews` | Course reviews (one per user per course) |
| `Notifications` | User notifications |
| `CourseReports` | User-submitted course reports |
| `PlatformSettings` | Commission rate and platform balance |

To run migrations manually (if needed):

```bash
dotnet ef database update --project DigiVaultAPI
```

---

## Authentication

The API uses **JWT Bearer** tokens.

1. Obtain a token via `POST /api/auth/login` or `POST /api/auth/register`.
2. Include the token in the `Authorization` header of subsequent requests:

```
Authorization: Bearer <your_token>
```

**Token claims:**

| Claim | Value |
|-------|-------|
| `IdUser` | User's database ID |
| `Login` | Username |
| `ClaimTypes.Role` | `User` or `Worker` |
| `FirstName` / `LastName` | Display name |

**Roles:**

| Role | Description |
|------|-------------|
| `User` | Standard authenticated user — can buy courses, review, manage cart, etc. |
| `Worker` | Administrator — has access to all `User` endpoints plus `/api/admin/*` |

Swagger UI in Development includes a **Authorize** button for pasting a Bearer token directly.

---

## Seeded Data

On startup the `DigiVaultSeeder` populates an empty database with sample data so the API is fully explorable via Swagger or any HTTP client without any manual setup.

### Test accounts

| Login | Password | Role | Notes |
|-------|----------|------|-------|
| `test` | `test` | User | Has balance, orders, and purchased courses |
| `test2` | `test` | User | |
| `test3` | `test` | User | |
| `test4` | `test` | User | |
| `test5` | `test` | User | |
| `admin` | `admin` | Worker (Admin) | Full access to `/api/admin/*` endpoints |

### Other seeded content

- Platform settings (commission rate)
- Categories and a rich set of courses
- Cart items, wishlist items, orders, order items
- Reviews, notifications, and course reports

---

## License

This project is open-source and available under the [MIT License](LICENSE).
