using Microsoft.EntityFrameworkCore;
using WebServiceProject;
using WebServiceProject.Data;
using WebServiceProject.Interfaces;
using WebServiceProject.Models;
using WebServiceProject.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using WebServiceProject.Services;
using WebServiceProject.Intefraces;

var builder = WebApplication.CreateBuilder(args);

// 1. Ajouter les services de contr�leurs
builder.Services.AddControllers();

// 2. Ajout du service pour le seeding des donn�es (donn�es de test)
builder.Services.AddTransient<Seed>();

// 3. Ajout des repositories pour l'injection de d�pendances
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// 4. Ajouter TokenService pour l'authentification JWT
builder.Services.AddSingleton<TokenService>();

// 5. Configurer CORS pour autoriser les requ�tes depuis des applications externes (comme React en local)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.WithOrigins("http://localhost:5175/")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// 6. Configurer l'authentification JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// 7. Ajouter une autorisation g�n�rale
builder.Services.AddAuthorization();

// 8. Configuration de Swagger pour la documentation de l'API et l'authentification JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebServiceProject API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Entrez 'Bearer' suivi d'un espace et du token JWT."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "bearer",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

    // Forcer Swagger � utiliser HTTP
    options.AddServer(new OpenApiServer { Url = "http://localhost:5175" });
});

// 9. Configuration de la base de donn�es avec Entity Framework Core
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// 10. Activer CORS pour autoriser les requ�tes de l'application React
app.UseCors("AllowReactApp");

// 11. Activer l'authentification et l'autorisation dans le pipeline
app.UseAuthentication();
app.UseAuthorization();

// 12. Activer le seeding de la base de donn�es si le param�tre "seeddata" est pass� en ligne de commande
if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = 
        scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

// 13. Configurer Swagger dans l'environnement de d�veloppement uniquement
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 14. Rediriger les requ�tes HTTP vers HTTPS si n�cessaire (comment� ici pour rester en HTTP uniquement)
// app.UseHttpsRedirection();

// 15. Ajouter les contr�leurs au pipeline pour g�rer les requ�tes HTTP vers les routes API
app.MapControllers();

// 16. Lancer l'application
app.Run();
