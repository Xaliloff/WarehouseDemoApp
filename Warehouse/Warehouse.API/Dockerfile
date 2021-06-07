#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Warehouse/Warehouse.API/Warehouse.API.csproj", "Services/Warehouse/Warehouse.API/"]
COPY ["Services/Warehouse/Warehouse.App/Warehouse.App.csproj", "Services/Warehouse/Warehouse.App/"]
COPY ["Services/Warehouse/Warehouse.Domain/Warehouse.Domain.csproj", "Services/Warehouse/Warehouse.Domain/"]
COPY ["CommonBlocks/EventContracts/EventContracts.csproj", "CommonBlocks/EventContracts/"]
COPY ["Services/Warehouse/Warehouse.Infrastructure/Warehouse.Infrastructure.csproj", "Services/Warehouse/Warehouse.Infrastructure/"]
RUN dotnet restore "Services/Warehouse/Warehouse.API/Warehouse.API.csproj"
COPY . .
WORKDIR "/src/Services/Warehouse/Warehouse.API"
RUN dotnet build "Warehouse.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Warehouse.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Warehouse.API.dll"]