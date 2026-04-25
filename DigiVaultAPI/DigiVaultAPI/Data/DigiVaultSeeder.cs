using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Data;

public static class DigiVaultSeeder
{
    public static void Seed(DigiVaultDbContext db)
    {
        // Kolejność ważna — najpierw tabele bez FK, potem zależne

        SeedPlatformSettings(db);
        SeedUsers(db);
        SeedCategories(db);
        SeedCourses(db);
        SeedCartItems(db);
        SeedCourseReports(db);
        SeedNotifications(db);
        SeedOrders(db);
        SeedOrderItems(db);
        SeedReviews(db);
        SeedUserCourses(db);
        SeedWishlistItems(db);
        ResetSequences(db);
    }

    private static void ResetSequences(DigiVaultDbContext db)
    {
        db.Database.ExecuteSqlRaw(@"
            SELECT setval(pg_get_serial_sequence('""Categories""',    'IdCategory'),    COALESCE(MAX(""IdCategory""),    1)) FROM ""Categories"";
            SELECT setval(pg_get_serial_sequence('""Users""',         'IdUser'),         COALESCE(MAX(""IdUser""),         1)) FROM ""Users"";
            SELECT setval(pg_get_serial_sequence('""Courses""',       'IdCourse'),       COALESCE(MAX(""IdCourse""),       1)) FROM ""Courses"";
            SELECT setval(pg_get_serial_sequence('""Orders""',        'IdOrder'),        COALESCE(MAX(""IdOrder""),        1)) FROM ""Orders"";
            SELECT setval(pg_get_serial_sequence('""OrderItems""',    'IdOrderItem'),    COALESCE(MAX(""IdOrderItem""),    1)) FROM ""OrderItems"";
            SELECT setval(pg_get_serial_sequence('""Reviews""',       'IdReview'),       COALESCE(MAX(""IdReview""),       1)) FROM ""Reviews"";
            SELECT setval(pg_get_serial_sequence('""Notifications""', 'IdNotification'), COALESCE(MAX(""IdNotification""), 1)) FROM ""Notifications"";
            SELECT setval(pg_get_serial_sequence('""CourseReports""', 'IdCourseReport'), COALESCE(MAX(""IdCourseReport""), 1)) FROM ""CourseReports"";
            SELECT setval(pg_get_serial_sequence('""CartItems""',     'IdCartItem'),     COALESCE(MAX(""IdCartItem""),     1)) FROM ""CartItems"";
            SELECT setval(pg_get_serial_sequence('""UserCourses""',   'IdUserCourse'),   COALESCE(MAX(""IdUserCourse""),   1)) FROM ""UserCourses"";
            SELECT setval(pg_get_serial_sequence('""WishlistItems""', 'IdWishlistItem'), COALESCE(MAX(""IdWishlistItem""), 1)) FROM ""WishlistItems"";
        ");
    }

    private static void SeedPlatformSettings(DigiVaultDbContext db)
    {
        if (db.PlatformSettings.Any()) return;

        db.PlatformSettings.Add(new PlatformSettings
        {
            IdPlatformSettings = 1,
            CommissionRate     = 0.0500m,
            PlatformBalance    = 28.40m
        });
        db.SaveChanges();
    }

    private static void SeedUsers(DigiVaultDbContext db)
    {
        if (db.Users.Any()) return;

        db.Users.AddRange(
            new User { IdUser = 1, Login = "test",  Email = "test",              PasswordHash = "$2a$12$o2zvFRqvFuU42bxJ4xj54.PnMxSGqyELLbK98.4eOE9RdX/LfzKqm", FirstName = "Aleksandra", LastName = "Chełmińska", Role = UserRole.User, Balance = 351.47m, TotalWithdrawn = 0.00m, WarningsCount = 0, IsActive = true },
            new User { IdUser = 2, Login = "test2", Email = "testowy",           PasswordHash = "$2a$12$o2zvFRqvFuU42bxJ4xj54.PnMxSGqyELLbK98.4eOE9RdX/LfzKqm", FirstName = "Damian",    LastName = "Mrowka",     Role = UserRole.User, Balance = 0.00m,   TotalWithdrawn = 0.00m, WarningsCount = 0, IsActive = true },
            new User { IdUser = 3, Login = "test3", Email = "emailtest",         PasswordHash = "$2a$12$o2zvFRqvFuU42bxJ4xj54.PnMxSGqyELLbK98.4eOE9RdX/LfzKqm", FirstName = "Agnieszka", LastName = "Kowalska",   Role = UserRole.User, Balance = 188.10m, TotalWithdrawn = 0.00m, WarningsCount = 0, IsActive = true },
            new User { IdUser = 4, Login = "admin", Email = "admin",             PasswordHash = "$2a$12$WKawXjn18nlhxZAaPJxGpuLW7.8daW1PTy8yqMqWMwukaOKF72vpm", FirstName = "Ola",       LastName = "Chełmińska", Role = UserRole.Worker, Balance = 0.00m,   TotalWithdrawn = 0.00m, WarningsCount = 0, IsActive = true },
            new User { IdUser = 5, Login = "test4", Email = "test4",             PasswordHash = "$2a$12$o2zvFRqvFuU42bxJ4xj54.PnMxSGqyELLbK98.4eOE9RdX/LfzKqm", FirstName = "Alicja",   LastName = "Kowal",      Role = UserRole.User, Balance = 0.00m,   TotalWithdrawn = 0.00m, WarningsCount = 0, IsActive = true },
            new User { IdUser = 6, Login = "test5", Email = "bguzik@gmail.com",  PasswordHash = "$2a$12$o2zvFRqvFuU42bxJ4xj54.PnMxSGqyELLbK98.4eOE9RdX/LfzKqm", FirstName = "Bartosz",  LastName = "Guzik",      Role = UserRole.User, Balance = 0.00m,   TotalWithdrawn = 0.00m, WarningsCount = 0, IsActive = true }
        );
        db.SaveChanges();
    }

    private static void SeedCategories(DigiVaultDbContext db)
    {
        if (db.Categories.Any()) return;

        db.Categories.AddRange(
            new Category { IdCategory = 1,  Name = "ReactJS",                 IsActive = true },
            new Category { IdCategory = 2,  Name = "Web Development",          IsActive = true },
            new Category { IdCategory = 3,  Name = "Mobile Development",       IsActive = true },
            new Category { IdCategory = 4,  Name = "DevOps & Cloud",           IsActive = true },
            new Category { IdCategory = 5,  Name = "Databases",                IsActive = true },
            new Category { IdCategory = 6,  Name = "Cybersecurity",            IsActive = true },
            new Category { IdCategory = 7,  Name = "Data Science & AI",        IsActive = true },
            new Category { IdCategory = 8,  Name = "UI/UX Design",             IsActive = true },
            new Category { IdCategory = 9,  Name = "Programming Languages",    IsActive = true },
            new Category { IdCategory = 10, Name = "Software Architecture",    IsActive = true },
            new Category { IdCategory = 11, Name = "Productivity & Tools",     IsActive = true }
        );
        db.SaveChanges();
    }

    private static void SeedCourses(DigiVaultDbContext db)
    {
        if (db.Courses.Any()) return;

        db.Courses.AddRange(
            new Course { IdCourse = 1,  Title = "React od Podstaw - ucz się tworząc grę.",                       Description = "Twórz profesjonalne aplikacje front-endowe z użyciem React. Stwórz mini grę AutoClicker, ucz się praktykując!",                                                                       Price = 100.00m,  ImageUrl = "https://images.unsplash.com/photo-1633356122544-f134324a6cee?w=400", IdCategory = 3,  IdUser = 1, SalesCount = 0,   RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 10, 21, 55, 42, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 2,  Title = "React Native od zera — zbuduj własną aplikację",                 Description = "Kompleksowy kurs React Native. Nauczysz się tworzyć aplikacje mobilne na iOS i Android od podstaw.",                                                                                 Price = 149.99m,  ImageUrl = "https://images.unsplash.com/photo-1551650975-87deedd944c3?w=400", IdCategory = 1,  IdUser = 1, SalesCount = 0,   RatingsCount = 2, AverageRating = 3.50m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 10, 21, 59, 54, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 3,  Title = "ASP.NET Core Web API — buduj backendy jak profesjonalista",      Description = "Naucz się tworzyć REST API w .NET 9 z użyciem CQRS, MediatR, Entity Framework Core i PostgreSQL.",                                                                                   Price = 199.99m,  ImageUrl = "https://images.unsplash.com/photo-1587620962725-abab7fe55159?w=400", IdCategory = 1,  IdUser = 1, SalesCount = 0,   RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 10, 22,  0, 26, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 4,  Title = "PostgreSQL — bazy danych dla programistów",                      Description = "Praktyczny kurs PostgreSQL od podstaw. Modelowanie danych, zaawansowane zapytania SQL, indeksy, transakcje i optymalizacja wydajności.",                                           Price = 99.99m,   ImageUrl = "https://images.unsplash.com/photo-1593642632559-0c6d3fc62b89?w=400", IdCategory = 1,  IdUser = 1, SalesCount = 0,   RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 10, 22,  0, 33, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 5,  Title = "Docker i Kubernetes — konteneryzacja w praktyce",                Description = "Od podstaw Dockera przez Docker Compose aż po wdrożenia na Kubernetes.",                                                                                                           Price = 179.99m,  ImageUrl = "https://images.unsplash.com/photo-1618401471353-b98afee0b2eb?w=400", IdCategory = 1,  IdUser = 1, SalesCount = 0,   RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 10, 22,  0, 45, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 6,  Title = "Figma dla programistów — projektuj UI jak designer",             Description = "Kurs projektowania interfejsów w Figmie skierowany do programistów.",                                                                                                               Price = 129.99m,  ImageUrl = "https://images.unsplash.com/photo-1561070791-2526d30994b5?w=400", IdCategory = 1,  IdUser = 1, SalesCount = 0,   RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 10, 22,  0, 51, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 7,  Title = "React od Podstaw – Kompletny Kurs",                              Description = "Naucz się Reacta od zera. Komponenty, hooki, state management i wiele więcej.",                                                                                                   Price = 149.99m,  ImageUrl = "https://images.unsplash.com/photo-1633356122544-f134324a6cee?w=400", IdCategory = 1,  IdUser = 1, SalesCount = 95,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 8,  Title = "React Hooks w Praktyce",                                         Description = "useState, useEffect, useContext, useReducer – wszystko o hookach w jednym miejscu.",                                                                                               Price = 99.99m,   ImageUrl = "https://images.unsplash.com/photo-1587620962725-abab7fe55159?w=400", IdCategory = 1,  IdUser = 1, SalesCount = 64,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 9,  Title = "Redux Toolkit – Zarządzanie Stanem",                             Description = "Naucz się Redux Toolkit i opanuj zarządzanie globalnym stanem w dużych aplikacjach.",                                                                                              Price = 129.99m,  ImageUrl = "https://images.unsplash.com/photo-1555066931-4365d14bab8c?w=400", IdCategory = 1,  IdUser = 1, SalesCount = 41,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = false, CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 10, Title = "React z TypeScript – Profesjonalne Aplikacje",                   Description = "Połącz Reacta z TypeScriptem i pisz bezpieczny, skalowalny kod.",                                                                                                                 Price = 179.99m,  ImageUrl = "https://images.unsplash.com/photo-1516116216624-53e697fedbea?w=400", IdCategory = 1,  IdUser = 1, SalesCount = 160, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 11, Title = "Next.js – React dla Produkcji",                                  Description = "SSR, SSG, API Routes i wdrożenie na Vercel. Pełny kurs Next.js.",                                                                                                                 Price = 199.99m,  ImageUrl = "https://images.unsplash.com/photo-1618477388954-7852f32655ec?w=400", IdCategory = 1,  IdUser = 1, SalesCount = 280, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 12, Title = "HTML i CSS od Zera",                                             Description = "Fundamenty tworzenia stron internetowych. Idealne dla początkujących.",                                                                                                           Price = 49.99m,   ImageUrl = "https://images.unsplash.com/photo-1507721999472-8ed4421c4af2?w=400", IdCategory = 2,  IdUser = 1, SalesCount = 381, RatingsCount = 2, AverageRating = 3.50m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 13, Title = "JavaScript – Pełny Kurs 2024",                                   Description = "ES6+, DOM, asynchroniczność, fetch API. Zostań JS developerem.",                                                                                                                 Price = 149.99m,  ImageUrl = "https://images.unsplash.com/photo-1579468118864-1b9ea3c0db4a?w=400", IdCategory = 2,  IdUser = 1, SalesCount = 290, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 14, Title = "CSS Grid i Flexbox w Praktyce",                                  Description = "Opanuj nowoczesne layouty CSS i twórz responsywne strony.",                                                                                                                      Price = 79.99m,   ImageUrl = "https://images.unsplash.com/photo-1621839673705-6617adf9e890?w=400", IdCategory = 2,  IdUser = 1, SalesCount = 150, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 15, Title = "TypeScript – Od Podstaw do Zaawansowanych",                      Description = "Typy, interfejsy, generyki – wszystko czego potrzebujesz by pisać lepszy JS.",                                                                                                  Price = 129.99m,  ImageUrl = "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=400", IdCategory = 2,  IdUser = 1, SalesCount = 175, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 16, Title = "Vue.js 3 – Kompletny Przewodnik",                                Description = "Composition API, Pinia, Vue Router. Zbuduj profesjonalne aplikacje webowe.",                                                                                                    Price = 159.99m,  ImageUrl = "https://images.unsplash.com/photo-1585079374502-415f8516dcc3?w=400", IdCategory = 2,  IdUser = 1, SalesCount = 72,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 17, Title = "React Native od Zera",                                           Description = "Twórz aplikacje mobilne na iOS i Android używając JavaScript i React.",                                                                                                          Price = 149.99m,  ImageUrl = "https://images.unsplash.com/photo-1512941937669-90a1b58e7e9c?w=400", IdCategory = 3,  IdUser = 1, SalesCount = 195, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 18, Title = "Flutter – Aplikacje Mobilne w Dart",                             Description = "Jeden kod, dwie platformy. Naucz się Fluttera od podstaw.",                                                                                                                    Price = 169.99m,  ImageUrl = "https://images.unsplash.com/photo-1551650975-87deedd944c3?w=400", IdCategory = 3,  IdUser = 1, SalesCount = 118, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 19, Title = "iOS Development z Swift",                                         Description = "SwiftUI, UIKit, CoreData. Twórz natywne aplikacje na iPhone.",                                                                                                                 Price = 199.99m,  ImageUrl = "https://images.unsplash.com/photo-1611532736597-de2d4265fba3?w=400", IdCategory = 3,  IdUser = 1, SalesCount = 67,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 20, Title = "Android Development z Kotlin",                                    Description = "Jetpack Compose, Room, ViewModel. Nowoczesny Android development.",                                                                                                             Price = 189.99m,  ImageUrl = "https://images.unsplash.com/photo-1607252650355-f7fd0460ccdb?w=400", IdCategory = 3,  IdUser = 1, SalesCount = 88,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 21, Title = "React Native – Zaawansowane Techniki",                           Description = "Animacje, natywne moduły, optymalizacja wydajności w React Native.",                                                                                                           Price = 179.99m,  ImageUrl = "https://images.unsplash.com/photo-1526925539332-aa3b66e35444?w=400", IdCategory = 3,  IdUser = 1, SalesCount = 49,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 22, Title = "Docker od Podstaw",                                              Description = "Kontenery, obrazy, Docker Compose. Naucz się Dockera w praktyce.",                                                                                                             Price = 129.99m,  ImageUrl = "https://images.unsplash.com/photo-1605745341112-85968b19335b?w=400", IdCategory = 4,  IdUser = 1, SalesCount = 162, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 23, Title = "Kubernetes – Orkiestracja Kontenerów",                           Description = "Pody, serwisy, deploymenty. Opanuj K8s i zarządzaj aplikacjami w chmurze.",                                                                                                   Price = 219.99m,  ImageUrl = "https://images.unsplash.com/photo-1558494949-ef010cbdcc31?w=400", IdCategory = 4,  IdUser = 1, SalesCount = 64,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 24, Title = "AWS – Usługi Chmurowe dla Developerów",                          Description = "EC2, S3, Lambda, RDS. Zbuduj infrastrukturę w Amazon Web Services.",                                                                                                          Price = 249.99m,  ImageUrl = "https://images.unsplash.com/photo-1544197150-b99a580bb7a8?w=400", IdCategory = 4,  IdUser = 1, SalesCount = 122, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 25, Title = "CI/CD z GitHub Actions",                                         Description = "Automatyzuj testy i wdrożenia. Zbuduj profesjonalny pipeline CI/CD.",                                                                                                         Price = 99.99m,   ImageUrl = "https://images.unsplash.com/photo-1618401471353-b98afee0b2eb?w=400", IdCategory = 4,  IdUser = 1, SalesCount = 89,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 26, Title = "Terraform – Infrastruktura jako Kod",                            Description = "Zarządzaj infrastrukturą chmurową za pomocą kodu z Terraform.",                                                                                                               Price = 179.99m,  ImageUrl = "https://images.unsplash.com/photo-1667372393119-3d4c48d07fc9?w=400", IdCategory = 4,  IdUser = 1, SalesCount = 38,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 27, Title = "PostgreSQL – Bazy Danych dla Developerów",                       Description = "SQL, indeksy, transakcje, optymalizacja zapytań w PostgreSQL.",                                                                                                               Price = 119.99m,  ImageUrl = "https://images.unsplash.com/photo-1544383835-bda2bc66a55d?w=400", IdCategory = 5,  IdUser = 1, SalesCount = 148, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 28, Title = "MongoDB – NoSQL w Praktyce",                                     Description = "Dokumenty, kolekcje, agregacje. Naucz się MongoDB od zera.",                                                                                                                  Price = 109.99m,  ImageUrl = "https://images.unsplash.com/photo-1558494949-ef010cbdcc31?w=400", IdCategory = 5,  IdUser = 1, SalesCount = 105, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 29, Title = "Redis – Bazy Danych w Pamięci",                                  Description = "Cache, pub/sub, sesje. Przyspiesz swoją aplikację z Redis.",                                                                                                                  Price = 89.99m,   ImageUrl = "https://images.unsplash.com/photo-1518186285589-2f7649de83e0?w=400", IdCategory = 5,  IdUser = 1, SalesCount = 55,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 30, Title = "Entity Framework Core – ORM w .NET",                             Description = "Code First, migracje, zapytania LINQ. Bazy danych w ekosystemie .NET.",                                                                                                       Price = 139.99m,  ImageUrl = "https://images.unsplash.com/photo-1515879218367-8466d910aaa4?w=400", IdCategory = 5,  IdUser = 1, SalesCount = 77,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 31, Title = "Projektowanie Baz Danych – Od Teorii do Praktyki",               Description = "Normalizacja, relacje, diagramy ERD. Zaprojektuj solidną bazę danych.",                                                                                                       Price = 99.99m,   ImageUrl = "https://images.unsplash.com/photo-1489389944381-3471b5b30f04?w=400", IdCategory = 5,  IdUser = 1, SalesCount = 89,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 32, Title = "Etyczny Hacking – Kurs dla Początkujących",                      Description = "Penetration testing, OWASP Top 10, narzędzia bezpieczeństwa.",                                                                                                               Price = 199.99m,  ImageUrl = "https://images.unsplash.com/photo-1550751827-4bd374c3f58b?w=400", IdCategory = 6,  IdUser = 1, SalesCount = 1,   RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 33, Title = "Bezpieczeństwo Aplikacji Webowych",                              Description = "SQL Injection, XSS, CSRF. Zabezpiecz swoją aplikację przed atakami.",                                                                                                         Price = 169.99m,  ImageUrl = "https://images.unsplash.com/photo-1614064641938-3bbee52942c7?w=400", IdCategory = 6,  IdUser = 1, SalesCount = 118, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 34, Title = "Kryptografia dla Programistów",                                  Description = "Szyfrowanie, hashing, podpisy cyfrowe. Kryptografia w praktyce.",                                                                                                             Price = 149.99m,  ImageUrl = "https://images.unsplash.com/photo-1526374965328-7f61d4dc18c5?w=400", IdCategory = 6,  IdUser = 1, SalesCount = 65,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 35, Title = "Bezpieczeństwo Sieci Komputerowych",                             Description = "Firewalle, VPN, IDS/IPS. Chroń infrastrukturę sieciową.",                                                                                                                   Price = 179.99m,  ImageUrl = "https://images.unsplash.com/photo-1558494949-ef010cbdcc31?w=400", IdCategory = 6,  IdUser = 1, SalesCount = 48,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 36, Title = "OWASP – Bezpieczeństwo API",                                     Description = "Zabezpiecz swoje REST API zgodnie z wytycznymi OWASP.",                                                                                                                      Price = 129.99m,  ImageUrl = "https://images.unsplash.com/photo-1555066931-4365d14bab8c?w=400", IdCategory = 6,  IdUser = 1, SalesCount = 72,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 37, Title = "Python dla Data Science",                                        Description = "NumPy, Pandas, Matplotlib. Analiza danych w Pythonie od podstaw.",                                                                                                            Price = 159.99m,  ImageUrl = "https://images.unsplash.com/photo-1504868584819-f8e8b4b6d7e3?w=400", IdCategory = 7,  IdUser = 1, SalesCount = 245, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 38, Title = "Machine Learning z Scikit-Learn",                                Description = "Regresja, klasyfikacja, clustering. Naucz się ML w praktyce.",                                                                                                               Price = 199.99m,  ImageUrl = "https://images.unsplash.com/photo-1677442135703-1787eea5ce01?w=400", IdCategory = 7,  IdUser = 1, SalesCount = 168, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 39, Title = "Deep Learning z TensorFlow",                                     Description = "Sieci neuronowe, CNN, RNN. Zaawansowane modele AI.",                                                                                                                         Price = 249.99m,  ImageUrl = "https://images.unsplash.com/photo-1620712943543-bcc4688e7485?w=400", IdCategory = 7,  IdUser = 1, SalesCount = 142, RatingsCount = 1, AverageRating = 5.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 40, Title = "NLP – Przetwarzanie Języka Naturalnego",                         Description = "Transformery, BERT, GPT. Naucz się budować modele językowe.",                                                                                                               Price = 229.99m,  ImageUrl = "https://images.unsplash.com/photo-1655720828018-edd2daec9349?w=400", IdCategory = 7,  IdUser = 1, SalesCount = 88,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 41, Title = "Analiza Danych z Power BI",                                      Description = "Dashboardy, raporty, wizualizacje. Opanuj Power BI w tydzień.",                                                                                                             Price = 129.99m,  ImageUrl = "https://images.unsplash.com/photo-1551288049-bebda4e38f71?w=400", IdCategory = 7,  IdUser = 1, SalesCount = 118, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 42, Title = "Figma – Projektowanie Interfejsów",                              Description = "Komponenty, auto layout, prototypy. Projektuj w Figmie jak profesjonalista.",                                                                                               Price = 119.99m,  ImageUrl = "https://images.unsplash.com/photo-1561070791-2526d30994b5?w=400", IdCategory = 8,  IdUser = 1, SalesCount = 1,   RatingsCount = 1, AverageRating = 5.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 43, Title = "UX Design – Badania i Projektowanie",                            Description = "User research, persony, wireframy. Twórz produkty które ludzie kochają.",                                                                                                  Price = 149.99m,  ImageUrl = "https://images.unsplash.com/photo-1586717791821-3f44a563fa4c?w=400", IdCategory = 8,  IdUser = 1, SalesCount = 145, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 44, Title = "Design System – Budowanie od Zera",                              Description = "Tokeny, komponenty, dokumentacja. Stwórz spójny system projektowania.",                                                                                                    Price = 169.99m,  ImageUrl = "https://images.unsplash.com/photo-1558655146-364adaf1fcc9?w=400", IdCategory = 8,  IdUser = 1, SalesCount = 65,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 45, Title = "Animacje w UI – Framer Motion",                                  Description = "Płynne animacje i przejścia. Ożyw swój interfejs z Framer Motion.",                                                                                                        Price = 99.99m,   ImageUrl = "https://images.unsplash.com/photo-1550745165-9bc0b252726f?w=400", IdCategory = 8,  IdUser = 1, SalesCount = 48,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 46, Title = "Dostępność w Projektowaniu (a11y)",                              Description = "WCAG, czytniki ekranu, kontrast. Projektuj dla wszystkich użytkowników.",                                                                                                  Price = 89.99m,   ImageUrl = "https://images.unsplash.com/photo-1573496359142-b8d87734a5a2?w=400", IdCategory = 8,  IdUser = 1, SalesCount = 38,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 47, Title = "Python – Kompletny Kurs dla Początkujących",                     Description = "Składnia, funkcje, OOP, biblioteki. Naucz się Pythona od zera.",                                                                                                           Price = 99.99m,   ImageUrl = "https://images.unsplash.com/photo-1526379095098-d400fd0bf935?w=400", IdCategory = 9,  IdUser = 1, SalesCount = 385, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 48, Title = "C# – Programowanie Obiektowe",                                   Description = "Klasy, interfejsy, LINQ, async/await. Zostań .NET developerem.",                                                                                                           Price = 139.99m,  ImageUrl = "https://images.unsplash.com/photo-1515879218367-8466d910aaa4?w=400", IdCategory = 9,  IdUser = 1, SalesCount = 162, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 49, Title = "Go – Język dla Backend Developerów",                             Description = "Goroutines, channels, REST API w Go. Szybki i wydajny backend.",                                                                                                           Price = 159.99m,  ImageUrl = "https://images.unsplash.com/photo-1569012871812-f38ee64cd54c?w=400", IdCategory = 9,  IdUser = 1, SalesCount = 88,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 50, Title = "Rust – Bezpieczne Programowanie Systemowe",                      Description = "Ownership, borrowing, lifetimes. Naucz się Rusta od podstaw.",                                                                                                             Price = 179.99m,  ImageUrl = "https://images.unsplash.com/photo-1542831371-29b0f74f9713?w=400", IdCategory = 9,  IdUser = 1, SalesCount = 54,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 51, Title = "Java – Od Podstaw do Spring Boot",                               Description = "OOP, kolekcje, Spring Boot, REST API. Zostań Java developerem.",                                                                                                           Price = 149.99m,  ImageUrl = "https://images.unsplash.com/photo-1517077304055-6e89abbf09b0?w=400", IdCategory = 9,  IdUser = 1, SalesCount = 138, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 52, Title = "Wzorce Projektowe w Praktyce",                                   Description = "SOLID, DRY, wzorce GoF. Pisz kod który jest łatwy w utrzymaniu.",                                                                                                          Price = 159.99m,  ImageUrl = "https://images.unsplash.com/photo-1507238691740-187a5b1d37b8?w=400", IdCategory = 10, IdUser = 1, SalesCount = 118, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 53, Title = "Architektura Mikroserwisów",                                     Description = "DDD, event sourcing, API Gateway. Projektuj skalowalne systemy.",                                                                                                          Price = 229.99m,  ImageUrl = "https://images.unsplash.com/photo-1558494949-ef010cbdcc31?w=400", IdCategory = 10, IdUser = 1, SalesCount = 89,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 54, Title = "CQRS i Event Sourcing",                                          Description = "Command Query Responsibility Segregation w praktyce z MediatR.",                                                                                                           Price = 199.99m,  ImageUrl = "https://images.unsplash.com/photo-1516116216624-53e697fedbea?w=400", IdCategory = 10, IdUser = 1, SalesCount = 65,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 55, Title = "Clean Architecture w .NET",                                      Description = "Warstwy, zależności, testy. Buduj aplikacje zgodnie z Clean Architecture.",                                                                                               Price = 189.99m,  ImageUrl = "https://images.unsplash.com/photo-1555066931-4365d14bab8c?w=400", IdCategory = 10, IdUser = 1, SalesCount = 76,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 56, Title = "Domain Driven Design – Praktyczny Kurs",                         Description = "Agregaty, encje, value objects, bounded contexts.",                                                                                                                        Price = 219.99m,  ImageUrl = "https://images.unsplash.com/photo-1507721999472-8ed4421c4af2?w=400", IdCategory = 10, IdUser = 1, SalesCount = 48,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 57, Title = "Git i GitHub – Kontrola Wersji",                                 Description = "Branche, merge, rebase, pull requesty. Opanuj Gita w praktyce.",                                                                                                          Price = 69.99m,   ImageUrl = "https://images.unsplash.com/photo-1618401471353-b98afee0b2eb?w=400", IdCategory = 11, IdUser = 1, SalesCount = 345, RatingsCount = 1, AverageRating = 5.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 58, Title = "Linux – Wiersz Poleceń dla Developerów",                         Description = "Bash, skrypty, procesy, SSH. Zostań mistrzem terminala.",                                                                                                                Price = 89.99m,   ImageUrl = "https://images.unsplash.com/photo-1629654297299-c8506221ca97?w=400", IdCategory = 11, IdUser = 1, SalesCount = 210, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 59, Title = "VS Code – Produktywność na Maksa",                               Description = "Skróty, rozszerzenia, debugger. Pracuj szybciej w VS Code.",                                                                                                              Price = 49.99m,   ImageUrl = "https://images.unsplash.com/photo-1542831371-29b0f74f9713?w=400", IdCategory = 11, IdUser = 1, SalesCount = 162, RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 60, Title = "Agile i Scrum w Praktyce",                                       Description = "Sprinty, retrospektywy, user stories. Pracuj zwinnie w zespole.",                                                                                                        Price = 79.99m,   ImageUrl = "https://images.unsplash.com/photo-1611224923853-80b023f02d71?w=400", IdCategory = 11, IdUser = 1, SalesCount = 118, RatingsCount = 1, AverageRating = 1.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 61, Title = "Regex – Wyrażenia Regularne od Zera",                            Description = "Wzorce, grupy, lookahead. Opanuj regex raz na zawsze.",                                                                                                                  Price = 59.99m,   ImageUrl = "https://images.unsplash.com/photo-1516116216624-53e697fedbea?w=400", IdCategory = 11, IdUser = 1, SalesCount = 72,  RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 3, 17, 15, 52, 59, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 62, Title = "test",                                                           Description = "Test test",                                                                                                                                                              Price = 169.00m,  ImageUrl = "https://images.unsplash.com/photo-1618477388954-7852f32655ec?w=400", IdCategory = 2,  IdUser = 3, SalesCount = 1,   RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 4,  6,  0, 34, 29, TimeSpan.Zero).UtcDateTime },
            new Course { IdCourse = 63, Title = "test2222",                                                       Description = "bduehduecvdydvwdyd",                                                                                                                                                    Price = 29.00m,   ImageUrl = "https://images.unsplash.com/photo-1526925539332-aa3b66e35444?w=400", IdCategory = 9,  IdUser = 3, SalesCount = 1,   RatingsCount = 0, AverageRating = 0.00m, IsActive = true, IsVisible = true,  CreatedAt = new DateTimeOffset(2026, 4,  6,  0, 54, 34, TimeSpan.Zero).UtcDateTime }
        );
        db.SaveChanges();
    }

