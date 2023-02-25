using System.Configuration;
using System.Data;
using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using shareCalendar_api.Repositories;
using shareCalendar_api.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("*");
            builder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});


builder.Services.AddSingleton<IUserRepository, UserRepositoryImpl>();
builder.Services.AddSingleton<ICalendarRepository, CalendarRepositoryImpl>();
builder.Services.AddSingleton<IBlankCalendarRepository, BlankCalendarRepositoryImpl>();


BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    return new MongoClient("mongodb+srv://admin:qaz123@calendar-api-db.k6ntb7p.mongodb.net/?retryWrites=true&w=majority");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers().WithOpenApi();

// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllers();
// });

app.Run();