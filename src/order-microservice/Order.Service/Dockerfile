FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
COPY ["src/order-microservice/Order.Service/Order.Service.csproj", "Order.Service/"]
COPY ["src/local-nuget-packages/", "/local-nuget-packages/"]
COPY ["src/order-microservice/nuget.config", "./"]
RUN dotnet restore "Order.Service/Order.Service.csproj"

COPY ["src/order-microservice/Order.Service/", "Order.Service/"]
WORKDIR "/Order.Service"
RUN dotnet build "Order.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Order.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Service.dll"]