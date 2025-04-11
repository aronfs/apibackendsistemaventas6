# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiamos todo el código y config
COPY . .

# Asegurar que los archivos de configuración estén incluidos en el publish
COPY SistemaVenta.API/appsettings*.json SistemaVenta.API/

# Restaurar dependencias
RUN dotnet restore "APISistemaVentas.sln"

# Publicar la app (esto generará los binarios y config en /app/out)
RUN dotnet publish "SistemaVenta.API/SistemaVenta.API.csproj" -c Release -o /app/out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Puerto expuesto (ajústalo si cambiaste en Program.cs)
EXPOSE 5185
ENV ASPNETCORE_URLS=http://+:5185
ENV ASPNETCORE_ENVIRONMENT=Production

# Copiar el resultado de la publicación
COPY --from=build /app/out .

# Ejecutar la aplicación
ENTRYPOINT ["dotnet", "SistemaVenta.API.dll"]
