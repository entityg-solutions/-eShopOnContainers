#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["eShopOnContainers.CatalogService.API/eShopOnContainers.CatalogService.API.csproj", "eShopOnContainers.CatalogService.API/"]
RUN dotnet restore "eShopOnContainers.CatalogService.API/eShopOnContainers.CatalogService.API.csproj"
COPY . .
WORKDIR "/src/eShopOnContainers.CatalogService.API"
RUN dotnet build "eShopOnContainers.CatalogService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShopOnContainers.CatalogService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShopOnContainers.CatalogService.API.dll"]