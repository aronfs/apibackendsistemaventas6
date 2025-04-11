# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar todo
COPY . .

# Restaurar dependencias
RUN dotnet restore "APISistemaVentas.sln"

# Publicar la app
RUN dotnet publish "SistemaVenta.API/SistemaVenta.API.csproj" -c Release -o /app/out

# Etapa final: runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

# Exponer el puerto
EXPOSE 5185
ENV ASPNETCORE_URLS=http://+:5185
ENV ASPNETCORE_ENVIRONMENT=Production

# Copiar salida publicada
COPY --from=build /app/out .

# 🚨 FORZAR aquí los archivos de configuración si no llegaron con el publish
COPY SistemaVenta.API/appsettings.json ./appsettings.json
COPY SistemaVenta.API/appsettings.Development.json ./appsettings.Development.json

# Ejecutar la app
ENTRYPOINT ["dotnet", "SistemaVenta.API.dll"]
