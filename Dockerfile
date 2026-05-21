# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CleanArch.API.Host/CleanArch.API.Host.csproj", "CleanArch.API.Host/"]
COPY ["CleanArch.Infrastructure/CleanArch.Infrastructure.csproj", "CleanArch.Infrastructure/"]
COPY ["CleanArch.Application/CleanArch.Application.csproj", "CleanArch.Application/"]
COPY ["CleanArch.Domain/CleanArch.Domain.csproj", "CleanArch.Domain/"]
COPY ["CleanArch.Share/CleanArch.Share.csproj", "CleanArch.Share/"]
RUN dotnet restore "./CleanArch.API.Host/CleanArch.API.Host.csproj"
COPY . .
WORKDIR "/src/CleanArch.API.Host"
RUN dotnet build "./CleanArch.API.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CleanArch.API.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArch.API.Host.dll"]
