# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ../Passeio.Api/Passeio.Api.csproj Passeio.Api/
COPY ../Passeio.Data/Passeio.Data.csproj Passeio.Data/
COPY ../Passeio.Negocio/Passeio.Negocio.csproj Passeio.Negocio/
RUN dotnet restore Passeio.Api/Passeio.Api.csproj

COPY ../Passeio.Api/ Passeio.Api/
COPY ../Passeio.Data/ Passeio.Data/
COPY ../Passeio.Negocio/ Passeio.Negocio/
WORKDIR /src/Passeio.Api
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Passeio.Api.dll"]
