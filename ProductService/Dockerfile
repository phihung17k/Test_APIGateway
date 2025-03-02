#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
# flow:
# - src: 
#	+ copy file to /src, restore file .csproj
#	+ copy source code to /src, build RELEASE file .csproj to /app/build
#	+ publish RELEASE file .csproj to /app/publish
# - app:
#	+ dotnet run file dll in /app 

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY "ProductService/ProductService.csproj" "ProductService/"
RUN dotnet restore "./ProductService/ProductService.csproj"
COPY . .
WORKDIR "/src/ProductService"
RUN dotnet build "./ProductService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY "https/aspnetapp.pfx" "https/"
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.dll"]