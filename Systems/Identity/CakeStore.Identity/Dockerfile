#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Identity/CakeStore.Identity/CakeStore.Identity.csproj", "Systems/Identity/CakeStore.Identity/"]
COPY ["Data/CakeStore.Context/CakeStore.Context.csproj", "Data/CakeStore.Context/"]
COPY ["Services/CakeStore.Services.Settings/CakeStore.Services.Settings.csproj", "Services/CakeStore.Services.Settings/"]
COPY ["Shared/CakeStore.Common/CakeStore.Common.csproj", "Shared/CakeStore.Common/"]
COPY ["Data/CakeStore.Context.Entities/CakeStore.Context.Entities.csproj", "Data/CakeStore.Context.Entities/"]
RUN dotnet restore "./Systems/Identity/CakeStore.Identity/CakeStore.Identity.csproj"
COPY . .
WORKDIR "/src/Systems/Identity/CakeStore.Identity"
RUN dotnet build "./CakeStore.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CakeStore.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CakeStore.Identity.dll"]