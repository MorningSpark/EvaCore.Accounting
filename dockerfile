FROM mcr.microsoft.com/dotnet/aspnet:9.0-windowsservercore-ltsc2019 AS base

WORKDIR /src

COPY ["src/EvaCore.Accounting.Api/EvaCore.Accounting.Api.csproj", "EvaCore.Accounting.Api/"]
COPY ["src/EvaCore.Accounting.Domain/EvaCore.Accounting.Domain.csproj", "EvaCore.Accounting.Domain/"]
COPY ["src/EvaCore.Accounting.Application/EvaCore.Accounting.Application.csproj", "EvaCore.Accounting.Application/"]
COPY ["src/EvaCore.Accounting.Infrastructure/EvaCore.Accounting.Infrastructure.csproj", "EvaCore.Accounting.Infrastructure/"]
RUN dotnet restore "src/EvaCore.Accounting.Api/EvaCore.Accounting.Api.csproj"
COPY . .
WORKDIR "/src/EvaCore.Accounting.Api"
RUN dotnet build "EvaCore.Accounting.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EvaCore.Accounting.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5002
ENV ASPNETCORE_URLS=https://+:5002
ENTRYPOINT ["dotnet", "EvaCore.Accounting.Api.dll"]

