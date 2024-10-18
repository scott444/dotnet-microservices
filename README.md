# Dotnet Microservices

```
dotnet tool install dotnet-ef --global -–version 8.0.5
dotnet ef migrations add Initial -o Infrastructure\Data\EntityFramework\Migrations
```

Run RabbitMQ
```
docker run -d --hostname rabbitmq-host --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

* 5672 - publishing/receiving messages
* 15672 - Admin interface
* login: username: guest password: guest

Build & Run Basket microservice in Docker
```
docker build -t basket.service:v1.0 -f src\basket-microservice\Basket.Service\Dockerfile .
docker run -it --rm -p 8000:8080 basket.service:v1.0
```

Build & Run Order microservice in Docker
```
docker build -t order.service:v1.0 -f src\order-microservice\Order.Service\Dockerfile .
docker run -it --rm -p 8001:8080 -e RabbitMq__HostName=host.docker.internal order.service:v1.0
```

Shared Library
```
dotnet pack
dotnet nuget push ECommerce.Shared.1.0.0.nupkg -s C:\dev\dotnet-microservices\local-nuget-packages
```
