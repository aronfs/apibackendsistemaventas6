# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Establece el directorio de trabajo dentro del contenedor
WORKDIR /src

# Copia los archivos de solución y proyectos
COPY ./APISistemaVentas.sln .
COPY ./SistemaVenta.API/SistemaVenta.API.csproj ./SistemaVenta.API/
COPY ./SistemaVenta.BLL/SistemaVenta.BLL.csproj ./SistemaVenta.BLL/
COPY ./SistemaVenta.DAL/SistemaVenta.DAL.csproj ./SistemaVenta.DAL/
COPY ./SistemaVenta.DTO/SistemaVenta.DTO.csproj ./SistemaVenta.DTO/
COPY ./SistemaVenta.IOC/SistemaVenta.IOC.csproj ./SistemaVenta.IOC/
COPY ./SistemaVenta.Model/SistemaVenta.Model.csproj ./SistemaVenta.Model/
COPY ./SistemaVenta.Utility/SistemaVenta.Utility.csproj ./SistemaVenta.Utility/

# Restaura paquetes NuGet
RUN dotnet restore

# Copia todo el resto del código fuente
COPY . .

# Publica la aplicación en modo release
RUN dotnet publish SistemaVenta.API/SistemaVenta.API.csproj -c Release -o /app/publish

# Etapa 2: Imagen de producción
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

WORKDIR /app
COPY --from=build /app/publish .

# Expone el puerto de la API (ajusta si usas otro)
EXPOSE 80

# Comando de arranque
ENTRYPOINT ["dotnet", "SistemaVenta.API.dll"]
