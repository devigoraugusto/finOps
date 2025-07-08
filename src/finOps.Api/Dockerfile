FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/finOps.Core/finOps.Core.csproj src/finOps.Core/
COPY src/finOps.Application/finOps.Application.csproj src/finOps.Application/
COPY src/finOps.Infra/finOps.Infra.csproj src/finOps.Infra/
COPY src/finOps.Api/finOps.Api.csproj src/finOps.Api/

RUN dotnet restore src/finOps.Api/finOps.Api.csproj

COPY . .
RUN dotnet publish src/finOps.Api/finOps.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "finOps.Api.dll"]

