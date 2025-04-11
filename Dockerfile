# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiamos los archivos de proyecto
COPY SistemaVenta.API/SistemaVenta.API.csproj SistemaVenta.API/
COPY SistemaVenta.BLL/SistemaVenta.BLL.csproj SistemaVenta.BLL/
COPY SistemaVenta.DAL/SistemaVenta.DAL.csproj SistemaVenta.DAL/
COPY SistemaVenta.DTO/SistemaVenta.DTO.csproj SistemaVenta.DTO/
COPY SistemaVenta.IOC/SistemaVenta.IOC.csproj SistemaVenta.IOC/
COPY SistemaVenta.Model/SistemaVenta.Model.csproj SistemaVenta.Model/
COPY SistemaVenta.Utility/SistemaVenta.Utility.csproj SistemaVenta.Utility/

# Restauramos dependencias
RUN dotnet restore SistemaVenta.API/SistemaVenta.API.csproj

# Copiamos todo el código fuente
COPY . .

# Nos posicionamos en la carpeta del API
WORKDIR /app/SistemaVenta.API

# Publicamos en una carpeta de salida estándar
RUN dotnet publish -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copiamos la salida del publish
COPY --from=build /app/out .

# Exponemos el puerto (opcional si Railway lo hace solo)
EXPOSE 80

# Comando de ejecución
ENTRYPOINT ["dotnet", "SistemaVenta.API.dll"]


