using HealthMed.Core.Domain;
using HealthMed.Core.UseCases.RegisterDoctor;
using HealthMed.Core.UseCases.SearchDoctorByLocation;
using HealthMed.Infrastructure.IoC;
using HealthMed.Infrastructure.MongoDb.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories();
builder.Services.AddUseCases();
builder.Services.AddMongoDb(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/doctors", 
    async (
        [FromServices] ISearchDoctorByLocationUseCase useCase, 
        [FromQuery] double latitude, 
        [FromQuery] double longitude, 
        [FromQuery] double distance, 
        [FromQuery] string speciality) =>
    {
        var results = await useCase.SearchDoctorByLocationAsync(latitude, longitude, distance, speciality);
        return Results.Ok(results);
    });

app.MapPost("/doctors",
    async (
        [FromServices] IRegisterDoctorUseCase useCase,
        [FromBody] RegisterDoctorRequest request) =>
    {
        return Results.Ok(await useCase.RegisterDoctorAsync(request));
    });

app.Run();