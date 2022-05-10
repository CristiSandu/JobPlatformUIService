using JobPlatformUIService.Authorization;
using JobPlatformUIService.Infrastructure.Data;
using JobPlatformUIService.Infrastructure.Data.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<FirestoreSettings>(builder.Configuration.GetSection("FirestoreSettings"));
builder.Services.AddTransient<IFirestoreContext, FirestoreContext>();
builder.Services.AddTransient(typeof(IFirestoreService<>), typeof(FirestoreService<>));

builder.AddAuth();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Job Platform UI Service API", Version = "v1" });
    var security = new Dictionary<string, IEnumerable<string>>
    {
        {"Bearer", System.Array.Empty<string>()},
    };

    c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = ParameterLocation.Header,
        Description = "Enter the JWT in the following format 'Bearer AUTH_CODE'",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "JWT"
                }
            },
            System.Array.Empty<string>()
        }
    });

});

builder.Services.AddMediatR(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseHttpLogging();

app.MapControllers().RequireAuthorization();

app.UseAuthorization();
app.UseAuthenticationAndAuthorization();

app.MapControllers();

app.Run();
