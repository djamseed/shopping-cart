FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app


COPY src/ShoppingCart.Application/publish /app
COPY src/ShoppingCart.Core/publish /app
COPY src/ShoppingCart.Infrastructure.Persistence/publish /app
COPY src/ShoppingCart.Infrastructure.ReadModel/publish /app
COPY src/ShoppingCart.WebApi/publish /app

ENTRYPOINT ["dotnet", "ShoppingCart.WebApi.dll"]
