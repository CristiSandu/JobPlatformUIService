using JobPlatformUIService.Authorization;
using JobPlatformUIService.Helper;
using JobPlatformUIService.Infrastructure.Data;
using JobPlatformUIService.Infrastructure.Data.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FirestoreSettings>(builder.Configuration.GetSection("FirestoreSettings"));
builder.Services.AddTransient<IFirestoreContext, FirestoreContext>();
builder.Services.AddTransient(typeof(IFirestoreService<>), typeof(FirestoreService<>));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<IJWTParser, JWTParser>();

builder.AddAuth();

builder.Services.AddControllers();

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

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseHttpLogging();

app.MapControllers().RequireAuthorization();

app.UseAuthenticationAndAuthorization();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.MapControllers();

app.Run();
