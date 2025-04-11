# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

# Copia los csproj primero (para aprovechar cache de docker)
COPY APISistemaVentas.sln ./
COPY SistemaVenta.API/SistemaVenta.API.csproj SistemaVenta.API/
COPY SistemaVenta.BLL/SistemaVenta.BLL.csproj SistemaVenta.BLL/
COPY SistemaVenta.DAL/SistemaVenta.DAL.csproj SistemaVenta.DAL/
COPY SistemaVenta.DTO/SistemaVenta.DTO.csproj SistemaVenta.DTO/
COPY SistemaVenta.IOC/SistemaVenta.IOC.csproj SistemaVenta.IOC/
COPY SistemaVenta.Model/SistemaVenta.Model.csproj SistemaVenta.Model/
COPY SistemaVenta.Utility/SistemaVenta.Utility.csproj SistemaVenta.Utility/

# Restaura paquetes NuGet
RUN dotnet restore

# Copia el resto del código
COPY . .

# Publica la app
RUN dotnet publish SistemaVenta.API/SistemaVenta.API.csproj -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app

# Copia los binarios desde la etapa de build
COPY --from=build /app/publish .

# Expone el puerto de la API
EXPOSE 80

# Inicia la app
ENTRYPOINT ["dotnet", "SistemaVenta.API.dll"]
