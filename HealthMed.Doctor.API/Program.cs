using HealthMed.Core.UseCases.RegisterDoctor;
using HealthMed.Core.UseCases.SearchDoctorByLocation;
using HealthMed.Doctor.Core.UseCases.RateDoctor;
using HealthMed.Doctor.Core.UseCases.UpdateDoctor;
using HealthMed.Infrastructure.WebAPI;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.AddWebApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseWebApi();

app.MapGet("/doctors", 
    async (
        [FromServices] ISearchDoctorByLocationUseCase useCase, 
        [FromQuery] double? latitude, 
        [FromQuery] double? longitude, 
        [FromQuery] double? distance, 
        [FromQuery] string? speciality,
        [FromQuery] decimal? rating) =>
    {
        var results = await useCase.SearchDoctorByLocationAsync(latitude, longitude, distance, speciality, rating);
        return Results.Ok(results);
    })
    .WithDescription("Retrieves a doctor by location, speciality and/or rating");

app.MapPost("/doctors",
    async (
        [FromServices] IRegisterDoctorUseCase useCase,
        [FromBody] RegisterDoctorRequest request) =>
    {
        return Results.Ok(await useCase.RegisterDoctorAsync(request));
    })
    .WithDescription("Creates a new doctor");

app.MapPut("/doctors",
    async (
        [FromServices] IUpdateDoctorUseCase useCase,
        [FromBody] UpdateDoctorRequest request) =>
    {
        return Results.Ok(await useCase.UpdateDoctorAsync(request));
    })
    .WithDescription("Updates information for a doctor");

app.MapPatch("/doctors",
    async (
        [FromServices] IRateDoctorUseCase useCase,
        [FromBody] RateDoctorRequest request) =>
    {
        return Results.Ok(await useCase.RateDoctor(request));
    })
    .WithDescription("Rates a doctor with a rating from 0 to 5");

app.Run();