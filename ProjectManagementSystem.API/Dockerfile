
# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProjectManagementSystem.API/ProjectManagementSystem.API.csproj", "ProjectManagementSystem.API/"]
COPY ["ProjectManagementSystem.Infrastructure/ProjectManagementSystem.Infrastructure.csproj", "ProjectManagementSystem.Infrastructure/"]
COPY ["ProjectManagmentSystem.Application/ProjectManagementSystem.Application.csproj", "ProjectManagmentSystem.Application/"]
COPY ["ProjectManagementSystem.Domain/ProjectManagementSystem.Domain.csproj", "ProjectManagementSystem.Domain/"]
RUN dotnet restore "./ProjectManagementSystem.API/ProjectManagementSystem.API.csproj"
COPY . .
WORKDIR "/src/ProjectManagementSystem.API"
RUN dotnet build "./ProjectManagementSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProjectManagementSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectManagementSystem.API.dll"]