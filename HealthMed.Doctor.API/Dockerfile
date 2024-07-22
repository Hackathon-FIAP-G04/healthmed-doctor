FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HealthMed.Doctor.API/HealthMed.Doctor.API.csproj", "HealthMed.Doctor.API/"]
COPY ["HealthMed.Doctor.Core/HealthMed.Doctor.Core.csproj", "HealthMed.Doctor.Core/"]
COPY ["HealthMed.Doctor.Infrastructure/HealthMed.Doctor.Infrastructure.csproj", "HealthMed.Doctor.Infrastructure/"]
RUN dotnet restore "HealthMed.Doctor.API/HealthMed.Doctor.API.csproj"
COPY . .
WORKDIR "/src/HealthMed.Doctor.API"
RUN dotnet build "HealthMed.Doctor.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HealthMed.Doctor.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HealthMed.Doctor.API.dll"]
