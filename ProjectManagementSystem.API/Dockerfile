# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["ProjectManagementSystem.API/ProjectManagementSystem.API.csproj", "ProjectManagementSystem.API/"]
COPY ["ProjectManagementSystem.Infrastructure/ProjectManagementSystem.Infrastructure.csproj", "ProjectManagementSystem.Infrastructure/"]
COPY ["ProjectManagmentSystem.Application/ProjectManagementSystem.Application.csproj", "ProjectManagmentSystem.Application/"]
COPY ["ProjectManagementSystem.Domain/ProjectManagementSystem.Domain.csproj", "ProjectManagementSystem.Domain/"]

RUN dotnet restore "ProjectManagementSystem.API/ProjectManagementSystem.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/ProjectManagementSystem.API"
RUN dotnet build "ProjectManagementSystem.API.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "ProjectManagementSystem.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectManagementSystem.API.dll"]