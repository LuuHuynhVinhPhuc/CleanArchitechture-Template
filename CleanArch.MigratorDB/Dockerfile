# Build context must be the solution root:  docker build -f CleanArch.MigratorDB/Dockerfile .
FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CleanArch.MigratorDB/CleanArch.MigratorDB.csproj", "CleanArch.MigratorDB/"]
COPY ["CleanArch.Infrastructure/CleanArch.Infrastructure.csproj", "CleanArch.Infrastructure/"]
COPY ["CleanArch.Application/CleanArch.Application.csproj", "CleanArch.Application/"]
COPY ["CleanArch.Domain/CleanArch.Domain.csproj", "CleanArch.Domain/"]
COPY ["CleanArch.Share/CleanArch.Share.csproj", "CleanArch.Share/"]
RUN dotnet restore "./CleanArch.MigratorDB/CleanArch.MigratorDB.csproj"
COPY . .
WORKDIR "/src/CleanArch.MigratorDB"
RUN dotnet build "./CleanArch.MigratorDB.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CleanArch.MigratorDB.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArch.MigratorDB.dll"]
