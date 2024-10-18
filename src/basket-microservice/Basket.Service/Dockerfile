FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
COPY ["Basket.Service/Basket.Service.csproj", "Basket.Service/"]
RUN dotnet restore "Basket.Service/Basket.Service.csproj"

COPY ["Basket.Service/", "Basket.Service/"]
WORKDIR "/Basket.Service"
RUN dotnet build "Basket.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Service.dll"]