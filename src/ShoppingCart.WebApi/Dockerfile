FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 20195
EXPOSE 44381

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["src/ShoppingCart.WebApi/ShoppingCart.WebApi.csproj", "src/ShoppingCart.WebApi/"]
COPY ["src/ShoppingCart.Infrastructure.ReadModel/ShoppingCart.Infrastructure.ReadModel.csproj", "src/ShoppingCart.Infrastructure.ReadModel/"]
COPY ["src/ShoppingCart.Core/ShoppingCart.Core.csproj", "src/ShoppingCart.Core/"]
COPY ["src/ShoppingCart.Application/ShoppingCart.Application.csproj", "src/ShoppingCart.Application/"]
COPY ["src/ShoppingCart.Infrastructure.Persistence/ShoppingCart.Infrastructure.Persistence.csproj", "src/ShoppingCart.Infrastructure.Persistence/"]
RUN dotnet restore "src/ShoppingCart.WebApi/ShoppingCart.WebApi.csproj"
COPY . .
WORKDIR "/src/src/ShoppingCart.WebApi"
RUN dotnet build "ShoppingCart.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ShoppingCart.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ShoppingCart.WebApi.dll"]