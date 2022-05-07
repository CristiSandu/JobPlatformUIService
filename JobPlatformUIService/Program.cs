using JobPlatformUIService.Infrastructure.Data;
using JobPlatformUIService.Infrastructure.Data.Firestore;
using JobPlatformUIService.Infrastructure.Data.Firestore.Interfaces;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<FirestoreSettings>(builder.Configuration.GetSection("FirestoreSettings"));
builder.Services.AddTransient<IFirestoreContext, FirestoreContext>();
builder.Services.AddTransient(typeof(IFirestoreService<>), typeof(FirestoreService<>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMediatR(typeof(Program));


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
