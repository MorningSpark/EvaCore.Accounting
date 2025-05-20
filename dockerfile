# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copiamos la solución y los proyectos
COPY EvaCore.Accounting.sln ./
COPY src/EvaCore.Accounting.Api/*.csproj ./src/EvaCore.Accounting.Api/
COPY src/EvaCore.Accounting.Application/*.csproj ./src/EvaCore.Accounting.Application/
COPY src/EvaCore.Accounting.Domain/*.csproj ./src/EvaCore.Accounting.Domain/
COPY src/EvaCore.Accounting.Infrastructure/*.csproj ./src/EvaCore.Accounting.Infrastructure/

RUN dotnet restore "EvaCore.Accounting.sln"

# Copiamos el resto del código fuente
COPY . .

# Publicamos el proyecto API
WORKDIR /app/src/EvaCore.Accounting.Api
RUN dotnet publish -c Release -o /app/publish

# Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5002
ENV ASPNETCORE_URLS=https://+:5002
ENTRYPOINT ["dotnet", "EvaCore.Accounting.Api.dll"]