    private static void SeedCartItems(DigiVaultDbContext db)
    {
        if (db.CartItems.Any()) return;

        db.CartItems.Add(new CartItem { IdCartItem = 46, IdUser = 2, IdCourse = 38, AddedAt = new DateTimeOffset(2026, 4, 18, 6, 42, 36, TimeSpan.Zero).UtcDateTime });
        db.SaveChanges();
    }

    private static void SeedCourseReports(DigiVaultDbContext db)
    {
        if (db.CourseReports.Any()) return;

        db.CourseReports.AddRange(
            new CourseReport { IdCourseReport = 1, IdCourse = 2,  IdUser = 3, Reason = "Kurs nie pokrywa sie z opisiem kursu. Wprowadza kupującego w błąd", CreatedAt = new DateTimeOffset(2026, 4, 5, 23, 43, 36, TimeSpan.Zero).UtcDateTime, IsResolved = false },
            new CourseReport { IdCourseReport = 2, IdCourse = 47, IdUser = 3, Reason = "Kurs niekompletny",                                                  CreatedAt = new DateTimeOffset(2026, 4, 6,  0, 12,  5, TimeSpan.Zero).UtcDateTime, IsResolved = false },
            new CourseReport { IdCourseReport = 3, IdCourse = 12, IdUser = 3, Reason = "Prowadzacy stosuje mowe nienawisci",                                 CreatedAt = new DateTimeOffset(2026, 4, 6,  0, 13,  1, TimeSpan.Zero).UtcDateTime, IsResolved = false }
        );
        db.SaveChanges();
    }

