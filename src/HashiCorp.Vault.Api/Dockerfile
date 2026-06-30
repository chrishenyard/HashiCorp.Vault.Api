FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

USER root

WORKDIR /app
EXPOSE 9030

#COPY ["certs/localhost.crt", "/usr/local/share/ca-certificates/localhost.crt"]
#RUN update-ca-certificates
#
USER $APP_UID

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["HashiCorp.Vault.Api/HashiCorp.Vault.Api.csproj", "HashiCorp.Vault.Api/"]
RUN dotnet restore "./HashiCorp.Vault.Api/HashiCorp.Vault.Api.csproj"
COPY . .
WORKDIR "/src/HashiCorp.Vault.Api"
RUN dotnet build "./HashiCorp.Vault.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HashiCorp.Vault.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HashiCorp.Vault.Api.dll"]