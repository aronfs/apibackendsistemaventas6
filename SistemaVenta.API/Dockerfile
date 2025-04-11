# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiamos todos los proyectos necesarios
COPY SistemaVenta.BLL/SistemaVenta.BLL.csproj SistemaVenta.BLL/
COPY SistemaVenta.DAL/SistemaVenta.DAL.csproj SistemaVenta.DAL/
COPY SistemaVenta.Model/SistemaVenta.Model.csproj SistemaVenta.Model/
COPY SistemaVenta.IOC/SistemaVenta.IOC.csproj SistemaVenta.IOC/
COPY SistemaVenta.API/SistemaVenta.API.csproj SistemaVenta.API/

# Restauramos dependencias
RUN dotnet restore SistemaVenta.API/SistemaVenta.API.csproj

# Copiamos el resto del código
COPY . .

# Publicamos la app
WORKDIR /src/SistemaVenta.API
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copiamos los archivos publicados desde la etapa anterior
COPY --from=build /app/publish .

# Exponemos el puerto
EXPOSE 80

# Ejecutamos la app
ENTRYPOINT ["dotnet", "SistemaVenta.API.dll"]
