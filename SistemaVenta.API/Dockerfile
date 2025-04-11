# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiamos los proyectos con dependencias
COPY SistemaVenta.BLL/SistemaVenta.BLL.csproj SistemaVenta.BLL/
COPY SistemaVenta.DAL/SistemaVenta.DAL.csproj SistemaVenta.DAL/
COPY SistemaVenta.Model/SistemaVenta.Model.csproj SistemaVenta.Model/
COPY SistemaVenta.IOC/SistemaVenta.IOC.csproj SistemaVenta.IOC/
COPY SistemaVenta.API/SistemaVenta.API.csproj SistemaVenta.API/

# Restauramos dependencias
RUN dotnet restore SistemaVenta.API/SistemaVenta.API.csproj

# Copiamos todo el código restante
COPY . .

# Publicamos la aplicación
WORKDIR /src/SistemaVenta.API
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copiamos los archivos publicados desde la etapa de build
COPY --from=build /app/publish .

# 🔒 Si por alguna razón no se copió el appsettings.json, forzalo (opcional backup)
 COPY SistemaVenta.API/appsettings.json ./appsettings.json

# Exponemos el puerto
EXPOSE 80

# Iniciamos la aplicación
ENTRYPOINT ["dotnet", "SistemaVenta.API.dll"]
