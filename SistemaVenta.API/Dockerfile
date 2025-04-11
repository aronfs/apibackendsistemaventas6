# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar archivos de solución y proyectos
COPY APISistemaVentas.sln ./
COPY SistemaVenta.API/SistemaVenta.API.csproj SistemaVenta.API/
COPY SistemaVenta.BLL/SistemaVenta.BLL.csproj SistemaVenta.BLL/
COPY SistemaVenta.DAL/SistemaVenta.DAL.csproj SistemaVenta.DAL/
COPY SistemaVenta.DTO/SistemaVenta.DTO.csproj SistemaVenta.DTO/
COPY SistemaVenta.IOC/SistemaVenta.IOC.csproj SistemaVenta.IOC/
COPY SistemaVenta.Model/SistemaVenta.Model.csproj SistemaVenta.Model/
COPY SistemaVenta.Utility/SistemaVenta.Utility.csproj SistemaVenta.Utility/

# Restaurar dependencias
RUN dotnet restore

# Copiar todo el código fuente
COPY . .

# Publicar la aplicación
WORKDIR /src/SistemaVenta.API
RUN dotnet publish -c Release -o /app/publish
RUN cp appsettings*.json /app/publish/

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Exponer el puerto
EXPOSE 5185
ENV ASPNETCORE_URLS=http://+:5185
ENV ASPNETCORE_ENVIRONMENT=Production

# Copiar el contenido publicado
COPY --from=build /app/publish .

# Iniciar la aplicación
ENTRYPOINT ["dotnet", "SistemaVenta.API.dll"]
