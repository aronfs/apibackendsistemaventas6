# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiamos los .csproj necesarios
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

# Establecemos carpeta de trabajo al proyecto principal
WORKDIR /src/SistemaVenta.API

# Publicamos la app en /out
RUN dotnet publish -c Release -o /out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copiamos los archivos publicados
COPY --from=build /out .

# Exponemos el puerto
EXPOSE 80

# Ejecutamos la app
ENTRYPOINT ["dotnet", "SistemaVenta.API.dll"]
