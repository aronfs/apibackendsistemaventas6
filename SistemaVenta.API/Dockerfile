# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar todo el código
COPY . .

# Restaurar dependencias (ajusta la ruta si es necesario)
RUN dotnet restore src/SistemaVenta.API/SistemaVenta.API.csproj

# Publicar el proyecto API
RUN dotnet publish src/SistemaVenta.API/SistemaVenta.API.csproj -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copiar los archivos publicados
COPY --from=build /app/publish .

# Exponer el puerto (opcional)
EXPOSE 80

# Ejecutar la aplicación
ENTRYPOINT ["dotnet", "SistemaVenta.API.dll"]
