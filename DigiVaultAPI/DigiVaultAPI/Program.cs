using System.Text;
using DigiVaultAPI.Data;
using DigiVaultAPI.Features.Auth.Providers;
using DigiVaultAPI.Features.Auth.Services;
using DigiVaultAPI.Features.Courses.Mappings;
using DigiVaultAPI.Features.Courses.Providers;
using DigiVaultAPI.Features.Courses.Services;
using DigiVaultAPI.Features.Categories.Providers;
using DigiVaultAPI.Features.Wishlist.Providers;
using DigiVaultAPI.Features.Wishlist.Services;
using DigiVaultAPI.Features.Cart.Providers;
using DigiVaultAPI.Features.Cart.Services;
using DigiVaultAPI.Features.Orders.Providers;
using DigiVaultAPI.Features.Orders.Services;
using DigiVaultAPI.Features.Profile.Providers;
using DigiVaultAPI.Features.Profile.Services;
using DigiVaultAPI.Features.Review.Providers;
using DigiVaultAPI.Features.Review.Services;
using DigiVaultAPI.Features.Reports.Services;
using DigiVaultAPI.Features.Notifications.Providers;
using DigiVaultAPI.Features.Notifications.Services;
using DigiVaultAPI.Features.Admin.Providers;
using DigiVaultAPI.Features.Admin.Services;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using DigiVaultAPI.Behaviors;
using FluentValidation;
using MediatR;
using DigiVaultAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ── SERVICES ──────────────────────────────────────────────────────────────────

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Wpisz token JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// EF Core — PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DigiVaultDbContext>(options =>
    options.UseNpgsql(connectionString));

// MediatR + ValidationBehavior
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});

// FluentValidation — rejestracja wszystkich walidatorów z assembly
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Mapster
var mapsterConfig = TypeAdapterConfig.GlobalSettings;
mapsterConfig.Scan(typeof(CourseMappingConfig).Assembly);
builder.Services.AddSingleton(mapsterConfig);
builder.Services.AddScoped<IMapper, ServiceMapper>();

// Auth
builder.Services.AddScoped<IAuthProvider, AuthProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Courses
builder.Services.AddScoped<ICourseProvider, CourseProvider>();
builder.Services.AddScoped<ICourseService, CourseService>();

//Categories
builder.Services.AddScoped<ICategoryProvider, CategoryProvider>();

//Wishlist
builder.Services.AddScoped<IWishlistProvider, WishlistProvider>();
builder.Services.AddScoped<IWishlistService, WishlistService>();

//Cart
builder.Services.AddScoped<ICartProvider, CartProvider>();
builder.Services.AddScoped<ICartService, CartService>();

//Orders
builder.Services.AddScoped<IOrderProvider, OrderProvider>();
builder.Services.AddScoped<IOrderService, OrderService>();

//Profile
builder.Services.AddScoped<IProfileProvider, ProfileProvider>();
builder.Services.AddScoped<IProfileService, ProfileService>();

//Review
builder.Services.AddScoped<IReviewProvider, ReviewProvider>();
builder.Services.AddScoped<IReviewService, ReviewService>();

//Reports
builder.Services.AddScoped<IReportService, ReportService>();

//Notifications
builder.Services.AddScoped<INotificationProvider, NotificationProvider>();
builder.Services.AddScoped<INotificationService, NotificationService>();

//Admin
builder.Services.AddScoped<IAdminProvider, AdminProvider>();
builder.Services.AddScoped<IAdminService, AdminService>();

// JWT
var jwtKey      = builder.Configuration["Jwt:Key"]!;
var jwtIssuer   = builder.Configuration["Jwt:Issuer"]!;
var jwtAudience = builder.Configuration["Jwt:Audience"]!;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = jwtIssuer,
            ValidAudience            = jwtAudience,
            IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

// ── APP ───────────────────────────────────────────────────────────────────────

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAll");

// Automatyczne migracje przy starcie
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DigiVaultDbContext>();
    db.Database.Migrate();
    DigiVaultSeeder.Seed(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();