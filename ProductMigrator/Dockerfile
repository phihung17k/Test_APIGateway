# Use .NET SDK to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only the project file for caching
COPY "ProductMigrator/ProductMigrator.csproj" "ProductMigrator/"
RUN dotnet restore "ProductMigrator/ProductMigrator.csproj"

# Copy the entire source code
COPY . .
WORKDIR "/src/ProductMigrator"

# Build the migration project
RUN dotnet build "ProductMigrator.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the migration project
FROM build AS publish
RUN dotnet publish "ProductMigrator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use lightweight .NET runtime to run the migration
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app

# Copy published files from build stage
COPY --from=publish /app/publish .

# Set the entrypoint to run the migration
ENTRYPOINT ["dotnet", "ProductMigrator.dll"]
