#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Api/CakeStore.Api/CakeStore.Api.csproj", "Systems/Api/CakeStore.Api/"]
COPY ["Data/CakeStore.Context.Migrations/CakeStore.Context.Migrations.MSSQL/CakeStore.Context.Migrations.MSSQL.csproj", "Data/CakeStore.Context.Migrations/CakeStore.Context.Migrations.MSSQL/"]
COPY ["Data/CakeStore.Context.Entities/CakeStore.Context.Entities.csproj", "Data/CakeStore.Context.Entities/"]
COPY ["Data/CakeStore.Context/CakeStore.Context.csproj", "Data/CakeStore.Context/"]
COPY ["Services/CakeStore.Services.Settings/CakeStore.Services.Settings.csproj", "Services/CakeStore.Services.Settings/"]
COPY ["Shared/CakeStore.Common/CakeStore.Common.csproj", "Shared/CakeStore.Common/"]
COPY ["Data/CakeStore.Context.Migrations/CakeStore.Context.Migrations.MySql/CakeStore.Context.Migrations.MySql.csproj", "Data/CakeStore.Context.Migrations/CakeStore.Context.Migrations.MySql/"]
COPY ["Data/CakeStore.Context.Migrations/CakeStore.Context.Migrations.PgSql/CakeStore.Context.Migrations.PgSql.csproj", "Data/CakeStore.Context.Migrations/CakeStore.Context.Migrations.PgSql/"]
COPY ["Data/CakeStore.Context.Seeder/CakeStore.Context.Seeder.csproj", "Data/CakeStore.Context.Seeder/"]
COPY ["Services/CakeStore.Services.Logger/CakeStore.Services.Logger.csproj", "Services/CakeStore.Services.Logger/"]
COPY ["Services/CakeStore.Services.Products/CakeStore.Services.Products.csproj", "Services/CakeStore.Services.Products/"]
COPY ["Systems/Api/CakeStore.Api.Settings/CakeStore.Api.Settings.csproj", "Systems/Api/CakeStore.Api.Settings/"]
RUN dotnet restore "./Systems/Api/CakeStore.Api/CakeStore.Api.csproj"
COPY . .
WORKDIR "/src/Systems/Api/CakeStore.Api"
RUN dotnet build "./CakeStore.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CakeStore.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CakeStore.Api.dll"]