FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
COPY ["src/product-microservice/Product.Service/Product.Service.csproj", "Product.Service/"]
COPY ["src/local-nuget-packages/", "/local-nuget-packages/"]
COPY ["src/product-microservice/nuget.config", "./"]
RUN dotnet restore "Product.Service/Product.Service.csproj"

COPY ["src/product-microservice/Product.Service/", "Product.Service/"]
WORKDIR "/Product.Service"
RUN dotnet build "Product.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Product.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product.Service.dll"]