    private static void SeedNotifications(DigiVaultDbContext db)
    {
        if (db.Notifications.Any()) return;

        db.Notifications.AddRange(
            new Notification { IdNotification = 6,  IdUser = 3, Title = "Kurs o numerze 5433555 zostal zablokowany", Message = "Jeden z kupujących zgłosił Twój kurs jako niezgodny z opisem. Kurs został zablokowany przed zespół DigiVault.", IsRead = true, CreatedAt = new DateTimeOffset(2026, 4, 6,  8, 53, 15, TimeSpan.Zero).UtcDateTime },
            new Notification { IdNotification = 8,  IdUser = 3, Title = "Testowy komunikat",                         Message = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.test",                                                                       IsRead = true, CreatedAt = new DateTimeOffset(2026, 4, 7, 14, 45, 35, TimeSpan.Zero).UtcDateTime },
            new Notification { IdNotification = 9,  IdUser = 3, Title = "Kolejny testowy komunikat",                 Message = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.test",                                                                       IsRead = true, CreatedAt = new DateTimeOffset(2026, 4, 7, 14, 53, 42, TimeSpan.Zero).UtcDateTime },
            new Notification { IdNotification = 10, IdUser = 3, Title = "Trzeci komunikat testowy",                  Message = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.test",                                                                       IsRead = true, CreatedAt = new DateTimeOffset(2026, 4, 7, 15,  0, 27, TimeSpan.Zero).UtcDateTime }
        );
        db.SaveChanges();
    }

    private static void SeedOrders(DigiVaultDbContext db)
    {
        if (db.Orders.Any()) return;

        db.Orders.AddRange(
            new Order { IdOrder = 1,  IdUser = 2, TotalPrice = 349.98m, CreatedAt = new DateTimeOffset(2026, 3, 31,  9, 42, 12, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 2,  IdUser = 2, TotalPrice = 249.98m, CreatedAt = new DateTimeOffset(2026, 3, 31, 19, 53, 40, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 3,  IdUser = 2, TotalPrice = 709.96m, CreatedAt = new DateTimeOffset(2026, 3, 31, 20, 11, 59, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 4,  IdUser = 2, TotalPrice = 149.99m, CreatedAt = new DateTimeOffset(2026, 3, 31, 20, 21,  0, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 7,  IdUser = 2, TotalPrice = 59.99m,  CreatedAt = new DateTimeOffset(2026, 3, 31, 20, 30,  5, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 8,  IdUser = 2, TotalPrice = 79.99m,  CreatedAt = new DateTimeOffset(2026, 3, 31, 20, 33, 15, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 9,  IdUser = 2, TotalPrice = 49.99m,  CreatedAt = new DateTimeOffset(2026, 3, 31, 20, 43, 39, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 10, IdUser = 2, TotalPrice = 89.99m,  CreatedAt = new DateTimeOffset(2026, 3, 31, 20, 44, 51, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 11, IdUser = 2, TotalPrice = 99.99m,  CreatedAt = new DateTimeOffset(2026, 3, 31, 20, 51,  6, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 12, IdUser = 2, TotalPrice = 189.99m, CreatedAt = new DateTimeOffset(2026, 3, 31, 20, 58, 48, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 13, IdUser = 2, TotalPrice = 179.99m, CreatedAt = new DateTimeOffset(2026, 3, 31, 21,  0, 25, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 14, IdUser = 2, TotalPrice = 359.97m, CreatedAt = new DateTimeOffset(2026, 4,  5, 19,  4, 57, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 15, IdUser = 3, TotalPrice = 399.98m, CreatedAt = new DateTimeOffset(2026, 4,  5, 21, 42, 18, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 16, IdUser = 3, TotalPrice = 49.99m,  CreatedAt = new DateTimeOffset(2026, 4,  5, 21, 48, 29, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 17, IdUser = 3, TotalPrice = 69.99m,  CreatedAt = new DateTimeOffset(2026, 4,  5, 21, 50, 28, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 18, IdUser = 3, TotalPrice = 89.99m,  CreatedAt = new DateTimeOffset(2026, 4,  6,  0,  5, 51, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 19, IdUser = 3, TotalPrice = 29.00m,  CreatedAt = new DateTimeOffset(2026, 4,  6,  0, 56, 12, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 20, IdUser = 3, TotalPrice = 159.99m, CreatedAt = new DateTimeOffset(2026, 4,  7, 14, 54, 49, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 21, IdUser = 2, TotalPrice = 319.98m, CreatedAt = new DateTimeOffset(2026, 4,  7, 16, 48,  6, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 22, IdUser = 6, TotalPrice = 49.99m,  CreatedAt = new DateTimeOffset(2026, 4,  7, 17, 43, 17, TimeSpan.Zero).UtcDateTime },
            new Order { IdOrder = 23, IdUser = 5, TotalPrice = 198.00m, CreatedAt = new DateTimeOffset(2026, 4,  8, 20,  7, 17, TimeSpan.Zero).UtcDateTime }
        );
        db.SaveChanges();
    }

    private static void SeedOrderItems(DigiVaultDbContext db)
    {
        if (db.OrderItems.Any()) return;

        db.OrderItems.AddRange(
            new OrderItem { IdOrderItem = 1,  IdOrder = 1,  IdCourse = 2,  Price = 149.99m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 2,  IdOrder = 1,  IdCourse = 3,  Price = 199.99m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 3,  IdOrder = 2,  IdCourse = 12, Price = 49.99m,  CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 4,  IdOrder = 2,  IdCourse = 19, Price = 199.99m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 5,  IdOrder = 3,  IdCourse = 11, Price = 199.99m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 6,  IdOrder = 3,  IdCourse = 37, Price = 159.99m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 7,  IdOrder = 3,  IdCourse = 39, Price = 249.99m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 8,  IdOrder = 3,  IdCourse = 47, Price = 99.99m,  CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 9,  IdOrder = 4,  IdCourse = 43, Price = 149.99m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 12, IdOrder = 7,  IdCourse = 61, Price = 59.99m,  CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 13, IdOrder = 8,  IdCourse = 60, Price = 79.99m,  CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 14, IdOrder = 9,  IdCourse = 59, Price = 49.99m,  CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 15, IdOrder = 10, IdCourse = 58, Price = 89.99m,  CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 16, IdOrder = 11, IdCourse = 25, Price = 99.99m,  CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 17, IdOrder = 12, IdCourse = 55, Price = 189.99m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 18, IdOrder = 13, IdCourse = 50, Price = 179.99m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 19, IdOrder = 14, IdCourse = 13, Price = 149.99m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 20, IdOrder = 14, IdCourse = 48, Price = 139.99m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 21, IdOrder = 14, IdCourse = 57, Price = 69.99m,  CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 22, IdOrder = 15, IdCourse = 2,  Price = 149.99m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 23, IdOrder = 15, IdCourse = 39, Price = 249.99m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 24, IdOrder = 16, IdCourse = 12, Price = 49.99m,  CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 25, IdOrder = 17, IdCourse = 57, Price = 69.99m,  CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 26, IdOrder = 18, IdCourse = 58, Price = 89.99m,  CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 27, IdOrder = 19, IdCourse = 63, Price = 29.00m,  CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 28, IdOrder = 20, IdCourse = 37, Price = 159.99m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 29, IdOrder = 21, IdCourse = 42, Price = 119.99m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 30, IdOrder = 21, IdCourse = 32, Price = 199.99m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 31, IdOrder = 22, IdCourse = 12, Price = 49.99m,  CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 32, IdOrder = 23, IdCourse = 62, Price = 169.00m, CommissionRate = 0.0500m },
            new OrderItem { IdOrderItem = 33, IdOrder = 23, IdCourse = 63, Price = 29.00m,  CommissionRate = 0.0500m }
        );
        db.SaveChanges();
    }

    private static void SeedReviews(DigiVaultDbContext db)
    {
        if (db.Reviews.Any()) return;

        db.Reviews.AddRange(
            new Review { IdReview = 5,  IdCourse = 11, IdUser = 2, Rating = 5, Comment = "Super",                   CreatedAt = new DateTimeOffset(2026, 4, 5, 21, 26, 21, TimeSpan.Zero).UtcDateTime },
            new Review { IdReview = 6,  IdCourse = 2,  IdUser = 2, Rating = 3, Comment = "Srednie",                 CreatedAt = new DateTimeOffset(2026, 4, 5, 21, 31, 57, TimeSpan.Zero).UtcDateTime },
            new Review { IdReview = 8,  IdCourse = 60, IdUser = 2, Rating = 1, Comment = "Dno",                     CreatedAt = new DateTimeOffset(2026, 4, 5, 21, 35, 33, TimeSpan.Zero).UtcDateTime },
            new Review { IdReview = 9,  IdCourse = 39, IdUser = 2, Rating = 5, Comment = "dobry",                   CreatedAt = new DateTimeOffset(2026, 4, 5, 21, 37, 38, TimeSpan.Zero).UtcDateTime },
            new Review { IdReview = 11, IdCourse = 2,  IdUser = 3, Rating = 4, Comment = "Bardzo pomocny",          CreatedAt = new DateTimeOffset(2026, 4, 5, 21, 43, 23, TimeSpan.Zero).UtcDateTime },
            new Review { IdReview = 12, IdCourse = 57, IdUser = 3, Rating = 5, Comment = "Bardzo dobry kurs!",      CreatedAt = new DateTimeOffset(2026, 4, 5, 21, 51,  1, TimeSpan.Zero).UtcDateTime },
            new Review { IdReview = 13, IdCourse = 12, IdUser = 3, Rating = 3, Comment = "Srednie kurs",            CreatedAt = new DateTimeOffset(2026, 4, 6,  0,  4, 58, TimeSpan.Zero).UtcDateTime },
            new Review { IdReview = 14, IdCourse = 42, IdUser = 2, Rating = 5, Comment = "Git",                     CreatedAt = new DateTimeOffset(2026, 4, 7, 16, 48, 32, TimeSpan.Zero).UtcDateTime },
            new Review { IdReview = 15, IdCourse = 12, IdUser = 6, Rating = 4, Comment = "Nawet trzyma poziom :)",  CreatedAt = new DateTimeOffset(2026, 4, 7, 17, 44, 12, TimeSpan.Zero).UtcDateTime }
        );
        db.SaveChanges();
    }

    private static void SeedUserCourses(DigiVaultDbContext db)
    {
        if (db.UserCourses.Any()) return;

        db.UserCourses.AddRange(
            new UserCourse { IdUserCourse = 1,  IdUser = 2, IdCourse = 2,  PurchasedAt = new DateTimeOffset(2026, 3, 31,  9, 46, 38, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 2,  IdUser = 2, IdCourse = 3,  PurchasedAt = new DateTimeOffset(2026, 3, 31,  9, 46, 47, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 3,  IdUser = 2, IdCourse = 12, PurchasedAt = new DateTimeOffset(2026, 3, 31, 19, 53, 40, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 4,  IdUser = 2, IdCourse = 19, PurchasedAt = new DateTimeOffset(2026, 3, 31, 19, 53, 40, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 5,  IdUser = 2, IdCourse = 11, PurchasedAt = new DateTimeOffset(2026, 3, 31, 20, 11, 59, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 6,  IdUser = 2, IdCourse = 37, PurchasedAt = new DateTimeOffset(2026, 3, 31, 20, 11, 59, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 7,  IdUser = 2, IdCourse = 39, PurchasedAt = new DateTimeOffset(2026, 3, 31, 20, 11, 59, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 8,  IdUser = 2, IdCourse = 47, PurchasedAt = new DateTimeOffset(2026, 3, 31, 20, 11, 59, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 9,  IdUser = 2, IdCourse = 43, PurchasedAt = new DateTimeOffset(2026, 3, 31, 20, 21,  0, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 12, IdUser = 2, IdCourse = 61, PurchasedAt = new DateTimeOffset(2026, 3, 31, 20, 30,  5, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 13, IdUser = 2, IdCourse = 60, PurchasedAt = new DateTimeOffset(2026, 3, 31, 20, 33, 15, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 14, IdUser = 2, IdCourse = 59, PurchasedAt = new DateTimeOffset(2026, 3, 31, 20, 43, 39, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 15, IdUser = 2, IdCourse = 58, PurchasedAt = new DateTimeOffset(2026, 3, 31, 20, 44, 51, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 16, IdUser = 2, IdCourse = 25, PurchasedAt = new DateTimeOffset(2026, 3, 31, 20, 51,  6, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 17, IdUser = 2, IdCourse = 55, PurchasedAt = new DateTimeOffset(2026, 3, 31, 20, 58, 48, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 18, IdUser = 2, IdCourse = 50, PurchasedAt = new DateTimeOffset(2026, 3, 31, 21,  0, 25, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 19, IdUser = 2, IdCourse = 13, PurchasedAt = new DateTimeOffset(2026, 4,  5, 19,  4, 57, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 20, IdUser = 2, IdCourse = 48, PurchasedAt = new DateTimeOffset(2026, 4,  5, 19,  4, 57, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 21, IdUser = 2, IdCourse = 57, PurchasedAt = new DateTimeOffset(2026, 4,  5, 19,  4, 57, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 22, IdUser = 3, IdCourse = 2,  PurchasedAt = new DateTimeOffset(2026, 4,  5, 21, 42, 18, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 23, IdUser = 3, IdCourse = 39, PurchasedAt = new DateTimeOffset(2026, 4,  5, 21, 42, 18, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 24, IdUser = 3, IdCourse = 12, PurchasedAt = new DateTimeOffset(2026, 4,  5, 21, 48, 29, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 25, IdUser = 3, IdCourse = 57, PurchasedAt = new DateTimeOffset(2026, 4,  5, 21, 50, 28, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 26, IdUser = 3, IdCourse = 58, PurchasedAt = new DateTimeOffset(2026, 4,  6,  0,  5, 51, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 27, IdUser = 3, IdCourse = 63, PurchasedAt = new DateTimeOffset(2026, 4,  6,  0, 56, 12, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 28, IdUser = 3, IdCourse = 37, PurchasedAt = new DateTimeOffset(2026, 4,  7, 14, 54, 49, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 29, IdUser = 2, IdCourse = 42, PurchasedAt = new DateTimeOffset(2026, 4,  7, 16, 48,  6, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 30, IdUser = 2, IdCourse = 32, PurchasedAt = new DateTimeOffset(2026, 4,  7, 16, 48,  6, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 31, IdUser = 6, IdCourse = 12, PurchasedAt = new DateTimeOffset(2026, 4,  7, 17, 43, 17, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 32, IdUser = 5, IdCourse = 62, PurchasedAt = new DateTimeOffset(2026, 4,  8, 20,  7, 17, TimeSpan.Zero).UtcDateTime },
            new UserCourse { IdUserCourse = 33, IdUser = 5, IdCourse = 63, PurchasedAt = new DateTimeOffset(2026, 4,  8, 20,  7, 17, TimeSpan.Zero).UtcDateTime }
        );
        db.SaveChanges();
    }

    private static void SeedWishlistItems(DigiVaultDbContext db)
    {
        if (db.WishlistItems.Any()) return;

        db.WishlistItems.AddRange(
            new WishlistItem { IdWishlistItem = 11, IdUser = 2, IdCourse = 48, AddedAt = new DateTimeOffset(2026, 3, 24, 12, 45, 59, TimeSpan.Zero).UtcDateTime },
            new WishlistItem { IdWishlistItem = 23, IdUser = 2, IdCourse = 38, AddedAt = new DateTimeOffset(2026, 4, 18,  6, 42, 29, TimeSpan.Zero).UtcDateTime }
        );
        db.SaveChanges();
    }
}
