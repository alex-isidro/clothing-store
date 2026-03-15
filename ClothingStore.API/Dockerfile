FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ClothingStore.API/ClothingStore.API.csproj", "ClothingStore.API/"]
COPY ["ClothingStore.Infrastructure/ClothingStore.Infrastructure.csproj", "ClothingStore.Infrastructure/"]
COPY ["ClothingStore.Domain/ClothingStore.Domain.csproj", "ClothingStore.Domain/"]
COPY ["ClothingStore.Application/ClothingStore.Application.csproj", "ClothingStore.Application/"]
RUN dotnet restore "ClothingStore.API/ClothingStore.API.csproj"
COPY . .
WORKDIR "/src/ClothingStore.API"
RUN dotnet build "./ClothingStore.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ClothingStore.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClothingStore.API.dll"]
