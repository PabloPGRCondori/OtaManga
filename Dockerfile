# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image for compiling the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files first to leverage caching
COPY ["OtoMangaStore.sln", "./"]
COPY ["OtoMangaStore.Api/OtoMangaStore.Api.csproj", "OtoMangaStore.Api/"]
COPY ["OtoMangaStore.Application/OtoMangaStore.Application.csproj", "OtoMangaStore.Application/"]
COPY ["OtoMangaStore.Domain/OtoMangaStore.Domain.csproj", "OtoMangaStore.Domain/"]
COPY ["OtoMangaStore.Infrastructure/OtoMangaStore.Infrastructure.csproj", "OtoMangaStore.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "OtoMangaStore.sln"

# Copy the rest of the source code
COPY . .

# Build the API project
WORKDIR "/src/OtoMangaStore.Api"
RUN dotnet build "OtoMangaStore.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "OtoMangaStore.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OtoMangaStore.Api.dll"]
