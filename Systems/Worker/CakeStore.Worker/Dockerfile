#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Worker/CakeStore.Worker/CakeStore.Worker.csproj", "Systems/Worker/CakeStore.Worker/"]
COPY ["Data/CakeStore.Context/CakeStore.Context.csproj", "Data/CakeStore.Context/"]
COPY ["Services/CakeStore.Services.Settings/CakeStore.Services.Settings.csproj", "Services/CakeStore.Services.Settings/"]
COPY ["Shared/CakeStore.Common/CakeStore.Common.csproj", "Shared/CakeStore.Common/"]
COPY ["Data/CakeStore.Context.Entities/CakeStore.Context.Entities.csproj", "Data/CakeStore.Context.Entities/"]
COPY ["Services/CakeStore.Services.Actions/CakeStore.Services.Actions.csproj", "Services/CakeStore.Services.Actions/"]
COPY ["Services/CakeStore.Services.RabbitMq/CakeStore.Services.RabbitMq.csproj", "Services/CakeStore.Services.RabbitMq/"]
COPY ["Services/CakeStore.Services.Logger/CakeStore.Services.Logger.csproj", "Services/CakeStore.Services.Logger/"]
RUN dotnet restore "./Systems/Worker/CakeStore.Worker/CakeStore.Worker.csproj"
COPY . .
WORKDIR "/src/Systems/Worker/CakeStore.Worker"
RUN dotnet build "./CakeStore.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CakeStore.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CakeStore.Worker.dll"]